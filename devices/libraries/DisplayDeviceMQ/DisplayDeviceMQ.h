#ifndef DisplayDeviceMQ_h
#define DisplayDeviceMQ_h

#include "Arduino.h"

namespace ART
{
	class DisplayDevice;

	class DisplayDeviceMQ
	{
	public:
		DisplayDeviceMQ(DisplayDevice* displayDevice);
		~DisplayDeviceMQ();

		static void				create(DisplayDeviceMQ* (&displayDeviceMQ), DisplayDevice* displayDevice);

		void					begin();

		void					printConnected();
		void					printSent(bool on);
		void					printReceived(bool on);

	private:

		DisplayDevice *			_displayDevice;

		int			         	_x;
		int			         	_y;

		bool					onDeviceMQSubscription(char* topicKey, char* json);

	};
}

#endif