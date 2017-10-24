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
	void					printSent(bool on);	
	void					printReceived(bool on);	
	
private:

	DisplayManager*       	_displayManager;
	DebugManager*         	_debugManager;
	
	int			         	_x;
	int			         	_y;

};

#endif