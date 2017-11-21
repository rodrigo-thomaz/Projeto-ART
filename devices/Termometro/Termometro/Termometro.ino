#include "DebugManager.h"
#include "ConfigurationManager.h"
#include "DSFamilyTempSensorManager.h"
#include "TemperatureScaleManager.h"
#include "TemperatureScaleConverter.h"
#include "NTPManager.h"
#include "DisplayManager.h"
#include "WiFiManager.h"
#include "BuzzerManager.h"
#include "DisplayAccessManager.h"
#include "DisplayWiFiManager.h"
#include "DisplayMQTTManager.h"
#include "DisplayNTPManager.h"
#include "DisplayTemperatureSensorManager.h"
#include "EEPROMManager.h"
#include "MQQTManager.h"

//defines - mapeamento de pinos do NodeMCU
#define D0    16
#define D1    5
#define D2    4
#define D3    0
#define D4    2
#define D5    14
#define D6    12
#define D7    13
#define D8    15
#define D9    3
#define D10   1

#define HOST  "192.168.1.12"
#define PORT  80
#define URI   "/ART.Domotica.WebApi/"

struct config_t
{
    String hardwaresInApplicationId;
    String hardwareId;
} configuration;

int configurationEEPROMAddr = 0;

#define TOPIC_SUB_UPDATE_PIN "UpdatePinIoT"
#define TOPIC_SUB_INSERT_IN_APPLICATION "InsertInApplicationIoT"
#define TOPIC_SUB_DELETE_FROM_APPLICATION "DeleteFromApplicationIoT"
#define TOPIC_SUB_SET_SCALE "DSFamilyTempSensor.SetScaleIoT"
#define TOPIC_SUB_SET_RESOLUTION "DSFamilyTempSensor.SetResolutionIoT"
#define TOPIC_SUB_SET_ALARM_ON "DSFamilyTempSensor.SetAlarmOnIoT"
#define TOPIC_SUB_SET_ALARM_CELSIUS "DSFamilyTempSensor.SetAlarmCelsiusIoT"
#define TOPIC_SUB_SET_ALARM_BUZZER_ON "DSFamilyTempSensor.SetAlarmBuzzerOnIoT"
#define TOPIC_SUB_SET_CHART_LIMITER_CELSIUS "DSFamilyTempSensor.SetChartLimiterCelsiusIoT"

#define TOPIC_PUB_TEMP   "ARTPUBTEMP"    //tópico MQTT de envio de informações para Broker

uint64_t publishMessageTimestamp = 0;

#define READTEMP_INTERVAL 2000
uint64_t readTempTimestamp = 0;

DebugManager debugManager(D6);
WiFiManager wifiManager(D5, debugManager);
ConfigurationManager configurationManager(debugManager, wifiManager, HOST, PORT, URI);
NTPManager ntpManager(debugManager, configurationManager);
MQQTManager mqqtManager(debugManager, configurationManager, wifiManager);
DisplayManager displayManager(debugManager);
BuzzerManager buzzerManager(D7, debugManager);
DSFamilyTempSensorManager dsFamilyTempSensorManager(debugManager, configurationManager, mqqtManager, buzzerManager);
TemperatureScaleManager temperatureScaleManager(debugManager, configurationManager, mqqtManager);
TemperatureScaleConverter temperatureScaleConverter(debugManager);

DisplayAccessManager displayAccessManager(debugManager, displayManager);
DisplayWiFiManager displayWiFiManager(displayManager, wifiManager, debugManager);
DisplayMQTTManager displayMQTTManager(displayManager, debugManager);
DisplayNTPManager displayNTPManager(displayManager, ntpManager, debugManager);
DisplayTemperatureSensorManager displayTemperatureSensorManager(displayManager, dsFamilyTempSensorManager, debugManager, temperatureScaleManager, temperatureScaleConverter);

