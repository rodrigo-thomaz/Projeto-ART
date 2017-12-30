#include "DeviceSensors.h"

DeviceSensors::DeviceSensors(ESPDevice* espDevice, int publishIntervalInSeconds)
{
	_espDevice = espDevice;
	_publishIntervalInSeconds = publishIntervalInSeconds;
}

DeviceSensors::~DeviceSensors()
{
}

ESPDevice* DeviceSensors::getESPDevice()
{	
	return _espDevice;
}

int DeviceSensors::getPublishIntervalInSeconds()
{	
	return _publishIntervalInSeconds;
}

void DeviceSensors::setPublishIntervalInSeconds(int value)
{	
	_publishIntervalInSeconds = value;
}

