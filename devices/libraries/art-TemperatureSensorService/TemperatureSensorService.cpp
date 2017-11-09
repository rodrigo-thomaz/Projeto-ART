#include "TemperatureSensorService.h"

// Data wire is plugged into port 2 on the Arduino
//#define ONE_WIRE_BUS 0

// Setup a oneWire instance to communicate with any OneWire devices (not just Maxim/Dallas temperature ICs)
//OneWire oneWire(ONE_WIRE_BUS);

// Pass our oneWire reference to Dallas Temperature. 
//DallasTemperature _dallas(&oneWire);


// TemperatureSensor

TemperatureSensor2::TemperatureSensor2(String dsFamilyTempSensorId, byte deviceAddress[8], String family, int resolution, byte temperatureScaleId)
{
	this->_dsFamilyTempSensorId = dsFamilyTempSensorId;
	this->_deviceAddress = deviceAddress;
	this->_family = family;
	this->_validFamily = true;
	this->_resolution = resolution;
	this->_temperatureScaleId = temperatureScaleId;
	this->_hasAlarm = false;
	this->_lowAlarm = 0;
	this->_highAlarm = 0;
}

TemperatureSensor2::TemperatureSensor2(String dsFamilyTempSensorId, byte deviceAddress[8], String family, int resolution, byte temperatureScaleId, float lowAlarm, float highAlarm)
{
	this->_dsFamilyTempSensorId = dsFamilyTempSensorId;
	this->_deviceAddress = deviceAddress;
	this->_family = family;
	this->_validFamily = true;
	this->_resolution = resolution;
	this->_temperatureScaleId = temperatureScaleId;
	this->_hasAlarm = true;
	this->_lowAlarm = lowAlarm;
	this->_highAlarm = highAlarm;
}

String TemperatureSensor2::getDSFamilyTempSensorId()
{
	return this->_dsFamilyTempSensorId;
}

byte* TemperatureSensor2::getDeviceAddress()
{
	return this->_deviceAddress;
}

String TemperatureSensor2::getFamily()
{
	return this->_family;
}

bool TemperatureSensor2::getValidFamily()
{
	return this->_validFamily;
}

int TemperatureSensor2::getResolution()
{
	return this->_resolution;
}

void TemperatureSensor2::setResolution(int value)
{
	this->_resolution = value;
}

byte TemperatureSensor2::getTemperatureScaleId()
{
	return this->_temperatureScaleId;
}

bool TemperatureSensor2::getHasAlarm()
{
	return this->_hasAlarm;
}

void TemperatureSensor2::setHasAlarm(bool value)
{
	this->_hasAlarm = value;
}

float TemperatureSensor2::getLowAlarm()
{
	return this->_lowAlarm;
}

void TemperatureSensor2::setLowAlarm(float value)
{
	this->_lowAlarm = value;
}

float TemperatureSensor2::getHighAlarm()
{
	return this->_highAlarm;
}

void TemperatureSensor2::setHighAlarm(float value)
{
	this->_highAlarm = value;
}

bool TemperatureSensor2::getConnected()
{
	return this->_connected;
}

void TemperatureSensor2::setConnected(bool value)
{
	this->_connected = value;
}

float TemperatureSensor2::getRawTemperature()
{
	return this->_rawTemperature;
}

void TemperatureSensor2::setRawTemperature(float value)
{
	this->_rawTemperature = value;
}

long TemperatureSensor2::getEpochTimeUtc()
{
	return this->_epochTimeUtc;
}

void TemperatureSensor2::setEpochTimeUtc(long value)
{
	this->_epochTimeUtc = value;
}

// TemperatureSensorService

TemperatureSensorService::TemperatureSensorService(DebugManager& debugManager, NTPManager& ntpManager, ConfigurationManager& configurationManager, MQQTManager& mqqtManager)
{ 
	this->_debugManager = &debugManager;
	this->_ntpManager = &ntpManager;
	this->_configurationManager = &configurationManager;
	this->_mqqtManager = &mqqtManager;
}

bool TemperatureSensorService::begin()
{	
	if(this->_begin) return true;	

	if(!this->_configurationManager->initialized()) return false;	
	
	if(this->_beginning) return false;	
	
	PubSubClient* mqqt = this->_mqqtManager->getMQQT();
 
	if(!mqqt->connected()) return false;	
	
	// Begin
	
	this->_beginning = true;
	
	String hardwareId = this->_configurationManager->getHardwareSettings()->getHardwareId();      
	String hardwareInApplicationId = this->_configurationManager->getHardwareSettings()->getHardwareInApplicationId();      

	StaticJsonBuffer<TEMPERATURE_SENSOR_GET_ALL_BY_HARDWARE_IN_APPLICATION_ID_REQUEST_JSON_SIZE> JSONbuffer;
	JsonObject& root = JSONbuffer.createObject();
	root["hardwareId"] = hardwareId;
	root["hardwareInApplicationId"] = hardwareInApplicationId;

	int len = root.measureLength();
	char result[len + 1]; 
	root.printTo(result, sizeof(result));
	
	Serial.println("[TemperatureSensorService::begin] beginning...]");
	
	mqqt->publish(TEMPERATURE_SENSOR_GET_ALL_BY_HARDWARE_IN_APPLICATION_ID_MQQT_TOPIC_PUB, result); 
	
	return true;	
}

void TemperatureSensorService::setSensorsByMQQTCallback(String json)
{
	Serial.println("[TemperatureSensorService::setSensorsByMQQTCallback]");
	
	this->_begin = true;
	this->_beginning = false;

	DynamicJsonBuffer jsonBuffer;
	//StaticJsonBuffer<TEMPERATURE_SENSOR_GET_ALL_BY_HARDWARE_IN_APPLICATION_ID_RESPONSE_JSON_SIZE> jsonBuffer;

	JsonArray& jsonArray = jsonBuffer.parseArray(json);

	if (!jsonArray.success()) {
		Serial.print("[TemperatureSensorService::setSensorsByMQQTCallback] parse failed: ");
		Serial.println(json);
		return;
	}			

	for(JsonArray::iterator it=jsonArray.begin(); it!=jsonArray.end(); ++it) 
	{
		Serial.println("EntrouEntrouEntrouEntrouEntrouEntrouEntrouEntrouEntrouEntrouEntrouEntrouEntrouEntrouEntrou");
		
		JsonObject& root = it->as<JsonObject>();		
		
		root.printTo(Serial);
		
		byte deviceAddress[8];			
		for (uint8_t i = 0; i < 8; i++)
		{
			deviceAddress[i] = root["deviceAddress"][i];
			Serial.println(deviceAddress[i]);
		}		
		
		String dsFamilyTempSensorId = root["dsFamilyTempSensorId"];
		String family = root["family"];		
		int resolution = int(root["resolutionBits"]);				
		byte temperatureScaleId = byte(root["temperatureScaleId"]);			
		
		bool hasAlarm = bool(root["hasAlarm"]);
		
		if(hasAlarm){
			
			double lowAlarm = double(root["lowAlarm"]);				
			double highAlarm = double(root["highAlarm"]);			
			
			this->_sensors.push_back(TemperatureSensor2(
				dsFamilyTempSensorId,
				deviceAddress,
				family,
				resolution,
				temperatureScaleId,
				lowAlarm,
				highAlarm));
		}
		else{			
			this->_sensors.push_back(TemperatureSensor2(
				dsFamilyTempSensorId,
				deviceAddress,
				family,
				resolution,
				temperatureScaleId));				
		}
	}
				
	Serial.println("[TemperatureSensorService:: begin]");
}
