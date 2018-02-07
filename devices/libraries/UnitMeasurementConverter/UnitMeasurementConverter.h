#ifndef UnitMeasurementConverter_h
#define UnitMeasurementConverter_h

#include "Arduino.h"
#include "../UnitMeasurement/UnitMeasurementEnum.h"

namespace ART
{
	class UnitMeasurementConverter
	{
	public:

		static float						convertFromCelsius(UnitMeasurementEnum unitMeasurementId, float celsius);
		static float						convertCelsiusToFahrenheit(float celsius);

	private:

	};
}

#endif