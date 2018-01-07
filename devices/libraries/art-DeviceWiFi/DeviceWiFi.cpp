#include "DeviceWiFi.h"
#include "ESPDevice.h"

DeviceWiFi::DeviceWiFi(ESPDevice* espDevice)
{
	_espDevice = espDevice;	
	
	_stationMacAddress 				= strdup(WiFi.macAddress().c_str());	
	_softAPMacAddress				= strdup(WiFi.softAPmacAddress().c_str());		
}

DeviceWiFi::~DeviceWiFi()
{
	delete (_espDevice);
	delete (_stationMacAddress);
	delete (_softAPMacAddress);
	delete (_hostName);
}

void DeviceWiFi::load(JsonObject& jsonObject)
{	
	char* hostName = strdup(jsonObject["hostName"]);
	_hostName = new char(sizeof(strlen(hostName)));
	_hostName = hostName;
	
	printf("DeviceWiFi", "load", "HostName: %s\n", _hostName);
}

char* DeviceWiFi::getStationMacAddress()
{	
	return _stationMacAddress;
}

char* DeviceWiFi::getSoftAPMacAddress()
{	
	return _softAPMacAddress;
}

char* DeviceWiFi::getHostName()
{	
	return _hostName;
}
