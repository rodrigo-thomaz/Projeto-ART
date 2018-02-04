#ifndef DeviceMQ_h
#define DeviceMQ_h

#include "functional"
#include "vector"
#include "ArduinoJson.h"
#include "RemoteDebug.h"
#include "PubSubClient.h"

namespace ART
{
	class ESPDevice;

	class DeviceMQ
	{

	public:

		DeviceMQ(ESPDevice* espDevice);
		~DeviceMQ();

		static void													create(DeviceMQ* (&deviceMQ), ESPDevice* espDevice);

		void														load(JsonObject& jsonObject);

		void														loop();

		char*														getHost() const;
		int															getPort();
		char*														getUser() const;
		char*														getPassword() const;
		char*														getClientId() const;
		char*														getDeviceTopic() const;

		bool														begin();

		bool														autoConnect();

		bool														connected();

		void														publishInApplication(const char* topic, const char* payload);

		void														subscribeInApplication(const char* topic);
		void														subscribeInDevice(const char* topic);

		void														unSubscribeInApplication(const char* topic);
		void														unSubscribeInDevice(const char* topic);

		String 														getTopicKey(char* routingKey);		

		template<typename Function>
		void														addConnectedNotInApplicationCallback(Function && fn)
		{
			_connectedNotInApplicationCallbacks.push_back(std::forward<Function>(fn));
		}

		template<typename Function>
		void														addConnectedInApplicationCallback(Function && fn)
		{
			_connectedInApplicationCallbacks.push_back(std::forward<Function>(fn));
		}

		template<typename Function>
		void														addSubscriptionCallback(Function && fn)
		{
			_subscriptionCallbacks.push_back(std::forward<Function>(fn));
		}		

	private:

		ESPDevice *													_espDevice;

		char*														_host;
		int															_port;
		char*														_user;
		char*														_password;
		char*														_clientId;
		char*														_deviceTopic;

		bool														_begin;

		WiFiClient	 												_espClient;
		PubSubClient* 												_mqqt;		

		String 														getApplicationRoutingKey(const char* topic);
		String 														getDeviceRoutingKey(const char* topic);

		typedef std::function<void()>								connectedSignature;
		typedef std::function<void(char*, uint8_t*, unsigned int)>	subscriptionSignature;

		void														onMQQTCallback(char* topic, uint8_t* payload, unsigned int length);

		std::vector<connectedSignature>								_connectedNotInApplicationCallbacks;
		std::vector<connectedSignature>								_connectedInApplicationCallbacks;
			
		std::vector<subscriptionSignature>							_subscriptionCallbacks;

	};
}

#endif