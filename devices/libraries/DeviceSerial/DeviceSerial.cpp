#include "DeviceSerial.h"
#include "../ESPDevice/ESPDevice.h"

namespace ART
{
	DeviceSerial::DeviceSerial(ESPDevice* espDevice)
	{
		_espDevice = espDevice;
	}

	DeviceSerial::~DeviceSerial()
	{

	}	

	void DeviceSerial::begin()
	{

	}

	bool DeviceSerial::onDeviceMQSubscription(const char* topicKey, const char* json)
	{
		return false;
	}
}