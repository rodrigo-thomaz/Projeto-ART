#include "DeviceMQ.h"
#include "ESPDevice.h"

DeviceMQ::DeviceMQ(ESPDevice* espDevice)
{
	_espDevice = espDevice;
}

DeviceMQ::~DeviceMQ()
{
	delete (_espDevice);
	delete (_host);
	delete (_user);
	delete (_password);
	delete (_clientId);	
	delete (_deviceTopic);
}

void DeviceMQ::load(JsonObject& jsonObject)
{			
	char* host = strdup(jsonObject["host"]);
	_host = new char(sizeof(strlen(host)));
	_host = host;
	
	_port = jsonObject["port"];
	
	char* user = strdup(jsonObject["user"]);
	_user = new char(sizeof(strlen(user)));
	_user = user;

	char* password = strdup(jsonObject["password"]);
	_password = new char(sizeof(strlen(password)));
	_password = password;	
	
	char* clientId = strdup(jsonObject["clientId"]);
	_clientId = new char(sizeof(strlen(clientId)));
	_clientId = clientId;
	
	char* deviceTopic = strdup(jsonObject["deviceTopic"]);
	_deviceTopic = new char(sizeof(strlen(deviceTopic)));
	_deviceTopic = deviceTopic;
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

char* DeviceMQ::getDeviceTopic()
{	
	return _deviceTopic;
}