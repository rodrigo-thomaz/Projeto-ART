#ifndef DeviceSensors_h
#define DeviceSensors_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "vector"
#include "OneWire.h"
#include "DallasTemperature.h"

namespace ART
{
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
		
	class Sensor
	{
		
	public:

		Sensor(char* sensorId, DeviceAddress deviceAddress, char* family, char* label, int resolution, byte unitOfMeasurementId, TempSensorAlarm lowAlarm, TempSensorAlarm highAlarm, float lowChartLimiterCelsius, float highChartLimiterCelsius);

		char*								getSensorId();		

		const uint8_t*		 				getDeviceAddress();	

		char*								getFamily();
		bool								getValidFamily();	
			
		char* 								getLabel();
		void 								setLabel(char* value);

		int 								getResolution();
		void 								setResolution(int value);

		byte 								getUnitOfMeasurementId();
		void 								setUnitOfMeasurementId(int value);

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

		char* 								_sensorId;	

		std::vector<uint8_t> 				_deviceAddress;

		char* 								_family;
		bool 								_validFamily;

		char* 								_label;

		int 								_resolution;

		byte								_unitOfMeasurementId;
			
		std::vector<TempSensorAlarm> 		_alarms; 

		bool 								_connected;	

		float 								_tempCelsius;	

		long 								_epochTimeUtc;	

		float 								_lowChartLimiterCelsius;
		float 								_highChartLimiterCelsius;

		//friend class 						DSFamilyTempSensorManager;
		
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
}

#endif