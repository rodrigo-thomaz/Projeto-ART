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

	void								load(JsonObject& jsonObject);
	
	bool								getRemoteEnabled();
	void								setRemoteEnabled(String json);
	
	bool								getResetCmdEnabled();
	void								setResetCmdEnabled(String json);
	
	bool								getSerialEnabled();
	void								setSerialEnabled(String json);
	
	bool								getShowColors();
	void								setShowColors(String json);
	
	bool								getShowDebugLevel();
	void								setShowDebugLevel(String json);
	
	bool								getShowProfiler();
	void								setShowProfiler(String json);
	
	bool								getShowTime();
	void								setShowTime(String json);
		
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