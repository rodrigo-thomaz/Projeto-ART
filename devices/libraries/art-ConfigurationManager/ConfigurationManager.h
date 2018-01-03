#ifndef ConfigurationManager_h
#define ConfigurationManager_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "WiFiManager.h"
#include "ESP8266HTTPClient.h"

#include "ESPDevice.h"

class DeviceInApplication {
  public:

    DeviceInApplication					(String applicationId);

	String								getApplicationId();	
		
  private:
    
	String								_applicationId;

	void								setApplicationId(String value);	
	
    friend class ConfigurationManager;
};

class ConfigurationManager
{
  public:
  
    ConfigurationManager(DebugManager& debugManager, WiFiManager& wifiManager, ESPDevice& espDevice, String host, uint16_t port, String uri = "/");
		
	void								begin();
	
	void								autoInitialize();
	
	bool								initialized();
	
	DeviceInApplication*				getDeviceInApplication();
	
	ESPDevice*							getESPDevice();

	void								insertInApplication(String json);
	void								deleteFromApplication();		
	
	void								setUtcTimeOffsetInSecond(String json);
	void								setUpdateIntervalInMilliSecond(String json);
	
  private:			
			
	DebugManager*          				_debugManager;	
	WiFiManager*          				_wifiManager;

	bool 								_initialized = false;
	
	String 								_host;
	uint16_t 							_port;
	String 								_uri;
	
	int									_chipId;
	int									_flashChipId;
	String								_macAddress;

	DeviceInApplication*				_deviceInApplication;	
	
	ESPDevice*							_espDevice;		
};

#endif
