#ifndef TemperatureSensorService_h
#define TemperatureSensorService_h

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
#define TEMPERATURE_SENSOR_GET_ALL_BY_HARDWARE_IN_APPLICATION_ID_RESPONSE_JSON_SIZE 			4096

#define TEMPERATURE_SENSOR_GET_ALL_BY_HARDWARE_IN_APPLICATION_ID_MQQT_TOPIC_PUB   				"DSFamilyTempSensor.GetAllByHardwareInApplicationId" 
#define TEMPERATURE_SENSOR_GET_ALL_BY_HARDWARE_IN_APPLICATION_ID_COMPLETED_MQQT_TOPIC_SUB   	"DSFamilyTempSensor.GetAllByHardwareInApplicationIdCompleted"

class TemperatureSensor2
{
	
  public:
  
	TemperatureSensor2(String dsFamilyTempSensorId, byte deviceAddress[8], String family, int resolution, byte temperatureScaleId);
	TemperatureSensor2(String dsFamilyTempSensorId, byte deviceAddress[8], String family, int resolution, byte temperatureScaleId, float lowAlarm, float highAlarm);

    String								getDSFamilyTempSensorId();
	
	byte*								getDeviceAddress();	
	
	String								getFamily();	
	bool								getValidFamily();	
	
	int 								getResolution();
	void 								setResolution(int value);
	
	byte 								getTemperatureScaleId();
	
	bool 								getHasAlarm();	
	void 								setHasAlarm(bool value);	
	
	float 								getLowAlarm();
	void 								setLowAlarm(float value);
	
	float 								getHighAlarm();
	void 								setHighAlarm(float value);
	
	bool 								getConnected();	
	void 								setConnected(bool value);

	float 								getRawTemperature();
	void 								setRawTemperature(float value);
	
	long 								getEpochTimeUtc();
	void 								setEpochTimeUtc(long value);	
	
  private:
  
	String 								_dsFamilyTempSensorId;	
	
	byte* 								_deviceAddress;
	
	String 								_family;
	bool 								_validFamily;
	
	int 								_resolution;
	
	byte								_temperatureScaleId;
	
	bool 								_hasAlarm;	
	float 								_lowAlarm;
	float 								_highAlarm;
	
	bool 								_connected;	
	
	float 								_rawTemperature;
	
	long 								_epochTimeUtc;	
	
	friend class DSFamilyTempSensorManager;
	
};

class TemperatureSensorService
{
  public:
  
    TemperatureSensorService(DebugManager& debugManager, NTPManager& ntpManager, ConfigurationManager& configurationManager, MQQTManager& mqqtManager);
	
	bool 								begin();
				
	void 								setSensorsByMQQTCallback(String json);				
				
  private:			
			
	DebugManager*          				_debugManager;
	NTPManager*          				_ntpManager;
	ConfigurationManager*				_configurationManager;	
	MQQTManager* 		                _mqqtManager;
	
	bool								_begin;
	bool								_beginning;
	
	std::vector<TemperatureSensor2> 		_sensors;
	
};

#endif
