#ifndef TemperatureSensorManager_h
#define TemperatureSensorManager_h

#include "Arduino.h"
#include <vector>
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "NTPManager.h"

class TemperatureSensor
{
  public:
    TemperatureSensor();	
	String dsFamilyTempSensorId;
	byte deviceAddress[8];
	String deviceAddressStr;
	bool validFamily;
	String family;
	bool isConnected;	
	int resolution;
	float tempCelsius;
	float tempFahrenheit;
	bool hasAlarm;	
	char lowAlarm;
	char highAlarm;
	long epochTime;
};

class TemperatureSensorManager
{
  public:
  
    TemperatureSensorManager(DebugManager& debugManager, NTPManager& ntpManager);
	
	void 								begin();
				
	void 								refresh();	
			
	TemperatureSensor 					*getSensors();
	char 								*getSensorsJson();		
				
	void 								setResolution(String json);
	void 								setLowAlarm(String json);
	void 								setHighAlarm(String json);
				
  private:			
			
	DebugManager*          				_debugManager;
	NTPManager*          				_ntpManager;
				
	const uint8_t 						*getDeviceAddress(String deviceAddress);
	String 								getFamily(byte deviceAddress[8]);
	void								generateNestedSensor(TemperatureSensor temperatureSensor, JsonArray& root);
	
	std::vector<TemperatureSensor> 		_sensors;
	
};

#endif
