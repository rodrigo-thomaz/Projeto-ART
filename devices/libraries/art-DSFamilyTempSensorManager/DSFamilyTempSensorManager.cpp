#include "DSFamilyTempSensorManager.h"

// Data wire is plugged into port 0
#define ONE_WIRE_BUS 0

// Setup a oneWire instance to communicate with any OneWire devices (not just Maxim/Dallas temperature ICs)
OneWire oneWire(ONE_WIRE_BUS);

// Pass our oneWire reference to Dallas Temperature. 
DallasTemperature _dallas(&oneWire);


// TempSensorAlarm

TempSensorAlarm::TempSensorAlarm(bool alarmOn, float alarmValue, bool alarmBuzzerOn, TempSensorAlarmPosition alarmPosition)
{
	this->_alarmOn = alarmOn;
	this->_alarmValue = alarmValue;
	this->_alarmBuzzerOn = alarmBuzzerOn;
	this->_alarmPosition = alarmPosition;
}

bool TempSensorAlarm::getAlarmOn()	
{
	return this->_alarmOn;
}

void TempSensorAlarm::setAlarmOn(bool value)
{
	this->_alarmOn = value;
}

float TempSensorAlarm::getAlarmValue()
{
	return this->_alarmValue;
}

void TempSensorAlarm::setAlarmValue(float value)
{
	this->_alarmValue = value;
}

bool TempSensorAlarm::getAlarmBuzzerOn()
{
	return this->_alarmBuzzerOn;
}

void TempSensorAlarm::setAlarmBuzzerOn(bool value)
{
	this->_alarmBuzzerOn = value;
}

bool TempSensorAlarm::hasAlarm()
{
	if(!this->_alarmOn) return false;
		
	switch(this->_alarmPosition)
	{
		case Low  : return this->_rawTemperature < this->_alarmValue;
		case High : return this->_rawTemperature > this->_alarmValue;
	}
}

bool TempSensorAlarm::hasAlarmBuzzer()
{
	return this->hasAlarm() && this->_alarmBuzzerOn;
}

void TempSensorAlarm::setRawTemperature(float value)
{
	this->_rawTemperature = value;
}

// DSFamilyTempSensor

DSFamilyTempSensor::DSFamilyTempSensor(String dsFamilyTempSensorId, DeviceAddress deviceAddress, String family, int resolution, byte temperatureScaleId, TempSensorAlarm lowAlarm, TempSensorAlarm highAlarm)
{
	this->_dsFamilyTempSensorId = dsFamilyTempSensorId;
	this->_family = family;
	this->_validFamily = true;
	this->_resolution = resolution;
	this->_temperatureScaleId = temperatureScaleId;
	
	for (uint8_t i = 0; i < 8; i++) this->_deviceAddress.push_back(deviceAddress[i]);	
		
	_alarms.push_back(lowAlarm);
	_alarms.push_back(highAlarm);
}

String DSFamilyTempSensor::getDSFamilyTempSensorId()
{
	return this->_dsFamilyTempSensorId;
}

const uint8_t* DSFamilyTempSensor::getDeviceAddress()
{
	return this->_deviceAddress.data();
}

String DSFamilyTempSensor::getFamily()
{
	return this->_family;
}

bool DSFamilyTempSensor::getValidFamily()
{
	return this->_validFamily;
}

int DSFamilyTempSensor::getResolution()
{
	return this->_resolution;
}

void DSFamilyTempSensor::setResolution(int value)
{
	this->_resolution = value;
}

byte DSFamilyTempSensor::getTemperatureScaleId()
{
	return this->_temperatureScaleId;
}

void DSFamilyTempSensor::setTemperatureScaleId(int value)
{
	this->_temperatureScaleId = value;
}

TempSensorAlarm& DSFamilyTempSensor::getLowAlarm()
{
	return this->_alarms[0];
}

TempSensorAlarm& DSFamilyTempSensor::getHighAlarm()
{
	return this->_alarms[1];
}

bool DSFamilyTempSensor::getConnected()
{
	return this->_connected;
}

void DSFamilyTempSensor::setConnected(bool value)
{
	this->_connected = value;
}

float DSFamilyTempSensor::getRawTemperature()
{
	return this->_rawTemperature;
}

void DSFamilyTempSensor::setRawTemperature(float value)
{
	this->_rawTemperature = value;
	this->_alarms[0].setRawTemperature(value);
	this->_alarms[1].setRawTemperature(value);	
}

float DSFamilyTempSensor::getTemperatureWithScale()
{
	return this->_rawTemperature;
}

bool DSFamilyTempSensor::hasAlarm()
{
	return this->_alarms[0].hasAlarm() || this->_alarms[1].hasAlarm();
}


