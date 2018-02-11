#include "DeviceSerialItem.h"

namespace ART
{
	DeviceSerialItem::DeviceSerialItem(JsonObject& jsonObject)
	{
		Serial.println(F("[DeviceSerialItem:DeviceSerialItem] constructor begin"));

		setDeviceSerialId(jsonObject["deviceSerialId"]);

		setIndex(jsonObject["index"]);		

		setSerialMode(static_cast<SerialMode>(jsonObject["serialMode"].as<int>()));

		setAllowPinSwapRX(jsonObject["allowPinSwapRX"]);
		setAllowPinSwapTX(jsonObject["allowPinSwapTX"]);

		setPinRX(jsonObject["pinRX"]);
		setPinTX(jsonObject["pinTX"]);		

		setEnabled(jsonObject["enabled"]);
		
		Serial.println(F("[DeviceSerialItem:DeviceSerialItem] constructor end"));
	}

	DeviceSerialItem::~DeviceSerialItem()
	{
		Serial.println(F("[DeviceSerialItem:DeviceSerialItem] destructor"));

		delete[] _deviceSerialId;
	}

	void DeviceSerialItem::setDeviceSerialId(const char * value)
	{
		_deviceSerialId = new char[strlen(value) + 1];
		strcpy(_deviceSerialId, value);
		_deviceSerialId[strlen(value) + 1] = '\0';
	}

	char * DeviceSerialItem::getDeviceSerialId() const
	{
		return (_deviceSerialId);
	}

	void DeviceSerialItem::setIndex(short value)
	{
		_index = value;

		if (_index == 0)
			_hardwareSerial = &Serial;
		else if (_index == 1)
			_hardwareSerial = &Serial1;
	}

	void DeviceSerialItem::setEnabled(bool value)
	{
		_enabled = value;

		if (_enabled) {
			_hardwareSerial->begin(9600);
		}
		else {
			_hardwareSerial->end();
		}
	}

	void DeviceSerialItem::setSerialMode(SerialMode value)
	{
		_serialMode = value;
	}

	void DeviceSerialItem::setAllowPinSwapRX(bool value)
	{
		_allowPinSwapRX = value;
	}

	void DeviceSerialItem::setAllowPinSwapTX(bool value)
	{
		_allowPinSwapTX = value;
	}

	void DeviceSerialItem::setPinRX(short value)
	{
		_pinRX = value;
	}

	void DeviceSerialItem::setPinTX(short value)
	{
		_pinTX = value;
	}
}