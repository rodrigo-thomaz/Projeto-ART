#ifndef SensorTrigger_h
#define SensorTrigger_h

#include "Arduino.h"
#include "ArduinoJson.h"

#define SENSOR_TRIGGER_INSERT_TOPIC_SUB "SensorTrigger/InsertIoT"
#define SENSOR_TRIGGER_DELETE_TOPIC_SUB "SensorTrigger/DeleteIoT"
#define SENSOR_TRIGGER_SET_TRIGGER_ON_TOPIC_SUB "SensorTrigger/SetTriggerOnIoT"
#define SENSOR_TRIGGER_SET_BUZZER_ON_TOPIC_SUB "SensorTrigger/SetBuzzerOnIoT"
#define SENSOR_TRIGGER_SET_TRIGGER_VALUE_TOPIC_SUB "SensorTrigger/SetTriggerValueIoT"

namespace ART
{
	class Sensor;

	class SensorTrigger
	{

	public:
		SensorTrigger(Sensor* sensor, JsonObject& jsonObject);
		~SensorTrigger();
		
		static SensorTrigger				create(Sensor* sensor, JsonObject& jsonObject);

		char* 								getSensorTriggerId() const;

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