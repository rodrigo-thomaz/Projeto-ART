#include "DeviceSerialItem.h"

namespace ART
{
	DeviceSerialItem::DeviceSerialItem(JsonObject& jsonObject)
	{
		Serial.println(F("[DeviceSerialItem:DeviceSerialItem] constructor"));

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