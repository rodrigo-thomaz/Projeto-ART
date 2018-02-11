#include "DeviceSerialItem.h"

namespace ART
{
	DeviceSerialItem::DeviceSerialItem(JsonObject& jsonObject)
	{
		Serial.println(F("[DeviceSerialItem:DeviceSerialItem] constructor"));

		const char* deviceSerialId = jsonObject["deviceSerialId"];
		_deviceSerialId = new char[strlen(deviceSerialId) + 1];
		strcpy(_deviceSerialId, deviceSerialId);
		_deviceSerialId[strlen(deviceSerialId) + 1] = '\0';

		_enabled = int(jsonObject["enabled"]);

		_hasRX = int(jsonObject["hasRX"]);
		_hasTX = int(jsonObject["hasTX"]);

		_allowPinSwapRX = int(jsonObject["allowPinSwapRX"]);
		_allowPinSwapTX = int(jsonObject["allowPinSwapTX"]);

		_pinRX = int(jsonObject["pinRX"]);
		_pinTX = int(jsonObject["pinTX"]);
	}

	DeviceSerialItem::~DeviceSerialItem()
	{
		Serial.println(F("[DeviceSerialItem:DeviceSerialItem] destructor"));

		delete[] _deviceSerialId;
	}

	char * DeviceSerialItem::getDeviceSerialId() const
	{
		return (_deviceSerialId);
	}

	void DeviceSerialItem::setEnabled(bool value)
	{
		_enabled = value;
	}

	void DeviceSerialItem::setHasRX(bool value)
	{
		_hasRX = value;
	}

	void DeviceSerialItem::setHasTX(bool value)
	{
		_hasTX = value;
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