#include "DisplayDeviceBinary.h"

DisplayDeviceBinary::DisplayDeviceBinary(DisplayDevice& displayDevice)
{
	this->_displayDevice = &displayDevice;
}

DisplayDeviceBinary::~DisplayDeviceBinary()
{
}

