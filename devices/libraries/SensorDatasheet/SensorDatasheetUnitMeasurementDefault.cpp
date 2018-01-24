#include "SensorDatasheetUnitMeasurementDefault.h"
#include "SensorDatasheet.h"

namespace ART
{
	SensorDatasheetUnitMeasurementDefault::SensorDatasheetUnitMeasurementDefault(SensorDatasheet* sensorDatasheet, JsonObject& jsonObject)
	{
		Serial.println("[SensorDatasheetUnitMeasurementDefault constructor]");

		_sensorDatasheet = sensorDatasheet;
	}

	SensorDatasheetUnitMeasurementDefault::~SensorDatasheetUnitMeasurementDefault()
	{
		Serial.println("[SensorDatasheetUnitMeasurementDefault destructor]");
	}
}