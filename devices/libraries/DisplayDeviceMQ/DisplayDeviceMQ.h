#ifndef DisplayDeviceMQ_h
#define DisplayDeviceMQ_h

#include "Arduino.h"
#include "DisplayManager.h"

class DisplayDeviceMQ
{
public:
	DisplayDeviceMQ(DisplayManager& displayManager);
	~DisplayDeviceMQ();

	void					printConnected();
	void					printSent(bool on);
	void					printReceived(bool on);

private:

	DisplayManager * _displayManager;

	int			         	_x;
	int			         	_y;

};

#endif