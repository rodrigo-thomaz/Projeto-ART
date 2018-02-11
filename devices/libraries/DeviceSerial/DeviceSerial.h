#ifndef DeviceSerial_h
#define DeviceSerial_h

#include "vector"
#include "tuple"

#include "DeviceSerialItem.h"

#define DEVICE_SERIAL_GET_ALL_BY_DEVICE_KEY_TOPIC_PUB   				"DeviceSerial/GetAllByDeviceKeyIoT" 
#define DEVICE_SERIAL_GET_ALL_BY_DEVICE_KEY_COMPLETED_TOPIC_SUB			"DeviceSerial/GetAllByDeviceKeyCompletedIoT"

#define DEVICE_SERIAL_SET_ENABLED_TOPIC_SUB								"DeviceSerial/SetEnabledIoT"
#define DEVICE_SERIAL_SET_PIN_TOPIC_SUB									"DeviceSerial/SetPinIoT"

namespace ART
{
	class ESPDevice;

	class DeviceSerial
	{

	public:

		DeviceSerial(ESPDevice* espDevice);
		~DeviceSerial();

		void										begin();

		std::tuple<DeviceSerialItem**, short>		getItems();

		bool										initialized();

	private:

		ESPDevice *									_espDevice;

		std::vector<DeviceSerialItem*>				_items;

		bool										_initialized;
		bool										_initializing;

		DeviceSerialItem*							getItemById(const char* id);

		void										getAllPub();
		void										getAllSub(const char* json);

		void										setEnabledSub(const char* json);
		void										setPinSub(const char* json);

		void										onDeviceMQSubscribeDeviceInApplication();
		void										onDeviceMQUnSubscribeDeviceInApplication();
		bool										onDeviceMQSubscription(const char* topicKey, const char* json);

	};
}

#endif