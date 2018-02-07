#ifndef DisplayDeviceSensors_h
#define DisplayDeviceSensors_h

#include "Arduino.h"
#include "DisplayDevice.h"
#include "ESPDevice.h"
#include "UnitMeasurementConverter.h"

namespace ART
{
	class DisplayDeviceSensors
	{

	public:
		DisplayDeviceSensors(DisplayDevice& displayDevice, ESPDevice& espDevice, UnitMeasurementConverter& unitMeasurementConverter);
		~DisplayDeviceSensors();

		void							printUpdate(bool on);
		void							printSensors();

	private:

		DisplayDevice * _displayDevice;
		ESPDevice*  					_espDevice;
		UnitMeasurementConverter*  		_unitMeasurementConverter;

		void							printBar(Sensor* sensor, int x, int y, int width, int height);
		void							printBarValue(Sensor* sensor, int x, int y, int width, int height);
		void							printText(Sensor* sensor, int x, int y);
	};
}

#endif