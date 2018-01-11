#include "SensorInDevice.h"
#include "DeviceSensors.h"

namespace ART
{
	SensorInDevice::SensorInDevice(DeviceSensors* deviceSensors, JsonObject& jsonObject)
	{
		Serial.println("[SensorInDevice constructor]");

		_deviceSensors = deviceSensors;
		_ordination = jsonObject["ordination"];
	}

	SensorInDevice::~SensorInDevice()
	{
		Serial.println("[SensorInDevice destructor]");
	}

	short SensorInDevice::getOrdination()
	{
		return _ordination;
	}

	void SensorInDevice::setOrdination(short value)
	{
		_ordination = value;
	}	
}