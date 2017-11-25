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

#define TOPIC_SUB_ESPDEVICE_UPDATE_PIN "ESPDevice/UpdatePinIoT"
#define TOPIC_SUB_ESPDEVICE_INSERT_IN_APPLICATION "ESPDevice/InsertInApplicationIoT"
#define TOPIC_SUB_ESPDEVICE_DELETE_FROM_APPLICATION "ESPDevice/DeleteFromApplicationIoT"
#define TOPIC_SUB_ESPDEVICE_SET_UTC_TIME_OFF_SET_IN_SECOND "ESPDevice/SetUtcTimeOffsetInSecondIoT"
#define TOPIC_SUB_ESPDEVICE_SET_UPDATE_INTERVAL_IN_MILLI_SECOND "ESPDevice/SetUpdateIntervalInMilliSecondIoT"
#define TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_SCALE "DSFamilyTempSensor/SetScaleIoT"
#define TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_RESOLUTION "DSFamilyTempSensor/SetResolutionIoT"
#define TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_ALARM_ON "DSFamilyTempSensor/SetAlarmOnIoT"
#define TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_ALARM_CELSIUS "DSFamilyTempSensor/SetAlarmCelsiusIoT"
#define TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_ALARM_BUZZER_ON "DSFamilyTempSensor/SetAlarmBuzzerOnIoT"
#define TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_CHART_LIMITER_CELSIUS "DSFamilyTempSensor/SetChartLimiterCelsiusIoT"

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
  Serial.println("[MQQT::mqtt_ConnectedCallback] initializing ...");

  if(configurationManager.getDeviceInApplication()->getDeviceInApplicationId() == ""){
    subscribeNotInApplication();
  }
  else{
    subscribeInApplication();
  }

  Serial.println("[MQQT::mqtt_ConnectedCallback] Initialized with success !");
}

void subscribeNotInApplication()
{
  Serial.println("[MQQT::subscribeNotInApplication] initializing ...");
  
  mqqtManager.subscribeInDevice(TOPIC_SUB_ESPDEVICE_UPDATE_PIN);
  mqqtManager.subscribeInDevice(TOPIC_SUB_ESPDEVICE_INSERT_IN_APPLICATION);

  Serial.println("[MQQT::subscribeNotInApplication] Initialized with success !");
}

void unSubscribeNotInApplication()
{
  Serial.println("[MQQT::unSubscribeNotInApplication] initializing ...");

  mqqtManager.unSubscribeInDevice(TOPIC_SUB_ESPDEVICE_UPDATE_PIN);
  mqqtManager.unSubscribeInDevice(TOPIC_SUB_ESPDEVICE_INSERT_IN_APPLICATION);
  
  Serial.println("[MQQT::unSubscribeNotInApplication] Initialized with success !");
}

void subscribeInApplication()
{
  Serial.println("[MQQT::subscribeInApplication] initializing ...");

  mqqtManager.subscribeInDevice(TOPIC_SUB_ESPDEVICE_DELETE_FROM_APPLICATION);
  mqqtManager.subscribeInApplication(TOPIC_SUB_ESPDEVICE_SET_UTC_TIME_OFF_SET_IN_SECOND);
  mqqtManager.subscribeInApplication(TOPIC_SUB_ESPDEVICE_SET_UPDATE_INTERVAL_IN_MILLI_SECOND);
  mqqtManager.subscribeInApplication(TOPIC_SUB_TEMPERATURE_SCALE_GET_ALL_FOR_IOT_COMPLETED);
  mqqtManager.subscribeInApplication(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED);
  mqqtManager.subscribeInApplication(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_SCALE);
  mqqtManager.subscribeInApplication(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_RESOLUTION);
  mqqtManager.subscribeInApplication(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_ALARM_ON);
  mqqtManager.subscribeInApplication(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_ALARM_CELSIUS);
  mqqtManager.subscribeInApplication(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_ALARM_BUZZER_ON);
  mqqtManager.subscribeInApplication(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_CHART_LIMITER_CELSIUS);

  Serial.println("[MQQT::subscribeInApplication] Initialized with success !");
}

