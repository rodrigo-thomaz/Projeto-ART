#ifndef SensorInDevice_h
#define SensorInDevice_h

#include "../ArduinoJson/ArduinoJson.h"

#include "Sensor.h"
#include "SensorDatasheet.h"

namespace ART
{
	class DeviceSensor;

	class SensorInDevice
	{

	public:
		SensorInDevice(DeviceSensor* deviceSensor, JsonObject& jsonObject);
		~SensorInDevice();

		Sensor*								getSensor();
		DeviceSensor*						getDeviceSensor();

		short								getOrdination();

	private:

		DeviceSensor *						_deviceSensor;
		Sensor *							_sensor;		

		short 								_ordination;

		void								setOrdination(short value);

		friend class						DeviceSensor;

	};
}

#endif