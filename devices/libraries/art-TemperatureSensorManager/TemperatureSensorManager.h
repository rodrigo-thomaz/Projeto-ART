#ifndef TemperatureSensorManager_h
#define TemperatureSensorManager_h

#include "Arduino.h"
#include "DebugManager.h"
#include "TemperatureSensor.h"
#include "NTPManager.h"

class TemperatureSensorManager
{
  public:
    TemperatureSensorManager(DebugManager& debugManager, NTPManager& ntpManager);
	
	void begin();
	void refresh();
	
	void setResolution(String json);
	void setLowAlarm(String json);
	void setHighAlarm(String json);

	char *convertSensorsToJson();
	
	TemperatureSensor 		*Sensors;
	
  private:
	
	DebugManager*          	_debugManager;
	NTPManager*          	_ntpManager;
	const uint8_t *getDeviceAddress(String deviceAddress);
};

#endif
