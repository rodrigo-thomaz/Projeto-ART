#include "ESPDevice.h"

ESPDevice::ESPDevice(char* deviceId, short deviceDatasheetId, char* stationMacAddress, char* softAPMacAddress, char* label, JsonObject& jsonObject)
{
	_deviceId 						= new char(sizeof(strlen(deviceId)));
	_deviceId 						= deviceId;
	
	_deviceDatasheetId 				= deviceDatasheetId;	
	
	_chipId							= ESP.getChipId();	
	_flashChipId					= ESP.getFlashChipId();	
	_chipSize						= ESP.getFlashChipSize();	
	
	_stationMacAddress				= new char(sizeof(strlen(stationMacAddress)));
	_stationMacAddress 				= stationMacAddress;
	
	_softAPMacAddress				= new char(sizeof(strlen(softAPMacAddress)));	
	_softAPMacAddress				= softAPMacAddress;    
	
	_label 							= new char(sizeof(strlen(label)));
	_label 							= label;
	
	DeviceSensors::createDeviceSensors(_deviceSensors, this, jsonObject);		
}

ESPDevice::~ESPDevice()
{
	delete (_deviceId);
	delete (_stationMacAddress);
	delete (_label);
	delete (_deviceSensors);
}

char* ESPDevice::getDeviceId()
{	
	return _deviceId;
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

long ESPDevice::getChipSize()
{	
	return _chipSize;
}

char* ESPDevice::getStationMacAddress()
{	
	return _stationMacAddress;
}

char* ESPDevice::getSoftAPMacAddress()
{	
	return _softAPMacAddress;
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