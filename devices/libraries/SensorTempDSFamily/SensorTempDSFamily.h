#ifndef SensorTempDSFamily_h
#define SensorTempDSFamily_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "DallasTemperature.h"

#define SENSOR_TEMP_DS_FAMILY_SET_RESOLUTION_TOPIC_SUB "SensorTempDSFamily/SetResolutionIoT"

namespace ART
{
	class Sensor;

	class SensorTempDSFamily
	{

	public:
		SensorTempDSFamily(Sensor* sensor, JsonObject& jsonObject);
		~SensorTempDSFamily();

		static void							create(SensorTempDSFamily* (&sensorTempDSFamily), Sensor* sensor, JsonObject& jsonObject);

		int 								getResolution();
		void 								setResolution(int value);		

		static String getFamily(byte deviceAddress[8])
		{
			switch (deviceAddress[0]) {
			case DS18S20MODEL:
				return "DS18S20";
			case DS18B20MODEL:
				return "DS18B20";
			case DS1822MODEL:
				return "DS1822";
			case DS1825MODEL:
				return "DS1825";
			case DS28EA00MODEL:
				return "DS28EA00";
			default:
				return "";
			}
		}

	private:

		Sensor *							_sensor;

		int 								_resolution;

	};
}

#endif