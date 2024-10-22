#include "SensorTempDSFamily.h"
#include "Sensor.h"

namespace ART
{	
	SensorTempDSFamily::SensorTempDSFamily(Sensor* sensor, JsonObject& jsonObject)
	{
		Serial.println(F("[SensorTempDSFamily constructor]"));

		_sensor = sensor;

		_resolution = int(jsonObject["resolutionBits"]);
	}

	SensorTempDSFamily::~SensorTempDSFamily()
	{
		Serial.println(F("[SensorTempDSFamily destructor]"));
	}

	int SensorTempDSFamily::getResolution()
	{
		return _resolution;
	}

	void SensorTempDSFamily::setResolution(int value)
	{
		_resolution = value;
	}
}