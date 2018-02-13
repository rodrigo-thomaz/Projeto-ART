#include "DeviceDisplayNTP.h"
#include "DeviceDisplay.h"
#include "ESPDevice.h"
#include "DeviceNTP.h"

namespace ART
{
	DeviceDisplayNTP::DeviceDisplayNTP(DeviceDisplay* deviceDisplay)
	{
		_deviceDisplay = deviceDisplay;

		this->_updateCallback = [=](bool update, bool forceUpdate) { this->updateCallback(update, forceUpdate); };		
		_deviceDisplay->getESPDevice()->getDeviceNTP()->setUpdateCallback(this->_updateCallback);
	}

	DeviceDisplayNTP::~DeviceDisplayNTP()
	{

	}

	void DeviceDisplayNTP::create(DeviceDisplayNTP *(&deviceDisplayNTP), DeviceDisplay * deviceDisplay)
	{
		deviceDisplayNTP = new DeviceDisplayNTP(deviceDisplay);
	}

	void DeviceDisplayNTP::updateCallback(bool update, bool forceUpdate)
	{
		if (update) {
			this->printTime();
			this->printUpdate(forceUpdate);
		}
	}

	void DeviceDisplayNTP::printTime()
	{
		_deviceDisplay->display.setFont();
		_deviceDisplay->display.setTextSize(2);
		_deviceDisplay->display.setTextColor(WHITE);
		_deviceDisplay->display.setCursor(0, 1);
		String formattedTime = _deviceDisplay->getESPDevice()->getDeviceNTP()->getFormattedTime();
		_deviceDisplay->display.println(formattedTime);
	}

	void DeviceDisplayNTP::printUpdate(bool on)
	{
		_deviceDisplay->display.setFont();
		_deviceDisplay->display.setTextSize(1);
		if (on)
			_deviceDisplay->display.setTextColor(BLACK, WHITE);
		else
			_deviceDisplay->display.setTextColor(WHITE, BLACK);

		_deviceDisplay->display.setCursor(66, 0);
		_deviceDisplay->display.println("T");
	}
}