#include "DeviceNTP.h"
#include "ESPDevice.h"

DeviceNTP::DeviceNTP(ESPDevice* espDevice)
{
	_espDevice = espDevice;	
}

DeviceNTP::~DeviceNTP()
{
	delete (_espDevice);
	delete (_host);
}

void DeviceNTP::load(JsonObject& jsonObject)
{	
	char* host = strdup(jsonObject["host"]);
	_host = new char(sizeof(strlen(host)));
	_host = host;
	
	_port = jsonObject["port"];
	
	_utcTimeOffsetInSecond = jsonObject["utcTimeOffsetInSecond"];
	_updateIntervalInMilliSecond = jsonObject["updateIntervalInMilliSecond"];	
}

char* DeviceNTP::getHost()
{	
	return _host;
}

int DeviceNTP::getPort()
{	
	return _port;
}

int DeviceNTP::getUtcTimeOffsetInSecond()
{	
	return _utcTimeOffsetInSecond;
}

void DeviceNTP::setUtcTimeOffsetInSecond(String json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		Serial.print("[ConfigurationManager::setUtcTimeOffsetInSecond] parse failed: ");
		Serial.println(json);
		return;
	}	

	_utcTimeOffsetInSecond = root["utcTimeOffsetInSecond"];
	
	Serial.println("[ConfigurationManager::setUtcTimeOffsetInSecond] ");
	Serial.print("utcTimeOffsetInSecond: ");
	Serial.println(_utcTimeOffsetInSecond);	
}

int DeviceNTP::getUpdateIntervalInMilliSecond()
{	
	return _updateIntervalInMilliSecond;
}

void DeviceNTP::setUpdateIntervalInMilliSecond(String json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		Serial.print("[ConfigurationManager::setUpdateIntervalInMilliSecond] parse failed: ");
		Serial.println(json);
		return;
	}	

	_updateIntervalInMilliSecond = root["updateIntervalInMilliSecond"];
	
	Serial.println("[ConfigurationManager::setUpdateIntervalInMilliSecond] ");
	Serial.print("updateIntervalInMilliSecond: ");
	Serial.println(_updateIntervalInMilliSecond);
}