// DSFamilyTempSensorManager

DSFamilyTempSensorManager::DSFamilyTempSensorManager(DebugManager& debugManager, ConfigurationManager& configurationManager, MQQTManager& mqqtManager, BuzzerManager& buzzerManager)
{ 
	this->_debugManager = &debugManager;
	this->_configurationManager = &configurationManager;
	this->_mqqtManager = &mqqtManager;
	this->_buzzerManager = &buzzerManager;
}

void DSFamilyTempSensorManager::begin()
{	
	_dallas.begin();  	
	this->initialized();
}
	
bool DSFamilyTempSensorManager::initialized()
{
	if(this->_initialized) return true;	

	if(!this->_configurationManager->initialized()) return false;	
	
	if(this->_initializing) return false;	
	
	PubSubClient* mqqt = this->_mqqtManager->getMQQT();
 
	if(!mqqt->connected()) return false;	
	
	// initializing
	
	this->_initializing = true;
	
	Serial.println("[DSFamilyTempSensorManager::initialized] initializing...]");
	
	String deviceId = this->_configurationManager->getDeviceSettings()->getDeviceId();      
	String deviceInApplicationId = this->_configurationManager->getDeviceSettings()->getDeviceInApplicationId();      

	StaticJsonBuffer<DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_REQUEST_JSON_SIZE> JSONbuffer;
	JsonObject& root = JSONbuffer.createObject();
	
	root["deviceId"] = deviceId;
	root["deviceInApplicationId"] = deviceInApplicationId;

	// device addresses prepare	
	uint8_t deviceCount = _dallas.getDeviceCount();		
	if(deviceCount > 0) {	
		JsonArray& deviceAddressJsonArray = root.createNestedArray("deviceAddresses");
		for(int i = 0; i < deviceCount; ++i){		
			DeviceAddress deviceAddress;		
			if (_dallas.getAddress(deviceAddress, i))
			{ 	
				deviceAddressJsonArray.add(this->convertDeviceAddressToString(deviceAddress));				
			}
			else{
			  Serial.print("Não foi possível encontrar um endereço para o Device: ");
			  Serial.println(i);
			}
		}
	}
	
	int len = root.measureLength();
	char result[len + 1]; 
	root.printTo(result, sizeof(result));
	
	mqqt->publish(DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_MQQT_TOPIC_PUB, result); 
	
	return true;
}

void DSFamilyTempSensorManager::setSensorsByMQQTCallback(String json)
{
	Serial.println("[DSFamilyTempSensorManager::setSensorsByMQQTCallback] Enter");
	
	this->_initialized = true;
	this->_initializing = false;

	DynamicJsonBuffer jsonBuffer;
	//StaticJsonBuffer<DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_RESPONSE_JSON_SIZE> jsonBuffer;

	JsonArray& jsonArray = jsonBuffer.parseArray(json);

	if (!jsonArray.success()) {
		Serial.print("[DSFamilyTempSensorManager::setSensorsByMQQTCallback] parse failed: ");
		Serial.println(json);
		return;
	}			

	for(JsonArray::iterator it=jsonArray.begin(); it!=jsonArray.end(); ++it) 
	{
		JsonObject& deviceJsonObject = it->as<JsonObject>();		
		
		// DeviceAddress
		DeviceAddress 	deviceAddress;			
		for (uint8_t i = 0; i < 8; i++) deviceAddress[i] = deviceJsonObject["deviceAddress"][i];
		
		String 			dsFamilyTempSensorId 	= deviceJsonObject["dsFamilyTempSensorId"];
		String 			family 					= deviceJsonObject["family"];		
		int 			resolution 				= int(deviceJsonObject["resolutionBits"]);				
		byte 			temperatureScaleId 		= byte(deviceJsonObject["temperatureScaleId"]);		
		
		JsonObject& 	lowAlarmJsonObject 		= deviceJsonObject["lowAlarm"].as<JsonObject>();	
		JsonObject& 	highAlarmJsonObject 	= deviceJsonObject["highAlarm"].as<JsonObject>();	
		
		bool 			lowAlarmOn 				= bool(lowAlarmJsonObject["alarmOn"]);
		double 			lowAlarmValue 			= double(lowAlarmJsonObject["alarmValue"]);
		bool 			lowAlarmBuzzerOn 			= bool(lowAlarmJsonObject["buzzerOn"]);
		
		bool 			highAlarmOn 			= bool(highAlarmJsonObject["alarmOn"]);
		double 			highAlarmValue 			= double(highAlarmJsonObject["alarmValue"]);
		bool 			highAlarmBuzzerOn 			= bool(highAlarmJsonObject["alarmBuzzerOn"]);
				
		TempSensorAlarm lowAlarm 				= TempSensorAlarm(lowAlarmOn, lowAlarmValue, lowAlarmBuzzerOn, Low);
		TempSensorAlarm highAlarm 				= TempSensorAlarm(highAlarmOn, highAlarmValue, highAlarmBuzzerOn, High);
		
		this->_sensors.push_back(DSFamilyTempSensor(
				dsFamilyTempSensorId,
				deviceAddress,
				family,
				resolution,
				temperatureScaleId,
				lowAlarm,
				highAlarm));
				
		_dallas.setResolution(deviceAddress, resolution);		
		_dallas.resetAlarmSearch();		
	}
				
	Serial.println("[DSFamilyTempSensorManager::setSensorsByMQQTCallback] initialized with success !");
}

