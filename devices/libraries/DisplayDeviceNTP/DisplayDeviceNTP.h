#ifndef DisplayDeviceNTP_h
#define DisplayDeviceNTP_h

#include "Arduino.h"
#include "DisplayDevice.h"
#include "ESPDevice.h"

namespace ART
{
	class DisplayDeviceNTP
	{
	public:
		DisplayDeviceNTP(ESPDevice& espDevice);
		~DisplayDeviceNTP();

	private:

		ESPDevice*          							_espDevice;

		void											printTime();
		void											printUpdate(bool on);

		void											updateCallback(bool update, bool forceUpdate);

		DEVICE_NTP_SET_UPDATE_CALLBACK_SIGNATURE		_updateCallback;

	};
}

#endif