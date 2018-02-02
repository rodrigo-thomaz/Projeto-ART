#include "ESPDevice.h"
#include "UnitMeasurementConverter.h"
#include "DisplayManager.h"
#include "DeviceBuzzer.h"
#include "DisplayAccessManager.h"
#include "DisplayWiFiManager.h"
#include "DisplayMQTTManager.h"
#include "DisplayNTPManager.h"
#include "DisplayTemperatureSensorManager.h"
#include "EEPROMManager.h"

#include <ESP8266WiFi.h>
#include <DNSServer.h>
#include "ESP8266mDNS.h"

#define HOST_NAME "remotedebug-sample"

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

#define WEBAPI_HOST  "192.168.1.12"
#define WEBAPI_PORT  80
#define WEBAPI_URI   "/ART.Domotica.WebApi/"

struct config_t
{
	String hardwaresInApplicationId;
	String hardwareId;
} configuration;

int configurationEEPROMAddr = 0;

#define TOPIC_SUB_ESPDEVICE_UPDATE_PIN "ESPDevice/UpdatePinIoT"
#define TOPIC_SUB_ESPDEVICE_INSERT_IN_APPLICATION "ESPDevice/InsertInApplicationIoT"
#define TOPIC_SUB_ESPDEVICE_DELETE_FROM_APPLICATION "ESPDevice/DeleteFromApplicationIoT"
#define TOPIC_SUB_ESPDEVICE_SET_LABEL "ESPDevice/SetLabelIoT"

#define TOPIC_SUB_DEVICE_NTP_SET_UTC_TIME_OFF_SET_IN_SECOND "DeviceNTP/SetUtcTimeOffsetInSecondIoT"
#define TOPIC_SUB_DEVICE_NTP_SET_UPDATE_INTERVAL_IN_MILLI_SECOND "DeviceNTP/SetUpdateIntervalInMilliSecondIoT"

#define TOPIC_SUB_DEVICE_WIFI_SET_HOST_NAME "DeviceWiFi/SetHostNameIoT"
#define TOPIC_SUB_DEVICE_WIFI_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS "DeviceWiFi/SetPublishIntervalInMilliSecondsIoT"
#define TOPIC_PUB_DEVICE_WIFI_MESSAGE "DeviceWiFi/MessageIoT" 

#define TOPIC_SUB_DEVICE_DEBUG_SET_REMOTE_ENABLED "DeviceDebug/SetRemoteEnabledIoT"
#define TOPIC_SUB_DEVICE_DEBUG_SET_RESET_CMD_ENABLED "DeviceDebug/SetResetCmdEnabledIoT"
#define TOPIC_SUB_DEVICE_DEBUG_SET_SERIAL_ENABLED "DeviceDebug/SetSerialEnabledIoT"
#define TOPIC_SUB_DEVICE_DEBUG_SET_SHOW_COLORS "DeviceDebug/SetShowColorsIoT"
#define TOPIC_SUB_DEVICE_DEBUG_SET_SHOW_DEBUG_LEVEL "DeviceDebug/SetShowDebugLevelIoT"
#define TOPIC_SUB_DEVICE_DEBUG_SET_SHOW_PROFILER "DeviceDebug/SetShowProfilerIoT"
#define TOPIC_SUB_DEVICE_DEBUG_SET_SHOW_TIME "DeviceDebug/SetShowTimeIoT"

#define TOPIC_SUB_DEVICE_SENSORS_SET_READ_INTERVAL_IN_MILLI_SECONDS "DeviceSensors/SetReadIntervalInMilliSecondsIoT"
#define TOPIC_SUB_DEVICE_SENSORS_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS "DeviceSensors/SetPublishIntervalInMilliSecondsIoT"
#define TOPIC_PUB_DEVICE_SENSORS_MESSAGE "DeviceSensors/MessageIoT"

#define TOPIC_SUB_SENSOR_IN_DEVICE_SET_ORDINATION "SensorInDevice/SetOrdinationIoT"

#define TOPIC_SUB_SENSOR_SET_LABEL "Sensor/SetLabelIoT"

#define TOPIC_SUB_SENSOR_TEMP_DS_FAMILY_SET_RESOLUTION "SensorTempDSFamily/SetResolutionIoT"

