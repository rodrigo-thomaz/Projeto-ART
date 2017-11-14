#ifndef DSFamilyTempSensorManager_h
#define DSFamilyTempSensorManager_h

#include "Arduino.h"
#include "vector"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "NTPManager.h"
#include "ConfigurationManager.h"
#include "MQQTManager.h"
#include "OneWire.h"
#include "DallasTemperature.h"

#define DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_REQUEST_JSON_SIZE 			200
#define DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_RESPONSE_JSON_SIZE 			4096

#define DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_MQQT_TOPIC_PUB   				"DSFamilyTempSensor.GetAllByDeviceInApplicationIdIoT" 
#define DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED_MQQT_TOPIC_SUB   	"DSFamilyTempSensor.GetAllByDeviceInApplicationIdCompletedIoT"


class DSFamilyTempSensor
{
	
  public:
  
	DSFamilyTempSensor(String dsFamilyTempSensorId, DeviceAddress deviceAddress, String family, int resolution, byte temperatureScaleId);
	DSFamilyTempSensor(String dsFamilyTempSensorId, DeviceAddress deviceAddress, String family, int resolution, byte temperatureScaleId, float lowAlarm, float highAlarm);

    String								getDSFamilyTempSensorId();		
	
	const uint8_t*		 				getDeviceAddress();	
	
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
	
	float 								getTemperatureWithScale();
	
	long 								getEpochTimeUtc();
	void 								setEpochTimeUtc(long value);	
	
  private:
  
	String 								_dsFamilyTempSensorId;	
	
	std::vector<uint8_t> 				_deviceAddress;
	
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

class DSFamilyTempSensorManager
{
  public:
  
    DSFamilyTempSensorManager(DebugManager& debugManager, NTPManager& ntpManager, ConfigurationManager& configurationManager, MQQTManager& mqqtManager);
	
	void 								begin();
				
	bool								initialized();
	void 								setSensorsByMQQTCallback(String json);				
	
	void 								refresh();	
			
	DSFamilyTempSensor 					*getSensors();
	char 								*getSensorsJson();		
				
	void 								setResolution(String json);
	void 								setLowAlarm(String json);
	void 								setHighAlarm(String json);
				
  private:			
			
	DebugManager*          				_debugManager;
	NTPManager*          				_ntpManager;
	ConfigurationManager*				_configurationManager;	
	MQQTManager* 		                _mqqtManager;				
	
	bool								_initialized;
	bool								_initializing;					
				
	const uint8_t* 						getDeviceAddressById(String deviceAddress);
	String 								getFamily(byte deviceAddress[8]);
	void								generateNestedSensor(DSFamilyTempSensor dsFamilyTempSensor, JsonArray& root);
	String 								convertDeviceAddressToString(const uint8_t* deviceAddress);
	
	std::vector<DSFamilyTempSensor> 	_sensors;
};

#endif
