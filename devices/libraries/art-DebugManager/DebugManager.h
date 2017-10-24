#ifndef DebugManager_h
#define DebugManager_h

#include "Arduino.h"

class DebugManager
{
public:
	DebugManager(int pin);
	~DebugManager();
	void 						update();
	bool 						isDebug();
private:	
	int 						_pin;
	bool 						_isDebug;
};

#endif