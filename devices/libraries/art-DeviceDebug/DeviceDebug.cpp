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
	
	if(_remoteEnabled){
		_debug->begin("remotedebug-sample");
	}
	else{
		_debug->stop(); 
	}	
	
	_debug->setSerialEnabled(_serialEnabled);
	_debug->setResetCmdEnabled(_resetCmdEnabled);
	_debug->showColors(_showColors);
	_debug->showDebugLevel(_showDebugLevel);
	_debug->showProfiler(_showProfiler);
	_debug->showTime(_showTime);
	
	printf("DeviceDebug", "load", "RemoteEnabled: %s\n", _remoteEnabled ? "true" : "false");
	printf("DeviceDebug", "load", "serialEnabled: %s\n", _serialEnabled ? "true" : "false");
	printf("DeviceDebug", "load", "resetCmdEnabled: %s\n", _resetCmdEnabled ? "true" : "false");
	printf("DeviceDebug", "load", "showColors: %s\n", _showColors ? "true" : "false");
	printf("DeviceDebug", "load", "showDebugLevel: %s\n", _showDebugLevel ? "true" : "false");
	printf("DeviceDebug", "load", "showProfiler: %s\n", _showProfiler ? "true" : "false");
	printf("DeviceDebug", "load", "showTime: %s\n", _showTime ? "true" : "false");
}

bool DeviceDebug::isActive(uint8_t debugLevel) 
{
	return _debug->isActive(debugLevel);
}

int DeviceDebug::printf(const char* className, const char* caller, const char* message)
{
	char* expression = createExpression(className, caller, message);	
	return _debug->printf(expression);
}

template<typename... Args> int DeviceDebug::printf(const char* className, const char* caller, const char* format, Args... args)
{
	char* expression = createExpression(className, caller, format);
	return _debug->printf(expression, args...);
}

char* DeviceDebug::createExpression(const char* className, const char* caller, const char* expression)
{
	int len = strlen(className) + strlen(caller) + strlen(expression) + 2;		
	char message[len];	
	strcpy(message, className);
	strcat(message, " ");
	strcat(message, caller);
	strcat(message, " ");
	strcat(message, expression);	
	return message;
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
	
	if(_remoteEnabled){
		_debug->begin("remotedebug-sample");
	}
	else{
		_debug->stop(); 
	}
	
	printf("DeviceDebug", "setRemoteEnabled", "RemoteEnabled: %s\n", _remoteEnabled ? "true" : "false");
}

void DeviceDebug::setResetCmdEnabled(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		printf("DeviceDebug", "setResetCmdEnabled", "Parse failed: %s\n", json);
		return;
	}	

	_resetCmdEnabled = root["value"];
	_debug->setResetCmdEnabled(_resetCmdEnabled);
	
	printf("DeviceDebug", "setRemoteEnabled", "ResetCmdEnabled: %s\n", _resetCmdEnabled ? "true" : "false");
}

void DeviceDebug::setSerialEnabled(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		printf("DeviceDebug", "setSerialEnabled", "Parse failed: %s\n", json);
		return;
	}	

	_serialEnabled = root["value"];
	_debug->setSerialEnabled(_serialEnabled);
	
	printf("DeviceDebug", "setRemoteEnabled", "SerialEnabled: %s\n", _serialEnabled ? "true" : "false");
}

void DeviceDebug::setShowColors(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		printf("DeviceDebug", "setShowColors", "Parse failed: %s\n", json);
		return;
	}	

	_showColors = root["value"];
	_debug->showColors(_showColors);
	
	printf("DeviceDebug", "setRemoteEnabled", "ShowColors: %s\n", _showColors ? "true" : "false");
}

void DeviceDebug::setShowDebugLevel(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		printf("DeviceDebug", "setShowDebugLevel", "Parse failed: %s\n", json);
		return;
	}	

	_showDebugLevel = root["value"];
	_debug->showDebugLevel(_showDebugLevel);
	
	printf("DeviceDebug", "setRemoteEnabled", "ShowDebugLevel: %s\n", _showDebugLevel ? "true" : "false");
}

void DeviceDebug::setShowProfiler(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		printf("DeviceDebug", "setShowProfiler", "Parse failed: %s\n", json);
		return;
	}	

	_showProfiler = root["value"];
	_debug->showProfiler(_showProfiler);
	
	printf("DeviceDebug", "setRemoteEnabled", "ShowProfiler: %s\n", _showProfiler ? "true" : "false");
}

void DeviceDebug::setShowTime(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		printf("DeviceDebug", "setShowTime", "Parse failed: %s\n", json);
		return;
	}	

	_showTime = root["value"];
	_debug->showTime(_showTime);
	
	printf("DeviceDebug", "setRemoteEnabled", "ShowTime: %s\n", _showTime ? "true" : "false");
}