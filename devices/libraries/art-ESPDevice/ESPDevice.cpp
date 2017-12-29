#include "ESPDevice.h"

ESPDevice::ESPDevice()
{
	// this->_espDeviceId 			= NULL;
	// this->_deviceDatasheetId 	= NULL;
	// this->_chipId				= NULL;
	// this->_flashChipId			= NULL;	
	// this->_stationMacAddress	= NULL;
	// this->_softAPMacAddress		= NULL;	
    // this->_sdkVersion			= NULL;
    // this->_chipSize				= NULL;	
	// this->_label 				= NULL;	
	this->_deviceSensors		= NULL;
}

ESPDevice::~ESPDevice()
{
}

String ESPDevice::getESPDeviceId()
{	
	return this->_espDeviceId;
}

void ESPDevice::setESPDeviceId(String value)
{	
	this->_espDeviceId = value;
}

short ESPDevice::getDeviceDatasheetId()
{	
	return this->_deviceDatasheetId;
}

void ESPDevice::setDeviceDatasheetId(short value)
{	
	this->_deviceDatasheetId = value;
}

int	ESPDevice::getChipId()
{	
	return this->_chipId;
}
int ESPDevice::getFlashChipId()
{	
	return this->_flashChipId;
}

String ESPDevice::getStationMacAddress()
{	
	return this->_stationMacAddress;
}

String ESPDevice::getSoftAPMacAddress()
{	
	return this->_softAPMacAddress;
}

String ESPDevice::getSDKVersion()
{	
	return this->_sdkVersion;
}

long ESPDevice::getChipSize()
{	
	return this->_chipSize;
}

String ESPDevice::getLabel()
{	
	return this->_label;
}

void ESPDevice::setLabel(String value)
{	
	this->_label = value;
}
		
DeviceSensors* ESPDevice::getDeviceSensors()
{	
	return this->_deviceSensors;
}