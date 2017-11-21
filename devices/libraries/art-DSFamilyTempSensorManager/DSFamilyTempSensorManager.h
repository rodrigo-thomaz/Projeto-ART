#ifndef DSFamilyTempSensorManager_h
#define DSFamilyTempSensorManager_h

#include "Arduino.h"
#include "vector"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "NTPManager.h"
#include "BuzzerManager.h"
#include "ConfigurationManager.h"
#include "MQQTManager.h"
#include "OneWire.h"
#include "DallasTemperature.h"

#define DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_REQUEST_JSON_SIZE 			200
#define DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_RESPONSE_JSON_SIZE 			4096

#define TOPIC_PUB_DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID   					"DSFamilyTempSensor.GetAllByDeviceInApplicationIdIoT" 
#define TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED		   	"DSFamilyTempSensor.GetAllByDeviceInApplicationIdCompletedIoT"

enum TempSensorAlarmPosition 
{ 
	High = 0, 
	Low  = 1, 
};


class TempSensorAlarm
{	
	public:
	
		TempSensorAlarm(bool alarmOn, float alarmCelsius, bool alarmBuzzerOn, TempSensorAlarmPosition alarmPosition);
		
		bool 								getAlarmOn();	
		void 								setAlarmOn(bool value);
		
		float 								getAlarmCelsius();
		void 								setAlarmCelsius(float value);
		
		bool 								getAlarmBuzzerOn();	
		void 								setAlarmBuzzerOn(bool value);
		
		bool 								hasAlarm();
		
		bool 								hasAlarmBuzzer();
		
		void 								setTempCelsius(float value);
	
	private:
	
		bool 								_alarmOn;		
		float 								_alarmCelsius;
		bool 								_alarmBuzzerOn;
		TempSensorAlarmPosition				_alarmPosition;
		
		float 								_tempCelsius;
};

class DSFamilyTempSensor
{
	
  public:
  
	DSFamilyTempSensor(String dsFamilyTempSensorId, DeviceAddress deviceAddress, String family, int resolution, byte temperatureScaleId, TempSensorAlarm lowAlarm, TempSensorAlarm highAlarm, float lowChartLimiterCelsius, float highChartLimiterCelsius);

    String								getDSFamilyTempSensorId();		
	
	const uint8_t*		 				getDeviceAddress();	
	
	String								getFamily();
	bool								getValidFamily();	
		
	int 								getResolution();
	void 								setResolution(int value);
	
	byte 								getTemperatureScaleId();
	void 								setTemperatureScaleId(int value);
	
	TempSensorAlarm& 					getLowAlarm();	
	TempSensorAlarm& 					getHighAlarm();	
	 
	bool 								getConnected();	
	void 								setConnected(bool value);

	float 								getTempCelsius();
	void 								setTempCelsius(float value);
	
	bool 								hasAlarm();	
	bool 								hasAlarmBuzzer();	
	
	float 								getLowChartLimiterCelsius();
	void 								setLowChartLimiterCelsius(float value);
	
	float 								getHighChartLimiterCelsius();
	void 								setHighChartLimiterCelsius(float value);
	
  private:	
	
	String 								_dsFamilyTempSensorId;	
	
	std::vector<uint8_t> 				_deviceAddress;
	
	String 								_family;
	bool 								_validFamily;
	
	int 								_resolution;
	
	byte								_temperatureScaleId;
		
	std::vector<TempSensorAlarm> 		_alarms; 
	
	bool 								_connected;	
	
	float 								_tempCelsius;	
	
	long 								_epochTimeUtc;	
	
	float 								_lowChartLimiterCelsius;
	float 								_highChartLimiterCelsius;
	
	friend class 						DSFamilyTempSensorManager;
	
};

class DSFamilyTempSensorManager
{
  public:
  
    DSFamilyTempSensorManager(DebugManager& debugManager, ConfigurationManager& configurationManager, MQQTManager& mqqtManager, BuzzerManager& buzzerManager);
	
	void 								begin();
				
	bool								initialized();
	void 								setSensorsByMQQTCallback(String json);				
	
	void 								refresh();	
			
	DSFamilyTempSensor 					*getSensors();
	
	void 								createSensorsJsonNestedArray(JsonObject& jsonObject);		
				
	void 								setScale(String json);
	void 								setResolution(String json);
	
	void 								setAlarmOn(String json);
	void 								setAlarmCelsius(String json);
	void 								setAlarmBuzzerOn(String json);
	void 								setChartLimiterCelsius(String json);
				
  private:			
			
	DebugManager*          				_debugManager;
	ConfigurationManager*				_configurationManager;	
	MQQTManager* 		                _mqqtManager;				
	BuzzerManager* 		                _buzzerManager;				
	
	bool								_initialized;
	bool								_initializing;					
				
	DSFamilyTempSensor&					getDSFamilyTempSensorById(String deviceAddress);
	String 								getFamily(byte deviceAddress[8]);
	void								createSensorJsonNestedObject(DSFamilyTempSensor dsFamilyTempSensor, JsonArray& root);
	String 								convertDeviceAddressToString(const uint8_t* deviceAddress);
	
	std::vector<DSFamilyTempSensor> 	_sensors;
};

#endif
