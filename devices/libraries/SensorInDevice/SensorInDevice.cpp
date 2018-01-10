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
}