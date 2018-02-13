#ifndef DeviceDisplaySensor_h
#define DeviceDisplaySensor_h

#include "Arduino.h"

#include "../DeviceSensor/Sensor.h"

namespace ART
{
	class DeviceDisplay;

	class DeviceDisplaySensor
	{

	public:
		DeviceDisplaySensor(DeviceDisplay* deviceDisplay);
		~DeviceDisplaySensor();

		static void						create(DeviceDisplaySensor* (&deviceDisplaySensor), DeviceDisplay* deviceDisplay);

		void							printUpdate(bool on);
		void							printSensors();

	private:

		DeviceDisplay *					_deviceDisplay;

		void							printBar(Sensor* sensor, int x, int y, int width, int height);
		void							printBarValue(Sensor* sensor, int x, int y, int width, int height);
		void							printText(Sensor* sensor, int x, int y);
	};
}

#endif