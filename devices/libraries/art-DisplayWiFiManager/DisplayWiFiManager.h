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
	
	void					begin();
	
	void 					startConfigPortalCallback ();
	void 					captivePortalCallback (String ip);
	void 					successConfigPortalCallback ();
	void 					failedConfigPortalCallback (int connectionResult);
	void 					connectingConfigPortalCallback ();
	void					printSignal(int x, int y, int barWidth, int margin, int barSignal);
	void					printNoSignal(int x, int y, int barWidth, int margin);
	
private:

	DisplayManager*       	_displayManager;	
	WiFiManager*          	_wifiManager;
	DebugManager*         	_debugManager;
	
	bool 					_firstTimecaptivePortalCallback = true;
	
	void					printPortalHeaderInDisplay(String title);
	void 					showEnteringSetup();
	void 					showWiFiConect();

};

#endif