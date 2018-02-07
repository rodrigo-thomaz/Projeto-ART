#ifndef DisplayDevice_h
#define DisplayDevice_h

#include "Arduino.h"
#include "Adafruit_SSD1306.h"

namespace ART
{
	class DisplayDevice
	{
	public:
		DisplayDevice();
		void begin();
		Adafruit_SSD1306 display;
	private:

	};
}

#endif