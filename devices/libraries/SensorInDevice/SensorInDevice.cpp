#include "SensorInDevice.h"
#include "DeviceSensors.h"

namespace ART
{
	SensorInDevice::SensorInDevice(DeviceSensors* deviceSensors, SensorDatasheet& sensorDatasheet, JsonObject& jsonObject)
	{
		Serial.println("[SensorInDevice constructor]");

		_deviceSensors = deviceSensors;
		_ordination = jsonObject["ordination"];

		Sensor::create(_sensor, this, sensorDatasheet, jsonObject["sensor"]);
	}

	SensorInDevice::~SensorInDevice()
	{
		Serial.println("[SensorInDevice destructor]");
	}

	Sensor * SensorInDevice::getSensor()
	{
		return _sensor;
	}

	DeviceSensors * SensorInDevice::getDeviceSensors()
	{
		return _deviceSensors;
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