#include "DeviceSensors.h"

DeviceSensors::DeviceSensors(ESPDevice& espDevice)
{
	this->_espDevice = &espDevice;
}

DeviceSensors::~DeviceSensors()
{
}

ESPDevice* DeviceSensors::getESPDevice()
{	
	return this->_espDevice;
}