#ifndef DeviceMQ_h
#define DeviceMQ_h

#include "Arduino.h"
#include "ArduinoJson.h"

class ESPDevice;

class DeviceMQ
{

public:

	DeviceMQ(ESPDevice* espDevice);
	~DeviceMQ();

	void								load(JsonObject& jsonObject);
	
	char*								getHost();
	int									getPort();
	char*								getUser();
	char*								getPassword();	
	char*								getClientId();	
	char*								getDeviceTopic();	
	
	static void createDeviceMQ(DeviceMQ* (&deviceMQ), ESPDevice* espDevice)
    {
		deviceMQ = new DeviceMQ(espDevice);
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