void unSubscribeInApplication()
{
  Serial.println("[MQQT::unSubscribeInApplication] initializing ...");  

  mqqtManager.unSubscribeInDevice(TOPIC_SUB_ESPDEVICE_DELETE_FROM_APPLICATION);
  mqqtManager.unSubscribeInApplication(TOPIC_SUB_ESPDEVICE_SET_UTC_TIME_OFF_SET_IN_SECOND);
  mqqtManager.unSubscribeInApplication(TOPIC_SUB_ESPDEVICE_SET_UPDATE_INTERVAL_IN_MILLI_SECOND);
  mqqtManager.unSubscribeInApplication(TOPIC_SUB_TEMPERATURE_SCALE_GET_ALL_FOR_IOT_COMPLETED);
  mqqtManager.unSubscribeInApplication(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED);
  mqqtManager.unSubscribeInApplication(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_SCALE);
  mqqtManager.unSubscribeInApplication(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_RESOLUTION);
  mqqtManager.unSubscribeInApplication(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_ALARM_ON);
  mqqtManager.unSubscribeInApplication(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_ALARM_CELSIUS);
  mqqtManager.unSubscribeInApplication(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_ALARM_BUZZER_ON);
  mqqtManager.unSubscribeInApplication(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_CHART_LIMITER_CELSIUS);
  
  Serial.println("[MQQT::unSubscribeInApplication] Initialized with success !");
}

void mqtt_SubCallback(char* topic, byte* payload, unsigned int length) 
{
    Serial.print("[MQQT::mqtt_SubCallback] Topic: ");
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

    String topicKey = mqqtManager.getTopicKey(topic);
    String payloadContract = root["contract"];

    Serial.print("topicKey: ");
    Serial.println(topicKey);

    if(topicKey == String(TOPIC_SUB_ESPDEVICE_UPDATE_PIN)){
      displayAccessManager.updatePin(payloadContract);
    }
    if(topicKey == String(TOPIC_SUB_ESPDEVICE_INSERT_IN_APPLICATION)){
      unSubscribeNotInApplication();
      configurationManager.insertInApplication(payloadContract);          
      subscribeInApplication();  
    }
    if(topicKey == String(TOPIC_SUB_ESPDEVICE_DELETE_FROM_APPLICATION)){
      unSubscribeInApplication();
      configurationManager.deleteFromApplication();            
      subscribeNotInApplication();
    }    
    if(topicKey == String(TOPIC_SUB_ESPDEVICE_SET_UTC_TIME_OFF_SET_IN_SECOND)){
      configurationManager.setUtcTimeOffsetInSecond(payloadContract);
    }
    if(topicKey == String(TOPIC_SUB_ESPDEVICE_SET_UPDATE_INTERVAL_IN_MILLI_SECOND)){
      configurationManager.setUpdateIntervalInMilliSecond(payloadContract);
    }    
    if(topicKey == String(TOPIC_SUB_TEMPERATURE_SCALE_GET_ALL_FOR_IOT_COMPLETED)){
      temperatureScaleManager.update(payloadContract);            
    }
    if(topicKey == String(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED)){
      dsFamilyTempSensorManager.setSensorsByMQQTCallback(payloadContract);      
    }
    if(topicKey == String(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_SCALE)){
      dsFamilyTempSensorManager.setScale(payloadContract);
    }
    if(topicKey == String(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_RESOLUTION)){
      dsFamilyTempSensorManager.setResolution(payloadContract);
    }
    if(topicKey == String(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_ALARM_ON)){
      dsFamilyTempSensorManager.setAlarmOn(payloadContract);
    }
    if(topicKey == String(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_ALARM_CELSIUS)){
      dsFamilyTempSensorManager.setAlarmCelsius(payloadContract);
    }
    if(topicKey == String(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_ALARM_BUZZER_ON)){
      dsFamilyTempSensorManager.setAlarmBuzzerOn(payloadContract);
    }
    if(topicKey == String(TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_SET_CHART_LIMITER_CELSIUS)){
      dsFamilyTempSensorManager.setChartLimiterCelsius(payloadContract);
    }        
}

void loop() {	  

  debugManager.update();      

  wifiManager.autoConnect(); //se não há conexão com o WiFI, a conexão é refeita
  configurationManager.autoInitialize(); 
  mqqtManager.autoConnect(); //se não há conexão com o Broker, a conexão é refeita

  DeviceInApplication* deviceInApplication = configurationManager.getDeviceInApplication();
  
  if(deviceInApplication != NULL && deviceInApplication->getDeviceInApplicationId() == ""){
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

      root["deviceInApplicationId"] = configurationManager.getDeviceInApplication()->getDeviceInApplicationId();
      root["wifiQuality"] = wifiManager.getQuality();
      root["epochTimeUtc"] = ntpManager.getEpochTimeUTC();    
      
      dsFamilyTempSensorManager.createSensorsJsonNestedArray(root);

      int sensorsJsonLen = root.measureLength();
      char sensorsJson[sensorsJsonLen + 1];
      root.printTo(sensorsJson, sizeof(sensorsJson));
  
      Serial.print("enviando para o servidor (Char Len)=> ");
      Serial.println(sensorsJsonLen);
      
      mqqt->publish(TOPIC_PUB_TEMP, sensorsJson); 
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

