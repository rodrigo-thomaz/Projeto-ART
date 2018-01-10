#include "UnitOfMeasurementConverter.h"

UnitOfMeasurementConverter::UnitOfMeasurementConverter()
{

}

float UnitOfMeasurementConverter::convertFromCelsius(int unitOfMeasurementId, float celsius)
{
	switch (unitOfMeasurementId)
	{
	case 101:
		return celsius;
	case 102:
		return convertCelsiusToFahrenheit(celsius);
	default:
		break;
	}
}

float UnitOfMeasurementConverter::convertCelsiusToFahrenheit(float celsius)
{
	return (celsius * 1.8) + 32;
}