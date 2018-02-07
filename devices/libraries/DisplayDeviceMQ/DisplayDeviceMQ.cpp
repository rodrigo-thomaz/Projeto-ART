#include "DisplayDeviceMQ.h"

namespace ART
{
	DisplayDeviceMQ::DisplayDeviceMQ(DisplayDevice& displayDevice)
	{
		this->_displayDevice = &displayDevice;

		this->_x = 80;
		this->_y = 0;
	}

	DisplayDeviceMQ::~DisplayDeviceMQ()
	{
	}

	void DisplayDeviceMQ::printConnected()
	{
		int y = this->_y + 9;

		this->_displayDevice->display.setTextSize(1);
		this->_displayDevice->display.setTextColor(WHITE, BLACK);
		this->_displayDevice->display.setCursor(this->_x, y);
		this->_displayDevice->display.println("ART");
	}

	void DisplayDeviceMQ::printSent(bool on)
	{
		if (on)
			this->_displayDevice->display.setTextColor(BLACK, WHITE);
		else
			this->_displayDevice->display.setTextColor(WHITE, BLACK);

		this->_displayDevice->display.setTextSize(1);
		this->_displayDevice->display.setCursor(this->_x, this->_y);
		this->_displayDevice->display.write(24); // ↑
	}

	void DisplayDeviceMQ::printReceived(bool on)
	{
		int x = this->_x + 12;

		if (on)
			this->_displayDevice->display.setTextColor(BLACK, WHITE);
		else
			this->_displayDevice->display.setTextColor(WHITE, BLACK);

		this->_displayDevice->display.setTextSize(1);
		this->_displayDevice->display.setCursor(x, this->_y);
		this->_displayDevice->display.write(25); // ↓
	}
}