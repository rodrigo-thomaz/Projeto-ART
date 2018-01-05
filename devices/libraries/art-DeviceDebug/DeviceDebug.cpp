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
}

void DeviceDebug::loop()
{	
    _debug->handle();
}

void DeviceDebug::load(JsonObject& jsonObject)
{	
	_remoteEnabled = jsonObject["remoteEnabled"];
	_serialEnabled = jsonObject["serialEnabled"];
	_resetCmdEnabled = jsonObject["resetCmdEnabled"];	
	_showColors = jsonObject["showColors"];
	_showDebugLevel = jsonObject["showDebugLevel"];
	_showProfiler = jsonObject["showProfiler"];
	_showTime = jsonObject["showTime"];
	
	_debug->setSerialEnabled(_serialEnabled);
	_debug->setResetCmdEnabled(_resetCmdEnabled);
	_debug->showColors(_showColors);
	_debug->showDebugLevel(_showDebugLevel);
	_debug->showProfiler(_showProfiler);
	_debug->showTime(_showTime);
}

RemoteDebug* DeviceDebug::getDebug()
{	
	return _debug;
}

bool DeviceDebug::getRemoteEnabled()
{	
	return _remoteEnabled;
}

void DeviceDebug::setRemoteEnabled(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;
	
	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		_debug->printf("DeviceDebug::setRemoteEnabled] parse failed: %s\n", json);
		return;
	}	

	_remoteEnabled = root["remoteEnabled"];
	
	_debug->printf("DeviceDebug::setRemoteEnabled] RemoteEnabled: %s\n", _remoteEnabled ? "true" : "false");
}

bool DeviceDebug::getResetCmdEnabled()
{	
	return _resetCmdEnabled;
}

void DeviceDebug::setResetCmdEnabled(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		_debug->printf("DeviceDebug::setResetCmdEnabled] parse failed: %s\n", json);
		return;
	}	

	_resetCmdEnabled = root["resetCmdEnabled"];
	_debug->setResetCmdEnabled(_resetCmdEnabled);
	
	_debug->printf("DeviceDebug::setResetCmdEnabled] ResetCmdEnabled: %s\n", _resetCmdEnabled ? "true" : "false");
}

bool DeviceDebug::getSerialEnabled()
{	
	return _serialEnabled;
}

void DeviceDebug::setSerialEnabled(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		_debug->printf("DeviceDebug::setSerialEnabled] parse failed: %s\n", json);
		return;
	}	

	_serialEnabled = root["serialEnabled"];
	_debug->setSerialEnabled(_serialEnabled);
	
	_debug->printf("DeviceDebug::setSerialEnabled] SerialEnabled: %s\n", _serialEnabled ? "true" : "false");
}

bool DeviceDebug::getShowColors()
{	
	return _showColors;
}

void DeviceDebug::setShowColors(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		_debug->printf("DeviceDebug::setShowColors] parse failed: %s\n", json);
		return;
	}	

	_showColors = root["showColors"];
	_debug->showColors(_showColors);
	
	_debug->printf("DeviceDebug::setShowColors] ShowColors: %s\n", _showColors ? "true" : "false");
}

bool DeviceDebug::getShowDebugLevel()
{	
	return _showDebugLevel;
}

void DeviceDebug::setShowDebugLevel(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		_debug->printf("DeviceDebug::setShowDebugLevel] parse failed: %s\n", json);
		return;
	}	

	_showDebugLevel = root["showDebugLevel"];
	_debug->showDebugLevel(_showDebugLevel);
	
	_debug->printf("DeviceDebug::setShowDebugLevel] ShowDebugLevel: %s\n", _showDebugLevel ? "true" : "false");
}

bool DeviceDebug::getShowProfiler()
{	
	return _showProfiler;
}

void DeviceDebug::setShowProfiler(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		_debug->printf("DeviceDebug::setShowProfiler] parse failed: %s\n", json);
		return;
	}	

	_showProfiler = root["showProfiler"];
	_debug->showProfiler(_showProfiler);
	
	_debug->printf("DeviceDebug::setShowProfiler] ShowProfiler: %s\n", _showProfiler ? "true" : "false");
}

bool DeviceDebug::getShowTime()
{	
	return _showTime;
}

void DeviceDebug::setShowTime(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		_debug->printf("DeviceDebug::setShowTime] parse failed: %s\n", json);
		return;
	}	

	_showTime = root["showTime"];
	_debug->showTime(_showTime);
	
	_debug->printf("DeviceDebug::setShowTime] ShowTime: %s\n", _showTime ? "true" : "false");
}