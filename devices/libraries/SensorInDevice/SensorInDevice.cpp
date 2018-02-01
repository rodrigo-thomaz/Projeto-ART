#include "SensorInDevice.h"
#include "DeviceSensors.h"
#include "ESPDevice.h"
#include "DeviceDebug.h"

namespace ART
{
	SensorInDevice::SensorInDevice(DeviceSensors* deviceSensors, JsonObject& jsonObject)
	{
		_deviceSensors = deviceSensors;
		_deviceDebug = _deviceSensors->getESPDevice()->getDeviceDebug();

		_deviceDebug->printlnLevel(DeviceDebug::DEBUG, "SensorInDevice", "constructor", "begin");
		
		_ordination = jsonObject["ordination"];

		Sensor::create(_sensor, this, jsonObject["sensor"]);

		_deviceDebug->printlnLevel(DeviceDebug::DEBUG, "SensorInDevice", "constructor", "end");
	}

	SensorInDevice::~SensorInDevice()
	{
		_deviceDebug->printlnLevel(DeviceDebug::DEBUG, "SensorInDevice", "destructor");
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
		bool result = _ordination < val._ordination;

		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) {

			_deviceDebug->printf("SensorInDevice", "operator", "current sensorId  : %s\n", _sensor->getSensorId());
			_deviceDebug->printf("SensorInDevice", "operator", "current ordination: %d\n", (char*)_ordination);
			_deviceDebug->printf("SensorInDevice", "operator", "param   sensorId  : %s\n", val._sensor->getSensorId());
			_deviceDebug->printf("SensorInDevice", "operator", "param   ordination: %d\n", (char*)val._ordination);

			yield();

			/*Serial.print("[operator] current sensorId = ");
			Serial.print(_sensor->getSensorId());
			Serial.print(" ordination = ");
			Serial.println(_ordination);*/

			/*Serial.print("[operator] param sensorId = ");
			Serial.print(val._sensor->getSensorId());
			Serial.print(" ordination = ");
			Serial.println(val._ordination);*/

			/*Serial.print("[operator] result = ");
			Serial.println(result);*/
		}

		return result;
	}
}