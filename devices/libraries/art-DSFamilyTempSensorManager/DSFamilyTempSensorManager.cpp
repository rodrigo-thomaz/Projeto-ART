#include "DSFamilyTempSensorManager.h"

// Data wire is plugged into port 0
#define ONE_WIRE_BUS 0

// Setup a oneWire instance to communicate with any OneWire devices (not just Maxim/Dallas temperature ICs)
OneWire oneWire(ONE_WIRE_BUS);

// Pass our oneWire reference to Dallas Temperature. 
DallasTemperature _dallas(&oneWire);


// TempSensorAlarm

TempSensorAlarm::TempSensorAlarm(bool alarmOn, float alarmCelsius, bool alarmBuzzerOn, TempSensorAlarmPosition alarmPosition)
{
	this->_alarmOn = alarmOn;
	this->_alarmCelsius = alarmCelsius;
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

float TempSensorAlarm::getAlarmCelsius()
{
	return this->_alarmCelsius;
}

void TempSensorAlarm::setAlarmCelsius(float value)
{
	this->_alarmCelsius = value;
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
		case Low  : return this->_tempCelsius < this->_alarmCelsius;
		case High : return this->_tempCelsius > this->_alarmCelsius;
	}
}

bool TempSensorAlarm::hasAlarmBuzzer()
{
	return this->hasAlarm() && this->_alarmBuzzerOn;
}

void TempSensorAlarm::setTempCelsius(float value)
{
	this->_tempCelsius = value;
}

// DSFamilyTempSensor

