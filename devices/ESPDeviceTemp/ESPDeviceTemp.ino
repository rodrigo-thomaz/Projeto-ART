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

  espDevice.getDeviceMQ()->addSubscriptionCallback(mqtt_SubCallback);

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

bool mqtt_SubCallback(char* topicKey, char* json)
{
  if (espDevice.getDeviceDebug()->isActive(DeviceDebug::DEBUG)) {
    espDevice.getDeviceDebug()->printf("Termometro", "mqtt_SubCallback", "Topic Key: %s\n", topicKey);
  }  

  if (strcmp(topicKey, ESP_DEVICE_UPDATE_PIN_TOPIC_SUB) == 0) {
    espDevice.getDisplayDevice()->getDisplayDeviceWiFiAccess()->updatePin(json);
    return true;
  }
/*
  else if (strcmp(topicKey, DEVICE_WIFI_SET_HOST_NAME_TOPIC_SUB) == 0) {
    espDevice.getDeviceWiFi()->setHostName(json);
    return true;
  }
  else if (strcmp(topicKey, DEVICE_WIFI_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB) == 0) {
   espDevice.getDeviceWiFi()->setPublishIntervalInMilliSeconds(json);
   return true;
  }
*/  
  else{
    return false;
  }
}

void loop() {

  espDevice.loop(); 

  DeviceInApplication* deviceInApplication = espDevice.getDeviceInApplication();

  if (deviceInApplication->inApplication()) {
      loopInApplication();
  }
  else{
    espDevice.getDisplayDevice()->getDisplayDeviceWiFiAccess()->loop();
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
  espDevice.getDisplayDevice()->display.clearDisplay();

  espDevice.getDeviceNTP()->update();

  if(espDevice.hasSensor()){
    DeviceSensors* deviceSensors = espDevice.getDeviceSensors();
    bool deviceSensorsReaded = deviceSensors->read();
    espDevice.getDisplayDevice()->getDisplayDeviceSensors()->printUpdate(deviceSensorsReaded);
  }
  
  // MQTT
  if(espDevice.getDeviceMQ()->connected()){
    loopMQQTConnected();
  }

  // Wifi
  espDevice.getDisplayDevice()->getDisplayDeviceWiFi()->printSignal();
  
  espDevice.getDisplayDevice()->display.display(); 

}

void loopMQQTConnected()
{ 
  Serial.println(F("[loopMQQTConnected] begin"));
  
  espDevice.getDisplayDevice()->getDisplayDeviceMQ()->printConnected();
  espDevice.getDisplayDevice()->getDisplayDeviceMQ()->printReceived(false);  

  bool mqqtPrintSent = false;

  // Wifi

  DeviceWiFi* deviceWiFi = espDevice.getDeviceWiFi();

  bool deviceWiFiPublished = deviceWiFi->publish();

  if(deviceWiFiPublished){
    mqqtPrintSent = true;
  }  
  
  Serial.printf("deviceWiFi->publish: %s\n", deviceWiFiPublished ? "true" : "false");

  // Sensor
  if(espDevice.hasSensor()){

    DeviceSensors* deviceSensors = espDevice.getDeviceSensors();

    if (deviceSensors->initialized()) {        

      espDevice.getDisplayDevice()->getDisplayDeviceSensors()->printSensors();

      bool deviceSensorsPublished = deviceSensors->publish();

      if(deviceSensorsPublished){
        mqqtPrintSent = true;
      }  

      Serial.printf("deviceSensors->publish: %s\n", deviceSensorsPublished ? "true" : "false");    
    }
  }
  
  espDevice.getDisplayDevice()->getDisplayDeviceMQ()->printSent(mqqtPrintSent); 

  Serial.println(F("[loopMQQTConnected] end"));
}

