#include "TemperatureScaleConverter.h"

TemperatureScaleConverter::TemperatureScaleConverter(DebugManager& debugManager)
{ 
	this->_debugManager = &debugManager;
}

float TemperatureScaleConverter::convertFromCelsius(int temperatureScaleId, float celsius)
{ 
	switch (temperatureScaleId)
	{
		case 1:
			return celsius;
		case 2:
			return convertCelsiusToFahrenheit(celsius);
		default:		
			break;
	}
}

float TemperatureScaleConverter::convertCelsiusToFahrenheit(float celsius)
{ 
	return (celsius * 1.8) + 32;
}