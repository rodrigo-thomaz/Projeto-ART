#ifndef DisplayMQTTManager_h
#define DisplayMQTTManager_h

#include "Arduino.h"
#include "DisplayManager.h"

class DisplayMQTTManager
{
public:
	DisplayMQTTManager(DisplayManager& displayManager);
	~DisplayMQTTManager();

	void					printConnected();
	void					printSent(bool on);
	void					printReceived(bool on);

private:

	DisplayManager * _displayManager;

	int			         	_x;
	int			         	_y;

};

#endif