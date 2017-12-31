#include "DeviceMQ.h"

DeviceMQ::DeviceMQ(ESPDevice* espDevice, char* host, int port, char* user, char* password, char* clientId, char* applicationTopic, char* deviceTopic)
{
	_espDevice = espDevice;	
	
	_host = new char(sizeof(strlen(host)));
	_host = host;
	
	_port = port;

	_user = new char(sizeof(strlen(user)));
	_user = user;

	_password = new char(sizeof(strlen(password)));
	_password = password;	
	
	_clientId = new char(sizeof(strlen(clientId)));
	_clientId = clientId;
	
	_applicationTopic = new char(sizeof(strlen(applicationTopic)));
	_applicationTopic = applicationTopic;
	
	_deviceTopic = new char(sizeof(strlen(deviceTopic)));
	_deviceTopic = deviceTopic;
}

DeviceMQ::~DeviceMQ()
{
	delete (_espDevice);
	delete (_host);
	delete (_user);
	delete (_password);
	delete (_clientId);
	delete (_applicationTopic);
	delete (_deviceTopic);
}

ESPDevice* DeviceMQ::getESPDevice()
{	
	return _espDevice;
}

char* DeviceMQ::getHost()
{	
	return _host;
}

int DeviceMQ::getPort()
{	
	return _port;
}

char* DeviceMQ::getUser()
{	
	return _user;
}

char* DeviceMQ::getPassword()
{	
	return _password;
}

char* DeviceMQ::getClientId()
{	
	return _clientId;
}

char* DeviceMQ::getApplicationTopic()
{	
	return _applicationTopic;
}

void DeviceMQ::setApplicationTopic(char* value)
{	
	_applicationTopic = new char(sizeof(strlen(value)));
	_applicationTopic = value;
}

char* DeviceMQ::getDeviceTopic()
{	
	return _deviceTopic;
}