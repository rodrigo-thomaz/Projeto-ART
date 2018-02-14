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

#define DEVICE_DISPLAY_GET_ALL_BY_KEY_TOPIC_PUB   						"DeviceDisplay/GetAllByKeyIoT" 
#define DEVICE_DISPLAY_GET_ALL_BY_KEY_COMPLETED_TOPIC_SUB				"DeviceDisplay/GetAllByKeyCompletedIoT"

namespace ART
{
	class ESPDevice;

	class DeviceDisplay
	{

	public:

		DeviceDisplay(ESPDevice* espDevice);
		~DeviceDisplay();

		void									begin();

		void									getAllPub();
		void									getAllSub(const char* json);

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