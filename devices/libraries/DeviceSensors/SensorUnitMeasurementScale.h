#ifndef SensorUnitMeasurementScale_h
#define SensorUnitMeasurementScale_h

#include "../ArduinoJson/ArduinoJson.h"
#include "../UnitMeasurement/UnitMeasurementEnum.h"

namespace ART
{
	class Sensor;	

	class SensorUnitMeasurementScale
	{

	public:
		SensorUnitMeasurementScale(Sensor* sensor, JsonObject& jsonObject);
		~SensorUnitMeasurementScale();

		UnitMeasurementEnum					getUnitMeasurementId();		
		float 								getRangeMax();
		float 								getRangeMin();
		float 								getChartLimiterMax();
		float 								getChartLimiterMin();		

	private:

		Sensor *							_sensor;

		UnitMeasurementEnum					_unitMeasurementId;

		float 								_rangeMax;
		float 								_rangeMin;

		float 								_chartLimiterMax;
		float 								_chartLimiterMin;

		void 								setUnitMeasurementId(UnitMeasurementEnum value);
		void 								setRangeMax(float value);
		void 								setRangeMin(float value);
		void 								setChartLimiterMax(float value);
		void 								setChartLimiterMin(float value);

		friend class						DeviceSensors;

	};
}

#endif