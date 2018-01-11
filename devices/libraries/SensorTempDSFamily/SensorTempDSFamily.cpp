#include "SensorTempDSFamily.h"
#include "Sensor.h"

namespace ART
{	
	SensorTempDSFamily::SensorTempDSFamily(Sensor* sensor, JsonObject& jsonObject)
	{
		Serial.println("[SensorTempDSFamily constructor]");

		_sensor = sensor;
	}

	SensorTempDSFamily::~SensorTempDSFamily()
	{
		Serial.println("[SensorTempDSFamily destructor]");
	}	
}