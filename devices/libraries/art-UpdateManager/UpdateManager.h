#ifndef UpdateManager_h
#define UpdateManager_h

#include "Arduino.h"
#include "DebugManager.h"
#include "WiFiManager.h"

#include <ESP8266HTTPClient.h>
#include <ESP8266httpUpdate.h>

#define CHECKFORUPDATES_INTERVAL 10000

class UpdateManager
{
public:
	UpdateManager(DebugManager& debugManager, WiFiManager& wifiManager, String host, uint16_t port, String uri = "/");
	~UpdateManager();
	
	void 						loop();
	
private:	

	DebugManager*          		_debugManager;	
	WiFiManager*          		_wifiManager;
	
	String 						_host;
	uint16_t 					_port;
	String 						_uri;

	uint64_t 					_checkForUpdatesTimestamp = 0;
	
	void 						update();
};

#endif