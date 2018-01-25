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
		
		static void create(SensorTrigger* (&sensorTrigger), Sensor* sensor, JsonObject& jsonObject)
		{
			sensorTrigger = new SensorTrigger(sensor, jsonObject);
		}		

		bool 								getTriggerOn();
		void 								setTriggerOn(bool value);

		bool 								getBuzzerOn();
		void 								setBuzzerOn(bool value);

		float 								getMax();
		void 								setMax(float value);

		float 								getMin();
		void 								setMin(float value);

	private:

		Sensor *							_sensor;
		
		bool 								_triggerOn;
		bool 								_buzzerOn;
		float 								_max;
		float 								_min;

	};
}

#endif