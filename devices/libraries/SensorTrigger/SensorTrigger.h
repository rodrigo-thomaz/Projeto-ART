#ifndef SensorTrigger_h
#define SensorTrigger_h

#include "Arduino.h"
#include "ArduinoJson.h"

namespace ART
{
	class Sensor;

	class SensorTrigger
	{

	public:
		SensorTrigger(Sensor* sensor, JsonObject& jsonObject);
		~SensorTrigger();
		
		static SensorTrigger create(Sensor* sensor, JsonObject& jsonObject)
		{
			return SensorTrigger(sensor, jsonObject);
		}		

		char* 								getSensorTriggerId();

		bool 								getTriggerOn();
		void 								setTriggerOn(bool value);

		bool 								getBuzzerOn();
		void 								setBuzzerOn(bool value);

		float 								getMax();
		void 								setMax(float value);

		float 								getMin();
		void 								setMin(float value);

		bool 								hasAlarm();
		bool 								hasAlarmBuzzer();

	private:

		Sensor *							_sensor;
		
		char* 								_sensorTriggerId;
		bool 								_triggerOn;
		bool 								_buzzerOn;
		float 								_max;
		float 								_min;

	};
}

#endif