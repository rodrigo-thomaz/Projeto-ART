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
		
	ESPDevice*          				getESPDevice();	
	
	RemoteDebug*						getDebug();		

	void								load(JsonObject& jsonObject);
	
	bool								getActive();
	void								setActive(String json);
		
	static void createDeviceDebug(DeviceDebug* (&deviceDebug), ESPDevice* espDevice)
    {
		deviceDebug = new DeviceDebug(
			espDevice);
    }
	
private:	

	ESPDevice*          				_espDevice;	
	
	RemoteDebug* 						_debug;
	
	bool								_active;
};

#endif