#ifndef TemperatureSensorManager_h
#define TemperatureSensorManager_h

#include "Arduino.h"
#include "DebugManager.h"
#include "NTPManager.h"

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

class TemperatureSensorManager
{
  public:
    TemperatureSensorManager(DebugManager& debugManager, NTPManager& ntpManager);
	
	void begin();
	void refresh();
	
	void setResolution(String json);
	void setLowAlarm(String json);
	void setHighAlarm(String json);

	char *convertSensorsToJson();
	
	TemperatureSensor 		*Sensors;
	
  private:
	
	DebugManager*          	_debugManager;
	NTPManager*          	_ntpManager;
	const uint8_t *getDeviceAddress(String deviceAddress);
};

#endif
