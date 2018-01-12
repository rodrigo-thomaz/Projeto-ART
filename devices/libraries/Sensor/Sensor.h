#ifndef Sensor_h
#define Sensor_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "ArduinoJson.h"
#include "vector"
#include "OneWire.h"
#include "DallasTemperature.h"
#include "SensorTempDSFamily.h"
#include "SensorUnitMeasurementScale.h"

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

		TempSensorAlarm& 					getLowAlarm();
		TempSensorAlarm& 					getHighAlarm();

		bool 								getConnected();
		void 								setConnected(bool value);

		float 								getTempCelsius();
		void 								setTempCelsius(float value);

		bool 								hasAlarm();
		bool 								hasAlarmBuzzer();		

		SensorTempDSFamily *				getSensorTempDSFamily();
		SensorUnitMeasurementScale *		getSensorUnitMeasurementScale();

	private:

		SensorInDevice * _sensorInDevice;

		SensorTempDSFamily *				_sensorTempDSFamily;
		SensorUnitMeasurementScale *		_sensorUnitMeasurementScale;

		char* 								_sensorId;

		std::vector<uint8_t> 				_deviceAddress;

		char* 								_family;
		bool 								_validFamily;

		char* 								_label;	

		std::vector<TempSensorAlarm> 		_alarms;

		bool 								_connected;

		float 								_tempCelsius;

		long 								_epochTimeUtc;		
	};
}

#endif