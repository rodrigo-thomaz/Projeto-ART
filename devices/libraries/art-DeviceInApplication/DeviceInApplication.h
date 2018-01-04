#ifndef DeviceInApplication_h
#define DeviceInApplication_h

#include "Arduino.h"
#include "ArduinoJson.h"

class ESPDevice;

class DeviceInApplication
{

public:

	DeviceInApplication(ESPDevice* espDevice, char* applicationId);
	~DeviceInApplication();
	
	char*								getApplicationId();
	void								setApplicationId(char* value);		
	
	static void createDeviceInApplication(DeviceInApplication* (&deviceInApplication), ESPDevice* espDevice, JsonObject& jsonObject)
    {
		deviceInApplication = new DeviceInApplication(
			espDevice,
			strdup(jsonObject["applicationId"]));
    }
	
private:	

	ESPDevice*          				_espDevice;	
	
	char*								_applicationId;
	
};

#endif