#define TOPIC_SUB_SENSOR_TRIGGER_INSERT "SensorTrigger/InsertIoT"
#define TOPIC_SUB_SENSOR_TRIGGER_DELETE "SensorTrigger/DeleteIoT"
#define TOPIC_SUB_SENSOR_TRIGGER_SET_TRIGGER_ON "SensorTrigger/SetTriggerOnIoT"
#define TOPIC_SUB_SENSOR_TRIGGER_SET_BUZZER_ON "SensorTrigger/SetBuzzerOnIoT"
#define TOPIC_SUB_SENSOR_TRIGGER_SET_TRIGGER_VALUE "SensorTrigger/SetTriggerValueIoT"

#define TOPIC_SUB_SENSOR_UNIT_MEASUREMENT_SCALE_SET_DATASHEET_UNIT_MEASUREMENT_SCALE "SensorUnitMeasurementScale/SetDatasheetUnitMeasurementScaleIoT"
#define TOPIC_SUB_SENSOR_UNIT_MEASUREMENT_SCALE_RANGE_SET_VALUE "SensorUnitMeasurementScale/SetRangeIoT"
#define TOPIC_SUB_SENSOR_UNIT_MEASUREMENT_SCALE_CHART_LIMITER_SET_VALUE "SensorUnitMeasurementScale/SetChartLimiterIoT"

uint64_t deviceSensorsReadTempTimestamp = 0;
uint64_t deviceSensorsPublishMessageTimestamp = 0;

uint64_t deviceWiFiPublishMessageTimestamp = 0;

using namespace ART;

ESPDevice espDevice(WEBAPI_HOST, WEBAPI_PORT, WEBAPI_URI);

UnitMeasurementConverter unitMeasurementConverter;

DisplayManager displayManager;
DisplayAccessManager displayAccessManager(displayManager);
DisplayWiFiManager displayWiFiManager(displayManager, espDevice);
DisplayMQTTManager displayMQTTManager(displayManager);
DisplayNTPManager displayNTPManager(displayManager, espDevice);
DisplayTemperatureSensorManager displayTemperatureSensorManager(displayManager, espDevice, unitMeasurementConverter);

void setup() {

	Serial.begin(9600);

	// Buzzer
	pinMode(D6, OUTPUT);

	pinMode(D4, INPUT);
	pinMode(D5, INPUT);

	displayManager.begin();

	Serial.println("Iniciando...");

	// text display tests
	displayManager.display.clearDisplay();
	displayManager.display.setTextSize(1);
	displayManager.display.setTextColor(WHITE);
	displayManager.display.setCursor(0, 0);
	displayManager.display.display();

	initConfiguration();

	espDevice.begin();

	espDevice.getDeviceSensors()->begin();

	espDevice.getDeviceMQ()->setConnectedCallback(mqtt_ConnectedCallback);
	espDevice.getDeviceMQ()->setSubCallback(mqtt_SubCallback);
	espDevice.getDeviceMQ()->begin();

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
}

void mqtt_ConnectedCallback(PubSubClient* mqqt)
{
  Serial.println("[MQQT::mqtt_ConnectedCallback] initializing ...");

  if(espDevice.getDeviceInApplication()->getApplicationId() == NULL){
    subscribeNotInApplication();
  }
  else {
    subscribeInApplication();
  }

	Serial.println("[MQQT::mqtt_ConnectedCallback] Initialized with success !");
}

void subscribeNotInApplication()
{
	Serial.println("[MQQT::subscribeNotInApplication] initializing ...");

	espDevice.getDeviceMQ()->subscribeInDevice(TOPIC_SUB_ESPDEVICE_UPDATE_PIN);
	espDevice.getDeviceMQ()->subscribeInDevice(TOPIC_SUB_ESPDEVICE_INSERT_IN_APPLICATION);

	Serial.println("[MQQT::subscribeNotInApplication] Initialized with success !");
}

void unSubscribeNotInApplication()
{
	Serial.println("[MQQT::unSubscribeNotInApplication] initializing ...");

	espDevice.getDeviceMQ()->unSubscribeInDevice(TOPIC_SUB_ESPDEVICE_UPDATE_PIN);
	espDevice.getDeviceMQ()->unSubscribeInDevice(TOPIC_SUB_ESPDEVICE_INSERT_IN_APPLICATION);

	Serial.println("[MQQT::unSubscribeNotInApplication] Initialized with success !");
}

