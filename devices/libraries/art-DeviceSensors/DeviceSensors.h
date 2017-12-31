#ifndef DeviceSensors_h
#define DeviceSensors_h

#include "Arduino.h"
#include "ArduinoJson.h"

class ESPDevice;

class DeviceSensors
{

public:

	DeviceSensors(ESPDevice* espDevice, int publishIntervalInSeconds);
	~DeviceSensors();
	
	ESPDevice*          				getESPDevice();	
	
	int									getPublishIntervalInSeconds();
	void								setPublishIntervalInSeconds(int value);
	
	static void createDeviceSensors(DeviceSensors* (&deviceSensors), ESPDevice* espDevice, JsonObject& jsonObject)
    {
		int publishIntervalInSeconds = jsonObject["publishIntervalInSeconds"];
		
		deviceSensors = new DeviceSensors(espDevice, publishIntervalInSeconds); 
    }
	
private:	

	ESPDevice*          				_espDevice;	
	
	int									_publishIntervalInSeconds;
	
};

#endif