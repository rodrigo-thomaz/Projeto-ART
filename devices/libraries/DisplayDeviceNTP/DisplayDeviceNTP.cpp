#include "DisplayDeviceNTP.h"

namespace ART
{
	DisplayDeviceNTP::DisplayDeviceNTP(ESPDevice& espDevice)
	{
		this->_espDevice = &espDevice;

		this->_updateCallback = [=](bool update, bool forceUpdate) { this->updateCallback(update, forceUpdate); };
		_espDevice->getDeviceNTP()->setUpdateCallback(this->_updateCallback);
	}

	DisplayDeviceNTP::~DisplayDeviceNTP()
	{

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
		_espDevice->getDisplayDevice()->display.setFont();
		_espDevice->getDisplayDevice()->display.setTextSize(2);
		_espDevice->getDisplayDevice()->display.setTextColor(WHITE);
		_espDevice->getDisplayDevice()->display.setCursor(0, 1);
		String formattedTime = _espDevice->getDeviceNTP()->getFormattedTime();
		_espDevice->getDisplayDevice()->display.println(formattedTime);
	}

	void DisplayDeviceNTP::printUpdate(bool on)
	{
		_espDevice->getDisplayDevice()->display.setFont();
		_espDevice->getDisplayDevice()->display.setTextSize(1);
		if (on)
			_espDevice->getDisplayDevice()->display.setTextColor(BLACK, WHITE);
		else
			_espDevice->getDisplayDevice()->display.setTextColor(WHITE, BLACK);

		_espDevice->getDisplayDevice()->display.setCursor(66, 0);
		_espDevice->getDisplayDevice()->display.println("T");
	}
}