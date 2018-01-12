#include "SensorUnitMeasurementScale.h"
#include "Sensor.h"

namespace ART
{
	SensorUnitMeasurementScale::SensorUnitMeasurementScale(Sensor* sensor, JsonObject& jsonObject)
	{
		Serial.println("[SensorUnitMeasurementScale constructor]");

		_sensor = sensor;

		_unitOfMeasurementId = byte(jsonObject["unitOfMeasurementId"]);
		_lowChartLimiterCelsius = float(jsonObject["lowChartLimiterCelsius"]);
		_highChartLimiterCelsius = float(jsonObject["highChartLimiterCelsius"]);
	}

	SensorUnitMeasurementScale::~SensorUnitMeasurementScale()
	{
		Serial.println("[SensorUnitMeasurementScale destructor]");
	}

	byte SensorUnitMeasurementScale::getUnitOfMeasurementId()
	{
		return _unitOfMeasurementId;
	}

	void SensorUnitMeasurementScale::setUnitOfMeasurementId(int value)
	{
		_unitOfMeasurementId = value;
	}

	float SensorUnitMeasurementScale::getLowChartLimiterCelsius()
	{
		return _lowChartLimiterCelsius;
	}

	void SensorUnitMeasurementScale::setLowChartLimiterCelsius(float value)
	{
		_lowChartLimiterCelsius = value;
	}

	float SensorUnitMeasurementScale::getHighChartLimiterCelsius()
	{
		return _highChartLimiterCelsius;
	}

	void SensorUnitMeasurementScale::setHighChartLimiterCelsius(float value)
	{
		_highChartLimiterCelsius = value;
	}
}