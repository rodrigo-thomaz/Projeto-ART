#ifndef TemperatureSensorManager_h
#define TemperatureSensorManager_h

#include "Arduino.h"
#include "vector"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "NTPManager.h"
#include "OneWire.h"
#include "DallasTemperature.h"


class TemperatureSensor
{
	
  public:
  
	TemperatureSensor(String dsFamilyTempSensorId, String deviceAddress, String family);

    String								getDSFamilyTempSensorId();		
	
	String								getDeviceAddress();	
	const uint8_t*						getDeviceAddressArray();		
	
	String								getFamily();
	bool								getValidFamily();	
		
	int 								getResolution();
	void 								setResolution(int value);
	
	bool 								getConnected();	
	void 								setConnected(bool value);	
	
	float 								getTempCelsius();
	void 								setTempCelsius(float value);
	
	float 								getTempFahrenheit();
	void 								setTempFahrenheit(float value);
	
	bool 								getHasAlarm();	
	void 								setHasAlarm(bool value);	
	
	char 								getLowAlarm();
	void 								setLowAlarm(char value);
	
	char 								getHighAlarm();
	void 								setHighAlarm(char value);
	
	long 								getEpochTimeUtc();
	void 								setEpochTimeUtc(long value);
	
  private:
  
	String 								_dsFamilyTempSensorId;
	
	String 								_deviceAddress;
	const uint8_t* 						_deviceAddressArray;	
	
	String 								_family;
	bool 								_validFamily;
	
	int 								_resolution;
	
	bool 								_connected;		
	float 								_tempCelsius;
	float 								_tempFahrenheit;
	bool 								_hasAlarm;	
	char 								_lowAlarm;
	char 								_highAlarm;
	
	long 								_epochTimeUtc;
	
	friend class TemperatureSensorManager;
	
};

class TemperatureSensorManager
{
  public:
  
    TemperatureSensorManager(DebugManager& debugManager, NTPManager& ntpManager);
	
	void 								begin();
				
	void 								refresh();	
			
	TemperatureSensor 					*getSensors();
	char 								*getSensorsJson();		
				
	void 								setResolution(String json);
	void 								setLowAlarm(String json);
	void 								setHighAlarm(String json);
				
  private:			
			
	DebugManager*          				_debugManager;
	NTPManager*          				_ntpManager;
				
	const uint8_t 						*getDeviceAddress(String deviceAddress);
	String 								getFamily(byte deviceAddress[8]);
	void								generateNestedSensor(TemperatureSensor temperatureSensor, JsonArray& root);
	
	std::vector<TemperatureSensor> 		_sensors;
	
};

#endif
