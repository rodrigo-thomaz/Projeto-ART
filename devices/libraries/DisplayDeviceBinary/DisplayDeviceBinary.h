#ifndef DisplayDeviceBinary_h
#define DisplayDeviceBinary_h

#include "Arduino.h"

namespace ART
{
	class DisplayDevice;

	class DisplayDeviceBinary
	{
	public:
		DisplayDeviceBinary(DisplayDevice& displayDevice);
		~DisplayDeviceBinary();

		static void					create(DisplayDeviceBinary* (&displayDeviceBinary), DisplayDevice& displayDevice);

	private:

		DisplayDevice *				_displayDevice;

	};
}

#endif