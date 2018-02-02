#ifndef SensorDatasheet_h
#define SensorDatasheet_h

#include "Arduino.h"
#include "ArduinoJson.h"

#include "SensorTypeEnum.h"
#include "SensorDatasheetEnum.h"
#include "SensorDatasheetUnitMeasurementDefault.h"

namespace ART
{
	class DeviceSensors;

	class SensorDatasheet
	{

	public:

		SensorDatasheet(DeviceSensors* deviceSensors, JsonObject& jsonObject);
		~SensorDatasheet();

		static SensorDatasheet						create(DeviceSensors* deviceSensors, JsonObject& jsonObject);

		SensorTypeEnum								getSensorTypeId();
		SensorDatasheetEnum							getSensorDatasheetId();

	private:

		DeviceSensors *								_deviceSensors;

		SensorTypeEnum								_sensorTypeId;
		SensorDatasheetEnum							_sensorDatasheetId;

		SensorDatasheetUnitMeasurementDefault *		_sensorDatasheetUnitMeasurementDefault;
	};
}

#endif