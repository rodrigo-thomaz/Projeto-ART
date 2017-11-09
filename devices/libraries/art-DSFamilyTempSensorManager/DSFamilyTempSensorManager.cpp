#include "DSFamilyTempSensorManager.h"

// Data wire is plugged into port 0
#define ONE_WIRE_BUS 0

// Setup a oneWire instance to communicate with any OneWire devices (not just Maxim/Dallas temperature ICs)
OneWire oneWire(ONE_WIRE_BUS);

// Pass our oneWire reference to Dallas Temperature. 
DallasTemperature _dallas(&oneWire);


// DSFamilyTempSensor

DSFamilyTempSensor::DSFamilyTempSensor(String dsFamilyTempSensorId, DeviceAddress deviceAddress, String family, int resolution, byte temperatureScaleId)
{
	this->_dsFamilyTempSensorId = dsFamilyTempSensorId;
	this->_family = family;
	this->_validFamily = true;
	this->_resolution = resolution;
	this->_temperatureScaleId = temperatureScaleId;
	this->_hasAlarm = false;
	this->_lowAlarm = 0;
	this->_highAlarm = 0;
	
	for (uint8_t i = 0; i < 8; i++) this->_deviceAddress.push_back(deviceAddress[i]);	
}

DSFamilyTempSensor::DSFamilyTempSensor(String dsFamilyTempSensorId, DeviceAddress deviceAddress, String family, int resolution, byte temperatureScaleId, float lowAlarm, float highAlarm)
{
	this->_dsFamilyTempSensorId = dsFamilyTempSensorId;
	this->_family = family;
	this->_validFamily = true;
	this->_resolution = resolution;
	this->_temperatureScaleId = temperatureScaleId;
	this->_hasAlarm = true;
	this->_lowAlarm = lowAlarm;
	this->_highAlarm = highAlarm;
	
	for (uint8_t i = 0; i < 8; i++) this->_deviceAddress.push_back(deviceAddress[i]);	
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

bool DSFamilyTempSensor::getHasAlarm()
{
	return this->_hasAlarm;
}

void DSFamilyTempSensor::setHasAlarm(bool value)
{
	this->_hasAlarm = value;
}

float DSFamilyTempSensor::getLowAlarm()
{
	return this->_lowAlarm;
}

void DSFamilyTempSensor::setLowAlarm(float value)
{
	this->_lowAlarm = value;
}

float DSFamilyTempSensor::getHighAlarm()
{
	return this->_highAlarm;
}

void DSFamilyTempSensor::setHighAlarm(float value)
{
	this->_highAlarm = value;
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
}

float DSFamilyTempSensor::getTemperatureWithScale()
{
	return this->_rawTemperature;
}

long DSFamilyTempSensor::getEpochTimeUtc()
{
	return this->_epochTimeUtc;
}

void DSFamilyTempSensor::setEpochTimeUtc(long value)
{
	this->_epochTimeUtc = value;
}

// DSFamilyTempSensorManager

DSFamilyTempSensorManager::DSFamilyTempSensorManager(DebugManager& debugManager, NTPManager& ntpManager)
{ 
	this->_debugManager = &debugManager;
	this->_ntpManager = &ntpManager;
}

void DSFamilyTempSensorManager::begin()
{	
	if(!this->_dallasInitialized){		
		_dallas.begin();  
		this->_dallasInitialized = true;
	}	

	// Localizando devices
	uint8_t deviceCount = _dallas.getDeviceCount();
		
	Serial.print("Localizando devices...");
	Serial.print("Encontrado(s) ");
	Serial.print(deviceCount);
	Serial.println(" device(s).");

	// report parasite power requirements
	Serial.print("Parasite power is: ");
	if (_dallas.isParasitePowerMode()) Serial.println("ON");
	else Serial.println("OFF");
	
	for(int i = 0; i < deviceCount; ++i){
		
		DeviceAddress deviceAddress;
		
		if (_dallas.getAddress(deviceAddress, i))
		{ 			
			// Print DeviceAddress
			Serial.print("Device address: ");
			for (uint8_t i = 0; i < 8; i++)
			{
				Serial.print((byte)deviceAddress[i]);
				if(i < 7) Serial.print(":");
			}
			Serial.println();
	
			//validFamily
			//bool validFamily = _dallas.validFamily(deviceAddress);
			
			String dsFamilyTempSensorId = "4fe0c742-b8a4-e711-9bee-707781d470bc";						
			int resolution = _dallas.getResolution(deviceAddress);
			byte temperatureScaleId = 1;
			String family = getFamily(deviceAddress);     		  		  

			this->_sensors.push_back(DSFamilyTempSensor(
				dsFamilyTempSensorId, 
				deviceAddress, 
				family,
				resolution,
				temperatureScaleId));
		}
		else{
		  Serial.print("Não foi possível encontrar um endereço para o Device ");
		  Serial.println(i);
		}
	}
}

void DSFamilyTempSensorManager::refresh()
{	
	_dallas.requestTemperatures();
	long epochTimeUtc = this->_ntpManager->getEpochTimeUTC();		
	for(int i = 0; i < this->_sensors.size(); ++i){		
		this->_sensors[i].setConnected(_dallas.isConnected(this->_sensors[i].getDeviceAddress()));
		this->_sensors[i].setResolution(_dallas.getResolution(this->_sensors[i].getDeviceAddress()));
		this->_sensors[i].setRawTemperature(_dallas.getTempC(this->_sensors[i].getDeviceAddress()));
		this->_sensors[i].setHasAlarm(_dallas.hasAlarm(this->_sensors[i].getDeviceAddress()));
		this->_sensors[i].setLowAlarm(_dallas.getLowAlarmTemp(this->_sensors[i].getDeviceAddress()));
		this->_sensors[i].setHighAlarm(_dallas.getHighAlarmTemp(this->_sensors[i].getDeviceAddress()));
		this->_sensors[i].setEpochTimeUtc(epochTimeUtc);
	}
}
	
DSFamilyTempSensor *DSFamilyTempSensorManager::getSensors()
{
	DSFamilyTempSensor* array = this->_sensors.data();
	return array;
}

char *DSFamilyTempSensorManager::getSensorsJson()
{	
	StaticJsonBuffer<900> JSONbuffer;

	JsonArray& device = JSONbuffer.createArray();

	for(int i = 0; i < this->_sensors.size(); ++i){	
		generateNestedSensor(this->_sensors[i], device);
	}
	
	int len = device.measureLength();

	char result[len + 1];

	device.printTo(result, sizeof(result));

	if (this->_debugManager->isDebug()) {
		Serial.print("tamanho device ==>");
		Serial.println(len);
		Serial.print("result device ==>");
		//device.printTo(Serial);  // está estourando erro aqui
		Serial.println();
	}

    return result;
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

	String dsFamilyTempSensorId = root["dsFamilyTempSensorId"];
	int value = root["dsFamilyTempSensorResolutionId"];

	_dallas.setResolution(getDeviceAddressById(dsFamilyTempSensorId), value);

	Serial.print("setResolution=");
	Serial.println(json);
}

void DSFamilyTempSensorManager::setLowAlarm(String json)
{
	StaticJsonBuffer<300> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);

	if (!root.success()) {
		Serial.println("parse setLowAlarm failed");
		return;
	}

	String dsFamilyTempSensorId = root["dsFamilyTempSensorId"];
	int value = root["dsFamilyTempSensorResolutionId"];

	_dallas.setLowAlarmTemp(getDeviceAddressById(dsFamilyTempSensorId), value);

	Serial.print("setLowAlarm=");
	Serial.println(json);
}

