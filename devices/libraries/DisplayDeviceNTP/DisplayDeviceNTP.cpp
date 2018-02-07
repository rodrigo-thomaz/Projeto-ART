#include "DisplayDeviceNTP.h"

using namespace ART;

DisplayDeviceNTP::DisplayDeviceNTP(DisplayDevice& displayDevice, ESPDevice& espDevice)
{
	this->_displayDevice = &displayDevice;
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
	this->_displayDevice->display.setFont();
	this->_displayDevice->display.setTextSize(2);
	this->_displayDevice->display.setTextColor(WHITE);
	this->_displayDevice->display.setCursor(0, 1);
	String formattedTime = _espDevice->getDeviceNTP()->getFormattedTime();
	this->_displayDevice->display.println(formattedTime);
}

void DisplayDeviceNTP::printUpdate(bool on)
{
	this->_displayDevice->display.setFont();
	this->_displayDevice->display.setTextSize(1);
	if (on)
		this->_displayDevice->display.setTextColor(BLACK, WHITE);
	else
		this->_displayDevice->display.setTextColor(WHITE, BLACK);

	this->_displayDevice->display.setCursor(66, 0);
	this->_displayDevice->display.println("T");
}
