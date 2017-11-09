#ifndef DisplayTemperatureSensorManager_h
#define DisplayTemperatureSensorManager_h

#include "Arduino.h"
#include "DebugManager.h"
#include "DisplayManager.h"
#include "DSFamilyTempSensorManager.h"

class DisplayTemperatureSensorManager
{
public:
	DisplayTemperatureSensorManager(DisplayManager& displayManager, DSFamilyTempSensorManager& dsFamilyTempSensorManager, DebugManager& debugManager);
	~DisplayTemperatureSensorManager();	
		
	void						printUpdate(bool on);	
	void						printSensors();

private:

	DisplayManager*       		_displayManager;	
	DSFamilyTempSensorManager*   _dsFamilyTempSensorManager;
	DebugManager*         		_debugManager;

	void						printSensor(DSFamilyTempSensor& dsFamilyTempSensor, int x, int y, int width, int height);
	
};

#endif