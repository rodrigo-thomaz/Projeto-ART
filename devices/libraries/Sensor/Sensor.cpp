#include "Sensor.h"
#include "SensorInDevice.h"

namespace ART
{
	// TempSensorAlarm

	TempSensorAlarm::TempSensorAlarm(bool alarmOn, float alarmCelsius, bool alarmBuzzerOn, TempSensorAlarmPosition alarmPosition)
	{
		this->_alarmOn = alarmOn;
		this->_alarmCelsius = alarmCelsius;
		this->_alarmBuzzerOn = alarmBuzzerOn;
		this->_alarmPosition = alarmPosition;
	}

	bool TempSensorAlarm::getAlarmOn()
	{
		return this->_alarmOn;
	}

	void TempSensorAlarm::setAlarmOn(bool value)
	{
		this->_alarmOn = value;
	}

	float TempSensorAlarm::getAlarmCelsius()
	{
		return this->_alarmCelsius;
	}

	void TempSensorAlarm::setAlarmCelsius(float value)
	{
		this->_alarmCelsius = value;
	}

	bool TempSensorAlarm::getAlarmBuzzerOn()
	{
		return this->_alarmBuzzerOn;
	}

	void TempSensorAlarm::setAlarmBuzzerOn(bool value)
	{
		this->_alarmBuzzerOn = value;
	}

	bool TempSensorAlarm::hasAlarm()
	{
		if (!this->_alarmOn) return false;

		switch (this->_alarmPosition)
		{
		case Max: return this->_tempCelsius > this->_alarmCelsius;
		case Min: return this->_tempCelsius < this->_alarmCelsius;
		}
	}

	bool TempSensorAlarm::hasAlarmBuzzer()
	{
		return this->hasAlarm() && this->_alarmBuzzerOn;
	}

	void TempSensorAlarm::setTempCelsius(float value)
	{
		this->_tempCelsius = value;
	}
}