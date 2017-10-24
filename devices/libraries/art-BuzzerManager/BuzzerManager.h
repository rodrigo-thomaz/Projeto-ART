#ifndef BuzzerManager_h
#define BuzzerManager_h

#include "Arduino.h"
#include "DebugManager.h"

class BuzzerManager
{
public:
	BuzzerManager(int pin, DebugManager& debugManager);
	~BuzzerManager();	
	
	void					test();
	
private:

	int 					_pin;
	DebugManager*         	_debugManager;

};

#endif