void setup() {
		
	Serial.begin(9600);

  // Buzzer
  pinMode(D6,OUTPUT);

  pinMode(D4, INPUT);
  pinMode(D5, INPUT);  

	debugManager.update();  

	displayManager.begin();

	dsFamilyTempSensorManager.begin();

	if (debugManager.isDebug()) Serial.println("Iniciando...");

	// text display tests
	displayManager.display.clearDisplay();
	displayManager.display.setTextSize(1);
	displayManager.display.setTextColor(WHITE);
	displayManager.display.setCursor(0, 0);	
	displayManager.display.display();
  
  wifiManager.autoConnect();

  initConfiguration();

  configurationManager.begin();  

  mqqtManager.setConnectedCallback(mqtt_ConnectedCallback);
  mqqtManager.setSubCallback(mqtt_SubCallback);  
  mqqtManager.begin();

  ntpManager.begin();  
}

void initConfiguration()
{
  EEPROM_readAnything(0, configuration);
}

void mqtt_ConnectedCallback(PubSubClient* mqqt)
{
  String clientId  = String(configurationManager.getDeviceSettings()->getDeviceId());
  
  mqqt->subscribe(getRoutingKey(clientId, TOPIC_SUB_UPDATE_PIN)); 
  mqqt->subscribe(getRoutingKey(clientId, TOPIC_SUB_INSERT_IN_APPLICATION)); 
  mqqt->subscribe(getRoutingKey(clientId, TOPIC_SUB_DELETE_FROM_APPLICATION)); 

  //mqqt.subscribe(TOPIC_SUB_SET_RESOLUTION); 
  //mqqt.subscribe(TOPIC_SUB_SET_HIGH_ALARM); 
  //mqqt.subscribe(TOPIC_SUB_SET_LOW_ALARM);    
  //mqqt.subscribe(TOPIC_SUB_GET_IN_APPLICATION_FOR_IOT_COMPLETED);            
}

const char* getRoutingKey(String clientId, String topic)
{
  String result = "ART/ESPDevice/" + clientId + "/" + topic;
  return result.c_str();
}

void mqtt_SubCallback(char* topic, byte* payload, unsigned int length) 
{
    Serial.println("Topic Topic Topic Topic Topic Topic Topic Topic Topic Topic Topic Topic Topic Topic Topic Topic Topic Topic :");
    Serial.println(topic);
  
    displayMQTTManager.printReceived(true);
    
    String json;
    
    //obtem a string do payload recebido
    for(int i = 0; i < length; i++) 
    {
       char c = (char)payload[i];
       json += c;
    }    

    //StaticJsonBuffer<3000> jsonBuffer;
    DynamicJsonBuffer jsonBuffer;
    
    JsonObject& root = jsonBuffer.parseObject(json);
  
    if (!root.success()) {
      Serial.print("parse payload failed: ");
      Serial.println(json);
      return;
    }

    String payloadTopic = root["topic"];
    String payloadContract = root["contract"];

    Serial.print("payloadTopic: ");
    Serial.println(payloadTopic);

    if(payloadTopic == String(TOPIC_SUB_UPDATE_PIN)){
      displayAccessManager.updatePin(payloadContract);
    }
    if(payloadTopic == String(TEMPERATURE_SCALE_GET_ALL_FOR_IOT_COMPLETED_MQQT_TOPIC_SUB)){
      temperatureScaleManager.update(payloadContract);            
    }
    if(payloadTopic == String(DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED_MQQT_TOPIC_SUB)){
      dsFamilyTempSensorManager.setSensorsByMQQTCallback(payloadContract);      
    }
    if(payloadTopic == String(TOPIC_SUB_SET_SCALE)){
      dsFamilyTempSensorManager.setScale(payloadContract);
    }
    if(payloadTopic == String(TOPIC_SUB_SET_RESOLUTION)){
      dsFamilyTempSensorManager.setResolution(payloadContract);
    }
    if(payloadTopic == String(TOPIC_SUB_SET_ALARM_ON)){
      dsFamilyTempSensorManager.setAlarmOn(payloadContract);
    }
    if(payloadTopic == String(TOPIC_SUB_SET_ALARM_CELSIUS)){
      dsFamilyTempSensorManager.setAlarmCelsius(payloadContract);
    }
    if(payloadTopic == String(TOPIC_SUB_SET_ALARM_BUZZER_ON)){
      dsFamilyTempSensorManager.setAlarmBuzzerOn(payloadContract);
    }
    if(payloadTopic == String(TOPIC_SUB_SET_CHART_LIMITER_CELSIUS)){
      dsFamilyTempSensorManager.setChartLimiterCelsius(payloadContract);
    }    
    if(payloadTopic == String(TOPIC_SUB_INSERT_IN_APPLICATION)){
      configurationManager.getDeviceSettings()->insertInApplication(payloadContract);      
    }
    if(payloadTopic == String(TOPIC_SUB_DELETE_FROM_APPLICATION)){
      configurationManager.getDeviceSettings()->deleteFromApplication();      
    }
}

