#ifndef DeviceSerialItem_h
#define DeviceSerialItem_h

#include "Arduino.h"
#include "../ArduinoJson/ArduinoJson.h"

namespace ART
{
	class DeviceSerialItem
	{

	public:

		DeviceSerialItem(JsonObject& jsonObject);
		~DeviceSerialItem();

		char*								getDeviceSerialId() const;

	private:

		char* 								_deviceSerialId;

		bool								_enabled;

		bool								_hasRX;
		bool								_hasTX;

		bool								_allowPinSwapRX;
		bool								_allowPinSwapTX;

		short								_pinRX;
		short								_pinTX;

		void								setEnabled(bool value);

		void								setHasRX(bool value);
		void								setHasTX(bool value);

		void								setAllowPinSwapRX(bool value);
		void								setAllowPinSwapTX(bool value);

		void								setPinRX(short value);
		void								setPinTX(short value);

		friend class						DeviceSerial;
	};
}

#endif