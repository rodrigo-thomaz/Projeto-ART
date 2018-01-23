#include "UnitMeasurementConverter.h"

namespace ART
{
	UnitMeasurementConverter::UnitMeasurementConverter()
	{

	}

	float UnitMeasurementConverter::convertFromCelsius(UnitMeasurementEnum unitMeasurementId, float celsius)
	{
		switch (unitMeasurementId)
		{
		case UnitMeasurementEnum::Celsius:
			return celsius;
		case UnitMeasurementEnum::Fahrenheit:
			return convertCelsiusToFahrenheit(celsius);
		default:
			break;
		}
	}

	float UnitMeasurementConverter::convertCelsiusToFahrenheit(float celsius)
	{
		return (celsius * 1.8) + 32;
	}
}