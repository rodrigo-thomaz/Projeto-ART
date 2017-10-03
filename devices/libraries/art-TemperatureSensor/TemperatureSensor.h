#ifndef TemperatureSensor_h
#define TemperatureSensor_h

#include "Arduino.h"

class TemperatureSensor
{
  public:
    TemperatureSensor();	
	String dsFamilyTempSensorId;
	byte deviceAddress[8];
	String deviceAddressStr;
	bool validFamily;
	String family;
	bool isConnected;	
	int resolution;
	float tempCelsius;
	float tempFahrenheit;
	bool hasAlarm;	
	char lowAlarm;
	char highAlarm;
	long epochTime;
};

#endif
