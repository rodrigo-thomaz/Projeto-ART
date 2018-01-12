#ifndef SensorUnitMeasurementScale_h
#define SensorUnitMeasurementScale_h

#include "ArduinoJson.h"

namespace ART
{
	class Sensor;

	class SensorUnitMeasurementScale
	{

	public:
		SensorUnitMeasurementScale(Sensor* sensor, JsonObject& jsonObject);
		~SensorUnitMeasurementScale();

		static void create(SensorUnitMeasurementScale* (&sensorUnitMeasurementScale), Sensor* sensor, JsonObject& jsonObject)
		{
			sensorUnitMeasurementScale = new SensorUnitMeasurementScale(sensor, jsonObject);
		}

	private:

		Sensor * _sensor;

	};
}

#endif