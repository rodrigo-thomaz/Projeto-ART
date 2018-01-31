#include "SensorInDevice.h"
#include "DeviceSensors.h"
#include "ESPDevice.h"

namespace ART
{
	SensorInDevice::SensorInDevice(DeviceSensors* deviceSensors, JsonObject& jsonObject)
	{
		_deviceSensors = deviceSensors;
		_deviceDebug = _deviceSensors->getESPDevice()->getDeviceDebug();

		_deviceDebug->println("SensorInDevice", "constructor", "begin");
		
		_ordination = jsonObject["ordination"];

		Sensor::create(_sensor, this, jsonObject["sensor"]);

		_deviceDebug->println("SensorInDevice", "constructor", "end");
	}

	SensorInDevice::~SensorInDevice()
	{
		_deviceDebug->println("SensorInDevice", "destructor");
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