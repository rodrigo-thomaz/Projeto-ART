#ifndef DeviceDebug_h
#define DeviceDebug_h

#include "Arduino.h"
#include "ArduinoJson.h"

class ESPDevice;

class DeviceDebug
{

public:

	DeviceDebug(ESPDevice* espDevice, bool active);
	~DeviceDebug();
	
	ESPDevice*          				getESPDevice();	
	
	bool								getActive();
	void								setActive(String json);
		
	static void createDeviceDebug(DeviceDebug* (&deviceDebug), ESPDevice* espDevice, JsonObject& jsonObject)
    {
		deviceDebug = new DeviceDebug(
			espDevice,
			jsonObject["active"]);
    }
	
private:	

	ESPDevice*          				_espDevice;	
	
	bool								_active;
};

#endif