#ifndef SensorInDevice_h
#define SensorInDevice_h

#include "ArduinoJson.h"

namespace ART
{
	class DeviceSensors;

	class SensorInDevice
	{

	public:
		SensorInDevice(DeviceSensors* deviceSensors, short ordination);
		~SensorInDevice();

		short								getOrdination();
		void								setOrdination(short value);

		static SensorInDevice create(DeviceSensors* deviceSensors, JsonObject& jsonObject)
		{
			return SensorInDevice(
				deviceSensors,
				jsonObject["ordination"]);
		}

	private:

		DeviceSensors *						_deviceSensors;

		short 								_ordination;
	};
}

#endif