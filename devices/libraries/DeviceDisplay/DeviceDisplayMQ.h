#ifndef DeviceDisplayMQ_h
#define DeviceDisplayMQ_h

#include "Arduino.h"

namespace ART
{
	class DeviceDisplay;

	class DeviceDisplayMQ
	{
	public:
		DeviceDisplayMQ(DeviceDisplay* deviceDisplay);
		~DeviceDisplayMQ();

		static void				create(DeviceDisplayMQ* (&deviceDisplayMQ), DeviceDisplay* deviceDisplay);

		void					begin();

		void					printConnected();
		void					printSent(bool on);
		void					printReceived(bool on);

	private:

		DeviceDisplay *			_deviceDisplay;

		int			         	_x;
		int			         	_y;

		bool					onDeviceMQSubscription(const char* topicKey, const char* json);

	};
}

#endif