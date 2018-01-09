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
		case Max : return this->_tempCelsius > this->_alarmCelsius;
		case Min : return this->_tempCelsius < this->_alarmCelsius;		
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

// Sensor

Sensor::Sensor(String dsFamilyTempSensorId, DeviceAddress deviceAddress, String family, int resolution, byte unitOfMeasurementId, TempSensorAlarm lowAlarm, TempSensorAlarm highAlarm, float lowChartLimiterCelsius, float highChartLimiterCelsius)
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

String Sensor::getDSFamilyTempSensorId()
{
	return this->_dsFamilyTempSensorId;
}

const uint8_t* Sensor::getDeviceAddress()
{
	return this->_deviceAddress.data();
}

String Sensor::getFamily()
{
	return this->_family;
}

bool Sensor::getValidFamily()
{
	return this->_validFamily;
}

int Sensor::getResolution()
{
	return this->_resolution;
}

void Sensor::setResolution(int value)
{
	this->_resolution = value;
}

byte Sensor::getUnitOfMeasurementId()
{
	return this->_unitOfMeasurementId;
}

void Sensor::setUnitOfMeasurementId(int value)
{
	this->_unitOfMeasurementId = value;
}

TempSensorAlarm& Sensor::getLowAlarm()
{
	return this->_alarms[0];
}

TempSensorAlarm& Sensor::getHighAlarm()
{
	return this->_alarms[1];
}

bool Sensor::getConnected()
{
	return this->_connected;
}

void Sensor::setConnected(bool value)
{
	this->_connected = value;
}

float Sensor::getTempCelsius()
{
	return this->_tempCelsius;
}

void Sensor::setTempCelsius(float value)
{
	this->_tempCelsius = value;
	this->_alarms[0].setTempCelsius(value);
	this->_alarms[1].setTempCelsius(value);	
}

bool Sensor::hasAlarm()
{
	return this->_alarms[0].hasAlarm() || this->_alarms[1].hasAlarm();
}

bool Sensor::hasAlarmBuzzer()
{
	return this->_alarms[0].hasAlarmBuzzer() || this->_alarms[1].hasAlarmBuzzer();
}

float Sensor::getLowChartLimiterCelsius()
{
	return this->_lowChartLimiterCelsius;
}

void Sensor::setLowChartLimiterCelsius(float value)
{
	this->_lowChartLimiterCelsius = value;
}

float Sensor::getHighChartLimiterCelsius()
{
	return this->_highChartLimiterCelsius;
}

void Sensor::setHighChartLimiterCelsius(float value)
{
	this->_highChartLimiterCelsius = value;
}


// DSFamilyTempSensorManager

