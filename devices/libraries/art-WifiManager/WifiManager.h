#ifndef WifiManager_h
#define WifiManager_h

#include "Arduino.h"
#include "DebugManager.h"

class WifiManager
{
public:
	WifiManager(DebugManager& debugManager);
	void begin();
	void connect();
private:
	DebugManager*          _debugManager;
};

#endif