DSFamilyTempSensor::DSFamilyTempSensor(String dsFamilyTempSensorId, DeviceAddress deviceAddress, String family, int resolution, byte unitOfMeasurementId, TempSensorAlarm lowAlarm, TempSensorAlarm highAlarm, float lowChartLimiterCelsius, float highChartLimiterCelsius)
{
	this->_dsFamilyTempSensorId = dsFamilyTempSensorId;
	this->_family = family;
	this->_validFamily = true;
	this->_resolution = resolution;
	this->_unitOfMeasurementId = unitOfMeasurementId;
	this->_lowChartLimiterCelsius = lowChartLimiterCelsius;
	this->_highChartLimiterCelsius = highChartLimiterCelsius;
	
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

byte DSFamilyTempSensor::getUnitOfMeasurementId()
{
	return this->_unitOfMeasurementId;
}

void DSFamilyTempSensor::setUnitOfMeasurementId(int value)
{
	this->_unitOfMeasurementId = value;
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

float DSFamilyTempSensor::getTempCelsius()
{
	return this->_tempCelsius;
}

void DSFamilyTempSensor::setTempCelsius(float value)
{
	this->_tempCelsius = value;
	this->_alarms[0].setTempCelsius(value);
	this->_alarms[1].setTempCelsius(value);	
}

bool DSFamilyTempSensor::hasAlarm()
{
	return this->_alarms[0].hasAlarm() || this->_alarms[1].hasAlarm();
}

bool DSFamilyTempSensor::hasAlarmBuzzer()
{
	return this->_alarms[0].hasAlarmBuzzer() || this->_alarms[1].hasAlarmBuzzer();
}

float DSFamilyTempSensor::getLowChartLimiterCelsius()
{
	return this->_lowChartLimiterCelsius;
}

void DSFamilyTempSensor::setLowChartLimiterCelsius(float value)
{
	this->_lowChartLimiterCelsius = value;
}

float DSFamilyTempSensor::getHighChartLimiterCelsius()
{
	return this->_highChartLimiterCelsius;
}

void DSFamilyTempSensor::setHighChartLimiterCelsius(float value)
{
	this->_highChartLimiterCelsius = value;
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
	
	String deviceId = this->_configurationManager->getDeviceInApplication()->getDeviceId();      
	String deviceInApplicationId = this->_configurationManager->getDeviceInApplication()->getDeviceInApplicationId();      

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
	
	this->_mqqtManager->publish(TOPIC_PUB_DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID, result); 
	
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
		byte 			unitOfMeasurementId 		= byte(deviceJsonObject["unitOfMeasurementId"]);		
		
		float 			lowChartLimiterCelsius	= float(deviceJsonObject["lowChartLimiterCelsius"]);
		float 			highChartLimiterCelsius	= float(deviceJsonObject["highChartLimiterCelsius"]);
		
		JsonObject& 	lowAlarmJsonObject 		= deviceJsonObject["lowAlarm"].as<JsonObject>();	
		JsonObject& 	highAlarmJsonObject 	= deviceJsonObject["highAlarm"].as<JsonObject>();	
						
		bool 			lowAlarmOn 				= bool(lowAlarmJsonObject["alarmOn"]);
		float 			lowAlarmCelsius			= float(lowAlarmJsonObject["alarmCelsius"]);
		bool 			lowAlarmBuzzerOn 		= bool(lowAlarmJsonObject["buzzerOn"]);
		
		bool 			highAlarmOn 			= bool(highAlarmJsonObject["alarmOn"]);
		float 			highAlarmCelsius		= float(highAlarmJsonObject["alarmCelsius"]);
		bool 			highAlarmBuzzerOn 		= bool(highAlarmJsonObject["alarmBuzzerOn"]);
				
		TempSensorAlarm lowAlarm 				= TempSensorAlarm(lowAlarmOn, lowAlarmCelsius, lowAlarmBuzzerOn, Low);
		TempSensorAlarm highAlarm 				= TempSensorAlarm(highAlarmOn, highAlarmCelsius, highAlarmBuzzerOn, High);
		
		this->_sensors.push_back(DSFamilyTempSensor(
				dsFamilyTempSensorId,
				deviceAddress,
				family,
				resolution,
				unitOfMeasurementId,
				lowAlarm,
				highAlarm,
				lowChartLimiterCelsius,
				highChartLimiterCelsius));
				
		_dallas.setResolution(deviceAddress, resolution);		
		_dallas.resetAlarmSearch();		
	}
				
	Serial.println("[DSFamilyTempSensorManager::setSensorsByMQQTCallback] initialized with success !");
}

void DSFamilyTempSensorManager::refresh()
{	
	bool hasAlarm = false;
	bool hasAlarmBuzzer = false;
	
	_dallas.requestTemperatures();
	for(int i = 0; i < this->_sensors.size(); ++i){		
		this->_sensors[i].setConnected(_dallas.isConnected(this->_sensors[i].getDeviceAddress()));
		this->_sensors[i].setResolution(_dallas.getResolution(this->_sensors[i].getDeviceAddress()));
		this->_sensors[i].setTempCelsius(_dallas.getTempC(this->_sensors[i].getDeviceAddress()));
		
		if(this->_sensors[i].hasAlarm()) 		hasAlarm 		= true;
		if(this->_sensors[i].hasAlarmBuzzer()) 	hasAlarmBuzzer 	= true;
	}
	if(hasAlarmBuzzer){
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
	int value 								= root["unitOfMeasurementId"];

	DSFamilyTempSensor& dsFamilyTempSensor 	= getDSFamilyTempSensorById(dsFamilyTempSensorId);
	dsFamilyTempSensor.setUnitOfMeasurementId(value);

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

void DSFamilyTempSensorManager::setAlarmCelsius(String json)
{
	StaticJsonBuffer<300> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);

	if (!root.success()) {
		Serial.println("parse setAlarmCelsius failed");
		return;
	}

	String dsFamilyTempSensorId 			= root["dsFamilyTempSensorId"];
	float alarmCelsius 						= root["alarmCelsius"];
	TempSensorAlarmPosition position 		= static_cast<TempSensorAlarmPosition>(root["position"].as<int>());	
	
	DSFamilyTempSensor& dsFamilyTempSensor  = getDSFamilyTempSensorById(dsFamilyTempSensorId);
	
	if(position == High)
		dsFamilyTempSensor.getHighAlarm().setAlarmCelsius(alarmCelsius);
	else if(position == Low)
		dsFamilyTempSensor.getLowAlarm().setAlarmCelsius(alarmCelsius);
	
	Serial.print("[DSFamilyTempSensorManager::setAlarmCelsius] ");
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

void DSFamilyTempSensorManager::setChartLimiterCelsius(String json)
{
	StaticJsonBuffer<300> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);

	if (!root.success()) {
		Serial.println("parse setChartLimiterCelsius failed");
		return;
	}

	String dsFamilyTempSensorId 			= root["dsFamilyTempSensorId"];
	float chartLimiterCelsius 				= root["chartLimiterCelsius"];
	TempSensorAlarmPosition position 		= static_cast<TempSensorAlarmPosition>(root["position"].as<int>());	

	DSFamilyTempSensor& dsFamilyTempSensor  = getDSFamilyTempSensorById(dsFamilyTempSensorId);
	
	if(position == High)
		dsFamilyTempSensor.setHighChartLimiterCelsius(chartLimiterCelsius);
	else if(position == Low)
		dsFamilyTempSensor.setLowChartLimiterCelsius(chartLimiterCelsius);

	Serial.print("[DSFamilyTempSensorManager::setChartLimiterCelsius] ");
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
	JSONencoder["tempCelsius"] = dsFamilyTempSensor.getTempCelsius();
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