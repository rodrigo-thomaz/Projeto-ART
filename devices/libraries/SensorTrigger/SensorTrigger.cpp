#include "SensorTrigger.h"
#include "Sensor.h"

namespace ART
{
	SensorTrigger::SensorTrigger(Sensor* sensor, JsonObject& jsonObject)
	{
		Serial.println("[SensorTrigger constructor]");

		_sensor = sensor;	

		char* sensorTriggerId = strdup(jsonObject["sensorTriggerId"]);
		_sensorTriggerId = new char(sizeof(strlen(sensorTriggerId)));
		_sensorTriggerId = sensorTriggerId;

		_triggerOn = bool(jsonObject["triggerOn"]);
		_buzzerOn = bool(jsonObject["buzzerOn"]);
		_max = float(jsonObject["max"]);
		_min = float(jsonObject["min"]);
	}

	SensorTrigger::~SensorTrigger()
	{
		Serial.println("[SensorTrigger destructor]");
	}

	char * SensorTrigger::getSensorTriggerId()
	{
		return _sensorTriggerId;
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
}