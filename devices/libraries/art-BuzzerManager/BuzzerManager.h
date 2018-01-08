#ifndef BuzzerManager_h
#define BuzzerManager_h

#include "Arduino.h"

class BuzzerManager
{
public:
	BuzzerManager(int pin);
	~BuzzerManager();	
	
	void					test();
	
private:

	int 					_pin;

};

#endif