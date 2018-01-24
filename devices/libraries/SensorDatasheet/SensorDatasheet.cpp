#include "SensorDatasheet.h"
#include "DeviceSensors.h"

namespace ART
{
	SensorDatasheet::SensorDatasheet(DeviceSensors* deviceSensors, JsonObject& jsonObject)
	{
		Serial.println("[SensorDatasheet constructor]");

		_deviceSensors = deviceSensors;

		_sensorTypeId = static_cast<SensorTypeEnum>(jsonObject["sensorTypeId"].as<short>());
		_sensorDatasheetId = static_cast<SensorDatasheetEnum>(jsonObject["sensorDatasheetId"].as<short>());

		SensorDatasheetUnitMeasurementDefault::create(_sensorDatasheetUnitMeasurementDefault, this, jsonObject["sensorDatasheetUnitMeasurementDefault"]);
	}

	SensorDatasheet::~SensorDatasheet()
	{
		Serial.println("[SensorDatasheet destructor]");
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