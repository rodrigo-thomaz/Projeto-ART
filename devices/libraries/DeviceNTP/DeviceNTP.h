#ifndef DeviceNTP_h
#define DeviceNTP_h

#include "Arduino.h"
#include "../ArduinoJson/ArduinoJson.h"
#include "RemoteDebug.h"
#include "Udp.h"
#include "WiFiUdp.h"

#define SEVENZYYEARS 2208988800UL
#define NTP_PACKET_SIZE 48

#define DEVICE_NTP_SET_UPDATE_CALLBACK_SIGNATURE std::function<void(bool, bool)>

#define DEVICE_NTP_SET_UTC_TIME_OFF_SET_IN_SECOND_TOPIC_SUB "DeviceNTP/SetUtcTimeOffsetInSecondIoT"
#define DEVICE_NTP_SET_UPDATE_INTERVAL_IN_MILLI_SECOND_TOPIC_SUB "DeviceNTP/SetUpdateIntervalInMilliSecondIoT"

namespace ART
{
	class ESPDevice;

	class DeviceNTP
	{

	public:

		DeviceNTP(ESPDevice* espDevice);
		~DeviceNTP();

		void											load(JsonObject& jsonObject);

		char*											getHost() const;
		int												getPort();

		int												getUtcTimeOffsetInSecond();		

		long											getUpdateIntervalInMilliSecond();

		/**
		* Starts the underlying UDP client with the default local port
		*/
		void											begin();

		/**
		* This should be called in the main loop of your application. By default an update from the NTP Server is only
		* made every 60 seconds. This can be configured in the DeviceNTP constructor.
		*
		* @return true on success, false on failure
		*/
		bool update();

		/**
		* This will force the update from the NTP Server.
		*
		* @return true on success, false on failure
		*/
		bool forceUpdate();

		int getDay();
		int getHours();
		int getMinutes();
		int getSeconds();

		/**
		* @return time formatted like `hh:mm:ss`
		*/
		String getFormattedTimeOld();

		String getFormattedTime();

		/**
		* @return time in seconds since Jan. 1, 1970
		*/
		unsigned long getEpochTime();
		unsigned long getEpochTimeUTC();

		/**
		* Stops the underlying UDP client
		*/
		void end();

		DeviceNTP& setUpdateCallback(DEVICE_NTP_SET_UPDATE_CALLBACK_SIGNATURE callback);		

	private:

		ESPDevice *										_espDevice;

		char*											_host;
		int												_port;
		int												_utcTimeOffsetInSecond;
		long											_updateIntervalInMilliSecond;

		UDP*          									_udp;
		bool          									_udpSetup = false;

		unsigned long 									_currentEpoc = 0;      // In s
		unsigned long 									_lastUpdate = 0;      // In ms

		byte          									_packetBuffer[NTP_PACKET_SIZE];

		void          									sendNTPPacket();

		DEVICE_NTP_SET_UPDATE_CALLBACK_SIGNATURE		_updateCallback;

		bool 											_loaded = false;

		void											setUtcTimeOffsetInSecond(const char* json);
		void											setUpdateIntervalInMilliSecond(const char* json);

		void											onDeviceMQSubscribeDeviceInApplication();
		void											onDeviceMQUnSubscribeDeviceInApplication();
		bool											onDeviceMQSubscription(const char* topicKey, const char* json);
	};
}

#endif