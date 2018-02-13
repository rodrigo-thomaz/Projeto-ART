#include "SensorDatasheet.h"
#include "DeviceSensor.h"

namespace ART
{
	SensorDatasheet::SensorDatasheet(DeviceSensor* deviceSensor, JsonObject& jsonObject)
	{
		Serial.println(F("[SensorDatasheet constructor]"));

		_deviceSensor = deviceSensor;

		_sensorTypeId = static_cast<SensorTypeEnum>(jsonObject["sensorTypeId"].as<short>());
		_sensorDatasheetId = static_cast<SensorDatasheetEnum>(jsonObject["sensorDatasheetId"].as<short>());
				
		_sensorDatasheetUnitMeasurementDefault = new SensorDatasheetUnitMeasurementDefault(this, jsonObject["sensorDatasheetUnitMeasurementDefault"]);
	}

	SensorDatasheet::~SensorDatasheet()
	{
		Serial.println(F("[SensorDatasheet destructor]"));

		delete (_sensorDatasheetUnitMeasurementDefault);
	}

	SensorTypeEnum SensorDatasheet::getSensorTypeId()
	{
		return _sensorTypeId;
	}

	SensorDatasheetEnum SensorDatasheet::getSensorDatasheetId()
	{
		return _sensorDatasheetId;
	}
}