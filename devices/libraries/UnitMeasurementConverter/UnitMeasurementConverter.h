#ifndef UnitMeasurementConverter_h
#define UnitMeasurementConverter_h

#include "Arduino.h"
#include "SensorUnitMeasurementScale.h"

namespace ART
{
	class UnitMeasurementConverter
	{
	public:

		UnitMeasurementConverter();

		float								convertFromCelsius(UnitMeasurementEnum unitMeasurementId, float celsius);
		float								convertCelsiusToFahrenheit(float celsius);

	private:

	};
}

#endif