void subscribeInApplication()
{
	Serial.println("[MQQT::subscribeInApplication] initializing ...");

	espDevice.getDeviceMQ()->subscribeInDevice(TOPIC_SUB_ESPDEVICE_DELETE_FROM_APPLICATION);
	espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_ESPDEVICE_SET_LABEL);

	espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_DEVICE_NTP_SET_UTC_TIME_OFF_SET_IN_SECOND);
	espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_DEVICE_NTP_SET_UPDATE_INTERVAL_IN_MILLI_SECOND);

	espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_DEVICE_WIFI_SET_HOST_NAME);
  espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_DEVICE_WIFI_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS);

	espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_DEVICE_DEBUG_SET_REMOTE_ENABLED);
	espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_DEVICE_DEBUG_SET_RESET_CMD_ENABLED);
	espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_DEVICE_DEBUG_SET_SERIAL_ENABLED);
	espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_DEVICE_DEBUG_SET_SHOW_COLORS);
	espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_DEVICE_DEBUG_SET_SHOW_DEBUG_LEVEL);
	espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_DEVICE_DEBUG_SET_SHOW_PROFILER);
	espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_DEVICE_DEBUG_SET_SHOW_TIME);

  espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_DEVICE_SENSORS_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED); 
	espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_DEVICE_SENSORS_SET_READ_INTERVAL_IN_MILLI_SECONDS);
  espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_DEVICE_SENSORS_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS);

  espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_SENSOR_IN_DEVICE_SET_ORDINATION);

	espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_SENSOR_SET_LABEL);

	espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_SENSOR_TEMP_DS_FAMILY_SET_RESOLUTION);
  
  espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_SENSOR_TRIGGER_INSERT);  
  espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_SENSOR_TRIGGER_DELETE);  
	espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_SENSOR_TRIGGER_SET_TRIGGER_ON);	
	espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_SENSOR_TRIGGER_SET_BUZZER_ON);
  espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_SENSOR_TRIGGER_SET_TRIGGER_VALUE);

  espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_SENSOR_UNIT_MEASUREMENT_SCALE_SET_DATASHEET_UNIT_MEASUREMENT_SCALE);
	espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_SENSOR_UNIT_MEASUREMENT_SCALE_RANGE_SET_VALUE);
  espDevice.getDeviceMQ()->subscribeInApplication(TOPIC_SUB_SENSOR_UNIT_MEASUREMENT_SCALE_CHART_LIMITER_SET_VALUE);

	Serial.println("[MQQT::subscribeInApplication] Initialized with success !");
}

void unSubscribeInApplication()
{
	Serial.println("[MQQT::unSubscribeInApplication] initializing ...");

	espDevice.getDeviceMQ()->unSubscribeInDevice(TOPIC_SUB_ESPDEVICE_DELETE_FROM_APPLICATION);
	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_ESPDEVICE_SET_LABEL);

	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_DEVICE_NTP_SET_UTC_TIME_OFF_SET_IN_SECOND);
	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_DEVICE_NTP_SET_UPDATE_INTERVAL_IN_MILLI_SECOND);

	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_DEVICE_WIFI_SET_HOST_NAME);
  espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_DEVICE_WIFI_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS);
  
	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_DEVICE_DEBUG_SET_REMOTE_ENABLED);
	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_DEVICE_DEBUG_SET_RESET_CMD_ENABLED);
	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_DEVICE_DEBUG_SET_SERIAL_ENABLED);
	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_DEVICE_DEBUG_SET_SHOW_COLORS);
	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_DEVICE_DEBUG_SET_SHOW_DEBUG_LEVEL);
	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_DEVICE_DEBUG_SET_SHOW_PROFILER);
	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_DEVICE_DEBUG_SET_SHOW_TIME);

	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_DEVICE_SENSORS_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED);
	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_DEVICE_SENSORS_SET_READ_INTERVAL_IN_MILLI_SECONDS);
  espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_DEVICE_SENSORS_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS);

  espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_SENSOR_IN_DEVICE_SET_ORDINATION);
  
	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_SENSOR_SET_LABEL);
	
	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_SENSOR_TEMP_DS_FAMILY_SET_RESOLUTION);

  espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_SENSOR_TRIGGER_INSERT);  
  espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_SENSOR_TRIGGER_DELETE);  
	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_SENSOR_TRIGGER_SET_TRIGGER_ON);	
	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_SENSOR_TRIGGER_SET_BUZZER_ON);
  espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_SENSOR_TRIGGER_SET_TRIGGER_VALUE);

  espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_SENSOR_UNIT_MEASUREMENT_SCALE_SET_DATASHEET_UNIT_MEASUREMENT_SCALE);
	espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_SENSOR_UNIT_MEASUREMENT_SCALE_RANGE_SET_VALUE);
  espDevice.getDeviceMQ()->unSubscribeInApplication(TOPIC_SUB_SENSOR_UNIT_MEASUREMENT_SCALE_CHART_LIMITER_SET_VALUE);

	Serial.println("[MQQT::unSubscribeInApplication] Initialized with success !");
}

