#include "DisplayDeviceMQ.h"
#include "DisplayDevice.h"

namespace ART
{
	DisplayDeviceMQ::DisplayDeviceMQ(DisplayDevice* displayDevice)
	{
		_displayDevice = displayDevice;

		_x = 80;
		_y = 0;
	}

	DisplayDeviceMQ::~DisplayDeviceMQ()
	{
	}

	void DisplayDeviceMQ::create(DisplayDeviceMQ *(&displayDeviceMQ), DisplayDevice * displayDevice)
	{
		displayDeviceMQ = new DisplayDeviceMQ(displayDevice);
	}

	void DisplayDeviceMQ::printConnected()
	{
		int y = _y + 9;

		_displayDevice->display.setTextSize(1);
		_displayDevice->display.setTextColor(WHITE, BLACK);
		_displayDevice->display.setCursor(_x, y);
		_displayDevice->display.println("ART");
	}

	void DisplayDeviceMQ::printSent(bool on)
	{
		if (on)
			_displayDevice->display.setTextColor(BLACK, WHITE);
		else
			_displayDevice->display.setTextColor(WHITE, BLACK);

		_displayDevice->display.setTextSize(1);
		_displayDevice->display.setCursor(_x, _y);
		_displayDevice->display.write(24); // ↑
	}

	void DisplayDeviceMQ::printReceived(bool on)
	{
		int x = _x + 12;

		if (on)
			_displayDevice->display.setTextColor(BLACK, WHITE);
		else
			_displayDevice->display.setTextColor(WHITE, BLACK);

		_displayDevice->display.setTextSize(1);
		_displayDevice->display.setCursor(x, _y);
		_displayDevice->display.write(25); // ↓
	}
}