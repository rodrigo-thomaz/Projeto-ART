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
	//char* hostName = _espDevice->getDeviceWiFi().getHostName();
	//Serial.println("HostBName::: ");
	//Serial.println(hostName);
	
	if (MDNS.begin("remotedebug-sample")) {
		Serial.print("* MDNS responder started. Hostname -> ");
		Serial.println("remotedebug-sample");
	}

	MDNS.addService("telnet", "tcp", 23);
}

void DeviceDebug::loop()
{	
    _debug->handle();
}

void DeviceDebug::load(JsonObject& jsonObject)
{	
	JsonObject& deviceDebugJO = jsonObject["deviceDebug"];
	
	_telnetTCPPort = deviceDebugJO["telnetTCPPort"];
	_remoteEnabled = deviceDebugJO["remoteEnabled"];
	_serialEnabled = deviceDebugJO["serialEnabled"];
	_resetCmdEnabled = deviceDebugJO["resetCmdEnabled"];	
	_showColors = deviceDebugJO["showColors"];
	_showDebugLevel = deviceDebugJO["showDebugLevel"];
	_showProfiler = deviceDebugJO["showProfiler"];
	_showTime = deviceDebugJO["showTime"];
	
	if(_remoteEnabled){
		JsonObject& deviceWiFiJO = jsonObject["deviceWiFi"];
		char* hostName = strdup(deviceWiFiJO["hostName"]);
		_debug->begin(hostName);
	}
	
	_debug->setSerialEnabled(_serialEnabled);
	_debug->setResetCmdEnabled(_resetCmdEnabled);
	_debug->showColors(_showColors);
	_debug->showDebugLevel(_showDebugLevel);
	_debug->showProfiler(_showProfiler);
	_debug->showTime(_showTime);
	
	printf("DeviceDebug", "load", "TelnetTCPPort: %d\n", (char*)_telnetTCPPort);
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

void DeviceDebug::setTelnetTCPPort(char* json)
{	
	StaticJsonBuffer<200> jsonBuffer;
	
	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		printf("DeviceDebug", "setTelnetTCPPort", "Parse failed: %s\n", json);
		return;
	}	
	
	_telnetTCPPort = root["value"];
	
	printf("DeviceDebug", "setTelnetTCPPort", "TelnetTCPPort: %d\n", (char*)_telnetTCPPort);
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