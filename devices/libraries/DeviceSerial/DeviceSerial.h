#ifndef DeviceSerial_h
#define DeviceSerial_h

namespace ART
{
	class ESPDevice;

	class DeviceSerial
	{

	public:

		DeviceSerial(ESPDevice* espDevice);
		~DeviceSerial();

		void								begin();

	private:

		ESPDevice *							_espDevice;

		bool								onDeviceMQSubscription(const char* topicKey, const char* json);

	};
}

#endif