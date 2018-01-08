#ifndef DisplayManager_h
#define DisplayManager_h

#include "Arduino.h"
#include "Adafruit_SSD1306.h"

class DisplayManager
{
public:
	DisplayManager();
	void begin();
	Adafruit_SSD1306 display;
private:
	
};

#endif