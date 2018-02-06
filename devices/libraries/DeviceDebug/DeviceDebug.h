#ifndef DeviceDebug_h
#define DeviceDebug_h

#include "ArduinoJson.h"
#include "RemoteDebug.h" 
#include "ESP8266mDNS.h"

#define DEVICE_DEBUG_SET_REMOTE_ENABLED_TOPIC_SUB "DeviceDebug/SetRemoteEnabledIoT"
#define DEVICE_DEBUG_SET_RESET_CMD_ENABLED_TOPIC_SUB "DeviceDebug/SetResetCmdEnabledIoT"
#define DEVICE_DEBUG_SET_SERIAL_ENABLED_TOPIC_SUB "DeviceDebug/SetSerialEnabledIoT"
#define DEVICE_DEBUG_SET_SHOW_COLORS_TOPIC_SUB "DeviceDebug/SetShowColorsIoT"
#define DEVICE_DEBUG_SET_SHOW_DEBUG_LEVEL_TOPIC_SUB "DeviceDebug/SetShowDebugLevelIoT"
#define DEVICE_DEBUG_SET_SHOW_PROFILER_TOPIC_SUB "DeviceDebug/SetShowProfilerIoT"
#define DEVICE_DEBUG_SET_SHOW_TIME_TOPIC_SUB "DeviceDebug/SetShowTimeIoT"

namespace ART
{
	class ESPDevice;

	class DeviceDebug
	{

	public:

		DeviceDebug(ESPDevice* espDevice);
		~DeviceDebug();

		static void							create(DeviceDebug* (&deviceDebug), ESPDevice* espDevice);

		void								begin();

		void								loop();

		bool 								isActive(uint8_t debugLevel = DEBUG);

		int									println();
		int									println(const char* className, const char* caller);
		int									println(const char* className, const char* caller, const char* message);

		int									print(const char* className, const char* caller, const char* message);

		template<typename... Args>
		int									printf(const char* className, const char* caller, const char* format, Args... args);	

		int									printlnLevel(uint8_t debugLevel);
		int									printlnLevel(uint8_t debugLevel, const char* className, const char* caller);
		int									printlnLevel(uint8_t debugLevel, const char* className, const char* caller, const char* message);

		int									printLevel(uint8_t debugLevel, const char* className, const char* caller, const char* message);		
				
		void								load(JsonObject& jsonObject);

		static const uint8_t				PROFILER = 0;
		static const uint8_t				VERBOSE = 1;
		static const uint8_t				DEBUG = 2;
		static const uint8_t				INFO = 3;
		static const uint8_t				WARNING = 4;
		static const uint8_t				ERROR = 5;
		static const uint8_t				ANY = 6;

	private:

		ESPDevice *							_espDevice;

		RemoteDebug* 						_debug;

		void								setRemoteEnabled(char* json);
		void								setResetCmdEnabled(char* json);
		void								setSerialEnabled(char* json);
		void								setShowColors(char* json);
		void								setShowDebugLevel(char* json);
		void								setShowProfiler(char* json);
		void								setShowTime(char* json);

		void								setHostName(char* value);
		void								setRemoteEnabled(bool value);
		void								setResetCmdEnabled(bool value);
		void								setSerialEnabled(bool value);
		void								setShowColors(bool value);
		void								setShowDebugLevel(bool value);
		void								setShowProfiler(bool value);
		void								setShowTime(bool value);

		char*								_hostName;
		bool								_remoteEnabled;
		bool								_resetCmdEnabled;
		bool								_serialEnabled;
		bool								_showColors;
		bool								_showDebugLevel;
		bool								_showProfiler;
		bool								_showTime;

		void								initTelnetServer();
		bool								_telnetServer;

		std::string							createExpression(const char* className, const char* caller, const char* expression);

		void								onDeviceMQSubscribeDeviceInApplication();
		void								onDeviceMQUnSubscribeDeviceInApplication();
		bool								onDeviceMQSubscription(char* topicKey, char* json);
	};
}

#endif