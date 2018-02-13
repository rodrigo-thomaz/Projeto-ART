#include "DeviceDisplayBinary.h"
#include "DeviceDisplay.h"

namespace ART
{
	DeviceDisplayBinary::DeviceDisplayBinary(DeviceDisplay* deviceDisplay)
	{
		_deviceDisplay = deviceDisplay;
	}

	DeviceDisplayBinary::~DeviceDisplayBinary()
	{

	}

	void DeviceDisplayBinary::create(DeviceDisplayBinary* (&deviceDisplayBinary), DeviceDisplay* deviceDisplay)
	{
		deviceDisplayBinary = new DeviceDisplayBinary(deviceDisplay);
	}
}