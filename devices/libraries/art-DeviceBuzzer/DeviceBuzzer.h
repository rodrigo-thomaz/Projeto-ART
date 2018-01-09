#ifndef DeviceBuzzer_h
#define DeviceBuzzer_h

#include "Arduino.h"

class DeviceBuzzer
{
public:
	DeviceBuzzer(int pin);
	~DeviceBuzzer();	
	
	void					test();
	
private:

	int 					_pin;

};

#endif