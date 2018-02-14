#include "ESPDevice.h"
#include "EEPROMManager.h"

#include <ESP8266WiFi.h>
#include <DNSServer.h>
#include "ESP8266mDNS.h"

//defines - mapeamento de pinos do NodeMCU
#define D0    16
#define D1    5
#define D2    4
#define D3    0
#define D4    2
#define D5    14 // WiFi Reset Button
#define D6    12 // Antigo DebugManager
#define D7    13
#define D8    15
#define D9    3
#define D10   1

#define HOST_NAME "remotedebug-sample"

#define WEBAPI_HOST  "192.168.1.12"
#define WEBAPI_PORT  80
#define WEBAPI_URI   "/ART.Domotica.WebApi/"

struct config_t
{
  String hardwaresInApplicationId;
  String hardwareId;
} configuration;

int configurationEEPROMAddr = 0;

using namespace ART;

ESPDevice espDevice(WEBAPI_HOST, WEBAPI_PORT, WEBAPI_URI);

void setup() {

  Serial.begin(9600);

  // Buzzer
  pinMode(D6, OUTPUT);

  pinMode(D4, INPUT);
  pinMode(D5, INPUT);

  Serial.println("Iniciando...");

  initConfiguration();

  espDevice.begin();  

  String hostNameWifi = HOST_NAME;
  hostNameWifi.concat(".local");

  if (MDNS.begin(HOST_NAME)) {
    Serial.print("* MDNS responder started. Hostname -> ");
    Serial.println(HOST_NAME);
  }

  WiFi.hostname(hostNameWifi);
}

void initConfiguration()
{
  EEPROM_readAnything(0, configuration);
  //EEPROM_writeAnything(configurationEEPROMAddr, configuration);
}

void loop() {

  espDevice.loop(); 

  DeviceInApplication* deviceInApplication = espDevice.getDeviceInApplication();

  if (deviceInApplication->inApplication()) {
      loopInApplication();
  }
  else{
    espDevice.getDeviceDisplay()->getDeviceDisplayWiFiAccess()->loop();
  }

  //keep-alive da comunicação com broker MQTT
  espDevice.getDeviceMQ()->loop();


  //using include ESP8266WiFi.h the following core cmds work for me..
  /*Serial.print(F("ESP.getBootMode(); "));
  Serial.println(ESP.getBootMode());
  Serial.print(F("ESP.getSdkVersion(); "));
  Serial.println(ESP.getSdkVersion());
  Serial.print(F("ESP.getBootVersion(); "));
  Serial.println(ESP.getBootVersion());
  Serial.print(F("ESP.getChipId(); "));
  Serial.println(ESP.getChipId());
  Serial.print(F("ESP.getFlashChipSize(); "));
  Serial.println(ESP.getFlashChipSize());
  Serial.print(F("ESP.getFlashChipRealSize(); "));
  Serial.println(ESP.getFlashChipRealSize());
  Serial.print(F("ESP.getFlashChipSizeByChipId(); "));
  Serial.println(ESP.getFlashChipSizeByChipId());
  Serial.print(F("ESP.getFlashChipId(); "));
  Serial.println(ESP.getFlashChipId());*/
  Serial.print(F("ESP.getFreeHeap(); "));
  Serial.println(ESP.getFreeHeap());

}

void loopInApplication()
{
  espDevice.getDeviceDisplay()->display.clearDisplay();

  espDevice.getDeviceNTP()->update();

  if(espDevice.hasDeviceSensor()){
    DeviceSensor* deviceSensor = espDevice.getDeviceSensor();
    bool deviceSensorReaded = deviceSensor->read();
    espDevice.getDeviceDisplay()->getDeviceDisplaySensor()->printUpdate(deviceSensorReaded);
  }
  
  // MQTT
  if(espDevice.getDeviceMQ()->connected()){
    loopMQQTConnected();
  }

  // Wifi
  espDevice.getDeviceDisplay()->getDeviceDisplayWiFi()->printSignal();
  
  espDevice.getDeviceDisplay()->display.display(); 

}

void loopMQQTConnected()
{ 
  Serial.println(F("[loopMQQTConnected] begin"));
  
  espDevice.getDeviceDisplay()->getDeviceDisplayMQ()->printConnected();
  espDevice.getDeviceDisplay()->getDeviceDisplayMQ()->printReceived(false);  

  bool mqqtPrintSent = false;

  // Wifi

  DeviceWiFi* deviceWiFi = espDevice.getDeviceWiFi();

  bool deviceWiFiPublished = deviceWiFi->publish();

  if(deviceWiFiPublished){
    mqqtPrintSent = true;
  }  
  
  Serial.printf("deviceWiFi->publish: %s\n", deviceWiFiPublished ? "true" : "false");

  // DeviceSerial
  if(espDevice.hasDeviceSerial()){

    DeviceSerial* deviceSerial = espDevice.getDeviceSerial();
    
    if (deviceSerial->initialized()) {        
      
    }    
  }
  
  // DeviceDebug
  if(espDevice.hasDeviceDebug()){

    DeviceDebug* deviceDebug = espDevice.getDeviceDebug();
    
    if (deviceDebug->initialized()) {        
      
    }    
  }

  // Sensor
  if(espDevice.hasDeviceSensor()){
    
    DeviceSensor* deviceSensor = espDevice.getDeviceSensor();
    
    if (deviceSensor->initialized()) {        
      
      espDevice.getDeviceDisplay()->getDeviceDisplaySensor()->printSensors();
      
      bool deviceSensorPublished = deviceSensor->publish();

      if(deviceSensorPublished){
        mqqtPrintSent = true;
      }  

      Serial.printf("deviceSensor->publish: %s\n", deviceSensorPublished ? "true" : "false");    
    }
  }
  
  espDevice.getDeviceDisplay()->getDeviceDisplayMQ()->printSent(mqqtPrintSent); 

  Serial.println(F("[loopMQQTConnected] end"));
}

