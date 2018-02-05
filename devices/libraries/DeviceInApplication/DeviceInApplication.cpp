#include "DeviceInApplication.h"
#include "ESPDevice.h"

namespace ART
{
	DeviceInApplication::DeviceInApplication(ESPDevice* espDevice)
	{
		_espDevice = espDevice;		
	}

	DeviceInApplication::~DeviceInApplication()
	{
		delete (_espDevice);
		delete (_applicationId);
		delete (_applicationTopic);
	}

	void DeviceInApplication::create(DeviceInApplication *(&deviceInApplication), ESPDevice * espDevice)
	{
		deviceInApplication = new DeviceInApplication(espDevice);
	}

	void DeviceInApplication::begin()
	{
		_espDevice->getDeviceMQ()->addSubscribeNotInApplicationCallback([=]() { return this->onDeviceMQSubscribeNotInApplication(); });
		_espDevice->getDeviceMQ()->addSubscribeInApplicationCallback([=]() { return this->onDeviceMQSubscribeInApplication(); });
		_espDevice->getDeviceMQ()->addUnSubscribeNotInApplicationCallback([=]() { return this->onDeviceMQUnSubscribeNotInApplication(); });
		_espDevice->getDeviceMQ()->addUnSubscribeInApplicationCallback([=]() { return this->onDeviceMQUnSubscribeInApplication(); });
		_espDevice->getDeviceMQ()->addSubscriptionCallback([=](char* topicKey, char* json) { return this->onDeviceMQSubscription(topicKey, json); });
	}

	void DeviceInApplication::load(JsonObject & jsonObject)
	{
		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		deviceDebug->print("DeviceInApplication", "load", "begin\n");

		char* applicationId = strdup(jsonObject["applicationId"]);
		_applicationId = new char(sizeof(strlen(applicationId)));
		_applicationId = applicationId;

		char* applicationTopic = strdup(jsonObject["applicationTopic"]);
		_applicationTopic = new char(sizeof(strlen(applicationTopic)));
		_applicationTopic = applicationTopic;

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) {

			deviceDebug->printf("DeviceInApplication", "load", "applicationId: %s\n", _applicationId);
			deviceDebug->printf("DeviceInApplication", "load", "applicationTopic: %s\n", _applicationTopic);

			deviceDebug->print("DeviceInApplication", "load", "end\n");
		}
	}

	char* DeviceInApplication::getApplicationId() const
	{
		return (_applicationId);
	}

	void DeviceInApplication::setApplicationId(char* value)
	{
		_applicationId = new char(sizeof(strlen(value)));
		_applicationId = value;
	}

	char* DeviceInApplication::getApplicationTopic() const
	{
		return (_applicationTopic);
	}

	void DeviceInApplication::setApplicationTopic(char* value)
	{
		_applicationTopic = new char(sizeof(strlen(value)));
		_applicationTopic = value;
	}

	void DeviceInApplication::insert(char* json)
	{
		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);

		if (!root.success()) {
			Serial.print("[DeviceInApplication::insert] parse failed: ");
			Serial.println(json);
			return;
		}

		char* applicationId = strdup(root["applicationId"]);
		char* applicationTopic = strdup(root["applicationTopic"]);

		setApplicationId(applicationId);
		setApplicationTopic(applicationTopic);

		Serial.println("[DeviceInApplication::insert] ");
		Serial.print("applicationId: ");
		Serial.println(applicationId);
		Serial.print("applicationTopic: ");
		Serial.println(applicationTopic);
	}

	void DeviceInApplication::remove()
	{
		setApplicationId("");
		setApplicationTopic("");

		Serial.println("[DeviceInApplication::remove] remove from Application with success !");
	}

	void DeviceInApplication::onDeviceMQSubscribeNotInApplication()
	{
		_espDevice->getDeviceMQ()->subscribeInDevice(DEVICE_IN_APPLICATION_INSERT_TOPIC_SUB);
	}

	void DeviceInApplication::onDeviceMQSubscribeInApplication()
	{
		_espDevice->getDeviceMQ()->subscribeInDevice(DEVICE_IN_APPLICATION_REMOVE_TOPIC_SUB);
	}

	void DeviceInApplication::onDeviceMQUnSubscribeNotInApplication()
	{
		_espDevice->getDeviceMQ()->unSubscribeInDevice(DEVICE_IN_APPLICATION_INSERT_TOPIC_SUB);
	}

	void DeviceInApplication::onDeviceMQUnSubscribeInApplication()
	{
		_espDevice->getDeviceMQ()->unSubscribeInDevice(DEVICE_IN_APPLICATION_REMOVE_TOPIC_SUB);
	}

	void DeviceInApplication::onDeviceMQSubscription(char* topicKey, char* json)
	{
		if (strcmp(topicKey, DEVICE_IN_APPLICATION_INSERT_TOPIC_SUB) == 0) {			
			insert(json);
			for (auto && fn : _insertCallbacks) fn();
		}
		if (strcmp(topicKey, DEVICE_IN_APPLICATION_REMOVE_TOPIC_SUB) == 0) {			
			remove();
			for (auto && fn : _removeCallbacks) fn();
		}
	}
}