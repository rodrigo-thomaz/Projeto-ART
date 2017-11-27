#include "UnitOfMeasurementConverter.h"

UnitOfMeasurementConverter::UnitOfMeasurementConverter(DebugManager& debugManager)
{ 
	this->_debugManager = &debugManager;
}

float UnitOfMeasurementConverter::convertFromCelsius(int unitOfMeasurementId, float celsius)
{ 
	switch (unitOfMeasurementId)
	{
		case 1:
			return celsius;
		case 2:
			return convertCelsiusToFahrenheit(celsius);
		default:		
			break;
	}
}

float UnitOfMeasurementConverter::convertCelsiusToFahrenheit(float celsius)
{ 
	return (celsius * 1.8) + 32;
}