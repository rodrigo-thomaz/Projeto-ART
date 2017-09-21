/*
  TemperatureSensorManager.h - Library for Led Light code.
  Created by Rodrigo Thomaz, June 4, 2017.
  Released into the public domain.
*/
#ifndef TemperatureSensorManager_h
#define TemperatureSensorManager_h

#include "Arduino.h"
#include "TemperatureSensor.h"
#include <NTPManager.h>

class TemperatureSensorManager
{
  public:
    TemperatureSensorManager(NTPManager& ntpManager);
	void begin();
    TemperatureSensor *getSensors();	
  private:
	  NTPManager*          _ntpManager;
};

#endif
