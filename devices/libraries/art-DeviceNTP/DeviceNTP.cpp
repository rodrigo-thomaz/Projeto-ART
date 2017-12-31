#include "DeviceNTP.h"

DeviceNTP::DeviceNTP(ESPDevice* espDevice, char* host, int port, int utcTimeOffsetInSecond, int updateIntervalInMilliSecond)
{
	_espDevice = espDevice;	
	
	_host = new char(sizeof(strlen(host)));
	_host = host;
	
	_port = port;
	
	_utcTimeOffsetInSecond = utcTimeOffsetInSecond;
	_updateIntervalInMilliSecond = updateIntervalInMilliSecond;	
}

DeviceNTP::~DeviceNTP()
{
	delete (_espDevice);
	delete (_host);
}

ESPDevice* DeviceNTP::getESPDevice()
{	
	return _espDevice;
}

char* DeviceNTP::getHost()
{	
	return _host;
}

int DeviceNTP::getPort()
{	
	return _port;
}

int DeviceNTP::getUtcTimeOffsetInSecond()
{	
	return _utcTimeOffsetInSecond;
}

void DeviceNTP::setUtcTimeOffsetInSecond(int value)
{	
	_utcTimeOffsetInSecond = value;
}

int DeviceNTP::getUpdateIntervalInMilliSecond()
{	
	return _updateIntervalInMilliSecond;
}

void DeviceNTP::setUpdateIntervalInMilliSecond(int value)
{	
	_updateIntervalInMilliSecond = value;
}