#ifndef DisplayNTPManager_h
#define DisplayNTPManager_h

#include "Arduino.h"
#include "DebugManager.h"
#include "DisplayManager.h"
#include "NTPManager.h"
#include "WiFiManager.h"

class DisplayNTPManager
{
public:
	DisplayNTPManager(DisplayManager& displayManager, NTPManager& ntpManager, DebugManager& debugManager, WiFiManager& wifiManager);
	~DisplayNTPManager();	
	
	void					printTime();
	void					printUpdate(bool on);
	
private:

	DisplayManager*       	_displayManager;	
	NTPManager*          	_ntpManager;
	DebugManager*         	_debugManager;
	WiFiManager*          	_wifiManager;

};

#endif