void loop() {	  

  debugManager.update();      

  wifiManager.autoConnect(); //se não há conexão com o WiFI, a conexão é refeita
  configurationManager.autoInitialize(); 
  mqqtManager.autoConnect(); //se não há conexão com o Broker, a conexão é refeita

  DeviceSettings* deviceSettings = configurationManager.getDeviceSettings();
  
  if(deviceSettings != NULL && deviceSettings->getDeviceInApplicationId() == ""){
    displayAccessManager.loop();
    //EEPROM_writeAnything(configurationEEPROMAddr, configuration);
  }    
  else{
    loopInApplication();  
  }  
    
  //keep-alive da comunicação com broker MQTT
  mqqtManager.getMQQT()->loop();  
  
}

void loopInApplication()
{
  displayManager.display.clearDisplay();
  
  ntpManager.update();
  
  uint64_t now = millis();   

  if(dsFamilyTempSensorManager.initialized()) {
    if(now - readTempTimestamp > READTEMP_INTERVAL) {
      readTempTimestamp = now;
      displayTemperatureSensorManager.printUpdate(true);
      dsFamilyTempSensorManager.refresh();
  
      // Temp
      temperatureScaleManager.begin();
    }  
    else{
      displayTemperatureSensorManager.printUpdate(false);
    }    
  }  
  
  // MQTT
  PubSubClient* mqqt = mqqtManager.getMQQT();
  
  if(mqqt->connected() && dsFamilyTempSensorManager.initialized()){
    
    displayMQTTManager.printConnected();  
    displayMQTTManager.printReceived(false);

    int publishMessageInterval = configurationManager.getPublishMessageInterval();
    
    if(now - publishMessageTimestamp > publishMessageInterval) {
      publishMessageTimestamp = now;
      displayMQTTManager.printSent(true);

      StaticJsonBuffer<2048> jsonBuffer;
      JsonObject& root = jsonBuffer.createObject();

      root["deviceInApplicationId"] = configurationManager.getDeviceSettings()->getDeviceInApplicationId();
      root["wifiQuality"] = wifiManager.getQuality();
      root["epochTimeUtc"] = ntpManager.getEpochTimeUTC();    
      
      dsFamilyTempSensorManager.createSensorsJsonNestedArray(root);

      int sensorsJsonLen = root.measureLength();
      char sensorsJson[sensorsJsonLen + 1];
      root.printTo(sensorsJson, sizeof(sensorsJson));
  
      Serial.print("enviando para o servidor (Char Len)=> ");
      Serial.println(sensorsJsonLen);
      
      mqqt->publish(TOPIC_PUB_TEMP, sensorsJson);            

      mqqt->publish("art/devices", sensorsJson);            
      
    } 
    else {
      displayMQTTManager.printSent(false);
    }

    // Sensor
    displayTemperatureSensorManager.printSensors();  
  }       

  // Wifi
  displayWiFiManager.printSignal();
  
  // Buzzer
  //buzzerManager.test();

  displayManager.display.display();
}

