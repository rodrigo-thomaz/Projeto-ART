#ifndef DisplayDeviceSensors_h
#define DisplayDeviceSensors_h

#include "Arduino.h"

#include "../Sensor/Sensor.h"

namespace ART
{
	class DisplayDevice;

	class DisplayDeviceSensors
	{

	public:
		DisplayDeviceSensors(DisplayDevice* displayDevice);
		~DisplayDeviceSensors();

		static void						create(DisplayDeviceSensors* (&displayDeviceSensors), DisplayDevice* displayDevice);

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