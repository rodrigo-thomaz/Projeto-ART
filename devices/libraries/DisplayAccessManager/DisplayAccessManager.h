#ifndef DisplayAccessManager_h
#define DisplayAccessManager_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "DisplayDevice.h"

#define MESSAGE_INTERVAL 1000

class DisplayAccessManager
{
public:

	DisplayAccessManager(DisplayDevice& displayDevice);

	void								updatePin(char* json);
	void								loop();

private:

	DisplayDevice * _displayDevice;

	String 								_pin;
	int 								_nextFireTimeInSeconds = -1;

	uint64_t 							_messageTimestamp = 0;
};

#endif
