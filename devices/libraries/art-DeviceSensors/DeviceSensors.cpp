#include "DeviceSensors.h"
#include "ESPDevice.h"

DeviceSensors::DeviceSensors(ESPDevice* espDevice, int publishIntervalInSeconds)
{
	_espDevice = espDevice;	
	_publishIntervalInSeconds = publishIntervalInSeconds;	
}

DeviceSensors::~DeviceSensors()
{
	delete (_espDevice);
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

