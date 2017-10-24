#ifndef DisplayTemperatureSensorManager_h
#define DisplayTemperatureSensorManager_h

#include "Arduino.h"
#include "DebugManager.h"
#include "DisplayManager.h"
#include "TemperatureSensorManager.h"

class DisplayTemperatureSensorManager
{
public:
	DisplayTemperatureSensorManager(DisplayManager& displayManager, TemperatureSensorManager& temperatureSensorManager, DebugManager& debugManager);
	~DisplayTemperatureSensorManager();	
	
	void						printSensors();
	
private:

	DisplayManager*       		_displayManager;	
	TemperatureSensorManager*   _temperatureSensorManager;
	DebugManager*         		_debugManager;

};

#endif