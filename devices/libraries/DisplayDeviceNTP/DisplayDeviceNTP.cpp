#include "DisplayDeviceNTP.h"

using namespace ART;

DisplayDeviceNTP::DisplayDeviceNTP(DisplayManager& displayManager, ESPDevice& espDevice)
{
	this->_displayManager = &displayManager;
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
	this->_displayManager->display.setFont();
	this->_displayManager->display.setTextSize(2);
	this->_displayManager->display.setTextColor(WHITE);
	this->_displayManager->display.setCursor(0, 1);
	String formattedTime = _espDevice->getDeviceNTP()->getFormattedTime();
	this->_displayManager->display.println(formattedTime);
}

void DisplayDeviceNTP::printUpdate(bool on)
{
	this->_displayManager->display.setFont();
	this->_displayManager->display.setTextSize(1);
	if (on)
		this->_displayManager->display.setTextColor(BLACK, WHITE);
	else
		this->_displayManager->display.setTextColor(WHITE, BLACK);

	this->_displayManager->display.setCursor(66, 0);
	this->_displayManager->display.println("T");
}