void DSFamilyTempSensorManager::setHighAlarm(String json)
{
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);

	if (!root.success()) {
		Serial.println("parse sethighAlarm failed");
		return;
	}

	String dsFamilyTempSensorId = root["dsFamilyTempSensorId"];
	int value = root["dsFamilyTempSensorResolutionId"];

	_dallas.setHighAlarmTemp(getDeviceAddressById(dsFamilyTempSensorId), value);

	Serial.print("sethighAlarm=");
	Serial.println(json);
}

const uint8_t* DSFamilyTempSensorManager::getDeviceAddressById(String dsFamilyTempSensorId) {
	for (int i = 0; i < this->_sensors.size(); ++i) {
		if (this->_sensors[i].getDSFamilyTempSensorId() == dsFamilyTempSensorId) {
			return this->_sensors[i].getDeviceAddress();
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

void DSFamilyTempSensorManager::generateNestedSensor(DSFamilyTempSensor dsFamilyTempSensor, JsonArray& root)
{	
	JsonObject& JSONencoder = root.createNestedObject();

	JSONencoder["deviceAddress"] = dsFamilyTempSensor.getDeviceAddress();
	JSONencoder["epochTimeUtc"] = dsFamilyTempSensor.getEpochTimeUtc();
	JSONencoder["validFamily"] = dsFamilyTempSensor.getValidFamily();
	JSONencoder["family"] = dsFamilyTempSensor.getFamily();
	JSONencoder["isConnected"] = dsFamilyTempSensor.getConnected();
	JSONencoder["resolution"] = dsFamilyTempSensor.getResolution();
	JSONencoder["rawTemperature"] = dsFamilyTempSensor.getRawTemperature();
	JSONencoder["hasAlarm"] = dsFamilyTempSensor.getHasAlarm();
	JSONencoder["lowAlarm"] = dsFamilyTempSensor.getLowAlarm();
	JSONencoder["highAlarm"] = dsFamilyTempSensor.getHighAlarm();
}