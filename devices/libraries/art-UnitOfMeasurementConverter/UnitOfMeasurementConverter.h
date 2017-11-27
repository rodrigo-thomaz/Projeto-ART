#ifndef UnitOfMeasurementConverter_h
#define UnitOfMeasurementConverter_h

#include "Arduino.h"
#include "DebugManager.h"

class UnitOfMeasurementConverter
{
  public:
  
    UnitOfMeasurementConverter(DebugManager& debugManager);
	
	float																	convertFromCelsius(int unitOfMeasurementId, float celsius);
	float																	convertCelsiusToFahrenheit(float celsius);
										
  private:												
												
	DebugManager*          													_debugManager;	
	
};

#endif