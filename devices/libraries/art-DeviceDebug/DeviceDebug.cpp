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
	_remoteEnabled = jsonObject["remoteEnabled"];
	_resetCmdEnabled = jsonObject["resetCmdEnabled"];
	_serialEnabled = jsonObject["serialEnabled"];
	_showColors = jsonObject["showColors"];
	_showDebugLevel = jsonObject["showDebugLevel"];
	_showProfiler = jsonObject["showProfiler"];
	_showTime = jsonObject["showTime"];
}

RemoteDebug* DeviceDebug::getDebug()
{	
	return _debug;
}

bool DeviceDebug::getRemoteEnabled()
{	
	return _remoteEnabled;
}

void DeviceDebug::setRemoteEnabled(String json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		_debug->printf("DeviceDebug::setRemoteEnabled] parse failed: %s\n", json.c_str());
		return;
	}	

	_remoteEnabled = root["remoteEnabled"];
	
	_debug->printf("DeviceDebug::setRemoteEnabled] RemoteEnabled: %s\n", _remoteEnabled ? "true" : "false");
}

bool DeviceDebug::getResetCmdEnabled()
{	
	return _resetCmdEnabled;
}

void DeviceDebug::setResetCmdEnabled(String json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		_debug->printf("DeviceDebug::setResetCmdEnabled] parse failed: %s\n", json.c_str());
		return;
	}	

	_resetCmdEnabled = root["resetCmdEnabled"];
	
	_debug->printf("DeviceDebug::setResetCmdEnabled] ResetCmdEnabled: %s\n", _resetCmdEnabled ? "true" : "false");
}

bool DeviceDebug::getSerialEnabled()
{	
	return _serialEnabled;
}

void DeviceDebug::setSerialEnabled(String json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		_debug->printf("DeviceDebug::setSerialEnabled] parse failed: %s\n", json.c_str());
		return;
	}	

	_serialEnabled = root["serialEnabled"];
	
	_debug->printf("DeviceDebug::setSerialEnabled] SerialEnabled: %s\n", _serialEnabled ? "true" : "false");
}

bool DeviceDebug::getShowColors()
{	
	return _showColors;
}

void DeviceDebug::setShowColors(String json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		_debug->printf("DeviceDebug::setShowColors] parse failed: %s\n", json.c_str());
		return;
	}	

	_showColors = root["showColors"];
	
	_debug->printf("DeviceDebug::setShowColors] ShowColors: %s\n", _showColors ? "true" : "false");
}

bool DeviceDebug::getShowDebugLevel()
{	
	return _showDebugLevel;
}

void DeviceDebug::setShowDebugLevel(String json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		_debug->printf("DeviceDebug::setShowDebugLevel] parse failed: %s\n", json.c_str());
		return;
	}	

	_showDebugLevel = root["showDebugLevel"];
	
	_debug->printf("DeviceDebug::setShowDebugLevel] ShowDebugLevel: %s\n", _showDebugLevel ? "true" : "false");
}

bool DeviceDebug::getShowProfiler()
{	
	return _showProfiler;
}

void DeviceDebug::setShowProfiler(String json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		_debug->printf("DeviceDebug::setShowProfiler] parse failed: %s\n", json.c_str());
		return;
	}	

	_showProfiler = root["showProfiler"];
	
	_debug->printf("DeviceDebug::setShowProfiler] ShowProfiler: %s\n", _showProfiler ? "true" : "false");
}

bool DeviceDebug::getShowTime()
{	
	return _showTime;
}

void DeviceDebug::setShowTime(String json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		_debug->printf("DeviceDebug::setShowTime] parse failed: %s\n", json.c_str());
		return;
	}	

	_showTime = root["showTime"];
	
	_debug->printf("DeviceDebug::setShowTime] ShowTime: %s\n", _showTime ? "true" : "false");
}