#ifndef SensorDatasheet_h
#define SensorDatasheet_h

#include "Arduino.h"
#include "ArduinoJson.h"

#include "SensorTypeEnum.h"
#include "SensorDatasheetEnum.h"

namespace ART
{
	class DeviceSensors;

	class SensorDatasheet
	{

	public:

		SensorDatasheet(DeviceSensors* deviceSensors, JsonObject& jsonObject);
		~SensorDatasheet();

		static void create(SensorDatasheet* (&sensorDatasheet), DeviceSensors* deviceSensors, JsonObject& jsonObject)
		{
			sensorDatasheet = new SensorDatasheet(deviceSensors, jsonObject);
		}

		SensorTypeEnum						getSensorTypeId();
		SensorDatasheetEnum					getSensorDatasheetId();

	private:

		DeviceSensors * _deviceSensors;

		SensorTypeEnum						_sensorTypeId;
		SensorDatasheetEnum					_sensorDatasheetId;
	};
}

#endif