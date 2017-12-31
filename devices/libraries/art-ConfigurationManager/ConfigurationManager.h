#ifndef ConfigurationManager_h
#define ConfigurationManager_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "WiFiManager.h"
#include "ESP8266HTTPClient.h"

#include "ESPDevice.h"

class DeviceNTP {
  public:

    DeviceNTP(String host, int port, int utcTimeOffsetInSecond, int updateIntervalInMilliSecond);

    String								getHost();
	int									getPort();
	int									getUtcTimeOffsetInSecond();
	int									getUpdateIntervalInMilliSecond();	
	
  private:
    
	String								_host;
	int									_port;
	int									_utcTimeOffsetInSecond;
	int									_updateIntervalInMilliSecond;	

	void								setUtcTimeOffsetInSecond(int value);
	void								setUpdateIntervalInMilliSecond(int value);	
	
    friend class ConfigurationManager;
};

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
  
    ConfigurationManager(DebugManager& debugManager, WiFiManager& wifiManager, String host, uint16_t port, String uri = "/");
		
	void								begin();
	
	void								autoInitialize();
	
	bool								initialized();
	
	DeviceNTP*							getDeviceNTP();
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

	DeviceNTP*							_deviceNTP;	
	DeviceInApplication*				_deviceInApplication;	
	
	ESPDevice*							_espDevice;		
};

#endif
