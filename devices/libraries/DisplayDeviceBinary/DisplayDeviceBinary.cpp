#include "DisplayDeviceBinary.h"
#include "DisplayDevice.h"

namespace ART
{
	DisplayDeviceBinary::DisplayDeviceBinary(DisplayDevice& displayDevice)
	{
		_displayDevice = &displayDevice;
	}

	DisplayDeviceBinary::~DisplayDeviceBinary()
	{

	}

	void DisplayDeviceBinary::create(DisplayDeviceBinary* (&displayDeviceBinary), DisplayDevice& displayDevice)
	{
		displayDeviceBinary = new DisplayDeviceBinary(displayDevice);
	}
}