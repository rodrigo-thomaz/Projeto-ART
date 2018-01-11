#ifndef Sensor_h
#define Sensor_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "ArduinoJson.h"
#include "vector"
#include "OneWire.h"
#include "DallasTemperature.h"
#include "SensorTempDSFamily.h"

namespace ART
{
	class SensorInDevice;

	enum TempSensorAlarmPosition
	{
		Max = 0,
		Min = 1,
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
		Sensor(SensorInDevice* sensorInDevice, JsonObject& jsonObject);
		~Sensor();		

		static String getFamily(byte deviceAddress[8]) {
			switch (deviceAddress[0]) {
			case DS18S20MODEL:
				return "DS18S20";
			case DS18B20MODEL:
				return "DS18B20";
			case DS1822MODEL:
				return "DS1822";
			case DS1825MODEL:
				return "DS1825";
			case DS28EA00MODEL:
				return "DS28EA00";
			default:
				return "";
			}
		}

		static void create(Sensor* (&sensor), SensorInDevice* sensorInDevice, JsonObject& jsonObject)
		{			
			sensor = new Sensor(sensorInDevice, jsonObject);
		}

		char*								getSensorId();

		const uint8_t*		 				getDeviceAddress();

		char*								getFamily() const;
		bool								getValidFamily();

		char* 								getLabel() const;
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

		SensorInDevice *					_sensorInDevice;

		SensorTempDSFamily *				_sensorTempDSFamily;

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
	};
}

#endif