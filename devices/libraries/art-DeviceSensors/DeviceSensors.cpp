#include "DeviceSensors.h"
#include "ESPDevice.h"

DeviceSensors::DeviceSensors(ESPDevice* espDevice, int publishIntervalInMilliSeconds)
{
	_espDevice = espDevice;	
	_publishIntervalInMilliSeconds = publishIntervalInMilliSeconds;	
}

DeviceSensors::~DeviceSensors()
{
	delete (_espDevice);
}

int DeviceSensors::getPublishIntervalInMilliSeconds()
{	
	return _publishIntervalInMilliSeconds;
}

void DeviceSensors::setPublishIntervalInMilliSeconds(int value)
{	
	_publishIntervalInMilliSeconds = value;
}

