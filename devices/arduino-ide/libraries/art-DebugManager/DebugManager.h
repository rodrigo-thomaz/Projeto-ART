#ifndef DebugManager_h
#define DebugManager_h

#include "Arduino.h"

class DebugManager
{
public:
	DebugManager();
	~DebugManager();
	void update();
	bool isDebug();
private:
	bool _isDebug;
};

#endif