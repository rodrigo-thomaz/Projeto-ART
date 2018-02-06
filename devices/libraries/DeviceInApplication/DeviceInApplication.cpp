#include "DeviceInApplication.h"
#include "ESPDevice.h"

namespace ART
{
	DeviceInApplication::DeviceInApplication(ESPDevice* espDevice)
	{
		_espDevice = espDevice;

		_deviceDebug = _espDevice->getDeviceDebug();

		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::constructor]\n");
	}

	DeviceInApplication::~DeviceInApplication()
	{
		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::destructor] begin\n");

		delete (_espDevice);
		delete (_applicationId);
		delete (_applicationTopic);

		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::destructor] end\n");
	}

	void DeviceInApplication::create(DeviceInApplication *(&deviceInApplication), ESPDevice * espDevice)
	{
		deviceInApplication = new DeviceInApplication(espDevice);
	}

	void DeviceInApplication::begin()
	{
		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::begin] begin\n");

		_espDevice->getDeviceMQ()->addSubscribeDeviceCallback([=]() { return onDeviceMQSubscribeDevice(); });
		_espDevice->getDeviceMQ()->addUnSubscribeDeviceCallback([=]() { return onDeviceMQUnSubscribeDevice(); });
		_espDevice->getDeviceMQ()->addSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQSubscribeDeviceInApplication(); });
		_espDevice->getDeviceMQ()->addUnSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQUnSubscribeDeviceInApplication(); });
		_espDevice->getDeviceMQ()->addSubscriptionCallback([=](char* topicKey, char* json) { return onDeviceMQSubscription(topicKey, json); });

		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::begin] end\n");
	}

	void DeviceInApplication::load(JsonObject & jsonObject)
	{
		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::load] begin\n");

		setApplicationId(strdup(jsonObject["applicationId"]));
		setApplicationTopic(strdup(jsonObject["applicationTopic"]));

		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::load] end\n");
	}

	char* DeviceInApplication::getApplicationId() const
	{
		return (_applicationId);
	}

	char* DeviceInApplication::getApplicationTopic() const
	{
		return (_applicationTopic);
	}

	void DeviceInApplication::setApplicationId(const char* value)
	{
		_applicationId = new char(sizeof(strlen(value)));
		_applicationId = (char*)value;

		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::setApplicationId] applicationId: %s\n", _applicationId);
	}	

	void DeviceInApplication::setApplicationTopic(const char* value)
	{
		_applicationTopic = new char(sizeof(strlen(value)));
		_applicationTopic = (char*)value;

		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::setApplicationTopic] applicationTopic: %s\n", _applicationTopic);
	}

	bool DeviceInApplication::inApplication()
	{
		bool result = !(_applicationId == NULL || _applicationId == "");

		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::inApplication] result: %s\n", result ? "true" : "false");

		return result;
	}

	void DeviceInApplication::insert(char* json)
	{
		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::insert] begin\n");

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& jsonObject = jsonBuffer.parseObject(json);

		if (!jsonObject.success()) {
			if (_deviceDebug->isActive(DeviceDebug::ERROR)) _deviceDebug->printf("DeviceInApplication::insert] parse failed json: %s\n", json);
			return;
		}

		setApplicationId(strdup(jsonObject["applicationId"]));
		setApplicationTopic(strdup(jsonObject["applicationTopic"]));

		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::insert] start: raise callbacks\n");

		for (auto && fn : _insertCallbacks) fn();

		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) {
			_deviceDebug->printf("DeviceInApplication::insert] finish: raise callbacks\n");
			_deviceDebug->printf("DeviceInApplication::insert] end\n");
		}
	}

	void DeviceInApplication::remove()
	{
		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::remove] begin\n");

		setApplicationId("");
		setApplicationTopic("");

		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::remove] start: raise callbacks\n");

		for (auto && fn : _removeCallbacks) fn();

		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) {
			_deviceDebug->printf("DeviceInApplication::remove] finish: raise callbacks\n");
			_deviceDebug->printf("DeviceInApplication::remove] end\n");
		}
	}

	void DeviceInApplication::onDeviceMQSubscribeDevice()
	{
		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::onDeviceMQSubscribeDevice] topic: %s\n", DEVICE_IN_APPLICATION_INSERT_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDevice(DEVICE_IN_APPLICATION_INSERT_TOPIC_SUB);
	}

	void DeviceInApplication::onDeviceMQUnSubscribeDevice()
	{
		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::onDeviceMQUnSubscribeDevice] topic: %s\n", DEVICE_IN_APPLICATION_INSERT_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDevice(DEVICE_IN_APPLICATION_INSERT_TOPIC_SUB);
	}

	void DeviceInApplication::onDeviceMQSubscribeDeviceInApplication()
	{
		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::onDeviceMQSubscribeDeviceInApplication] topic: %s\n", DEVICE_IN_APPLICATION_REMOVE_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_IN_APPLICATION_REMOVE_TOPIC_SUB);
	}

	void DeviceInApplication::onDeviceMQUnSubscribeDeviceInApplication()
	{
		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::onDeviceMQUnSubscribeDeviceInApplication] topic: %s\n", DEVICE_IN_APPLICATION_REMOVE_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_IN_APPLICATION_REMOVE_TOPIC_SUB);
	}

	bool DeviceInApplication::onDeviceMQSubscription(char* topicKey, char* json)
	{
		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) _deviceDebug->printf("DeviceInApplication::onDeviceMQSubscription] begin\n");

		bool result = false;

		if (strcmp(topicKey, DEVICE_IN_APPLICATION_INSERT_TOPIC_SUB) == 0) {
			insert(json);			
			result = true;
		}
		else if (strcmp(topicKey, DEVICE_IN_APPLICATION_REMOVE_TOPIC_SUB) == 0) {
			remove();			
			result = true;
		}
		else {			
			result = false;
		}

		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) {
			if(result) _deviceDebug->printf("DeviceInApplication::onDeviceMQSubscription] find topic : %s\n", topicKey);
			_deviceDebug->printf("DeviceInApplication::onDeviceMQSubscription] end return : %s\n", result ? "true" : "false");
		}

		return result;
	}
}