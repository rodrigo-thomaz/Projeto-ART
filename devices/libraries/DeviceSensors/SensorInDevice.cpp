#include "SensorInDevice.h"

#include "DeviceSensors.h"
#include "../ESPDevice/ESPDevice.h"
#include "../DeviceDebug/DeviceDebug.h"

namespace ART
{
	SensorInDevice::SensorInDevice(DeviceSensors* deviceSensors, JsonObject& jsonObject)
	{
		_deviceSensors = deviceSensors;

		DeviceDebug* deviceDebug = _deviceSensors->getESPDevice()->getDeviceDebug();

		deviceDebug->printlnLevel(DeviceDebug::DEBUG, "SensorInDevice", "constructor", "begin");
		
		_ordination = jsonObject["ordination"];

		_sensor = new Sensor(this, jsonObject["sensor"]);

		deviceDebug->printlnLevel(DeviceDebug::DEBUG, "SensorInDevice", "constructor", "end");
	}

	SensorInDevice::~SensorInDevice()
	{
		_deviceSensors->getESPDevice()->getDeviceDebug()->printlnLevel(DeviceDebug::DEBUG, "SensorInDevice", "destructor");
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