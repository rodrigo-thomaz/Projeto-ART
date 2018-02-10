#ifndef DeviceSerialItem_h
#define DeviceSerialItem_h

#include "../ArduinoJson/ArduinoJson.h"

namespace ART
{
	class DeviceSerialItem
	{

	public:

		DeviceSerialItem(JsonObject& jsonObject);
		~DeviceSerialItem();

	private:

		bool								_hasRX;
		bool								_hasTX;

		bool								_allowPinSwapRX;
		bool								_allowPinSwapTX;

		short								_pinRX;
		short								_pinTX;

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