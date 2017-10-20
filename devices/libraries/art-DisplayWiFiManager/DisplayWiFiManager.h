#ifndef DisplayWiFiManager_h
#define DisplayWiFiManager_h

#include "Arduino.h"
#include "DebugManager.h"
#include "DisplayManager.h"
#include "WiFiManager.h"

#include "Fonts/FreeSans9pt7b.h"
#include "Fonts/FreeSansBold9pt7b.h"

class DisplayWiFiManager
{
public:
	DisplayWiFiManager(DisplayManager& displayManager, WiFiManager& wifiManager, DebugManager& debugManager);
	~DisplayWiFiManager();	
	void					printPortalHeaderInDisplay(String title);
	void 					showEnteringSetup();
	void 					showWiFiConect();
private:
	DisplayManager*       	_displayManager;	
	WiFiManager*          	_wifiManager;
	DebugManager*         	_debugManager;
};

#endif