DSFamilyTempSensorManager::DSFamilyTempSensorManager(ESPDevice& espDevice, BuzzerManager& buzzerManager)
{ 
	this->_espDevice = &espDevice;
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

	if(!this->_espDevice->loaded()) return false;	
	
	if(this->_initializing) return false;	
	
	PubSubClient* mqqt = _espDevice->getDeviceMQ()->getMQQT();
 
	if(!mqqt->connected()) return false;	
	
	// initializing
	
	this->_initializing = true;
	
	Serial.println("[DSFamilyTempSensorManager::initialized] initializing...]");
	
	char* deviceId = this->_espDevice->getDeviceId();      
	short deviceDatasheetId = this->_espDevice->getDeviceDatasheetId();      
	char* applicationId = this->_espDevice->getDeviceInApplication()->getApplicationId();      

	StaticJsonBuffer<DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_REQUEST_JSON_SIZE> JSONbuffer;
	JsonObject& root = JSONbuffer.createObject();
	
	root["deviceId"] = deviceId;
	root["deviceDatasheetId"] = deviceDatasheetId;
	root["applicationId"] = applicationId;
	
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
	
	_espDevice->getDeviceMQ()->publish(TOPIC_PUB_DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID, result); 
	
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
		byte 			unitOfMeasurementId 	= byte(deviceJsonObject["unitOfMeasurementId"]);		
		
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
				
		TempSensorAlarm highAlarm 				= TempSensorAlarm(highAlarmOn, highAlarmCelsius, highAlarmBuzzerOn, Max);				
		TempSensorAlarm lowAlarm 				= TempSensorAlarm(lowAlarmOn, lowAlarmCelsius, lowAlarmBuzzerOn, Min);		
		
		this->_sensors.push_back(Sensor(
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

Sensor *DSFamilyTempSensorManager::getSensors()
{
	Sensor* array = this->_sensors.data();
	return array;
}

void DSFamilyTempSensorManager::createSensorsJsonNestedArray(JsonObject& jsonObject)
{	
	JsonArray& jsonArray = jsonObject.createNestedArray("dsFamilyTempSensors");     
	for(int i = 0; i < this->_sensors.size(); ++i){	
		createSensorJsonNestedObject(this->_sensors[i], jsonArray);
	}	
}

void DSFamilyTempSensorManager::setUnitOfMeasurement(String json)
{
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		Serial.print("parse setUnitOfMeasurement failed: ");
		Serial.println(json);
		return;
	}	

	String sensorId 			= root["dsFamilyTempSensorId"];
	int value 					= root["unitOfMeasurementId"];

	Sensor& sensor 	= getSensorById(sensorId);
	sensor.setUnitOfMeasurementId(value);

	Serial.print("setUnitOfMeasurement=");
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

	String sensorId 						= root["dsFamilyTempSensorId"];
	int value 								= root["dsFamilyTempSensorResolutionId"];

	Sensor& sensor = getSensorById(sensorId);
	sensor.setResolution(value);
	_dallas.setResolution(sensor.getDeviceAddress(), value);

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

	String sensorId 						= root["dsFamilyTempSensorId"];
	bool alarmOn 							= root["alarmOn"];
	TempSensorAlarmPosition position 		= static_cast<TempSensorAlarmPosition>(root["position"].as<int>());	

	Sensor& sensor 	= getSensorById(sensorId);
	
	if(position == Max)
		sensor.getHighAlarm().setAlarmOn(alarmOn);
	else if(position == Min)
		sensor.getLowAlarm().setAlarmOn(alarmOn);
	
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

	String sensorId 						= root["dsFamilyTempSensorId"];
	float alarmCelsius 						= root["alarmCelsius"];
	TempSensorAlarmPosition position 		= static_cast<TempSensorAlarmPosition>(root["position"].as<int>());	
	
	Sensor& sensor  = getSensorById(sensorId);
	
	if(position == Max)
		sensor.getHighAlarm().setAlarmCelsius(alarmCelsius);
	else if(position == Min)
		sensor.getLowAlarm().setAlarmCelsius(alarmCelsius);
	
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

	String sensorId 						= root["dsFamilyTempSensorId"];
	bool alarmBuzzerOn 						= root["alarmBuzzerOn"];
	TempSensorAlarmPosition position 		= static_cast<TempSensorAlarmPosition>(root["position"].as<int>());	

	Sensor& sensor  = getSensorById(sensorId);
	
	if(position == Max)
		sensor.getHighAlarm().setAlarmBuzzerOn(alarmBuzzerOn);
	else if(position == Min)
		sensor.getLowAlarm().setAlarmBuzzerOn(alarmBuzzerOn);

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

	String sensorId 						= root["dsFamilyTempSensorId"];
	float chartLimiterCelsius 				= root["value"];
	TempSensorAlarmPosition position 		= static_cast<TempSensorAlarmPosition>(root["position"].as<int>());	

	Sensor& sensor  = getSensorById(sensorId);
	
	if(position == Max)
		sensor.setHighChartLimiterCelsius(chartLimiterCelsius);
	else if(position == Min)
		sensor.setLowChartLimiterCelsius(chartLimiterCelsius);

	Serial.print("[DSFamilyTempSensorManager::setChartLimiterCelsius] ");
	Serial.println(json);
}

Sensor& DSFamilyTempSensorManager::getSensorById(String sensorId) {
	for (int i = 0; i < this->_sensors.size(); ++i) {
		if (this->_sensors[i].getDSFamilyTempSensorId() == sensorId) {
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

void DSFamilyTempSensorManager::createSensorJsonNestedObject(Sensor sensor, JsonArray& root)
{	
	JsonObject& JSONencoder = root.createNestedObject();

	JSONencoder["dsFamilyTempSensorId"] = sensor.getDSFamilyTempSensorId();
	JSONencoder["isConnected"] = sensor.getConnected();
	JSONencoder["resolution"] = sensor.getResolution();
	JSONencoder["tempCelsius"] = sensor.getTempCelsius();
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