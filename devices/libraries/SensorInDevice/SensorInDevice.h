#ifndef SensorInDevice_h
#define SensorInDevice_h

#include "ArduinoJson.h"
#include "Sensor.h"
#include "SensorDatasheet.h"

#define SENSOR_IN_DEVICE_SET_ORDINATION_TOPIC_SUB "SensorInDevice/SetOrdinationIoT"

namespace ART
{
	class DeviceSensors;

	class SensorInDevice
	{

	public:
		SensorInDevice(DeviceSensors* deviceSensors, JsonObject& jsonObject);
		~SensorInDevice();

		static SensorInDevice				create(DeviceSensors* deviceSensors, JsonObject& jsonObject);

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