void mqtt_SubCallback(char* topic, byte* payload, unsigned int length)
{
  String topicKey = espDevice.getDeviceMQ()->getTopicKey(topic);

  if (espDevice.getDeviceDebug()->isActive(DeviceDebug::DEBUG)) {
	  espDevice.getDeviceDebug()->printf("Termometro", "mqtt_SubCallback", "Topic: %s\n", topic);	
	  espDevice.getDeviceDebug()->printf("Termometro", "mqtt_SubCallback", "Topic Key: %s\n", topicKey.c_str());
  }
  
	displayMQTTManager.printReceived(true);

	String json;

	//obtem a string do payload recebido
	for (int i = 0; i < length; i++)
	{
		char c = (char)payload[i];
		json += c;
	}

	if (topicKey == String(TOPIC_SUB_ESPDEVICE_UPDATE_PIN)) {
		displayAccessManager.updatePin(json);
	}
	if (topicKey == String(TOPIC_SUB_ESPDEVICE_INSERT_IN_APPLICATION)) {
		unSubscribeNotInApplication();
		espDevice.getDeviceInApplication()->insertInApplication(strdup(json.c_str()));
		subscribeInApplication();
	}
	if (topicKey == String(TOPIC_SUB_ESPDEVICE_DELETE_FROM_APPLICATION)) {
		unSubscribeInApplication();
		espDevice.getDeviceInApplication()->deleteFromApplication();
		subscribeNotInApplication();
	}
	if (topicKey == String(TOPIC_SUB_ESPDEVICE_SET_LABEL)) {
		espDevice.setLabel(strdup(json.c_str()));
	}

	if (topicKey == String(TOPIC_SUB_DEVICE_NTP_SET_UTC_TIME_OFF_SET_IN_SECOND)) {
		espDevice.getDeviceNTP()->setUtcTimeOffsetInSecond(json);
	}
	if (topicKey == String(TOPIC_SUB_DEVICE_NTP_SET_UPDATE_INTERVAL_IN_MILLI_SECOND)) {
		espDevice.getDeviceNTP()->setUpdateIntervalInMilliSecond(json);
	}

	if (topicKey == String(TOPIC_SUB_DEVICE_WIFI_SET_HOST_NAME)) {
		espDevice.getDeviceWiFi()->setHostName(strdup(json.c_str()));
	}
  if (topicKey == String(TOPIC_SUB_DEVICE_WIFI_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS)) {
   espDevice.getDeviceWiFi()->setPublishIntervalInMilliSeconds(strdup(json.c_str()));
  }

	if (topicKey == String(TOPIC_SUB_DEVICE_DEBUG_SET_REMOTE_ENABLED)) {
		espDevice.getDeviceDebug()->setRemoteEnabled(strdup(json.c_str()));
	}
	if (topicKey == String(TOPIC_SUB_DEVICE_DEBUG_SET_RESET_CMD_ENABLED)) {
		espDevice.getDeviceDebug()->setResetCmdEnabled(strdup(json.c_str()));
	}
	if (topicKey == String(TOPIC_SUB_DEVICE_DEBUG_SET_SERIAL_ENABLED)) {
		espDevice.getDeviceDebug()->setSerialEnabled(strdup(json.c_str()));
	}
	if (topicKey == String(TOPIC_SUB_DEVICE_DEBUG_SET_SHOW_COLORS)) {
		espDevice.getDeviceDebug()->setShowColors(strdup(json.c_str()));
	}
	if (topicKey == String(TOPIC_SUB_DEVICE_DEBUG_SET_SHOW_DEBUG_LEVEL)) {
		espDevice.getDeviceDebug()->setShowDebugLevel(strdup(json.c_str()));
	}
	if (topicKey == String(TOPIC_SUB_DEVICE_DEBUG_SET_SHOW_PROFILER)) {
		espDevice.getDeviceDebug()->setShowProfiler(strdup(json.c_str()));
	}
	if (topicKey == String(TOPIC_SUB_DEVICE_DEBUG_SET_SHOW_TIME)) {
		espDevice.getDeviceDebug()->setShowTime(strdup(json.c_str()));
	}

  if (topicKey == String(TOPIC_SUB_DEVICE_SENSORS_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED)) {
    espDevice.getDeviceSensors()->setSensorsByMQQTCallback(json);
  }
	if (topicKey == String(TOPIC_SUB_DEVICE_SENSORS_SET_READ_INTERVAL_IN_MILLI_SECONDS)) {
		espDevice.getDeviceSensors()->setReadIntervalInMilliSeconds(strdup(json.c_str()));
	}
  if (topicKey == String(TOPIC_SUB_DEVICE_SENSORS_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS)) {
   espDevice.getDeviceSensors()->setPublishIntervalInMilliSeconds(strdup(json.c_str()));
  }

  if (topicKey == String(TOPIC_SUB_SENSOR_IN_DEVICE_SET_ORDINATION)) {
   espDevice.getDeviceSensors()->setOrdination(strdup(json.c_str()));
  }

	if (topicKey == String(TOPIC_SUB_SENSOR_SET_LABEL)) {
		espDevice.getDeviceSensors()->setLabel(strdup(json.c_str()));
	}
	
	if (topicKey == String(TOPIC_SUB_SENSOR_TEMP_DS_FAMILY_SET_RESOLUTION)) {
		espDevice.getDeviceSensors()->setResolution(strdup(json.c_str()));
	}
 
  if (topicKey == String(TOPIC_SUB_SENSOR_TRIGGER_INSERT)) {
   espDevice.getDeviceSensors()->insertTrigger(strdup(json.c_str()));
  } 
  if (topicKey == String(TOPIC_SUB_SENSOR_TRIGGER_DELETE)) {
    espDevice.getDeviceSensors()->deleteTrigger(strdup(json.c_str()));
  } 
	if (topicKey == String(TOPIC_SUB_SENSOR_TRIGGER_SET_TRIGGER_ON)) {
		espDevice.getDeviceSensors()->setTriggerOn(strdup(json.c_str()));
	}	
	if (topicKey == String(TOPIC_SUB_SENSOR_TRIGGER_SET_BUZZER_ON)) {
		espDevice.getDeviceSensors()->setBuzzerOn(strdup(json.c_str()));
	}
  if (topicKey == String(TOPIC_SUB_SENSOR_TRIGGER_SET_TRIGGER_VALUE)) {
    espDevice.getDeviceSensors()->setTriggerValue(strdup(json.c_str()));
  }
  
  if (topicKey == String(TOPIC_SUB_SENSOR_UNIT_MEASUREMENT_SCALE_SET_DATASHEET_UNIT_MEASUREMENT_SCALE)) {
    espDevice.getDeviceSensors()->setDatasheetUnitMeasurementScale(strdup(json.c_str()));
  }
	if (topicKey == String(TOPIC_SUB_SENSOR_UNIT_MEASUREMENT_SCALE_RANGE_SET_VALUE)) {
		espDevice.getDeviceSensors()->setRange(strdup(json.c_str()));
	}
  if (topicKey == String(TOPIC_SUB_SENSOR_UNIT_MEASUREMENT_SCALE_CHART_LIMITER_SET_VALUE)) {
    espDevice.getDeviceSensors()->setChartLimiter(strdup(json.c_str()));
  } 
}

