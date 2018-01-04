#include "DeviceInApplication.h"

DeviceInApplication::DeviceInApplication(ESPDevice* espDevice, char* applicationId, char* applicationTopic)
{
	_espDevice = espDevice;	
	
	if(applicationId != "null"){
		_applicationId = new char(sizeof(strlen(applicationId)));
		_applicationId = applicationId;	
	}	
	
	if(applicationTopic != "null"){
		_applicationTopic = new char(sizeof(strlen(applicationTopic)));
		_applicationTopic = applicationTopic;
	}
}

DeviceInApplication::~DeviceInApplication()
{
	delete (_espDevice);
	delete (_applicationId);
	delete (_applicationTopic);
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

char* DeviceInApplication::getApplicationTopic()
{	
	return _applicationTopic;
}

void DeviceInApplication::setApplicationTopic(char* value)
{	
	_applicationTopic = new char(sizeof(strlen(value)));
	_applicationTopic = value;
}