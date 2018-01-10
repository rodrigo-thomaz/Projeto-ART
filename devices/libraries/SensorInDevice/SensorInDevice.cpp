#include "SensorInDevice.h"
#include "DeviceSensors.h"

namespace ART
{
	SensorInDevice::SensorInDevice(DeviceSensors* deviceSensors)
	{
		_deviceSensors = deviceSensors;
	}

	SensorInDevice::~SensorInDevice()
	{
		delete (_deviceSensors);
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