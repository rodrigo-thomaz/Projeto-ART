#ifndef DisplayDeviceSensors_h
#define DisplayDeviceSensors_h

#include "Arduino.h"
#include "../DisplayDevice/DisplayDevice.h"
#include "../ESPDevice/ESPDevice.h"
#include "../Sensor/Sensor.h"

#include "../UnitMeasurement/UnitMeasurementEnum.h"
#include "../UnitMeasurementConverter/UnitMeasurementConverter.h"

namespace ART
{
	class DisplayDeviceSensors
	{

	public:
		DisplayDeviceSensors(ESPDevice& espDevice);
		~DisplayDeviceSensors();

		void							printUpdate(bool on);
		void							printSensors();

	private:

		ESPDevice*  					_espDevice;

		void							printBar(Sensor* sensor, int x, int y, int width, int height);
		void							printBarValue(Sensor* sensor, int x, int y, int width, int height);
		void							printText(Sensor* sensor, int x, int y);
	};
}

#endif