#include "DeviceInApplication.h"

DeviceInApplication::DeviceInApplication(ESPDevice* espDevice, char* applicationId)
{
	_espDevice = espDevice;	
	
	if(applicationId != "null"){
		_applicationId = new char(sizeof(strlen(applicationId)));
		_applicationId = applicationId;	
	}	
}

DeviceInApplication::~DeviceInApplication()
{
	delete (_espDevice);
	delete (_applicationId);
}

char* DeviceInApplication::getApplicationId()
{	
	return _applicationId;
}

void DeviceInApplication::setApplicationId(char* value)
{	
	_applicationId = new char(sizeof(strlen(value)));
	_applicationId = value;
}