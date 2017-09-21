#ifndef DisplayManager_h
#define DisplayManager_h

#include "Arduino.h"
#include "DebugManager.h"
#include "Adafruit_SSD1306.h"

class DisplayManager
{
public:
	DisplayManager(DebugManager& debugManager);
	void begin();
	Adafruit_SSD1306 display;
private:
	DebugManager*          _debugManager;
};

#endif