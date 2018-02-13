#ifndef DeviceDisplayNTP_h
#define DeviceDisplayNTP_h

#include "functional"
#include "vector"
#include "Arduino.h"
#include "DeviceNTP.h"

namespace ART
{
	class DeviceDisplay;

	class DeviceDisplayNTP
	{

	public:

		DeviceDisplayNTP(DeviceDisplay* deviceDisplay);
		~DeviceDisplayNTP();

		static void										create(DeviceDisplayNTP* (&deviceDisplayNTP), DeviceDisplay* deviceDisplay);

	private:

		DeviceDisplay *									_deviceDisplay;

		void											printTime();
		void											printUpdate(bool on);

		void											updateCallback(bool update, bool forceUpdate);

		DEVICE_NTP_SET_UPDATE_CALLBACK_SIGNATURE		_updateCallback;

	};
}

#endif