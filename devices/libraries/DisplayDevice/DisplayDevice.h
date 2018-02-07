#ifndef DisplayDevice_h
#define DisplayDevice_h

#include "DisplayDeviceBinary.h"

#include "Arduino.h"
#include "Adafruit_SSD1306.h"

namespace ART
{
	class DisplayDevice
	{

	public:

		DisplayDevice();
		~DisplayDevice();

		void									begin();

		DisplayDeviceBinary*					getDisplayDeviceBinary();

		Adafruit_SSD1306						display;

	private:

		DisplayDeviceBinary *					_displayDeviceBinary;

	};
}

#endif