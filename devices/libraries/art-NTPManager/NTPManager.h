#ifndef NTPManager_h
#define NTPManager_h

#include "Arduino.h"
#include "DebugManager.h"

class NTPManager
{
public:
	NTPManager(DebugManager& debugManager);
	~NTPManager();
	void begin();
	long getEpochTime();
private:
	DebugManager*          _debugManager;
};

#endif