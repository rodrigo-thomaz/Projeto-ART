#ifndef DeviceInApplication_h
#define DeviceInApplication_h

#include "functional"
#include "vector"
#include "Arduino.h"
#include "ArduinoJson.h"

#define DEVICE_IN_APPLICATION_INSERT_TOPIC_SUB "DeviceInApplication/InsertIoT"
#define DEVICE_IN_APPLICATION_REMOVE_TOPIC_SUB "DeviceInApplication/RemoveIoT"

namespace ART
{
	class ESPDevice;

	class DeviceInApplication
	{

	public:

		DeviceInApplication(ESPDevice* espDevice);
		~DeviceInApplication();

		static void							create(DeviceInApplication* (&deviceInApplication), ESPDevice* espDevice);

		void								begin();

		void								load(JsonObject& jsonObject);

		void								insert(char* json);
		void								remove();

		char*								getApplicationId() const;
		void								setApplicationId(char* value);

		char*								getApplicationTopic()  const;
		void								setApplicationTopic(char* value);

		bool								inApplication();

		template<typename Function>
		void								addInsertCallback(Function && fn)
		{
			_insertCallbacks.push_back(std::forward<Function>(fn));
		}

		template<typename Function>
		void								addRemoveCallback(Function && fn)
		{
			_removeCallbacks.push_back(std::forward<Function>(fn));
		}

	private:

		ESPDevice * _espDevice;

		char*								_applicationId;
		char*								_applicationTopic;

		typedef std::function<void()>		callbackSignature;

		std::vector<callbackSignature>		_insertCallbacks;
		std::vector<callbackSignature>		_removeCallbacks;

		void								onDeviceMQSubscribeDevice();
		void								onDeviceMQUnSubscribeDevice();
		void								onDeviceMQSubscribeDeviceInApplication();		
		void								onDeviceMQUnSubscribeDeviceInApplication();
		bool								onDeviceMQSubscription(char* topicKey, char* json);

	};
}

#endif