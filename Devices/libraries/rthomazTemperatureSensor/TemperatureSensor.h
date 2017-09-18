/*
  TemperatureSensor.h - Library for Led Light code.
  Created by Rodrigo Thomaz, June 4, 2017.
  Released into the public domain.
*/
#ifndef TemperatureSensor_h
#define TemperatureSensor_h

#include "Arduino.h"

class TemperatureSensor
{
  public:
    TemperatureSensor();	
	byte deviceAddress[8];
	bool validFamily;
	String family;
	bool isConnected;	
	int resolution;
	float tempCelsius;
	float tempFahrenheit;
	bool hasAlarm;	
	char lowAlarmTemp;
	char highAlarmTemp;
	long epochTime;
};

#endif
