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

		static void							create(SensorDatasheetUnitMeasurementDefault* (&sensorDatasheetUnitMeasurementDefault), SensorDatasheet* sensorDatasheet, JsonObject& jsonObject);

		UnitMeasurementEnum					getUnitMeasurementId();

		float 								getMax();
		float 								getMin();		

	private:

		SensorDatasheet *					_sensorDatasheet;

		UnitMeasurementEnum					_unitMeasurementId;

		float 								_max;
		float 								_min;

	};
}

#endif