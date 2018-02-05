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

		template<typename Function>
		void								addInsertingCallback(Function && fn)
		{
			_insertingCallbacks.push_back(std::forward<Function>(fn));
		}

		template<typename Function>
		void								addInsertedCallback(Function && fn)
		{
			_insertedCallbacks.push_back(std::forward<Function>(fn));
		}

		template<typename Function>
		void								addRemovingCallback(Function && fn)
		{
			_removingCallbacks.push_back(std::forward<Function>(fn));
		}

		template<typename Function>
		void								addRemovedCallback(Function && fn)
		{
			_removedCallbacks.push_back(std::forward<Function>(fn));
		}

	private:

		ESPDevice * _espDevice;

		char*								_applicationId;
		char*								_applicationTopic;

		typedef std::function<void()>		callbackSignature;

		std::vector<callbackSignature>		_insertingCallbacks;
		std::vector<callbackSignature>		_insertedCallbacks;
		std::vector<callbackSignature>		_removingCallbacks;		
		std::vector<callbackSignature>		_removedCallbacks;

		void								onDeviceMQSubscribeDevice();
		void								onDeviceMQSubscribeDeviceInApplication();
		void								onDeviceMQUnSubscribeDevice();
		void								onDeviceMQUnSubscribeDeviceInApplication();
		void								onDeviceMQSubscription(char* topicKey, char* json);

	};
}

#endif