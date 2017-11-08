#ifndef TemperatureSensorManager_h
#define TemperatureSensorManager_h

#include "Arduino.h"
#include "vector"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "NTPManager.h"
#include "OneWire.h"
#include "DallasTemperature.h"
#include "ConfigurationManager.h"
#include "MQQTManager.h"

#define TEMPERATURE_SENSOR_GET_ALL_BY_HARDWARE_IN_APPLICATION_ID_REQUEST_JSON_SIZE 				200
#define TEMPERATURE_SENSOR_GET_ALL_BY_HARDWARE_IN_APPLICATION_ID_RESPONSE_JSON_SIZE 			4000

#define TEMPERATURE_SENSOR_GET_ALL_BY_HARDWARE_IN_APPLICATION_ID_MQQT_TOPIC_PUB   				"DSFamilyTempSensor.GetAllByHardwareInApplicationId" 
#define TEMPERATURE_SENSOR_GET_ALL_BY_HARDWARE_IN_APPLICATION_ID_COMPLETED_MQQT_TOPIC_SUB   	"DSFamilyTempSensor.GetAllByHardwareInApplicationIdCompleted"

class TemperatureSensor
{
	
  public:
  
	TemperatureSensor(String dsFamilyTempSensorId, String deviceAddressStr, bool validFamily, String family);

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
  
    TemperatureSensorManager(DebugManager& debugManager, NTPManager& ntpManager, ConfigurationManager& configurationManager, MQQTManager& mqqtManager);
	
	bool 								begin();
				
	void 								refresh();	
			
	TemperatureSensor 					*getSensors();
	char 								*getSensorsJson();		
				
	void 								setResolution(String json);
	void 								setLowAlarm(String json);
	void 								setHighAlarm(String json);
				
	void 								setSensorsByMQQTCallback(String json);				
				
  private:			
			
	DebugManager*          				_debugManager;
	NTPManager*          				_ntpManager;
	ConfigurationManager*				_configurationManager;	
	MQQTManager* 		                _mqqtManager;
	
	bool								_begin;
	bool								_beginning;
	
	const uint8_t 						*getDeviceAddress(String deviceAddress);
	String 								getFamily(byte deviceAddress[8]);
	void								generateNestedSensor(TemperatureSensor temperatureSensor, JsonArray& root);
	
	std::vector<TemperatureSensor> 		_sensors;
	
};

#endif
