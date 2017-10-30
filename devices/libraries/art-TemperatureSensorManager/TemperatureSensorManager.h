#ifndef TemperatureSensorManager_h
#define TemperatureSensorManager_h

#include "Arduino.h"
#include "TemperatureSensor.h"
#include "DebugManager.h"
#include "NTPManager.h"
#include "ArduinoJson.h"

class TemperatureSensorManager
{
  public:
  
    TemperatureSensorManager(DebugManager& debugManager, NTPManager& ntpManager);
	
	void 					begin();
	
	void 					refresh();	

	TemperatureSensor 		*getSensors();
	char 					*getSensorsJson();		
	
	void 					setResolution(String json);
	void 					setLowAlarm(String json);
	void 					setHighAlarm(String json);
	
  private:
	
	DebugManager*          	_debugManager;
	NTPManager*          	_ntpManager;
	
	const uint8_t 			*getDeviceAddress(String deviceAddress);
	String 					getFamily(byte deviceAddress[8]);
	void					generateNestedSensor(TemperatureSensor temperatureSensor, JsonArray& root);
	
	TemperatureSensor* 		_sensors;
	
};

#endif
