#include "ESPDevice.h"

ESPDevice::ESPDevice()
{
	_chipId							= ESP.getChipId();	
	_flashChipId					= ESP.getFlashChipId();	
	_chipSize						= ESP.getFlashChipSize();	
		
	_stationMacAddress 				= strdup(WiFi.macAddress().c_str());	
	_softAPMacAddress				= strdup(WiFi.softAPmacAddress().c_str());				
}

ESPDevice::~ESPDevice()
{
	delete (_deviceId);
	delete (_stationMacAddress);
	delete (_label);
	
	delete (_deviceMQ);
	delete (_deviceNTP);
	delete (_deviceSensors);
}

void ESPDevice::load(String json)
{	
	DynamicJsonBuffer 				  jsonBuffer;
	JsonObject& jsonObject 			= jsonBuffer.parseObject(json);		
			
	_deviceId 						= strdup(jsonObject["deviceId"]);	
	_deviceDatasheetId 				= jsonObject["deviceDatasheetId"];	
	
	_label 							= strdup(jsonObject["label"]);
			
	DeviceInApplication::createDeviceInApplication(_deviceInApplication, this, jsonObject["deviceInApplication"]);		
	DeviceMQ::createDeviceMQ(_deviceMQ, this, jsonObject["deviceMQ"]);		
	DeviceNTP::createDeviceNTP(_deviceNTP, this, jsonObject["deviceNTP"]);		
	DeviceSensors::createDeviceSensors(_deviceSensors, this, jsonObject["deviceSensors"]);		
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

DeviceInApplication* ESPDevice::getDeviceInApplication()
{	
	return _deviceInApplication;
}

DeviceMQ* ESPDevice::getDeviceMQ()
{	
	return _deviceMQ;
}

DeviceNTP* ESPDevice::getDeviceNTP()
{	
	return _deviceNTP;
}
		
DeviceSensors* ESPDevice::getDeviceSensors()
{	
	return _deviceSensors;
}

RemoteDebug* ESPDevice::getDebug()
{	
	return _debug;
}