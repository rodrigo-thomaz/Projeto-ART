#ifndef TemperatureScaleConverter_h
#define TemperatureScaleConverter_h

#include "Arduino.h"
#include "DebugManager.h"

class TemperatureScaleConverter
{
  public:
  
    TemperatureScaleConverter(DebugManager& debugManager);
	
	float																	convertFromCelsius(int temperatureScaleId, float celsius);
	float																	convertCelsiusToFahrenheit(float celsius);
										
  private:												
												
	DebugManager*          													_debugManager;	
	
};

#endif