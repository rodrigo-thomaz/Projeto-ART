#ifndef DeviceDisplayWiFiAccess_h
#define DeviceDisplayWiFiAccess_h

#include "Arduino.h"
#include "../ArduinoJson/ArduinoJson.h"

#define MESSAGE_INTERVAL 1000

namespace ART
{
	class DeviceDisplay;

	class DeviceDisplayWiFiAccess
	{
	public:

		DeviceDisplayWiFiAccess(DeviceDisplay* deviceDisplay);
		~DeviceDisplayWiFiAccess();

		static void					create(DeviceDisplayWiFiAccess* (&deviceDisplayWiFiAccess), DeviceDisplay* deviceDisplay);

		void						begin();

		void						updatePin(const char* json);
		void						loop();

	private:

		DeviceDisplay *				_deviceDisplay;

		String 						_pin;
		int 						_nextFireTimeInSeconds = -1;

		uint64_t 					_messageTimestamp = 0;

		bool						onDeviceMQSubscription(const char* topicKey, const char* json);

	};
}

#endif
