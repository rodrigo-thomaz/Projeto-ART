#include "SensorDatasheetUnitMeasurementDefault.h"
#include "SensorDatasheet.h"

#include "Arduino.h"

namespace ART
{
	SensorDatasheetUnitMeasurementDefault::SensorDatasheetUnitMeasurementDefault(SensorDatasheet* sensorDatasheet, JsonObject& jsonObject)
	{
		Serial.println("[SensorDatasheetUnitMeasurementDefault constructor]");

		_sensorDatasheet = sensorDatasheet;

		_unitMeasurementId = static_cast<UnitMeasurementEnum>(jsonObject["unitMeasurementId"].as<short>());

		_max = float(jsonObject["max"]);
		_min = float(jsonObject["min"]);
	}

	SensorDatasheetUnitMeasurementDefault::~SensorDatasheetUnitMeasurementDefault()
	{
		Serial.println(F("[SensorDatasheetUnitMeasurementDefault destructor]"));
	}

	void SensorDatasheetUnitMeasurementDefault::create(SensorDatasheetUnitMeasurementDefault *(&sensorDatasheetUnitMeasurementDefault), SensorDatasheet * sensorDatasheet, JsonObject & jsonObject)
	{
		sensorDatasheetUnitMeasurementDefault = new SensorDatasheetUnitMeasurementDefault(sensorDatasheet, jsonObject);
	}

	UnitMeasurementEnum SensorDatasheetUnitMeasurementDefault::getUnitMeasurementId()
	{
		return _unitMeasurementId;
	}

	float SensorDatasheetUnitMeasurementDefault::getMax()
	{
		return _max;
	}

	float SensorDatasheetUnitMeasurementDefault::getMin()
	{
		return _min;
	}
}