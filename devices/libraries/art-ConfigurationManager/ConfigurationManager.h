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
  
    ConfigurationManager(WiFiManager& wifiManager, ESPDevice& espDevice);
		
	void								begin();
	
	void								autoInitialize();
	
	bool								initialized();
	
	ESPDevice*							getESPDevice();

	void								insertInApplication(String json);
	void								deleteFromApplication();		
	
  private:			
			
	WiFiManager*          				_wifiManager;

	bool 								_initialized = false;
	
	ESPDevice*							_espDevice;		
};

#endif