void loop() {

	espDevice.loop();	

	DeviceInApplication* deviceInApplication = espDevice.getDeviceInApplication();

	if (deviceInApplication != NULL && deviceInApplication->getApplicationId() == NULL) {
		displayAccessManager.loop();
		//EEPROM_writeAnything(configurationEEPROMAddr, configuration);
	}
	else {
		loopInApplication();
	}

	//keep-alive da comunicação com broker MQTT
	espDevice.getDeviceMQ()->getMQQT()->loop();

}

void loopInApplication()
{
	displayManager.display.clearDisplay();

	espDevice.getDeviceNTP()->update();

	uint64_t now = millis();

  DeviceSensors* deviceSensors = espDevice.getDeviceSensors();
  DeviceWiFi* deviceWiFi = espDevice.getDeviceWiFi();
  
	if (deviceSensors->initialized()) {
		if (now - deviceSensorsReadTempTimestamp > deviceSensors->getReadIntervalInMilliSeconds()) {
			deviceSensorsReadTempTimestamp = now;
			displayTemperatureSensorManager.printUpdate(true);
			deviceSensors->refresh();
		}
		else {
			displayTemperatureSensorManager.printUpdate(false);
		}
	}

	// MQTT
  if(espDevice.getDeviceMQ()->getMQQT()->connected()){
    loopMQQTConnected(now);
  }

	// Wifi
	displayWiFiManager.printSignal();
  
	displayManager.display.display();
}

