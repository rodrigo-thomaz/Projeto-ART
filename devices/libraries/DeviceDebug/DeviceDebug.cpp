#include "DeviceDebug.h"
#include "ESPDevice.h"

namespace ART
{
	DeviceDebug::DeviceDebug(ESPDevice* espDevice)
	{
		_espDevice = espDevice;
		_debug = new RemoteDebug();

		_initializing = false;
		_initialized = false;
	}

	DeviceDebug::~DeviceDebug()
	{
		delete (_espDevice);
		delete (_debug);
	}

	void DeviceDebug::begin()
	{
		_espDevice->getDeviceMQ()->addSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQSubscribeDeviceInApplication(); });
		_espDevice->getDeviceMQ()->addUnSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQUnSubscribeDeviceInApplication(); });
		_espDevice->getDeviceMQ()->addSubscriptionCallback([=](const char* topicKey, const char* json) { return onDeviceMQSubscription(topicKey, json); });
	}

	bool DeviceDebug::initialized()
	{
		if (_initialized) return true;

		if (!_espDevice->loaded()) {
			Serial.println(F("[DeviceDebug::initialized] espDevice not loaded"));
			return false;
		}

		if (!_espDevice->getDeviceMQ()->connected()) {
			Serial.println(F("[DeviceDebug::initialized] deviceMQ not connected"));
			return false;
		}

		if (_initializing) {
			Serial.println(F("[DeviceDebug::initialized] initializing: true"));
			return false;
		}

		// initializing

		Serial.println(F("[DeviceDebug::initialized] begin"));

		_initializing = true;

		getByKeyPub();

		Serial.println(F("[DeviceDebug::initialized] end"));

		return true;
	}

	void DeviceDebug::loop()
	{
		_debug->handle();
	}

	void DeviceDebug::getByKeyPub()
	{
		Serial.println(F("[DeviceDebug::getByKeyPub] begin"));
		_espDevice->getDeviceMQ()->publishInApplication(DEVICE_DEBUG_GET_BY_KEY_TOPIC_PUB, _espDevice->getDeviceKeyAsJson());
		Serial.println(F("[DeviceDebug::getByKeyPub] end"));
	}

	void DeviceDebug::getByKeySub(const char * json)
	{
		Serial.println(F("[DeviceDebug::getByKeySub] begin"));

		this->_initialized = true;
		this->_initializing = false;

		Serial.print(F("[DeviceDebug::getByKeySub] Pre buffer FreeHeap JsonObject: "));
		Serial.println(ESP.getFreeHeap());

		DynamicJsonBuffer jsonBuffer;
		JsonObject& jsonObject = jsonBuffer.parseObject(json);

		Serial.print(F("[DeviceDebug::getByKeySub] Pos buffer FreeHeap JsonObject: "));
		Serial.println(ESP.getFreeHeap());

		if (!jsonObject.success()) {
			Serial.print(F("[DeviceDebug::getByKeySub] parse failed: "));
			Serial.println(json);
			return;
		}

		const char* hostName = _espDevice->getDeviceWiFi()->getHostName();

		setHostName(strdup(hostName));
		setRemoteEnabled(jsonObject["remoteEnabled"].as<bool>());
		setSerialEnabled(jsonObject["serialEnabled"].as<bool>());
		setResetCmdEnabled(jsonObject["resetCmdEnabled"].as<bool>());
		setShowColors(jsonObject["showColors"].as<bool>());
		setShowDebugLevel(jsonObject["showDebugLevel"].as<bool>());
		setShowProfiler(jsonObject["showProfiler"].as<bool>());
		setShowTime(jsonObject["showTime"].as<bool>());

		Serial.println(F("[DeviceDebug::getByKeySub] end"));
	}

	bool DeviceDebug::isActive(uint8_t debugLevel)
	{
		return _debug->isActive(debugLevel);
	}

	int DeviceDebug::println()
	{
		return _debug->println();
	}

	int DeviceDebug::println(const char * className, const char * caller)
	{
		return println(className, caller, "");
	}

	int DeviceDebug::println(const char * className, const char * caller, const char* message)
	{
		return _debug->println(createExpression(className, caller, message).c_str());
	}

	int DeviceDebug::print(const char* className, const char* caller, const char* message)
	{
		return _debug->printf(createExpression(className, caller, message).c_str());
	}

	int DeviceDebug::printlnLevel(uint8_t debugLevel)
	{
		return _debug->isActive(debugLevel) ? println() : 0;
	}

	int DeviceDebug::printlnLevel(uint8_t debugLevel, const char * className, const char * caller)
	{
		return _debug->isActive(debugLevel) ? println(className, caller) : 0;
	}

	int DeviceDebug::printlnLevel(uint8_t debugLevel, const char * className, const char * caller, const char* message)
	{
		return _debug->isActive(debugLevel) ? println(className, caller, message) : 0;
	}

	int DeviceDebug::printLevel(uint8_t debugLevel, const char* className, const char* caller, const char* message)
	{
		return _debug->isActive(debugLevel) ? print(className, caller, message) : 0;
	}

	std::string DeviceDebug::createExpression(const char* className, const char* caller, const char* expression)
	{
		std::string str;
		str.append(className);
		str.append(" ");
		str.append(caller);
		str.append(" ");
		str.append(expression);
		return str;
	}

	void DeviceDebug::setRemoteEnabled(const char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceDebug", "setRemoteEnabled", "Parse failed: %s\n", json);
			return;
		}
		setRemoteEnabled(root["value"].as<bool>());
	}

	void DeviceDebug::setResetCmdEnabled(const char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceDebug", "setResetCmdEnabled", "Parse failed: %s\n", json);
			return;
		}
		setResetCmdEnabled(root["value"].as<bool>());
	}

	void DeviceDebug::setSerialEnabled(const char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceDebug", "setSerialEnabled", "Parse failed: %s\n", json);
			return;
		}
		setSerialEnabled(root["value"].as<bool>());
	}

	void DeviceDebug::setShowColors(const char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceDebug", "setShowColors", "Parse failed: %s\n", json);
			return;
		}
		setShowColors(root["value"].as<bool>());
	}

	void DeviceDebug::setShowDebugLevel(const char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceDebug", "setShowDebugLevel", "Parse failed: %s\n", json);
			return;
		}
		setShowDebugLevel(root["value"].as<bool>());
	}

	void DeviceDebug::setShowProfiler(const char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceDebug", "setShowProfiler", "Parse failed: %s\n", json);
			return;
		}
		setShowProfiler(root["value"].as<bool>());
	}

	void DeviceDebug::setShowTime(const char* json)
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
		if (!_telnetServer) {
			_telnetServer = true;
			MDNS.addService("telnet", "tcp", TELNET_PORT);
		}
	}

	void DeviceDebug::setHostName(const char* value)
	{
		_hostName = new char(sizeof(strlen(value)));
		_hostName = (char*)value;
		printf("DeviceDebug", "setHostName", "hostName: %s\n", _hostName);
	}

	void DeviceDebug::setRemoteEnabled(bool value)
	{
		_remoteEnabled = value;
		if (_remoteEnabled) {
			initTelnetServer();
			_debug->begin(_hostName);
		}
		else {
			_debug->stop();
		}
		printf("DeviceDebug", "setRemoteEnabled", "remoteEnabled: %s\n", _remoteEnabled ? "true" : "false");
	}

	void DeviceDebug::setResetCmdEnabled(bool value)
	{
		_resetCmdEnabled = value;
		_debug->setResetCmdEnabled(_resetCmdEnabled);
		printf("DeviceDebug", "setResetCmdEnabled", "resetCmdEnabled: %s\n", _resetCmdEnabled ? "true" : "false");
	}

	void DeviceDebug::setSerialEnabled(bool value)
	{
		_serialEnabled = value;
		_debug->setSerialEnabled(_serialEnabled);
		printf("DeviceDebug", "setSerialEnabled", "serialEnabled: %s\n", _serialEnabled ? "true" : "false");
	}

	void DeviceDebug::setShowColors(bool value)
	{
		_showColors = value;
		_debug->showColors(_showColors);
		printf("DeviceDebug", "setShowColors", "showColors: %s\n", _showColors ? "true" : "false");
	}

	void DeviceDebug::setShowDebugLevel(bool value)
	{
		_showDebugLevel = value;
		_debug->showDebugLevel(_showDebugLevel);
		printf("DeviceDebug", "setShowDebugLevel", "showDebugLevel: %s\n", _showDebugLevel ? "true" : "false");
	}

	void DeviceDebug::setShowProfiler(bool value)
	{
		_showProfiler = value;
		_debug->showProfiler(_showProfiler);
		printf("DeviceDebug", "setShowProfiler", "showProfiler: %s\n", _showProfiler ? "true" : "false");
	}

	void DeviceDebug::setShowTime(bool value)
	{
		_showTime = value;
		_debug->showTime(_showTime);
		printf("DeviceDebug", "setShowTime", "showTime: %s\n", _showTime ? "true" : "false");
	}

	void DeviceDebug::onDeviceMQSubscribeDeviceInApplication()
	{
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_DEBUG_GET_BY_KEY_COMPLETED_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_DEBUG_SET_REMOTE_ENABLED_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_DEBUG_SET_RESET_CMD_ENABLED_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_DEBUG_SET_SERIAL_ENABLED_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_DEBUG_SET_SHOW_COLORS_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_DEBUG_SET_SHOW_DEBUG_LEVEL_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_DEBUG_SET_SHOW_PROFILER_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_DEBUG_SET_SHOW_TIME_TOPIC_SUB);
	}

	void DeviceDebug::onDeviceMQUnSubscribeDeviceInApplication()
	{
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_DEBUG_GET_BY_KEY_COMPLETED_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_DEBUG_SET_REMOTE_ENABLED_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_DEBUG_SET_RESET_CMD_ENABLED_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_DEBUG_SET_SERIAL_ENABLED_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_DEBUG_SET_SHOW_COLORS_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_DEBUG_SET_SHOW_DEBUG_LEVEL_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_DEBUG_SET_SHOW_PROFILER_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_DEBUG_SET_SHOW_TIME_TOPIC_SUB);
	}

	bool DeviceDebug::onDeviceMQSubscription(const char* topicKey, const char* json)
	{
		if (strcmp(topicKey, DEVICE_DEBUG_GET_BY_KEY_COMPLETED_TOPIC_SUB) == 0) {
			getByKeySub(json);
			return true;
		}
		else if (strcmp(topicKey, DEVICE_DEBUG_SET_REMOTE_ENABLED_TOPIC_SUB) == 0) {
			setRemoteEnabled(json);
			return true;
		}
		else if (strcmp(topicKey, DEVICE_DEBUG_SET_RESET_CMD_ENABLED_TOPIC_SUB) == 0) {
			setResetCmdEnabled(json);
			return true;
		}
		else if (strcmp(topicKey, DEVICE_DEBUG_SET_SERIAL_ENABLED_TOPIC_SUB) == 0) {
			setSerialEnabled(json);
			return true;
		}
		else if (strcmp(topicKey, DEVICE_DEBUG_SET_SHOW_COLORS_TOPIC_SUB) == 0) {
			setShowColors(json);
			return true;
		}
		else if (strcmp(topicKey, DEVICE_DEBUG_SET_SHOW_DEBUG_LEVEL_TOPIC_SUB) == 0) {
			setShowDebugLevel(json);
			return true;
		}
		else if (strcmp(topicKey, DEVICE_DEBUG_SET_SHOW_PROFILER_TOPIC_SUB) == 0) {
			setShowProfiler(json);
			return true;
		}
		else if (strcmp(topicKey, DEVICE_DEBUG_SET_SHOW_TIME_TOPIC_SUB) == 0) {
			setShowTime(json);
			return true;
		}
		else {
			return false;
		}
	}
}