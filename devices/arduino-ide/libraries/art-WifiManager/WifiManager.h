#ifndef WifiManager_h
#define WifiManager_h

#include "Arduino.h"
#include "DebugManager.h"

class WifiManager
{
public:
	WifiManager(DebugManager& debugManager);
	bool connect();
private:
	DebugManager*          _debugManager;
};

#endif
