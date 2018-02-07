#ifndef DisplayDevice_h
#define DisplayDevice_h

#include "DisplayDeviceBinary.h"
#include "DisplayDeviceMQ.h"
#include "DisplayDeviceWiFiAccess.h"

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
		DisplayDeviceMQ*						getDisplayDeviceMQ();
		DisplayDeviceWiFiAccess*				getDisplayDeviceWiFiAccess();

		Adafruit_SSD1306						display;

	private:

		DisplayDeviceBinary *					_displayDeviceBinary;
		DisplayDeviceMQ *						_displayDeviceMQ;
		DisplayDeviceWiFiAccess *				_displayDeviceWiFiAccess;

	};
}

#endif