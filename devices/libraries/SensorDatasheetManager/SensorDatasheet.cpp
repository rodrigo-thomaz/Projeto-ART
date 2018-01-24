#include "SensorDatasheet.h"
#include "SensorDatasheetManager.h"

namespace ART
{
	SensorDatasheet::SensorDatasheet(SensorDatasheetManager* sensorDatasheetManager, JsonObject& jsonObject)
	{
		Serial.println("[SensorDatasheet constructor]");

		_sensorDatasheetManager = sensorDatasheetManager;
			
		_sensorTypeId = static_cast<SensorTypeEnum>(jsonObject["sensorTypeId"].as<short>());
		_sensorDatasheetId = static_cast<SensorDatasheetEnum>(jsonObject["sensorDatasheetId"].as<short>());
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