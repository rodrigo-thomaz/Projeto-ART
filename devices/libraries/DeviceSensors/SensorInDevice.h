#ifndef SensorInDevice_h
#define SensorInDevice_h

#include "../ArduinoJson/ArduinoJson.h"
#include "Sensor.h"
#include "SensorDatasheet.h"

namespace ART
{
	class DeviceSensors;

	class SensorInDevice
	{

	public:
		SensorInDevice(DeviceSensors* deviceSensors, JsonObject& jsonObject);
		~SensorInDevice();

		Sensor*								getSensor();
		DeviceSensors*						getDeviceSensors();

		short								getOrdination();
		void								setOrdination(short value);		

		bool								operator<(const SensorInDevice& val) const;

	private:

		DeviceSensors *						_deviceSensors;
		Sensor *							_sensor;		

		short 								_ordination;

	};
}

#endif