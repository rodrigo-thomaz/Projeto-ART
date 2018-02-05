#ifndef SensorUnitMeasurementScale_h
#define SensorUnitMeasurementScale_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "UnitMeasurementEnum.h"

#define SENSOR_UNIT_MEASUREMENT_SCALE_SET_DATASHEET_UNIT_MEASUREMENT_SCALE_TOPIC_SUB "SensorUnitMeasurementScale/SetDatasheetUnitMeasurementScaleIoT"
#define SENSOR_UNIT_MEASUREMENT_SCALE_RANGE_SET_VALUE_TOPIC_SUB "SensorUnitMeasurementScale/SetRangeIoT"
#define SENSOR_UNIT_MEASUREMENT_SCALE_CHART_LIMITER_SET_VALUE_TOPIC_SUB "SensorUnitMeasurementScale/SetChartLimiterIoT"

namespace ART
{
	class Sensor;	

	class SensorUnitMeasurementScale
	{

	public:
		SensorUnitMeasurementScale(Sensor* sensor, JsonObject& jsonObject);
		~SensorUnitMeasurementScale();

		static void							create(SensorUnitMeasurementScale* (&sensorUnitMeasurementScale), Sensor* sensor, JsonObject& jsonObject);

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

		Sensor *							_sensor;

		UnitMeasurementEnum					_unitMeasurementId;

		float 								_rangeMax;
		float 								_rangeMin;

		float 								_chartLimiterMax;
		float 								_chartLimiterMin;

	};
}

#endif