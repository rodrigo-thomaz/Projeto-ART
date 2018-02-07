#ifndef DisplayDeviceMQ_h
#define DisplayDeviceMQ_h

#include "Arduino.h"
#include "DisplayDevice.h"

namespace ART
{
	class DisplayDeviceMQ
	{
	public:
		DisplayDeviceMQ(DisplayDevice& displayDevice);
		~DisplayDeviceMQ();

		void					printConnected();
		void					printSent(bool on);
		void					printReceived(bool on);

	private:

		DisplayDevice * _displayDevice;

		int			         	_x;
		int			         	_y;

	};
}

#endif