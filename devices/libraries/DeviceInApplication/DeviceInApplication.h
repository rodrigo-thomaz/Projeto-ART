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

		static void							create(DeviceInApplication* (&deviceInApplication), ESPDevice* espDevice);

		void								load(JsonObject& jsonObject);

		void								insertInApplication(char* json);
		void								deleteFromApplication();

		char*								getApplicationId() const;
		void								setApplicationId(char* value);

		char*								getApplicationTopic()  const;
		void								setApplicationTopic(char* value);

	private:

		ESPDevice * _espDevice;

		char*								_applicationId;
		char*								_applicationTopic;

	};
}

#endif