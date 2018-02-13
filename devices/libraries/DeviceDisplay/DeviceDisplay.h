#ifndef DeviceDisplay_h
#define DeviceDisplay_h

#include "DeviceDisplayBinary.h"
#include "DeviceDisplayMQ.h"
#include "DeviceDisplayWiFiAccess.h"
#include "DeviceDisplayNTP.h"
#include "DeviceDisplayWiFi.h"
#include "DeviceDisplaySensor.h"

#include "Arduino.h"
#include "Adafruit_SSD1306.h"

namespace ART
{
	class ESPDevice;

	class DeviceDisplay
	{

	public:

		DeviceDisplay(ESPDevice* espDevice);
		~DeviceDisplay();

		static void								create(DeviceDisplay* (&deviceDisplay), ESPDevice* espDevice);

		void									begin();

		ESPDevice *								getESPDevice();

		DeviceDisplayBinary*					getDeviceDisplayBinary();
		DeviceDisplayMQ*						getDeviceDisplayMQ();
		DeviceDisplayWiFiAccess*				getDeviceDisplayWiFiAccess();
		DeviceDisplayNTP*						getDeviceDisplayNTP();
		DeviceDisplayWiFi*						getDeviceDisplayWiFi();
		DeviceDisplaySensor*					getDeviceDisplaySensor();

		Adafruit_SSD1306						display;

	private:

		ESPDevice *								_espDevice;

		DeviceDisplayBinary *					_deviceDisplayBinary;
		DeviceDisplayMQ *						_deviceDisplayMQ;
		DeviceDisplayWiFiAccess *				_deviceDisplayWiFiAccess;
		DeviceDisplayNTP *						_deviceDisplayNTP;
		DeviceDisplayWiFi *						_deviceDisplayWiFi;
		DeviceDisplaySensor *					_deviceDisplaySensor;
	};
}

#endif