#include "DeviceInApplication.h"
#include "ESPDevice.h"

namespace ART
{
	DeviceInApplication::DeviceInApplication(ESPDevice* espDevice)
	{
		_espDevice = espDevice;

		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();
		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::constructor]\n");
	}

	DeviceInApplication::~DeviceInApplication()
	{
		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::destructor] begin\n");

		delete (_espDevice);
		delete (_applicationId);
		delete (_applicationTopic);

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::destructor] end\n");
	}

	void DeviceInApplication::create(DeviceInApplication *(&deviceInApplication), ESPDevice * espDevice)
	{
		deviceInApplication = new DeviceInApplication(espDevice);
	}

	void DeviceInApplication::begin()
	{
		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::begin] begin\n");

		_espDevice->getDeviceMQ()->addSubscribeDeviceCallback([=]() { return onDeviceMQSubscribeDevice(); });
		_espDevice->getDeviceMQ()->addUnSubscribeDeviceCallback([=]() { return onDeviceMQUnSubscribeDevice(); });
		_espDevice->getDeviceMQ()->addSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQSubscribeDeviceInApplication(); });
		_espDevice->getDeviceMQ()->addUnSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQUnSubscribeDeviceInApplication(); });
		_espDevice->getDeviceMQ()->addSubscriptionCallback([=](char* topicKey, char* json) { return onDeviceMQSubscription(topicKey, json); });

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::begin] end\n");
	}

	void DeviceInApplication::load(JsonObject & jsonObject)
	{
		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::load] begin\n");

		setApplicationId(strdup(jsonObject["applicationId"]));
		setApplicationTopic(strdup(jsonObject["applicationTopic"]));

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::load] end\n");
	}

	char* DeviceInApplication::getApplicationId() const
	{
		return (_applicationId);
	}

	char* DeviceInApplication::getApplicationTopic() const
	{
		return (_applicationTopic);
	}

	void DeviceInApplication::setApplicationId(char* value)
	{
		_applicationId = new char(sizeof(strlen(value)));
		_applicationId = value;

		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::setApplicationId] applicationId: %s\n", _applicationId);
	}	

	void DeviceInApplication::setApplicationTopic(char* value)
	{
		_applicationTopic = new char(sizeof(strlen(value)));
		_applicationTopic = value;

		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::setApplicationTopic] applicationTopic: %s\n", _applicationTopic);
	}

	bool DeviceInApplication::inApplication()
	{
		bool result = !(_applicationId == NULL || _applicationId == "");

		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::inApplication] result: %s\n", result ? "true" : "false");

		return result;
	}

	void DeviceInApplication::insert(char* json)
	{
		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::insert] begin\n");

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& jsonObject = jsonBuffer.parseObject(json);

		if (!jsonObject.success()) {
			if (deviceDebug->isActive(DeviceDebug::ERROR)) deviceDebug->printf("DeviceInApplication::insert] parse failed json: %s\n", json);
			return;
		}

		setApplicationId(strdup(jsonObject["applicationId"]));
		setApplicationTopic(strdup(jsonObject["applicationTopic"]));

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::insert] start: raise callbacks\n");

		for (auto && fn : _insertCallbacks) fn();

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) {
			deviceDebug->printf("DeviceInApplication::insert] finish: raise callbacks\n");
			deviceDebug->printf("DeviceInApplication::insert] end\n");
		}
	}

	void DeviceInApplication::remove()
	{
		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::remove] begin\n");

		setApplicationId("");
		setApplicationTopic("");

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::remove] start: raise callbacks\n");

		for (auto && fn : _removeCallbacks) fn();

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) {
			deviceDebug->printf("DeviceInApplication::remove] finish: raise callbacks\n");
			deviceDebug->printf("DeviceInApplication::remove] end\n");
		}
	}

	void DeviceInApplication::onDeviceMQSubscribeDevice()
	{
		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::onDeviceMQSubscribeDevice] topic: %s\n", DEVICE_IN_APPLICATION_INSERT_TOPIC_SUB);

		_espDevice->getDeviceMQ()->subscribeDevice(DEVICE_IN_APPLICATION_INSERT_TOPIC_SUB);
	}

	void DeviceInApplication::onDeviceMQUnSubscribeDevice()
	{
		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::onDeviceMQUnSubscribeDevice] topic: %s\n", DEVICE_IN_APPLICATION_INSERT_TOPIC_SUB);

		_espDevice->getDeviceMQ()->unSubscribeDevice(DEVICE_IN_APPLICATION_INSERT_TOPIC_SUB);
	}

	void DeviceInApplication::onDeviceMQSubscribeDeviceInApplication()
	{
		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::onDeviceMQSubscribeDeviceInApplication] topic: %s\n", DEVICE_IN_APPLICATION_REMOVE_TOPIC_SUB);

		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_IN_APPLICATION_REMOVE_TOPIC_SUB);
	}

	void DeviceInApplication::onDeviceMQUnSubscribeDeviceInApplication()
	{
		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::onDeviceMQUnSubscribeDeviceInApplication] topic: %s\n", DEVICE_IN_APPLICATION_REMOVE_TOPIC_SUB);

		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_IN_APPLICATION_REMOVE_TOPIC_SUB);
	}

	bool DeviceInApplication::onDeviceMQSubscription(char* topicKey, char* json)
	{
		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) deviceDebug->printf("DeviceInApplication::onDeviceMQSubscription] begin\n");

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

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) {
			if(result) deviceDebug->printf("DeviceInApplication::onDeviceMQSubscription] find topic : %s\n", topicKey);
			deviceDebug->printf("DeviceInApplication::onDeviceMQSubscription] end return : %s\n", result ? "true" : "false");
		}

		return result;
	}
}