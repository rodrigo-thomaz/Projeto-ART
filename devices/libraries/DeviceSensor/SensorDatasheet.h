#ifndef SensorDatasheet_h
#define SensorDatasheet_h

#include "../ArduinoJson/ArduinoJson.h"

#include "SensorTypeEnum.h"
#include "SensorDatasheetEnum.h"
#include "SensorDatasheetUnitMeasurementDefault.h"

namespace ART
{
	class DeviceSensor;

	class SensorDatasheet
	{

	public:

		SensorDatasheet(DeviceSensor* deviceSensor, JsonObject& jsonObject);
		~SensorDatasheet();

		SensorTypeEnum								getSensorTypeId();
		SensorDatasheetEnum							getSensorDatasheetId();

	private:

		DeviceSensor *								_deviceSensor;

		SensorTypeEnum								_sensorTypeId;
		SensorDatasheetEnum							_sensorDatasheetId;

		SensorDatasheetUnitMeasurementDefault *		_sensorDatasheetUnitMeasurementDefault;
	};
}

#endif