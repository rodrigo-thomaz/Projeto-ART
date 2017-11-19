#ifndef DisplayTemperatureSensorManager_h
#define DisplayTemperatureSensorManager_h

#include "Arduino.h"
#include "DebugManager.h"
#include "DisplayManager.h"
#include "DSFamilyTempSensorManager.h"
#include "TemperatureScaleManager.h"
#include "TemperatureScaleConverter.h"

class DisplayTemperatureSensorManager
{
public:
	DisplayTemperatureSensorManager(DisplayManager& displayManager, DSFamilyTempSensorManager& dsFamilyTempSensorManager, DebugManager& debugManager, TemperatureScaleManager& temperatureScaleManager, TemperatureScaleConverter& temperatureScaleConverter);
	~DisplayTemperatureSensorManager();	
		
	void						printUpdate(bool on);	
	void						printSensors();

private:

	DisplayManager*       		_displayManager;	
	DSFamilyTempSensorManager*  _dsFamilyTempSensorManager;
	DebugManager*         		_debugManager;
	TemperatureScaleManager* 	_temperatureScaleManager;
	TemperatureScaleConverter*  _temperatureScaleConverter;

	void						printSensor(DSFamilyTempSensor& dsFamilyTempSensor, int x, int y, int width, int height);
	
};

#endif