void DSFamilyTempSensorManager::refresh()
{	
	bool hasAlarm = false;
	_dallas.requestTemperatures();
	for(int i = 0; i < this->_sensors.size(); ++i){		
		this->_sensors[i].setConnected(_dallas.isConnected(this->_sensors[i].getDeviceAddress()));
		this->_sensors[i].setResolution(_dallas.getResolution(this->_sensors[i].getDeviceAddress()));
		this->_sensors[i].setRawTemperature(_dallas.getTempC(this->_sensors[i].getDeviceAddress()));
		
		if(this->_sensors[i].hasAlarm()) hasAlarm = true;
	}
	if(hasAlarm){
		this->_buzzerManager->test();
	}
}

DSFamilyTempSensor *DSFamilyTempSensorManager::getSensors()
{
	DSFamilyTempSensor* array = this->_sensors.data();
	return array;
}

void DSFamilyTempSensorManager::createSensorsJsonNestedArray(JsonObject& jsonObject)
{	
	JsonArray& jsonArray = jsonObject.createNestedArray("dsFamilyTempSensors");     
	for(int i = 0; i < this->_sensors.size(); ++i){	
		createSensorJsonNestedObject(this->_sensors[i], jsonArray);
	}	
}

void DSFamilyTempSensorManager::setScale(String json)
{
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		Serial.print("parse setScale failed: ");
		Serial.println(json);
		return;
	}	

	String dsFamilyTempSensorId 			= root["dsFamilyTempSensorId"];
	int value 								= root["temperatureScaleId"];

	DSFamilyTempSensor& dsFamilyTempSensor 	= getDSFamilyTempSensorById(dsFamilyTempSensorId);
	dsFamilyTempSensor.setTemperatureScaleId(value);

	Serial.print("setScale=");
	Serial.println(json);
}

void DSFamilyTempSensorManager::setResolution(String json)
{
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		Serial.print("parse setResolution failed: ");
		Serial.println(json);
		return;
	}	

	String dsFamilyTempSensorId 			= root["dsFamilyTempSensorId"];
	int value 								= root["dsFamilyTempSensorResolutionId"];

	DSFamilyTempSensor& dsFamilyTempSensor 	= getDSFamilyTempSensorById(dsFamilyTempSensorId);
	dsFamilyTempSensor.setResolution(value);
	_dallas.setResolution(dsFamilyTempSensor.getDeviceAddress(), value);

	Serial.print("setResolution=");
	Serial.println(json);
}

void DSFamilyTempSensorManager::setAlarmOn(String json)
{
	StaticJsonBuffer<300> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);

	if (!root.success()) {
		Serial.println("parse setAlarmOn failed");
		return;
	}

	String dsFamilyTempSensorId 			= root["dsFamilyTempSensorId"];
	bool alarmOn 							= root["alarmOn"];
	TempSensorAlarmPosition position 		= static_cast<TempSensorAlarmPosition>(root["position"].as<int>());	

	DSFamilyTempSensor& dsFamilyTempSensor 	= getDSFamilyTempSensorById(dsFamilyTempSensorId);
	
	if(position == High)
		dsFamilyTempSensor.getHighAlarm().setAlarmOn(alarmOn);
	else if(position == Low)
		dsFamilyTempSensor.getLowAlarm().setAlarmOn(alarmOn);
	
	Serial.print("[DSFamilyTempSensorManager::setAlarmOn] ");
	Serial.println(json);
}

