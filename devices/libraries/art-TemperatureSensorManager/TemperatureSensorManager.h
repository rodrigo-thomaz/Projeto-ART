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
  
	TemperatureSensor(String dsFamilyTempSensorId, const uint8_t* deviceAddress, String deviceAddressStr, bool validFamily, String family);

    String								getDSFamilyTempSensorId();		
	
	const uint8_t*						getDeviceAddress();		
	
	String								getDeviceAddressStr();
	
	bool								getValidFamily();
	
	String								getFamily();
		
	bool 								getConnected();	
	void 								setConnected(bool value);	
	
	int 								getResolution();
	void 								setResolution(int value);
	
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
	
	long 								getEpochTime();
	void 								setEpochTime(long value);
	
  private:
  
	String 								_dsFamilyTempSensorId;
	const uint8_t* 						_deviceAddress;
	String 								_deviceAddressStr;
	bool 								_validFamily;
	String 								_family;
	bool 								_connected;	
	int 								_resolution;
	float 								_tempCelsius;
	float 								_tempFahrenheit;
	bool 								_hasAlarm;	
	char 								_lowAlarm;
	char 								_highAlarm;
	long 								_epochTime;
	
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
