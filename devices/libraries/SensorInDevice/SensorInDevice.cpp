#include "SensorInDevice.h"
#include "DeviceSensors.h"

namespace ART
{
	SensorInDevice::SensorInDevice(DeviceSensors* deviceSensors, JsonObject& jsonObject)
	{
		Serial.println("[SensorInDevice constructor]");

		_deviceSensors = deviceSensors;
		_ordination = jsonObject["ordination"];

		Sensor::create(_sensor, this, jsonObject["sensor"]);
	}

	SensorInDevice::~SensorInDevice()
	{
		Serial.println("[SensorInDevice destructor]");
	}

	SensorInDevice SensorInDevice::create(DeviceSensors * deviceSensors, JsonObject & jsonObject)
	{
		return SensorInDevice(deviceSensors, jsonObject);
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

	bool SensorInDevice::operator<(const SensorInDevice & val) const
	{
		Serial.println();
		Serial.print("[operator] current sensorId = ");
		Serial.print(_sensor->getSensorId());
		Serial.print(" ordination = ");
		Serial.println(_ordination);

		Serial.print("[operator] param sensorId = ");
		Serial.print(val._sensor->getSensorId());
		Serial.print(" ordination = ");
		Serial.println(val._ordination);

		bool result = _ordination < val._ordination;

		Serial.print("[operator] result = ");
		Serial.println(result);

		return result;
	}
}