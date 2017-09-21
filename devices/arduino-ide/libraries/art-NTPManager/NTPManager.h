#ifndef NTPManager_h
#define NTPManager_h

#include "Arduino.h"

class NTPManager
{
public:
	NTPManager();
	~NTPManager();
	void begin();
	long getEpochTime();
private:

};

#endif