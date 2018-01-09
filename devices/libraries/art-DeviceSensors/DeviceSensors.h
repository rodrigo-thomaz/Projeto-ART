#ifndef DeviceSensors_h
#define DeviceSensors_h

#include "Arduino.h"
#include "ArduinoJson.h"

class ESPDevice;

class DeviceSensors
{

public:

	DeviceSensors(ESPDevice* espDevice, int publishIntervalInMilliSeconds);
	~DeviceSensors();
	
	int									getPublishIntervalInMilliSeconds();
	void								setPublishIntervalInMilliSeconds(char* json);
	
	static void createDeviceSensors(DeviceSensors* (&deviceSensors), ESPDevice* espDevice, JsonObject& jsonObject)
    {
		int publishIntervalInMilliSeconds = jsonObject["publishIntervalInMilliSeconds"];
		
		deviceSensors = new DeviceSensors(espDevice, publishIntervalInMilliSeconds); 
    }
	
private:	

	ESPDevice*          				_espDevice;	
	
	int									_publishIntervalInMilliSeconds;
	
};

#endif