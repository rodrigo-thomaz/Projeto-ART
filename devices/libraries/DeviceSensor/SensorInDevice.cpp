#include "SensorInDevice.h"

#include "DeviceSensor.h"
#include "../ESPDevice/ESPDevice.h"
#include "../DeviceDebug/DeviceDebug.h"

namespace ART
{
	SensorInDevice::SensorInDevice(DeviceSensor* deviceSensor, JsonObject& jsonObject)
	{
		_deviceSensor = deviceSensor;

		DeviceDebug* deviceDebug = _deviceSensor->getESPDevice()->getDeviceDebug();

		deviceDebug->printlnLevel(DeviceDebug::DEBUG, "SensorInDevice", "constructor", "begin");
		
		_ordination = jsonObject["ordination"];

		_sensor = new Sensor(this, jsonObject["sensor"]);

		deviceDebug->printlnLevel(DeviceDebug::DEBUG, "SensorInDevice", "constructor", "end");
	}

	SensorInDevice::~SensorInDevice()
	{
		_deviceSensor->getESPDevice()->getDeviceDebug()->printlnLevel(DeviceDebug::DEBUG, "SensorInDevice", "destructor");
	}

	Sensor * SensorInDevice::getSensor()
	{
		return _sensor;
	}

	DeviceSensor * SensorInDevice::getDeviceSensor()
	{
		return _deviceSensor;
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