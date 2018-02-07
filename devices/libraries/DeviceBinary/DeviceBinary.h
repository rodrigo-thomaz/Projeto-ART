#ifndef DeviceBinary_h
#define DeviceBinary_h

#include "Arduino.h"
#include "RemoteDebug.h"
#include "ESP8266HTTPClient.h"
#include "ESP8266httpUpdate.h"

#define CHECKFORUPDATES_INTERVAL 10000

namespace ART
{
	class ESPDevice;

	class DeviceBinary
	{

	public:

		DeviceBinary(ESPDevice* espDevice);
		~DeviceBinary();

		static void					create(DeviceBinary* (&deviceBinary), ESPDevice* espDevice);

		void 						loop();		

	private:

		ESPDevice *					_espDevice;

		uint64_t 					_checkForUpdatesTimestamp = 0;

		void 						update();
	};
}

#endif