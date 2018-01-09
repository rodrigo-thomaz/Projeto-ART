#ifndef DisplayTemperatureSensorManager_h
#define DisplayTemperatureSensorManager_h

#include "Arduino.h"
#include "DisplayManager.h"
#include "DSFamilyTempSensorManager.h"
#include "UnitOfMeasurementConverter.h"

class DisplayTemperatureSensorManager
{
	
public:
	DisplayTemperatureSensorManager(DisplayManager& displayManager, DSFamilyTempSensorManager& dsFamilyTempSensorManager, UnitOfMeasurementConverter& unitOfMeasurementConverter);
	~DisplayTemperatureSensorManager();	
		
	void							printUpdate(bool on);	
	void							printSensors();

private:

	DisplayManager*       			_displayManager;	
	DSFamilyTempSensorManager*  	_dsFamilyTempSensorManager;
	UnitOfMeasurementConverter*  	_unitOfMeasurementConverter;

	void							printBar(Sensor& sensor, int x, int y, int width, int height);
	void							printBarValue(Sensor& sensor, int x, int y, int width, int height);
	void							printText(Sensor& sensor, int x, int y);
};

#endif