void loopMQQTConnected(uint64_t now)
{
  DeviceMQ* deviceMQ = espDevice.getDeviceMQ();
  DeviceSensors* deviceSensors = espDevice.getDeviceSensors();
  DeviceWiFi* deviceWiFi = espDevice.getDeviceWiFi();
  
  bool mqqtPrintSent = false;
  
  if (deviceSensors->initialized()) {

    displayMQTTManager.printConnected();
    displayMQTTManager.printReceived(false);

    int deviceSensorsPublishIntervalInMilliSeconds = deviceSensors->getPublishIntervalInMilliSeconds();

    if (now - deviceSensorsPublishMessageTimestamp > deviceSensorsPublishIntervalInMilliSeconds) {
      
      deviceSensorsPublishMessageTimestamp = now;
      
      mqqtPrintSent = true;

      StaticJsonBuffer<2048> jsonBuffer;
      JsonObject& root = jsonBuffer.createObject();

      root["deviceId"] = espDevice.getDeviceId();
      root["deviceDatasheetId"] = espDevice.getDeviceDatasheetId();
      root["epochTimeUtc"] = espDevice.getDeviceNTP()->getEpochTimeUTC();

      espDevice.getDeviceSensors()->createSensorsJsonNestedArray(root);

      int messageJsonLen = root.measureLength();
      char messageJson[messageJsonLen + 1];
      root.printTo(messageJson, sizeof(messageJson));

      Serial.print("DeviceSensors enviando para o servidor (Char Len)=> ");
      Serial.println(messageJsonLen);

      deviceMQ->publishInApplication(TOPIC_PUB_DEVICE_SENSORS_MESSAGE, messageJson);
    }

    // Sensor
    displayTemperatureSensorManager.printSensors();

    // Wifi
    
    int deviceWiFiPublishIntervalInMilliSeconds = deviceWiFi->getPublishIntervalInMilliSeconds();

    if (now - deviceWiFiPublishMessageTimestamp > deviceWiFiPublishIntervalInMilliSeconds) {
      
      deviceWiFiPublishMessageTimestamp = now;
      
      mqqtPrintSent = true;

      StaticJsonBuffer<2048> jsonBuffer;
      JsonObject& root = jsonBuffer.createObject();

      root["deviceId"] = espDevice.getDeviceId();
      root["deviceDatasheetId"] = espDevice.getDeviceDatasheetId();
      root["wifiQuality"] = espDevice.getDeviceWiFi()->getQuality();
      root["epochTimeUtc"] = espDevice.getDeviceNTP()->getEpochTimeUTC();
      root["localIPAddress"] = espDevice.getDeviceWiFi()->getLocalIPAddress();

      int messageJsonLen = root.measureLength();
      char messageJson[messageJsonLen + 1];
      root.printTo(messageJson, sizeof(messageJson));

      Serial.print("DeviceWiFi enviando para o servidor (Char Len)=> ");
      Serial.println(messageJsonLen);

      deviceMQ->publishInApplication(TOPIC_PUB_DEVICE_WIFI_MESSAGE, messageJson);
    }

    displayMQTTManager.printSent(mqqtPrintSent);
  }
  
}

