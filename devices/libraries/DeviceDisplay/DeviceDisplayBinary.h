#ifndef DeviceDisplayBinary_h
#define DeviceDisplayBinary_h

#include "Arduino.h"

namespace ART
{
	class DeviceDisplay;

	class DeviceDisplayBinary
	{
	public:
		DeviceDisplayBinary(DeviceDisplay* deviceDisplay);
		~DeviceDisplayBinary();

		static void					create(DeviceDisplayBinary* (&deviceDisplayBinary), DeviceDisplay* deviceDisplay);

	private:

		DeviceDisplay *				_deviceDisplay;

	};
}

#endif