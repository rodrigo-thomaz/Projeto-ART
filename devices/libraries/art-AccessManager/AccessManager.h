#ifndef AccessManager_h
#define AccessManager_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "WiFiManager.h"
#include "ESP8266HTTPClient.h"

class AccessManager
{
  public:
  
    AccessManager(DebugManager& debugManager, String host, uint16_t port, String uri = "/");
		
	void								begin();
	const char							*getBrokerHost();
	int									getBrokerPort();
				
  private:			
			
	DebugManager*          				_debugManager;	

	String 								_host;
	uint16_t 							_port;
	String 								_uri;
	
	int									_chipId;
	int									_flashChipId;
	String								_macAddress;
	
	void								getConfigurations();
	
	const char							*_brokerHost;
	int									_brokerPort;
	
};

#endif
