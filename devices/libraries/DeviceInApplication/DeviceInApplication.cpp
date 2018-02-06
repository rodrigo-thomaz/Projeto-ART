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
		_espDevice->getDeviceMQ()->addSubscribeDeviceCallback([=]() { return onDeviceMQSubscribeDevice(); });
		_espDevice->getDeviceMQ()->addUnSubscribeDeviceCallback([=]() { return onDeviceMQUnSubscribeDevice(); });
		_espDevice->getDeviceMQ()->addSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQSubscribeDeviceInApplication(); });		
		_espDevice->getDeviceMQ()->addUnSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQUnSubscribeDeviceInApplication(); });
		_espDevice->getDeviceMQ()->addSubscriptionCallback([=](char* topicKey, char* json) { return onDeviceMQSubscription(topicKey, json); });
	}

	void DeviceInApplication::load(JsonObject & jsonObject)
	{
		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		deviceDebug->print("DeviceInApplication", "load", "begin\n");		

		char* applicationId = strdup(jsonObject["applicationId"]);
		setApplicationId(applicationId);

		char* applicationTopic = strdup(jsonObject["applicationTopic"]);
		setApplicationTopic(applicationTopic);

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

	bool DeviceInApplication::inApplication()
	{
		return !(_applicationId == NULL || _applicationId == "");
	}

	void DeviceInApplication::setInsertCallback(callbackSignature callback)
	{
		_insertCallback = callback;
	}

	void DeviceInApplication::setRemoveCallback(callbackSignature callback)
	{
		_removeCallback = callback;
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

		root.printTo(Serial);
		
		char* applicationId = strdup(root["applicationId"]);
		setApplicationId(applicationId);

		char* applicationTopic = strdup(root["applicationTopic"]);
		setApplicationTopic(applicationTopic);

		_insertCallback();

		Serial.println("[DeviceInApplication::insert] ");
		Serial.print("ApplicationId: ");
		Serial.println(_applicationId);
		Serial.print("ApplicationTopic: ");
		Serial.println(_applicationTopic);
	}

	void DeviceInApplication::remove()
	{
		setApplicationId("");
		setApplicationTopic("");

		_removeCallback();

		Serial.println("[DeviceInApplication::remove] remove from Application with success !");
	}

	void DeviceInApplication::onDeviceMQSubscribeDevice()
	{
		_espDevice->getDeviceMQ()->subscribeDevice(DEVICE_IN_APPLICATION_INSERT_TOPIC_SUB);
	}

	void DeviceInApplication::onDeviceMQUnSubscribeDevice()
	{
		_espDevice->getDeviceMQ()->unSubscribeDevice(DEVICE_IN_APPLICATION_INSERT_TOPIC_SUB);
	}

	void DeviceInApplication::onDeviceMQSubscribeDeviceInApplication()
	{
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_IN_APPLICATION_REMOVE_TOPIC_SUB);
	}	

	void DeviceInApplication::onDeviceMQUnSubscribeDeviceInApplication()
	{
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_IN_APPLICATION_REMOVE_TOPIC_SUB);
	}

	bool DeviceInApplication::onDeviceMQSubscription(char* topicKey, char* json)
	{
		if (strcmp(topicKey, DEVICE_IN_APPLICATION_INSERT_TOPIC_SUB) == 0) {			
			insert(json);			
			return true;
		}
		else if (strcmp(topicKey, DEVICE_IN_APPLICATION_REMOVE_TOPIC_SUB) == 0) {
			remove();			
			return true;
		}
		else {
			return false;
		}
	}
}