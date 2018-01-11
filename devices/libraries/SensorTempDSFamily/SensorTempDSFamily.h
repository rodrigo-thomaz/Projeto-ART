#ifndef SensorTempDSFamily_h
#define SensorTempDSFamily_h

#include "ArduinoJson.h"

namespace ART
{
	class Sensor;

	class SensorTempDSFamily
	{

	public:
		SensorTempDSFamily(Sensor* sensor, JsonObject& jsonObject);
		~SensorTempDSFamily();

		static void create(SensorTempDSFamily* (&sensorTempDSFamily), Sensor* sensor, JsonObject& jsonObject)
		{
			sensorTempDSFamily = new SensorTempDSFamily(sensor, jsonObject);
		}

	private:

		Sensor *							_sensor;

	};
}

#endif