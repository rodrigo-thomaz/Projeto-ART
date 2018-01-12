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

		float 								getLowChartLimiterCelsius();
		void 								setLowChartLimiterCelsius(float value);

		float 								getHighChartLimiterCelsius();
		void 								setHighChartLimiterCelsius(float value);

	private:

		Sensor * _sensor;

		byte								_unitOfMeasurementId;

		float 								_lowChartLimiterCelsius;
		float 								_highChartLimiterCelsius;
	};
}

#endif