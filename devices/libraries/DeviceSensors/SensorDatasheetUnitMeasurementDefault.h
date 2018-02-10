#ifndef SensorDatasheetUnitMeasurementDefault_h
#define SensorDatasheetUnitMeasurementDefault_h

#include "../ArduinoJson/ArduinoJson.h"

#include "../UnitMeasurement/UnitMeasurementEnum.h"

namespace ART
{
	class SensorDatasheet;

	class SensorDatasheetUnitMeasurementDefault
	{

	public:

		SensorDatasheetUnitMeasurementDefault(SensorDatasheet* sensorDatasheet, JsonObject& jsonObject);
		~SensorDatasheetUnitMeasurementDefault();

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