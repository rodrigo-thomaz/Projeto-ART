#ifndef TemperatureSensorManager_h
#define TemperatureSensorManager_h

#include "Arduino.h"
#include <vector>
#include "ArduinoJson.h"
#include "TemperatureSensor.h"
#include "DebugManager.h"
#include "NTPManager.h"

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
