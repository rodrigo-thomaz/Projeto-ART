#ifndef DisplayMQTTManager_h
#define DisplayMQTTManager_h

#include "Arduino.h"
#include "DebugManager.h"
#include "DisplayManager.h"

class DisplayMQTTManager
{
public:
	DisplayMQTTManager(DisplayManager& displayManager, DebugManager& debugManager);
	~DisplayMQTTManager();	
	
	void					printConnected();	
	void					printSent();	
	void					printReceived();	
	
private:

	DisplayManager*       	_displayManager;
	DebugManager*         	_debugManager;

};

#endif