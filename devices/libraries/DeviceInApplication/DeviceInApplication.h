#ifndef DeviceInApplication_h
#define DeviceInApplication_h

#include "Arduino.h"
#include "ArduinoJson.h"

namespace ART
{
	class ESPDevice;

	class DeviceInApplication
	{

	public:

		DeviceInApplication(ESPDevice* espDevice);
		~DeviceInApplication();

		void								load(JsonObject& jsonObject);

		void								insertInApplication(String json);
		void								deleteFromApplication();

		char*								getApplicationId() const;
		void								setApplicationId(char* value);

		char*								getApplicationTopic()  const;
		void								setApplicationTopic(char* value);

		static void create(DeviceInApplication* (&deviceInApplication), ESPDevice* espDevice)
		{
			deviceInApplication = new DeviceInApplication(espDevice);
		}

	private:

		ESPDevice * _espDevice;

		char*								_applicationId;
		char*								_applicationTopic;

	};
}

#endif