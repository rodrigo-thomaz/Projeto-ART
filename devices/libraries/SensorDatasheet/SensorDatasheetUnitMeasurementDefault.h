#ifndef SensorDatasheetUnitMeasurementDefault_h
#define SensorDatasheetUnitMeasurementDefault_h

#include "Arduino.h"
#include "ArduinoJson.h"

namespace ART
{
	class SensorDatasheet;

	class SensorDatasheetUnitMeasurementDefault
	{

	public:

		SensorDatasheetUnitMeasurementDefault(SensorDatasheet* sensorDatasheet, JsonObject& jsonObject);
		~SensorDatasheetUnitMeasurementDefault();

		static void create(SensorDatasheetUnitMeasurementDefault* (&sensorDatasheetUnitMeasurementDefault), SensorDatasheet* sensorDatasheet, JsonObject& jsonObject)
		{
			sensorDatasheetUnitMeasurementDefault = new SensorDatasheetUnitMeasurementDefault(sensorDatasheet, jsonObject);
		}

	private:

		SensorDatasheet * _sensorDatasheet;

	};
}

#endif