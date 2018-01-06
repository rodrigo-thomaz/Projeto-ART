#ifndef DeviceDebug_h
#define DeviceDebug_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "RemoteDebug.h"        //https://github.com/JoaoLopesF/RemoteDebug

class ESPDevice;

class DeviceDebug
{

public:

	DeviceDebug(ESPDevice* espDevice);
	~DeviceDebug();
	
	void								begin();
	void								loop();
	
	bool 								isActive(uint8_t debugLevel);
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
	
	// Debug levels

	static const uint8_t PROFILER = 0; 	// Used for show time of execution of pieces of code(profiler)
	static const uint8_t VERBOSE = 1; 	// Used for show verboses messages
	static const uint8_t DEBUG = 2;   	// Used for show debug messages
	static const uint8_t INFO = 3;		// Used for show info messages
	static const uint8_t WARNING = 4;	// Used for show warning messages
	static const uint8_t ERROR = 5;		// Used for show error messages
	static const uint8_t ANY = 6;		// Used for show always messages, for any current debug level
	
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
};

#endif