#ifndef DeviceBinary_h
#define DeviceBinary_h

#include "Arduino.h"
#include "RemoteDebug.h"
#include "ESP8266HTTPClient.h"
#include "ESP8266httpUpdate.h"

#define CHECKFORUPDATES_INTERVAL 10000

#define DEVICE_BINARY_GET_ALL_BY_KEY_TOPIC_PUB   						"DeviceBinary/GetAllByKeyIoT" 
#define DEVICE_BINARY_GET_ALL_BY_KEY_COMPLETED_TOPIC_SUB				"DeviceBinary/GetAllByKeyCompletedIoT"

namespace ART
{
	class ESPDevice;

	class DeviceBinary
	{

	public:

		DeviceBinary(ESPDevice* espDevice);
		~DeviceBinary();

		void 						loop();		

		void						getAllPub();
		void						getAllSub(const char* json);

	private:

		ESPDevice *					_espDevice;

		uint64_t 					_checkForUpdatesTimestamp = 0;

		void 						update();
	};
}

#endif