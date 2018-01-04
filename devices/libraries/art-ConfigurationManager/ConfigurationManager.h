#ifndef ConfigurationManager_h
#define ConfigurationManager_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "WiFiManager.h"
#include "ESP8266HTTPClient.h"

#include "ESPDevice.h"

class ConfigurationManager
{
  public:
  
    ConfigurationManager(WiFiManager& wifiManager, ESPDevice& espDevice, String host, uint16_t port, String uri = "/");
		
	void								begin();
	
	void								autoInitialize();
	
	bool								initialized();
	
	ESPDevice*							getESPDevice();

	void								insertInApplication(String json);
	void								deleteFromApplication();		
	
	void								setUtcTimeOffsetInSecond(String json);
	void								setUpdateIntervalInMilliSecond(String json);
	
  private:			
			
	WiFiManager*          				_wifiManager;

	bool 								_initialized = false;
	
	String 								_host;
	uint16_t 							_port;
	String 								_uri;
	
	ESPDevice*							_espDevice;		
};

#endif
