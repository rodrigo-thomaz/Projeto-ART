#include "ESPDevice.h"

ESPDevice::ESPDevice(String espDeviceId, short deviceDatasheetId, String label)
{
	this->_espDeviceId 			= espDeviceId;
	this->_deviceDatasheetId 	= deviceDatasheetId;
	this->_label 				= label;
	
	this->_deviceSensors		= NULL;
}

ESPDevice::~ESPDevice()
{
}

DeviceSensors* ESPDevice::getDeviceSensors()
{	
	return this->_deviceSensors;
}