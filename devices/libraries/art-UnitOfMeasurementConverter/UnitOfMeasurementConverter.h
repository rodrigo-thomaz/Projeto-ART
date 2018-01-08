#ifndef UnitOfMeasurementConverter_h
#define UnitOfMeasurementConverter_h

#include "Arduino.h"

class UnitOfMeasurementConverter
{
  public:
  
    UnitOfMeasurementConverter();
	
	float																	convertFromCelsius(int unitOfMeasurementId, float celsius);
	float																	convertCelsiusToFahrenheit(float celsius);
										
  private:												
	
};

#endif