#ifndef SensorUnitMeasurementScale_h
#define SensorUnitMeasurementScale_h

#include "Arduino.h"
#include "ArduinoJson.h"

namespace ART
{
	class Sensor;

	enum UnitMeasurementEnum
	{
		// UnitMeasurementTypeEnum.Length = 3

		Meter = 3001,
		Inch = 3002,
		Foot = 3003,
		Yard = 3004,
		Mile = 3005,
		League = 3006,

		// UnitMeasurementTypeEnum.Temperature = 11

		Celsius = 11001,
		Fahrenheit = 11002,
		Kelvin = 11003,
		Rankine = 11004,
		Romer = 11005,
		Newton = 11006,
		Delisle = 11007,
		Reaumur = 11008,
	};

	class SensorUnitMeasurementScale
	{

	public:
		SensorUnitMeasurementScale(Sensor* sensor, JsonObject& jsonObject);
		~SensorUnitMeasurementScale();

		static void create(SensorUnitMeasurementScale* (&sensorUnitMeasurementScale), Sensor* sensor, JsonObject& jsonObject)
		{
			sensorUnitMeasurementScale = new SensorUnitMeasurementScale(sensor, jsonObject);
		}

		UnitMeasurementEnum					getUnitMeasurementId();
		void 								setUnitMeasurementId(UnitMeasurementEnum value);

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

		UnitMeasurementEnum					_unitMeasurementId;

		float 								_rangeMax;
		float 								_rangeMin;

		float 								_chartLimiterMax;
		float 								_chartLimiterMin;
	};
}

#endif