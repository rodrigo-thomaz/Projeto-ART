#ifndef DisplayDeviceNTP_h
#define DisplayDeviceNTP_h

#include "functional"
#include "vector"
#include "Arduino.h"
#include "DeviceNTP.h"

namespace ART
{
	class DisplayDevice;

	class DisplayDeviceNTP
	{

	public:

		DisplayDeviceNTP(DisplayDevice* displayDevice);
		~DisplayDeviceNTP();

		static void										create(DisplayDeviceNTP* (&displayDeviceNTP), DisplayDevice* displayDevice);

	private:

		DisplayDevice *									_displayDevice;

		void											printTime();
		void											printUpdate(bool on);

		void											updateCallback(bool update, bool forceUpdate);

		DEVICE_NTP_SET_UPDATE_CALLBACK_SIGNATURE		_updateCallback;

	};
}

#endif