#ifndef DSFamilyTempSensorManager_h
#define DSFamilyTempSensorManager_h

#include "Arduino.h"
#include "vector"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "NTPManager.h"
#include "OneWire.h"
#include "DallasTemperature.h"


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
  
    DSFamilyTempSensorManager(DebugManager& debugManager, NTPManager& ntpManager);
	
	void 								begin();
				
	void 								refresh();	
			
	DSFamilyTempSensor 					*getSensors();
	char 								*getSensorsJson();		
				
	void 								setResolution(String json);
	void 								setLowAlarm(String json);
	void 								setHighAlarm(String json);
				
  private:			
			
	DebugManager*          				_debugManager;
	NTPManager*          				_ntpManager;
				
	const uint8_t 						*getDeviceAddress(String deviceAddress);
	String 								getFamily(byte deviceAddress[8]);
	void								generateNestedSensor(DSFamilyTempSensor dsFamilyTempSensor, JsonArray& root);
	
	std::vector<DSFamilyTempSensor> 		_sensors;
	
};

#endif
