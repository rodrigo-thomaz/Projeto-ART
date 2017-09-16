/*
  TemperatureSensor.h - Library for Led Light code.
  Created by Rodrigo Thomaz, June 4, 2017.
  Released into the public domain.
*/
#ifndef TemperatureSensor_h
#define TemperatureSensor_h

#include "Arduino.h"

#include <OneWire.h>
#include <DallasTemperature.h>

class TemperatureSensor
{
  public:
    TemperatureSensor();	
	uint8_t *deviceAddress;
	int resolution;
	float tempCelsius;
	float tempFahrenheit;
};

#endif
