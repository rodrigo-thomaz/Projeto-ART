#ifndef UpdateManager_h
#define UpdateManager_h

#include "Arduino.h"
#include "ESPDevice.h"

#include <ESP8266HTTPClient.h>
#include <ESP8266httpUpdate.h>

#define CHECKFORUPDATES_INTERVAL 10000

class UpdateManager
{
public:
	UpdateManager(ESPDevice& espDevice);
	~UpdateManager();
	
	void 						loop();
	
private:	

	ESPDevice*          		_espDevice;	

	uint64_t 					_checkForUpdatesTimestamp = 0;
	
	void 						update();
};

#endif