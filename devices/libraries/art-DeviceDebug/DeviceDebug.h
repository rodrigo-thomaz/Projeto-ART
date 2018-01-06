#ifndef DeviceDebug_h
#define DeviceDebug_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "RemoteDebug.h" 

class ESPDevice;

class DeviceDebug
{

public:

	DeviceDebug(ESPDevice* espDevice);
	~DeviceDebug();
	
	void								loop();
	
	bool 								isActive(uint8_t debugLevel = DEBUG);
	
	int									print(const char* className, const char* caller, const char* message);
	template<typename... Args> int		printf(const char* className, const char* caller, const char* format, Args... args);

	void								load(JsonObject& jsonObject);
	
	void								setRemoteEnabled(char* json);	
	void								setResetCmdEnabled(char* json);	
	void								setSerialEnabled(char* json);	
	void								setShowColors(char* json);	
	void								setShowDebugLevel(char* json);	
	void								setShowProfiler(char* json);
	void								setShowTime(char* json);
		
	static void createDeviceDebug(DeviceDebug* (&deviceDebug), ESPDevice* espDevice)
    {
		deviceDebug = new DeviceDebug(
			espDevice);
    }

	static const uint8_t PROFILER = 0;
	static const uint8_t VERBOSE = 1;
	static const uint8_t DEBUG = 2;
	static const uint8_t INFO = 3;
	static const uint8_t WARNING = 4;
	static const uint8_t ERROR = 5;
	static const uint8_t ANY = 6;
	
private:	

	ESPDevice*          				_espDevice;	
	
	RemoteDebug* 						_debug;
	
	bool								_remoteEnabled;
	bool								_resetCmdEnabled;
	bool								_serialEnabled;
	bool								_showColors;
	bool								_showDebugLevel;
	bool								_showProfiler;
	bool								_showTime;
	
	std::string							createExpression(const char* className, const char* caller, const char* expression);
};

#endif