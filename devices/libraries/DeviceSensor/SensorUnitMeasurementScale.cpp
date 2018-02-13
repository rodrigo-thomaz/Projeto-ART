#include "SensorUnitMeasurementScale.h"
#include "Sensor.h"

namespace ART
{
	SensorUnitMeasurementScale::SensorUnitMeasurementScale(Sensor* sensor, JsonObject& jsonObject)
	{
		Serial.println(F("[SensorUnitMeasurementScale constructor]"));

		_sensor = sensor;

		_unitMeasurementId = static_cast<UnitMeasurementEnum>(jsonObject["unitMeasurementId"].as<short>());

		_rangeMax = float(jsonObject["rangeMax"]);
		_rangeMin = float(jsonObject["rangeMin"]);

		_chartLimiterMax = float(jsonObject["chartLimiterMax"]);
		_chartLimiterMin = float(jsonObject["chartLimiterMin"]);
	}

	SensorUnitMeasurementScale::~SensorUnitMeasurementScale()
	{
		Serial.println(F("[SensorUnitMeasurementScale destructor]"));
	}

	UnitMeasurementEnum SensorUnitMeasurementScale::getUnitMeasurementId()
	{
		return _unitMeasurementId;
	}

	void SensorUnitMeasurementScale::setUnitMeasurementId(UnitMeasurementEnum value)
	{
		_unitMeasurementId = value;
	}

	float SensorUnitMeasurementScale::getRangeMax()
	{
		return _rangeMax;
	}

	void SensorUnitMeasurementScale::setRangeMax(float value)
	{
		_rangeMax = value;
	}

	float SensorUnitMeasurementScale::getRangeMin()
	{
		return _rangeMin;
	}

	void SensorUnitMeasurementScale::setRangeMin(float value)
	{
		_rangeMin = value;
	}

	float SensorUnitMeasurementScale::getChartLimiterMax()
	{
		return _chartLimiterMax;
	}

	void SensorUnitMeasurementScale::setChartLimiterMax(float value)
	{
		_chartLimiterMax = value;
	}

	float SensorUnitMeasurementScale::getChartLimiterMin()
	{
		return _chartLimiterMin;
	}

	void SensorUnitMeasurementScale::setChartLimiterMin(float value)
	{
		_chartLimiterMin = value;
	}
}