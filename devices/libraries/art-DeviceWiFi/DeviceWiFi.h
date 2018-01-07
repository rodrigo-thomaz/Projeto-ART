#ifndef DeviceWiFi_h
#define DeviceWiFi_h

#include "ArduinoJson.h"
#include "ESP8266WiFi.h"

class ESPDevice;

class DeviceWiFi
{

public:

	DeviceWiFi(ESPDevice* espDevice);
	~DeviceWiFi();
	
	void						load(JsonObject& jsonObject);
	
	char *						getStationMacAddress();
	char *						getSoftAPMacAddress();		
	char *						getHostName();		
		
	static void createDeviceWiFi(DeviceWiFi* (&deviceWiFi), ESPDevice* espDevice)
    {
		deviceWiFi = new DeviceWiFi(
			espDevice);
    }
	
private:	

	ESPDevice*          		_espDevice;	
	
	char *						_stationMacAddress;
	char *						_softAPMacAddress;		
	char *						_hostName;		
};

#endif