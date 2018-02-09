#ifndef SensorTrigger_h
#define SensorTrigger_h

#include "Arduino.h"
#include "../ArduinoJson/ArduinoJson.h"

namespace ART
{
	class Sensor;

	class SensorTrigger
	{

	public:
		SensorTrigger(Sensor* sensor, JsonObject& jsonObject);
		~SensorTrigger();

		char* 								getSensorTriggerId() const;

		bool 								getTriggerOn();
		bool 								getBuzzerOn();
		float 								getMax();
		float 								getMin();

		bool 								hasAlarm();
		bool 								hasAlarmBuzzer();

	private:

		Sensor *							_sensor;
		
		char* 								_sensorTriggerId;
		bool 								_triggerOn;
		bool 								_buzzerOn;
		float 								_max;
		float 								_min;

		void 								setTriggerOn(bool value);
		void 								setBuzzerOn(bool value);
		void 								setMax(float value);
		void 								setMin(float value);

		friend class						DeviceSensors;
	};
}

#endif