void DSFamilyTempSensorManager::setAlarmValue(String json)
{
	StaticJsonBuffer<300> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);

	if (!root.success()) {
		Serial.println("parse setAlarmValue failed");
		return;
	}

	String dsFamilyTempSensorId 			= root["dsFamilyTempSensorId"];
	float alarmValue 						= root["alarmValue"];
	TempSensorAlarmPosition position 		= static_cast<TempSensorAlarmPosition>(root["position"].as<int>());	
	
	DSFamilyTempSensor& dsFamilyTempSensor  = getDSFamilyTempSensorById(dsFamilyTempSensorId);
	
	if(position == High)
		dsFamilyTempSensor.getHighAlarm().setAlarmValue(alarmValue);
	else if(position == Low)
		dsFamilyTempSensor.getLowAlarm().setAlarmValue(alarmValue);
	
	Serial.print("[DSFamilyTempSensorManager::setAlarmValue] ");
	Serial.println(json);
}

void DSFamilyTempSensorManager::setAlarmBuzzerOn(String json)
{
	StaticJsonBuffer<300> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);

	if (!root.success()) {
		Serial.println("parse setAlarmBuzzerOn failed");
		return;
	}

	String dsFamilyTempSensorId 			= root["dsFamilyTempSensorId"];
	bool alarmBuzzerOn 						= root["alarmBuzzerOn"];
	TempSensorAlarmPosition position 		= static_cast<TempSensorAlarmPosition>(root["position"].as<int>());	

	DSFamilyTempSensor& dsFamilyTempSensor  = getDSFamilyTempSensorById(dsFamilyTempSensorId);
	
	if(position == High)
		dsFamilyTempSensor.getHighAlarm().setAlarmBuzzerOn(alarmBuzzerOn);
	else if(position == Low)
		dsFamilyTempSensor.getLowAlarm().setAlarmBuzzerOn(alarmBuzzerOn);

	Serial.print("[DSFamilyTempSensorManager::setAlarmBuzzerOn] ");
	Serial.println(json);
}

DSFamilyTempSensor& DSFamilyTempSensorManager::getDSFamilyTempSensorById(String dsFamilyTempSensorId) {
	for (int i = 0; i < this->_sensors.size(); ++i) {
		if (this->_sensors[i].getDSFamilyTempSensorId() == dsFamilyTempSensorId) {
			return this->_sensors[i];
		}
	}
}

String DSFamilyTempSensorManager::getFamily(byte deviceAddress[8]){
  switch (deviceAddress[0]) {
    case DS18S20MODEL:
      return "DS18S20";
    case DS18B20MODEL:
      return "DS18B20";
    case DS1822MODEL:
      return "DS1822";
    case DS1825MODEL:
      return "DS1825";
    case DS28EA00MODEL:
      return "DS28EA00";
    default:
      return "";
  } 
}

void DSFamilyTempSensorManager::createSensorJsonNestedObject(DSFamilyTempSensor dsFamilyTempSensor, JsonArray& root)
{	
	JsonObject& JSONencoder = root.createNestedObject();

	JSONencoder["dsFamilyTempSensorId"] = dsFamilyTempSensor.getDSFamilyTempSensorId();
	JSONencoder["isConnected"] = dsFamilyTempSensor.getConnected();
	JSONencoder["resolution"] = dsFamilyTempSensor.getResolution();
	JSONencoder["rawTemperature"] = dsFamilyTempSensor.getRawTemperature();
	
	// Temporário
	// JSONencoder["lowAlarmValue"] = dsFamilyTempSensor.getLowAlarm()->getAlarmValue();
	// JSONencoder["lowAlarmOn"] = dsFamilyTempSensor.getLowAlarm()->getAlarmOn();
	// JSONencoder["lowAlarmBuzzerOn"] = dsFamilyTempSensor.getLowAlarm()->getAlarmBuzzerOn();	
	// JSONencoder["highAlarmValue"] = dsFamilyTempSensor.getHighAlarm()->getAlarmValue();
	// JSONencoder["highAlarmOn"] = dsFamilyTempSensor.getHighAlarm()->getAlarmOn();
	// JSONencoder["highAlarmBuzzerOn"] = dsFamilyTempSensor.getHighAlarm()->getAlarmBuzzerOn();
	
	Serial.println("Low Value Low Value Low Value Low Value Low Value Low Value Low Value Low Value Low Value  Low Value  ");
	Serial.println(dsFamilyTempSensor.getLowAlarm().getAlarmValue());
	Serial.println("High Value High Value High Value High Value High Value High Value High Value High Value High Value High Value ");
	Serial.println(dsFamilyTempSensor.getHighAlarm().getAlarmValue());	
}

String DSFamilyTempSensorManager::convertDeviceAddressToString(const uint8_t* deviceAddress)
{
	String result;	
	for (uint8_t i = 0; i < 8; i++)
	{
		result += String(deviceAddress[i]);
		if(i < 7) result += ":";
	}
	return result;
}