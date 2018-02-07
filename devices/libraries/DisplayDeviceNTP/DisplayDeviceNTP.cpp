#include "DisplayDeviceNTP.h"
#include "DisplayDevice.h"
#include "ESPDevice.h"
#include "DeviceNTP.h"

namespace ART
{
	DisplayDeviceNTP::DisplayDeviceNTP(DisplayDevice* displayDevice)
	{
		_displayDevice = displayDevice;

		this->_updateCallback = [=](bool update, bool forceUpdate) { this->updateCallback(update, forceUpdate); };		
		_displayDevice->getESPDevice()->getDeviceNTP()->setUpdateCallback(this->_updateCallback);
	}

	DisplayDeviceNTP::~DisplayDeviceNTP()
	{

	}

	void DisplayDeviceNTP::create(DisplayDeviceNTP *(&displayDeviceNTP), DisplayDevice * displayDevice)
	{
		displayDeviceNTP = new DisplayDeviceNTP(displayDevice);
	}

	void DisplayDeviceNTP::updateCallback(bool update, bool forceUpdate)
	{
		if (update) {
			this->printTime();
			this->printUpdate(forceUpdate);
		}
	}

	void DisplayDeviceNTP::printTime()
	{
		_displayDevice->display.setFont();
		_displayDevice->display.setTextSize(2);
		_displayDevice->display.setTextColor(WHITE);
		_displayDevice->display.setCursor(0, 1);
		String formattedTime = _displayDevice->getESPDevice()->getDeviceNTP()->getFormattedTime();
		_displayDevice->display.println(formattedTime);
	}

	void DisplayDeviceNTP::printUpdate(bool on)
	{
		_displayDevice->display.setFont();
		_displayDevice->display.setTextSize(1);
		if (on)
			_displayDevice->display.setTextColor(BLACK, WHITE);
		else
			_displayDevice->display.setTextColor(WHITE, BLACK);

		_displayDevice->display.setCursor(66, 0);
		_displayDevice->display.println("T");
	}
}