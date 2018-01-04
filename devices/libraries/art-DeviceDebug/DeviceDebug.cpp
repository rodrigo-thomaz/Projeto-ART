#include "DeviceDebug.h"

DeviceDebug::DeviceDebug(ESPDevice* espDevice, bool active)
{
	_espDevice = espDevice;	
	
	_active = active;
}

DeviceDebug::~DeviceDebug()
{
	delete (_espDevice);
}

ESPDevice* DeviceDebug::getESPDevice()
{	
	return _espDevice;
}

bool DeviceDebug::getActive()
{	
	return _active;
}

void DeviceDebug::setActive(String json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		Serial.print("[DeviceDebug::setActive] parse failed: ");
		Serial.println(json);
		return;
	}	

	_active = root["active"];
	
	Serial.println("[ConfigurationManager::setActive] ");
	Serial.print("Active: ");
	Serial.println(_active);	
}