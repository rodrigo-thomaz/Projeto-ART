#ifndef DeviceNTP_h
#define DeviceNTP_h

#include "Arduino.h"
#include "ArduinoJson.h"

class ESPDevice;

class DeviceNTP
{

public:

	DeviceNTP(ESPDevice* espDevice, char* host, int port, int utcTimeOffsetInSecond, int updateIntervalInMilliSecond);
	~DeviceNTP();
	
	char*								getHost();
	int									getPort();

	int									getUtcTimeOffsetInSecond();
	void								setUtcTimeOffsetInSecond(String json);
	
	int									getUpdateIntervalInMilliSecond();	
	void								setUpdateIntervalInMilliSecond(String json);		
		
	static void createDeviceNTP(DeviceNTP* (&deviceNTP), ESPDevice* espDevice, JsonObject& jsonObject)
    {
		deviceNTP = new DeviceNTP(
			espDevice,
			strdup(jsonObject["host"]), 
			jsonObject["port"], 
			jsonObject["utcTimeOffsetInSecond"],
			jsonObject["updateIntervalInMilliSecond"]);
    }
	
private:	

	ESPDevice*          				_espDevice;	
	
	char*								_host;
	int									_port;
	int									_utcTimeOffsetInSecond;
	int									_updateIntervalInMilliSecond;	
};

#endif