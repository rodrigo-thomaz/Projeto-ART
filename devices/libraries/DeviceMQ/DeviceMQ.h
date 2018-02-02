#ifndef DeviceMQ_h
#define DeviceMQ_h

#include "ArduinoJson.h"
#include "RemoteDebug.h"
#include "PubSubClient.h"

#define DEVICE_MQ_SUB_CALLBACK_SIGNATURE std::function<void(char*, uint8_t*, unsigned int)>
#define DEVICE_MQ_CONNECTED_CALLBACK_SIGNATURE std::function<void(PubSubClient*)>

namespace ART
{
	class ESPDevice;

	class DeviceMQ
	{

	public:

		DeviceMQ(ESPDevice* espDevice);
		~DeviceMQ();

		void												load(JsonObject& jsonObject);

		char*												getHost() const;
		int													getPort();
		char*												getUser() const;
		char*												getPassword() const;
		char*												getClientId() const;
		char*												getDeviceTopic() const;

		bool												begin();

		DeviceMQ& 											setSubCallback(DEVICE_MQ_SUB_CALLBACK_SIGNATURE callback);
		DeviceMQ& 											setConnectedCallback(DEVICE_MQ_CONNECTED_CALLBACK_SIGNATURE callback);

		bool												autoConnect();

		PubSubClient*										getMQQT();

		void												publishInApplication(const char* topic, const char* payload);

		void												subscribeInApplication(const char* topic);
		void												subscribeInDevice(const char* topic);

		void												unSubscribeInApplication(const char* topic);
		void												unSubscribeInDevice(const char* topic);

		String 												getTopicKey(char* routingKey);

		static void create(DeviceMQ* (&deviceMQ), ESPDevice* espDevice)
		{
			deviceMQ = new DeviceMQ(espDevice);
		}

	private:

		ESPDevice * _espDevice;

		char*												_host;
		int													_port;
		char*												_user;
		char*												_password;
		char*												_clientId;
		char*												_deviceTopic;

		bool												_begin;

		WiFiClient	 										_espClient;
		PubSubClient* 										_mqqt;

		DEVICE_MQ_SUB_CALLBACK_SIGNATURE					_subCallback;
		DEVICE_MQ_SUB_CALLBACK_SIGNATURE					_onSubCallback;

		DEVICE_MQ_CONNECTED_CALLBACK_SIGNATURE				_connectedCallback;

		void												onSubCallback(char* topic, byte* payload, unsigned int length);

		String 												getApplicationRoutingKey(const char* topic);
		String 												getDeviceRoutingKey(const char* topic);

	};
}

#endif