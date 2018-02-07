#include "DisplayDeviceBinary.h"

namespace ART
{
	DisplayDeviceBinary::DisplayDeviceBinary(DisplayDevice& displayDevice)
	{
		this->_displayDevice = &displayDevice;
	}

	DisplayDeviceBinary::~DisplayDeviceBinary()
	{
	}
}