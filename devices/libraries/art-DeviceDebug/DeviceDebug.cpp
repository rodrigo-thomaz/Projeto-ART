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

template<typename... Args> int DeviceDebug::printf(const char* className, const char* caller, const char* format, Args... args)
{
	int lenfullFormat = strlen(className) + strlen(caller) + strlen(format) + 2;		
	char fullFormat[lenfullFormat];	
	strcpy(fullFormat, className);
	strcat(fullFormat, " ");
	strcat(fullFormat, caller);
	strcat(fullFormat, " ");
	strcat(fullFormat, format);	
	return _debug->printf(fullFormat, args...);
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
		printf("DeviceDebug", "setRemoteEnabled", "Parse failed: %s\n", json);
		return;
	}	
	
	_remoteEnabled = root["value"];
	
	printf("DeviceDebug", "setRemoteEnabled", "RemoteEnabled: %s\n", _remoteEnabled ? "true" : "false");
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

	_resetCmdEnabled = root["value"];
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

	_serialEnabled = root["value"];
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

	_showColors = root["value"];
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

	_showDebugLevel = root["value"];
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

	_showProfiler = root["value"];
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

	_showTime = root["value"];
	_debug->showTime(_showTime);
	
	_debug->printf("DeviceDebug::setShowTime] ShowTime: %s\n", _showTime ? "true" : "false");
}