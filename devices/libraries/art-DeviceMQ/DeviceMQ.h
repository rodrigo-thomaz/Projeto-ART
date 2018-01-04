#ifndef DeviceMQ_h
#define DeviceMQ_h

#include "Arduino.h"
#include "ArduinoJson.h"

class ESPDevice;

class DeviceMQ
{

public:

	DeviceMQ(ESPDevice* espDevice, char* host, int port, char* user, char* password, char* clientId, char* deviceTopic);
	~DeviceMQ();
	
	ESPDevice*          				getESPDevice();	
	
	char*								getHost();
	int									getPort();
	char*								getUser();
	char*								getPassword();	
	char*								getClientId();	
	char*								getDeviceTopic();	
	
	static void createDeviceMQ(DeviceMQ* (&deviceMQ), ESPDevice* espDevice, JsonObject& jsonObject)
    {
		deviceMQ = new DeviceMQ(
			espDevice,
			strdup(jsonObject["host"]), 
			jsonObject["port"], 
			strdup(jsonObject["user"]), 
			strdup(jsonObject["password"]),
			strdup(jsonObject["clientId"]),			
			strdup(jsonObject["deviceTopic"]));
    }
	
private:	

	ESPDevice*          				_espDevice;	
	
	char*								_host;
	int									_port;
	char*								_user;
	char*								_password;
	char*								_clientId;
	char*								_deviceTopic;
	
};

#endif