#include "SensorTrigger.h"
#include "Sensor.h"

namespace ART
{
	SensorTrigger::SensorTrigger(Sensor* sensor, JsonObject& jsonObject)
	{
		Serial.println(F("[SensorTrigger constructor]"));

		_sensor = sensor;	
				
		char* sensorTriggerId = strdup(jsonObject["sensorTriggerId"]);
		_sensorTriggerId = new char(sizeof(strlen(sensorTriggerId)));
		_sensorTriggerId = sensorTriggerId;
		
		/*
		const char* sensorTriggerId = jsonObject["sensorTriggerId"];
		_sensorTriggerId = new char(strlen(sensorTriggerId) + 1);
		strcpy(_sensorTriggerId, sensorTriggerId);
		*/

		_triggerOn = bool(jsonObject["triggerOn"]);
		_buzzerOn = bool(jsonObject["buzzerOn"]);
		_max = float(jsonObject["max"]);
		_min = float(jsonObject["min"]);
	}

	SensorTrigger::~SensorTrigger()
	{
		Serial.println(F("[SensorTrigger destructor]"));
	}

	char * SensorTrigger::getSensorTriggerId() const
	{
		return (_sensorTriggerId);
	}

	bool SensorTrigger::getTriggerOn()
	{
		return _triggerOn;
	}

	void SensorTrigger::setTriggerOn(bool value)
	{
		_triggerOn = value;
	}

	bool SensorTrigger::getBuzzerOn()
	{
		return _buzzerOn;
	}

	void SensorTrigger::setBuzzerOn(bool value)
	{
		_buzzerOn = value;
	}

	float SensorTrigger::getMax()
	{
		return _max;
	}

	void SensorTrigger::setMax(float value)
	{
		_max = value;
	}

	float SensorTrigger::getMin()
	{
		return _min;
	}

	void SensorTrigger::setMin(float value)
	{
		_min = value;
	}

	bool SensorTrigger::hasAlarm()
	{
		float temp = _sensor->getValue();
		return temp >= _min && temp <= _max;
	}

	bool SensorTrigger::hasAlarmBuzzer()
	{
		return hasAlarm() && _buzzerOn;
	}
}