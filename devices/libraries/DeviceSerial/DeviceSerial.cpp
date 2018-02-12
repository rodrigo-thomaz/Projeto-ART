#include "DeviceSerial.h"
#include "../ESPDevice/ESPDevice.h"

namespace ART
{
	DeviceSerial::DeviceSerial(ESPDevice* espDevice)
	{
		_espDevice = espDevice;

		_initializing = false;
		_initialized = false;
	}

	DeviceSerial::~DeviceSerial()
	{

	}	

	void DeviceSerial::begin()
	{
		Serial.println(F("[DeviceSerial::begin] begin"));

		_espDevice->getDeviceMQ()->addSubscriptionCallback([=](const char* topicKey, const char* json) { return onDeviceMQSubscription(topicKey, json); });
		_espDevice->getDeviceMQ()->addSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQSubscribeDeviceInApplication(); });
		_espDevice->getDeviceMQ()->addUnSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQUnSubscribeDeviceInApplication(); });
				
		initialized();

		Serial.println(F("[DeviceSerial::begin] end"));
	}

	std::tuple<DeviceSerialItem**, short> DeviceSerial::getItems()
	{
		DeviceSerialItem** array = this->_items.data();
		return std::make_tuple(array, _items.size());
	}

	bool DeviceSerial::initialized()
	{
		if (_initialized) return true;		

		if (!_espDevice->loaded()) {
			Serial.println(F("[DeviceSerial::initialized] espDevice not loaded"));
			return false;
		}

		if (!_espDevice->getDeviceMQ()->connected()) {
			Serial.println(F("[DeviceSerial::initialized] deviceMQ not connected"));
			return false;
		}

		if (_initializing) {
			Serial.println(F("[DeviceSerial::initialized] initializing: true"));
			return false;
		}

		// initializing

		Serial.println(F("[DeviceSerial::initialized] begin"));

		_initializing = true;

		getAllPub();

		Serial.println(F("[DeviceSerial::initialized] end"));

		return true;
	}

	DeviceSerialItem* DeviceSerial::getItemById(const char* id) {
		for (int i = 0; i < _items.size(); ++i) {
			if (stricmp(_items[i]->getDeviceSerialId(), id) == 0) {
				return _items[i];
			}
		}
	}

	void DeviceSerial::getAllPub()
	{
		Serial.println(F("[DeviceSerial::getAllPub] begin"));
		_espDevice->getDeviceMQ()->publishInApplication(DEVICE_SERIAL_GET_ALL_BY_DEVICE_KEY_TOPIC_PUB, _espDevice->getDeviceKeyAsJson());
		Serial.println(F("[DeviceSerial::getAllPub] end"));
	}

	void DeviceSerial::getAllSub(const char * json)
	{
		Serial.println(F("[DeviceSerial::getAllSub] begin"));		

		this->_initialized = true;
		this->_initializing = false;

		Serial.print(F("[DeviceSerial::getAllSub] Pre buffer FreeHeap JsonObject: "));
		Serial.println(ESP.getFreeHeap());

		DynamicJsonBuffer jsonBuffer;
		JsonArray& jsonArray = jsonBuffer.parseArray(json);

		Serial.print(F("[DeviceSerial::getAllSub] Pos buffer FreeHeap JsonObject: "));
		Serial.println(ESP.getFreeHeap());

		if (!jsonArray.success()) {
			Serial.print(F("[DeviceSerial::getAllSub] parse failed: "));
			Serial.println(json);
			return;
		}			

		for (JsonArray::iterator it = jsonArray.begin(); it != jsonArray.end(); ++it)
		{
			Serial.print(F("[DeviceSerial::getAllSub] Pre buffer new DeviceSerialItem: "));
			Serial.println(ESP.getFreeHeap());

			JsonObject& jsonObject = it->as<JsonObject>();
			_items.push_back(new DeviceSerialItem(jsonObject));

			Serial.print(F("[DeviceSerial::getAllSub] Pos buffer new DeviceSerialItem: "));
			Serial.println(ESP.getFreeHeap());
		}

		Serial.println(F("[DeviceSerial::getAllSub] end"));
	}

	void DeviceSerial::setEnabledSub(const char * json)
	{
		Serial.println(F("[DeviceSerial::setEnabledSub] begin"));

		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSerial", "setEnabledSub", "Parse failed: %s\n", json);
			return;
		}

		bool value = root["value"];

		DeviceSerialItem* item = getItemById(root["deviceSerialId"]);

		item->setEnabled(value);

		Serial.println(F("[DeviceSerial::setEnabledSub] end"));
	}

	void DeviceSerial::setPinSub(const char * json)
	{
		Serial.println(F("[DeviceSerial::setPinSub] begin"));

		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSerial", "setPinSub", "Parse failed: %s\n", json);
			return;
		}

		short value = root["value"];
		short direction = root["direction"];

		DeviceSerialItem* item = getItemById(root["deviceSerialId"]);

		if(direction == 0) // RX
			item->setPinRX(value);
		else if (direction == 1) // TX
			item->setPinTX(value);

		Serial.println(F("[DeviceSerial::setPinSub] end"));
	}

	void DeviceSerial::onDeviceMQSubscribeDeviceInApplication()
	{
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_SERIAL_GET_ALL_BY_DEVICE_KEY_COMPLETED_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_SERIAL_SET_ENABLED_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_SERIAL_SET_PIN_TOPIC_SUB);
	}

	void DeviceSerial::onDeviceMQUnSubscribeDeviceInApplication()
	{
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_SERIAL_GET_ALL_BY_DEVICE_KEY_COMPLETED_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_SERIAL_SET_ENABLED_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_SERIAL_SET_PIN_TOPIC_SUB);
	}

	bool DeviceSerial::onDeviceMQSubscription(const char* topicKey, const char* json)
	{
		if (strcmp(topicKey, DEVICE_SERIAL_GET_ALL_BY_DEVICE_KEY_COMPLETED_TOPIC_SUB) == 0) {
			getAllSub(json);
			return true;
		}
		else if (strcmp(topicKey, DEVICE_SERIAL_SET_ENABLED_TOPIC_SUB) == 0) {
			setEnabledSub(json);
			return true;
		}
		else if (strcmp(topicKey, DEVICE_SERIAL_SET_PIN_TOPIC_SUB) == 0) {
			setPinSub(json);
			return true;
		}
		else {
			return false;
		}
	}
}