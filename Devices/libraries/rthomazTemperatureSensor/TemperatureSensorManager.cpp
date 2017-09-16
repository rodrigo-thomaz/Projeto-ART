/*
  TemperatureSensorManager.cpp - Library for Led Light code.
  Created by Rodrigo Thomaz, June 4, 2017.
  Released into the public domain.
*/

#include "Arduino.h"

#include "TemperatureSensorManager.h"
#include "TemperatureSensor.h"

#include <OneWire.h>
#include <DallasTemperature.h>

#define ONE_WIRE_BUS 2
#define TEMPERATURE_PRECISION 9

// Setup a oneWire instance to communicate with any OneWire devices (not just Maxim/Dallas temperature ICs)
OneWire oneWire(ONE_WIRE_BUS);

// Pass our oneWire reference to Dallas Temperature. 
DallasTemperature sensors(&oneWire);

TemperatureSensorManager::TemperatureSensorManager(int pin)
{ 
	sensors.begin();
}

TemperatureSensor *TemperatureSensorManager::getSensors()
{	
	sensors.requestTemperatures();

	int deviceCount;
	
	deviceCount = sensors.getDeviceCount();
	
	TemperatureSensor *arr = new TemperatureSensor[deviceCount]; 
	
	for (int i = 0; i < deviceCount; i++)
	{	
		// getAddress
		DeviceAddress deviceAddress;
		sensors.getAddress(deviceAddress, i);
		
		TemperatureSensor temperatureSensor;	
		
		// Address
		temperatureSensor.deviceAddress = deviceAddress;
				
		// Resolution
		temperatureSensor.resolution = sensors.getResolution(deviceAddress);
		
		// Temperature
		temperatureSensor.tempCelsius = sensors.getTempC(deviceAddress);
		temperatureSensor.tempFahrenheit = sensors.getTempF(deviceAddress);
		
		arr[i] = temperatureSensor;
	}
	
   return arr;
}