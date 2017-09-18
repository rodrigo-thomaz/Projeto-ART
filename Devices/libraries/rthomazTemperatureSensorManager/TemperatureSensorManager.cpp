/*
  TemperatureSensorManager.cpp - Library for Led Light code.
  Created by Rodrigo Thomaz, June 4, 2017.
  Released into the public domain.
*/

#include "Arduino.h"

#include "TemperatureSensorManager.h"
#include "TemperatureSensor.h"

// Include the libraries we need
#include <OneWire.h>
#include <DallasTemperature.h>

#include <NTPClient.h>

// Data wire is plugged into port 2 on the Arduino
#define ONE_WIRE_BUS 2
#define TEMPERATURE_PRECISION 11

// Setup a oneWire instance to communicate with any OneWire devices (not just Maxim/Dallas temperature ICs)
OneWire oneWire(ONE_WIRE_BUS);

// Pass our oneWire reference to Dallas Temperature. 
DallasTemperature sensors(&oneWire);

TemperatureSensor *arr;

TemperatureSensorManager::TemperatureSensorManager(NTPClient& timeClient)
{ 
	this->_timeClient = &timeClient;
}

String getFamily(byte deviceAddress[8]){
  switch (deviceAddress[0]) {
    case DS18S20MODEL:
      return "DS18S20";
    case DS18B20MODEL:
      return "DS18B20";
    case DS1822MODEL:
      return "DS1822";
    case DS1825MODEL:
      return "DS1825";
    case DS28EA00MODEL:
      return "DS28EA00";
    default:
      return "";
  } 
}

void TemperatureSensorManager::begin()
{	
	// Start up the library
	sensors.begin();  

	// Localizando devices
	uint8_t deviceCount;
	deviceCount = sensors.getDeviceCount();
	arr = new TemperatureSensor[deviceCount]; 
	Serial.print("Localizando devices...");
	Serial.print("Encontrado(s) ");
	Serial.print(deviceCount, DEC);
	Serial.println(" device(s).");

	// report parasite power requirements
	Serial.print("Parasite power is: "); 
	if (sensors.isParasitePowerMode()) Serial.println("ON");
	else Serial.println("OFF");

	for(int i = 0; i < deviceCount; ++i){
		DeviceAddress deviceAddress;
		if (sensors.getAddress(deviceAddress, i))
		{   
		  // address
		  for (uint8_t j = 0; j < 8; j++)
		  {
			arr[i].deviceAddress[j] = deviceAddress[j];
		  }

		  //validFamily
		  arr[i].validFamily = sensors.validFamily(arr[i].deviceAddress);

		  // family
		  arr[i].family = getFamily(arr[i].deviceAddress);     

		  // set the resolution per device
		  sensors.setResolution(arr[i].deviceAddress, TEMPERATURE_PRECISION);     
		}
		else{
		  Serial.print("Não foi possível encontrar um endereço para o Device "); 
		  Serial.println(i); 
		}
	}
}

TemperatureSensor *TemperatureSensorManager::getSensors()
{	
	sensors.requestTemperatures();

	long epochTime = this->_timeClient->getEpochTime();
	 
	for(int i = 0; i < sizeof(arr)/sizeof(int); ++i){
		arr[i].isConnected = sensors.isConnected(arr[i].deviceAddress);
		arr[i].resolution = sensors.getResolution(arr[i].deviceAddress);    
		arr[i].tempCelsius = sensors.getTempC(arr[i].deviceAddress);
		arr[i].tempFahrenheit = sensors.getTempF(arr[i].deviceAddress);
		arr[i].hasAlarm = sensors.hasAlarm(arr[i].deviceAddress);
		arr[i].lowAlarmTemp = sensors.getLowAlarmTemp(arr[i].deviceAddress);
		arr[i].highAlarmTemp = sensors.getHighAlarmTemp(arr[i].deviceAddress);
		arr[i].epochTime = epochTime;
	}
	
    return arr;
}