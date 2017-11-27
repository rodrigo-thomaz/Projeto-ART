#ifndef DisplayTemperatureSensorManager_h
#define DisplayTemperatureSensorManager_h

#include "Arduino.h"
#include "DebugManager.h"
#include "DisplayManager.h"
#include "DSFamilyTempSensorManager.h"
#include "UnitOfMeasurementConverter.h"

class DisplayTemperatureSensorManager
{
public:
	DisplayTemperatureSensorManager(DisplayManager& displayManager, DSFamilyTempSensorManager& dsFamilyTempSensorManager, DebugManager& debugManager, UnitOfMeasurementConverter& unitOfMeasurementConverter);
	~DisplayTemperatureSensorManager();	
		
	void						printUpdate(bool on);	
	void						printSensors();

private:

	DisplayManager*       		_displayManager;	
	DSFamilyTempSensorManager*  _dsFamilyTempSensorManager;
	DebugManager*         		_debugManager;
	UnitOfMeasurementConverter*  _unitOfMeasurementConverter;

	void						printBar(DSFamilyTempSensor& dsFamilyTempSensor, int x, int y, int width, int height);
	void						printBarValue(DSFamilyTempSensor& dsFamilyTempSensor, int x, int y, int width, int height);
	void						printText(DSFamilyTempSensor& dsFamilyTempSensor, int x, int y);
};

#endif