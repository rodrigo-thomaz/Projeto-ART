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
	char *getSensorsJson();
	void setCallback(void(*sensorInCallback)(TemperatureSensor));
  private:
	DebugManager*          _debugManager;
	NTPManager*          _ntpManager;
	void(*_sensorInCallback)(TemperatureSensor);
};

#endif
