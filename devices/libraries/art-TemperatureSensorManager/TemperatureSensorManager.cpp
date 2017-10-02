#include "TemperatureSensorManager.h"
#include "Arduino.h"
#include "TemperatureSensor.h"
#include "OneWire.h"
#include "DallasTemperature.h"
#include "NTPManager.h"
#include "DebugManager.h"
#include "ArduinoJson.h"

// Data wire is plugged into port 2 on the Arduino
#define ONE_WIRE_BUS 2

// Setup a oneWire instance to communicate with any OneWire devices (not just Maxim/Dallas temperature ICs)
OneWire oneWire(ONE_WIRE_BUS);

// Pass our oneWire reference to Dallas Temperature. 
DallasTemperature _dallas(&oneWire);

//TemperatureSensor *Sensors;

TemperatureSensorManager::TemperatureSensorManager(DebugManager& debugManager, NTPManager& ntpManager)
{ 
	this->_debugManager = &debugManager;
	this->_ntpManager = &ntpManager;
}

String getFamily(byte deviceAddress[8]){
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

void TemperatureSensorManager::begin()
{	
	// Start up the library
	_dallas.begin();  

	// Localizando devices
	uint8_t deviceCount;
	
	deviceCount = _dallas.getDeviceCount();
	
	Sensors = new TemperatureSensor[deviceCount]; 
	
	if (this->_debugManager->isDebug()) {
		Serial.print("Localizando devices...");
		Serial.print("Encontrado(s) ");
		Serial.print(deviceCount, DEC);
		Serial.println(" device(s).");

		// report parasite power requirements
		Serial.print("Parasite power is: ");
		if (_dallas.isParasitePowerMode()) Serial.println("ON");
		else Serial.println("OFF");
	}	
	
	for(int i = 0; i < deviceCount; ++i){
		DeviceAddress deviceAddress;
		if (_dallas.getAddress(deviceAddress, i))
		{   
		  // address
		  for (uint8_t j = 0; j < 8; j++)
		  {
			Sensors[i].deviceAddress[j] = deviceAddress[j];			
			Sensors[i].deviceAddressStr += String(Sensors[i].deviceAddress[j], HEX);	
		  }
		  
		  if (this->_debugManager->isDebug()) Serial.println(Sensors[i].deviceAddressStr);

		  //validFamily
		  Sensors[i].validFamily = _dallas.validFamily(Sensors[i].deviceAddress);

		  // family
		  Sensors[i].family = getFamily(Sensors[i].deviceAddress);     		  		  
		}
		else{
		  if (this->_debugManager->isDebug()) {
			  Serial.print("Não foi possível encontrar um endereço para o Device ");
			  Serial.println(i);
		  }		  
		}
	}
}

void TemperatureSensorManager::setCallback(void(*sensorInCallback)(TemperatureSensor))
{
	_sensorInCallback = sensorInCallback;
}

void generateNestedSensor(TemperatureSensor temperatureSensor, JsonArray& root)
{	
	JsonObject& JSONencoder = root.createNestedObject();

	JSONencoder["deviceAddress"] = temperatureSensor.deviceAddressStr;
	JSONencoder["epochTime"] = temperatureSensor.epochTime;
	JSONencoder["validFamily"] = temperatureSensor.validFamily;
	JSONencoder["family"] = temperatureSensor.family;
	JSONencoder["isConnected"] = temperatureSensor.isConnected;
	JSONencoder["resolution"] = temperatureSensor.resolution;
	JSONencoder["tempCelsius"] = temperatureSensor.tempCelsius;
	JSONencoder["tempFahrenheit"] = temperatureSensor.tempFahrenheit;
	JSONencoder["hasAlarm"] = temperatureSensor.hasAlarm;
	JSONencoder["lowAlarm"] = temperatureSensor.lowAlarm;
	JSONencoder["highAlarm"] = temperatureSensor.highAlarm;
}

char *TemperatureSensorManager::getSensorsJson()
{	
	StaticJsonBuffer<500> JSONbuffer;

	JsonArray& device = JSONbuffer.createArray();

	_dallas.requestTemperatures();

	long epochTime = this->_ntpManager->getEpochTime();

	for(int i = 0; i < sizeof(Sensors)/sizeof(int) + 1; ++i){	
		Sensors[i].isConnected = _dallas.isConnected(Sensors[i].deviceAddress);
		Sensors[i].resolution = _dallas.getResolution(Sensors[i].deviceAddress);    
		Sensors[i].tempCelsius = _dallas.getTempC(Sensors[i].deviceAddress);
		Sensors[i].tempFahrenheit = _dallas.getTempF(Sensors[i].deviceAddress);
		Sensors[i].hasAlarm = _dallas.hasAlarm(Sensors[i].deviceAddress);
		Sensors[i].lowAlarm = _dallas.getLowAlarmTemp(Sensors[i].deviceAddress);
		Sensors[i].highAlarm = _dallas.getHighAlarmTemp(Sensors[i].deviceAddress);
		Sensors[i].epochTime = epochTime;
		generateNestedSensor(Sensors[i], device);
		_sensorInCallback(Sensors[i]);
	}
	
	int len = device.measureLength();

	char result[len + 1];

	device.printTo(result, sizeof(result));

	if (this->_debugManager->isDebug()) {
		Serial.print("tamanho device ==>");
		Serial.println(len);
		Serial.print("result device ==>");
		device.printTo(Serial);
		Serial.println();
	}

    return result;
}

const uint8_t *TemperatureSensorManager::getDeviceAddress(String deviceAddressStr) {	
	for (int i = 0; i < sizeof(Sensors) / sizeof(int) + 1; ++i) {
		if (Sensors[i].deviceAddressStr == deviceAddressStr) {
			return Sensors[i].deviceAddress;
		}
	}
}

void TemperatureSensorManager::setResolution(String json)
{
	StaticJsonBuffer<200> jsonBuffer;
	
	JsonObject& root = jsonBuffer.parseObject(json);

	if (!root.success()) {
		Serial.print("parse setResolution failed: ");
		Serial.println(json);
		return;
	}

	String deviceAddressStr = root["deviceAddress"];
	int value = root["value"];

	_dallas.setResolution(getDeviceAddress(deviceAddressStr), value);

	Serial.print("setResolution=");
	Serial.println(json);
}

void TemperatureSensorManager::setLowAlarm(String json)
{
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);

	if (!root.success()) {
		Serial.println("parse setLowAlarm failed");
		return;
	}

	String deviceAddressStr = root["deviceAddress"];
	int value = root["value"];

	_dallas.setLowAlarmTemp(getDeviceAddress(deviceAddressStr), value);

	Serial.print("setLowAlarm=");
	Serial.println(json);
}

void TemperatureSensorManager::setHighAlarm(String json)
{
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);

	if (!root.success()) {
		Serial.println("parse sethighAlarm failed");
		return;
	}

	String deviceAddressStr = root["deviceAddress"];
	int value = root["value"];

	_dallas.setHighAlarmTemp(getDeviceAddress(deviceAddressStr), value);

	Serial.print("sethighAlarm=");
	Serial.println(json);
}