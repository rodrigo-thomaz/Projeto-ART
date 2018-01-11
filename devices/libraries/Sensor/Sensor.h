#ifndef Sensor_h
#define Sensor_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "ArduinoJson.h"
#include "vector"

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
		Sensor(SensorInDevice* sensorInDevice, short ordination);
		~Sensor();		

		static Sensor create(SensorInDevice* sensorInDevice, JsonObject& jsonObject)
		{
			return Sensor(
				sensorInDevice,
				jsonObject["ordination"]);
		}

	private:

		SensorInDevice *					_sensorInDevice;

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