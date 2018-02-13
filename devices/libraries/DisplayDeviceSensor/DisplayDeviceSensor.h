#ifndef DisplayDeviceSensor_h
#define DisplayDeviceSensor_h

#include "Arduino.h"

#include "../DeviceSensor/Sensor.h"

namespace ART
{
	class DisplayDevice;

	class DisplayDeviceSensor
	{

	public:
		DisplayDeviceSensor(DisplayDevice* displayDevice);
		~DisplayDeviceSensor();

		static void						create(DisplayDeviceSensor* (&displayDeviceSensor), DisplayDevice* displayDevice);

		void							printUpdate(bool on);
		void							printSensors();

	private:

		DisplayDevice *					_displayDevice;

		void							printBar(Sensor* sensor, int x, int y, int width, int height);
		void							printBarValue(Sensor* sensor, int x, int y, int width, int height);
		void							printText(Sensor* sensor, int x, int y);
	};
}

#endif