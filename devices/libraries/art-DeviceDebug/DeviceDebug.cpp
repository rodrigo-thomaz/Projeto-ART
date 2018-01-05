#include "DeviceDebug.h"

DeviceDebug::DeviceDebug(ESPDevice* espDevice)
{
	_espDevice 		= espDevice;	
	
	_debug			= new RemoteDebug();
}

DeviceDebug::~DeviceDebug()
{
	delete (_espDevice);
	delete (_debug);
}

void DeviceDebug::begin()
{		
	_debug->begin("remotedebug-sample"); // Initiaze the telnet server
	_debug->setResetCmdEnabled(true); // Enable the reset command	
	_debug->setSerialEnabled(true); // Setup after Debug.begin - All messages too send to serial too, and can be see in serial monitor
	//_debug->showDebugLevel(false); // To not show debug levels
	//_debug->showTime(true); // To show time
	//_debug->showProfiler(true); // To show profiler - time between messages of Debug
	// Good to "begin ...." and "end ...." messages

	_debug->showProfiler(true); // Profiler
	_debug->showColors(true); // Colors
}

void DeviceDebug::loop()
{	
    _debug->handle();
}

void DeviceDebug::load(JsonObject& jsonObject)
{	
	_active = jsonObject["active"];
}

ESPDevice* DeviceDebug::getESPDevice()
{	
	return _espDevice;
}

RemoteDebug* DeviceDebug::getDebug()
{	
	return _debug;
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