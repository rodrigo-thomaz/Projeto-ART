#ifndef SensorDatasheet_h
#define SensorDatasheet_h

#include "Arduino.h"
#include "ArduinoJson.h"

#include "SensorTypeEnum.h"
#include "SensorDatasheetEnum.h"

namespace ART
{
	class SensorDatasheetManager;

	class SensorDatasheet
	{

	public:

		SensorDatasheet(SensorDatasheetManager* sensorDatasheetManager, JsonObject& jsonObject);
		~SensorDatasheet();

		static void create(SensorDatasheet* (&sensorDatasheet), SensorDatasheetManager* sensorDatasheetManager, JsonObject& jsonObject)
		{
			sensorDatasheet = new SensorDatasheet(sensorDatasheetManager, jsonObject);
		}

		SensorTypeEnum						getSensorTypeId();
		SensorDatasheetEnum					getSensorDatasheetId();

	private:

		SensorDatasheetManager *			_sensorDatasheetManager;

		SensorTypeEnum						_sensorTypeId;
		SensorDatasheetEnum					_sensorDatasheetId;
	};
}

#endif