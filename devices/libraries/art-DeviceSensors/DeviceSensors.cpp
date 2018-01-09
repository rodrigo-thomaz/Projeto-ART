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

void DeviceSensors::setPublishIntervalInMilliSeconds(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;	
	JsonObject& root = jsonBuffer.parseObject(json);	
	if (!root.success()) {
		printf("DeviceSensors", "setPublishIntervalInMilliSeconds", "Parse failed: %s\n", json);
		return;
	}		
	_publishIntervalInMilliSeconds = root["value"].as<int>();
}

