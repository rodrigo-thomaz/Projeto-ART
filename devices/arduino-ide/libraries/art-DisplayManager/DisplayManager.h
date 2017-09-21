#include "Arduino.h"

#include <Adafruit_SSD1306.h>

class DisplayManager
{
public:
	DisplayManager(Adafruit_SSD1306& display);
	void begin();
private:
	Adafruit_SSD1306*          _display;
};