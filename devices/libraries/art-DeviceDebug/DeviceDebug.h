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
		
	RemoteDebug*						getDebug();		
	
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