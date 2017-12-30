#include "ESPDevice.h"

ESPDevice::ESPDevice(char* espDeviceId, short deviceDatasheetId, int chipId, int flashChipId, char* stationMacAddress, char* softAPMacAddress, char* sdkVersion, long chipSize, char* label, int publishIntervalInSeconds)
{
	_espDeviceId 					= new char(sizeof(strlen(espDeviceId)));
	_espDeviceId 					= espDeviceId;
	
	_deviceDatasheetId 				= deviceDatasheetId;	
	
	_chipId							= chipId;
	
	_flashChipId					= flashChipId;
	
	_stationMacAddress				= new char(sizeof(strlen(stationMacAddress)));
	_stationMacAddress 				= stationMacAddress;
	
	_softAPMacAddress				= new char(sizeof(strlen(softAPMacAddress)));	
	_softAPMacAddress				= softAPMacAddress;
	
    _sdkVersion						= new char(sizeof(strlen(sdkVersion)));
	_sdkVersion						= sdkVersion;
	
    _chipSize						= chipSize;	
	
	_label 							= new char(sizeof(strlen(label)));
	_label 							= label;
	
	DeviceSensors::createDeviceSensors(_deviceSensors, this, publishIntervalInSeconds);		
}

ESPDevice::~ESPDevice()
{
	delete (_espDeviceId);
	delete (_stationMacAddress);
	delete (_sdkVersion);
	delete (_label);
	delete (_deviceSensors);
}

char* ESPDevice::getESPDeviceId()
{	
	return _espDeviceId;
}

short ESPDevice::getDeviceDatasheetId()
{	
	return _deviceDatasheetId;
}

int	ESPDevice::getChipId()
{	
	return _chipId;
}

int ESPDevice::getFlashChipId()
{	
	return _flashChipId;
}

char* ESPDevice::getStationMacAddress()
{	
	return _stationMacAddress;
}

char* ESPDevice::getSoftAPMacAddress()
{	
	return _softAPMacAddress;
}

char* ESPDevice::getSDKVersion()
{	
	return _sdkVersion;
}

long ESPDevice::getChipSize()
{	
	return _chipSize;
}

char* ESPDevice::getLabel()
{	
	return _label;
}

void ESPDevice::setLabel(char* value)
{	
	_label = new char(sizeof(strlen(value)));
	_label = value;
}
		
DeviceSensors* ESPDevice::getDeviceSensors()
{	
	return _deviceSensors;
}