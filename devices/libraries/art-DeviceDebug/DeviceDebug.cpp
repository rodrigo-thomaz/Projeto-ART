#include "DeviceDebug.h"
#include "ESPDevice.h"

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
	JsonObject& deviceDebugJO = jsonObject["deviceDebug"];
	
	setRemoteEnabled(deviceDebugJO["remoteEnabled"].as<bool>());
	setSerialEnabled(deviceDebugJO["serialEnabled"].as<bool>());
	setResetCmdEnabled(deviceDebugJO["resetCmdEnabled"].as<bool>());	
	setShowColors(deviceDebugJO["showColors"].as<bool>());
	setShowDebugLevel(deviceDebugJO["showDebugLevel"].as<bool>());
	setShowProfiler(deviceDebugJO["showProfiler"].as<bool>());
	setShowTime(deviceDebugJO["showTime"].as<bool>());
	
	if(_remoteEnabled){
		
		initTelnetServer();
		
		JsonObject& deviceWiFiJO = jsonObject["deviceWiFi"];
		char* hostName = strdup(deviceWiFiJO["hostName"]);
		
		_debug->begin(hostName);		
	}
}

bool DeviceDebug::isActive(uint8_t debugLevel) 
{
	return _debug->isActive(debugLevel);
}

int DeviceDebug::print(const char* className, const char* caller, const char* message)
{
	return _debug->printf(createExpression(className, caller, message).c_str());
}

template<typename... Args> int DeviceDebug::printf(const char* className, const char* caller, const char* format, Args... args)
{	
	return _debug->printf(createExpression(className, caller, format).c_str(), args...);
}

std::string DeviceDebug::createExpression(const char* className, const char* caller, const char* expression)
{
	std::string str;
	str.append(className);
	str.append(" ");
	str.append(caller);
	str.append(" ");
	str.append(expression);
	return str.c_str();
}

void DeviceDebug::setRemoteEnabled(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;
	
	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		printf("DeviceDebug", "setRemoteEnabled", "Parse failed: %s\n", json);
		return;
	}	
	
	setRemoteEnabled(root["value"].as<bool>());
	
	if(_remoteEnabled){		
			
		initTelnetServer();
			
		char* hostName = _espDevice->getDeviceWiFi()->getHostName();		
		
		_debug->begin(hostName);
	}
	else{
		_debug->stop(); 
	}	
}

void DeviceDebug::setResetCmdEnabled(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;
	JsonObject& root = jsonBuffer.parseObject(json);	
	if (!root.success()) {
		printf("DeviceDebug", "setResetCmdEnabled", "Parse failed: %s\n", json);
		return;
	}	
	setResetCmdEnabled(root["value"].as<bool>());
}

void DeviceDebug::setSerialEnabled(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;
	JsonObject& root = jsonBuffer.parseObject(json);	
	if (!root.success()) {
		printf("DeviceDebug", "setSerialEnabled", "Parse failed: %s\n", json);
		return;
	}	
	setSerialEnabled(root["value"].as<bool>());
}

void DeviceDebug::setShowColors(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;
	JsonObject& root = jsonBuffer.parseObject(json);	
	if (!root.success()) {
		printf("DeviceDebug", "setShowColors", "Parse failed: %s\n", json);
		return;
	}	
	setShowColors(root["value"].as<bool>());
}

void DeviceDebug::setShowDebugLevel(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;
	JsonObject& root = jsonBuffer.parseObject(json);	
	if (!root.success()) {
		printf("DeviceDebug", "setShowDebugLevel", "Parse failed: %s\n", json);
		return;
	}	
	setShowDebugLevel(root["value"].as<bool>());
}

void DeviceDebug::setShowProfiler(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;
	JsonObject& root = jsonBuffer.parseObject(json);	
	if (!root.success()) {
		printf("DeviceDebug", "setShowProfiler", "Parse failed: %s\n", json);
		return;
	}	
	setShowProfiler(root["value"].as<bool>());
}

void DeviceDebug::setShowTime(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;
	JsonObject& root = jsonBuffer.parseObject(json);	
	if (!root.success()) {
		printf("DeviceDebug", "setShowTime", "Parse failed: %s\n", json);
		return;
	}	
	setShowTime(root["value"].as<bool>());
}

void DeviceDebug::initTelnetServer()
{
	if(!_telnetServer){
		_telnetServer = true;
		MDNS.addService("telnet", "tcp", TELNET_PORT);
	}
}

void DeviceDebug::setRemoteEnabled(bool value)
{	
	_remoteEnabled = value;	
	printf("DeviceDebug", "load", "remoteEnabled: %s\n", _remoteEnabled ? "true" : "false");	
}

void DeviceDebug::setResetCmdEnabled(bool value)
{	
	_resetCmdEnabled = value;
	_debug->setResetCmdEnabled(_resetCmdEnabled);	
	printf("DeviceDebug", "load", "resetCmdEnabled: %s\n", _resetCmdEnabled ? "true" : "false");	
}

void DeviceDebug::setSerialEnabled(bool value)
{	
	_serialEnabled = value;
	_debug->setSerialEnabled(_serialEnabled);
	printf("DeviceDebug", "load", "serialEnabled: %s\n", _serialEnabled ? "true" : "false");
}

void DeviceDebug::setShowColors(bool value)
{	
	_showColors = value;
	_debug->showColors(_showColors);
	printf("DeviceDebug", "load", "showColors: %s\n", _showColors ? "true" : "false");	
}

void DeviceDebug::setShowDebugLevel(bool value)
{	
	_showDebugLevel = value;
	_debug->showDebugLevel(_showDebugLevel);
	printf("DeviceDebug", "load", "showDebugLevel: %s\n", _showDebugLevel ? "true" : "false");	
}

void DeviceDebug::setShowProfiler(bool value)
{	
	_showProfiler = value;
	_debug->showProfiler(_showProfiler);
	printf("DeviceDebug", "load", "showProfiler: %s\n", _showProfiler ? "true" : "false");	
}

void DeviceDebug::setShowTime(bool value)
{	
	_showTime = value;
	_debug->showTime(_showTime);
	printf("DeviceDebug", "load", "showTime: %s\n", _showTime ? "true" : "false");
}