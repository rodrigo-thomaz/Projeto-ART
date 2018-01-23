#ifndef DisplayTemperatureSensorManager_h
#define DisplayTemperatureSensorManager_h

#include "Arduino.h"
#include "DisplayManager.h"
#include "ESPDevice.h"
#include "UnitMeasurementConverter.h"

using namespace ART;

class DisplayTemperatureSensorManager
{

public:
	DisplayTemperatureSensorManager(DisplayManager& displayManager, ESPDevice& espDevice, UnitMeasurementConverter& unitMeasurementConverter);
	~DisplayTemperatureSensorManager();

	void							printUpdate(bool on);
	void							printSensors();

private:

	DisplayManager * _displayManager;
	ESPDevice*  					_espDevice;
	UnitMeasurementConverter*  		_unitMeasurementConverter;

	void							printBar(Sensor* sensor, int x, int y, int width, int height);
	void							printBarValue(Sensor* sensor, int x, int y, int width, int height);
	void							printText(Sensor* sensor, int x, int y);
};

#endif