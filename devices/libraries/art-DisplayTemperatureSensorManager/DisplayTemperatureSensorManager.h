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
		
	void						printUpdate(bool on);	
	void						printSensors();

private:

	DisplayManager*       		_displayManager;	
	TemperatureSensorManager*   _temperatureSensorManager;
	DebugManager*         		_debugManager;

	void						printSensor(TemperatureSensor& temperatureSensor, int x, int y, int width, int height);
	
};

#endif