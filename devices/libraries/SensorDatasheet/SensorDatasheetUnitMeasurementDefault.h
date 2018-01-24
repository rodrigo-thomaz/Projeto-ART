#ifndef SensorDatasheetUnitMeasurementDefault_h
#define SensorDatasheetUnitMeasurementDefault_h

#include "Arduino.h"
#include "ArduinoJson.h"

#include "UnitMeasurementEnum.h"

namespace ART
{
	class SensorDatasheet;

	class SensorDatasheetUnitMeasurementDefault
	{

	public:

		SensorDatasheetUnitMeasurementDefault(SensorDatasheet* sensorDatasheet, JsonObject& jsonObject);
		~SensorDatasheetUnitMeasurementDefault();

		UnitMeasurementEnum					getUnitMeasurementId();

		float 								getMax();
		float 								getMin();

		static void create(SensorDatasheetUnitMeasurementDefault* (&sensorDatasheetUnitMeasurementDefault), SensorDatasheet* sensorDatasheet, JsonObject& jsonObject)
		{
			sensorDatasheetUnitMeasurementDefault = new SensorDatasheetUnitMeasurementDefault(sensorDatasheet, jsonObject);
		}

	private:

		SensorDatasheet *					_sensorDatasheet;

		UnitMeasurementEnum					_unitMeasurementId;

		float 								_max;
		float 								_min;

	};
}

#endif