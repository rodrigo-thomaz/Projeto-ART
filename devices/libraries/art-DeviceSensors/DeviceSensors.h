#ifndef DeviceSensors_h
#define DeviceSensors_h

#include "Arduino.h"
#include "ArduinoJson.h"

class ESPDevice;

enum TempSensorAlarmPosition 
{ 
	Max  = 0, 
	Min  = 1, 
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

class DeviceSensors
{

public:

	DeviceSensors(ESPDevice* espDevice);
	~DeviceSensors();
	
	void								load(JsonObject& jsonObject);
	
	int									getPublishIntervalInMilliSeconds();
	void								setPublishIntervalInMilliSeconds(char* json);
	
	static void createDeviceSensors(DeviceSensors* (&deviceSensors), ESPDevice* espDevice)
    {
		deviceSensors = new DeviceSensors(espDevice); 
    }
	
private:	

	ESPDevice*          				_espDevice;	
	
	int									_publishIntervalInMilliSeconds;
	
};

#endif