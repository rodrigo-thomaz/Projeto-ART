#include "SensorUnitMeasurementScale.h"
#include "Sensor.h"

namespace ART
{
	SensorUnitMeasurementScale::SensorUnitMeasurementScale(Sensor* sensor, JsonObject& jsonObject)
	{
		Serial.println("[SensorUnitMeasurementScale constructor]");

		_sensor = sensor;
	}

	SensorUnitMeasurementScale::~SensorUnitMeasurementScale()
	{
		Serial.println("[SensorUnitMeasurementScale destructor]");
	}
}