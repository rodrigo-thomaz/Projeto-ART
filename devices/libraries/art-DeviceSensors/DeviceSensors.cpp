#include "DeviceSensors.h"

DeviceSensors::DeviceSensors(ESPDevice* espDevice)
{
	this->_espDevice = espDevice;
}

DeviceSensors::~DeviceSensors()
{
}

ESPDevice* DeviceSensors::getESPDevice()
{	
	return this->_espDevice;
}

int DeviceSensors::getPublishIntervalInSeconds()
{	
	return this->_publishIntervalInSeconds;
}

void DeviceSensors::setPublishIntervalInSeconds(int value)
{	
	this->_publishIntervalInSeconds = value;
}

