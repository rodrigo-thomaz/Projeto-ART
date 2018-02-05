#ifndef DeviceInApplication_h
#define DeviceInApplication_h

#include "Arduino.h"
#include "ArduinoJson.h"

#define DEVICE_IN_APPLICATION_INSERT_TOPIC_SUB "ESPDevice/InsertInApplicationIoT"
#define DEVICE_IN_APPLICATION_REMOVE_TOPIC_SUB "ESPDevice/DeleteFromApplicationIoT"

namespace ART
{
	class ESPDevice;

	class DeviceInApplication
	{

	public:

		DeviceInApplication(ESPDevice* espDevice);
		~DeviceInApplication();

		static void							create(DeviceInApplication* (&deviceInApplication), ESPDevice* espDevice);

		void								load(JsonObject& jsonObject);

		void								insert(char* json);
		void								remove();

		char*								getApplicationId() const;
		void								setApplicationId(char* value);

		char*								getApplicationTopic()  const;
		void								setApplicationTopic(char* value);

	private:

		ESPDevice * _espDevice;

		char*								_applicationId;
		char*								_applicationTopic;

		void								onDeviceMQSubscribeNotInApplication();
		void								onDeviceMQSubscribeInApplication();
		void								onDeviceMQUnSubscribeNotInApplication();
		void								onDeviceMQUnSubscribeInApplication();
		void								onDeviceMQSubscription(char* topicKey, char* json);

	};
}

#endif