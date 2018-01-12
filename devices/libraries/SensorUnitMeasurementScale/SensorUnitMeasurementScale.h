#ifndef SensorUnitMeasurementScale_h
#define SensorUnitMeasurementScale_h

#include "Arduino.h"
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

		byte 								getUnitOfMeasurementId();
		void 								setUnitOfMeasurementId(int value);

		float 								getRangeMax();
		void 								setRangeMax(float value);

		float 								getRangeMin();
		void 								setRangeMin(float value);

		float 								getChartLimiterMax();
		void 								setChartLimiterMax(float value);

		float 								getChartLimiterMin();
		void 								setChartLimiterMin(float value);

	private:

		Sensor * _sensor;

		byte								_unitOfMeasurementId;

		float 								_rangeMax;
		float 								_rangeMin;

		float 								_chartLimiterMax;
		float 								_chartLimiterMin;
	};
}

#endif