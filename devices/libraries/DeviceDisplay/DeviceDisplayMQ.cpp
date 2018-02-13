#include "DeviceDisplayMQ.h"

#include "../DeviceDisplay/DeviceDisplay.h"
#include "../ESPDevice/ESPDevice.h"
#include "../DeviceMQ/DeviceMQ.h"

namespace ART
{
	DeviceDisplayMQ::DeviceDisplayMQ(DeviceDisplay* deviceDisplay)
	{
		_deviceDisplay = deviceDisplay;

		_x = 80;
		_y = 0;		
	}

	DeviceDisplayMQ::~DeviceDisplayMQ()
	{
	}

	void DeviceDisplayMQ::create(DeviceDisplayMQ *(&deviceDisplayMQ), DeviceDisplay * deviceDisplay)
	{
		deviceDisplayMQ = new DeviceDisplayMQ(deviceDisplay);
	}

	void DeviceDisplayMQ::begin()
	{
		_deviceDisplay->getESPDevice()->getDeviceMQ()->addSubscriptionCallback([=] (const char* topicKey, const char* json) { return onDeviceMQSubscription(topicKey, json); });
	}

	void DeviceDisplayMQ::printConnected()
	{
		int y = _y + 9;

		_deviceDisplay->display.setTextSize(1);
		_deviceDisplay->display.setTextColor(WHITE, BLACK);
		_deviceDisplay->display.setCursor(_x, y);
		_deviceDisplay->display.println("ART");
	}

	void DeviceDisplayMQ::printSent(bool on)
	{
		if (on)
			_deviceDisplay->display.setTextColor(BLACK, WHITE);
		else
			_deviceDisplay->display.setTextColor(WHITE, BLACK);

		_deviceDisplay->display.setTextSize(1);
		_deviceDisplay->display.setCursor(_x, _y);
		_deviceDisplay->display.write(24); // ↑
	}

	void DeviceDisplayMQ::printReceived(bool on)
	{
		int x = _x + 12;

		if (on)
			_deviceDisplay->display.setTextColor(BLACK, WHITE);
		else
			_deviceDisplay->display.setTextColor(WHITE, BLACK);

		_deviceDisplay->display.setTextSize(1);
		_deviceDisplay->display.setCursor(x, _y);
		_deviceDisplay->display.write(25); // ↓
	}

	bool DeviceDisplayMQ::onDeviceMQSubscription(const char * topicKey, const char * json)
	{
		Serial.println(F("[DeviceDisplayMQ::onDeviceMQSubscription] printReceived(true)"));
		printReceived(true);
		return false;
	}
}