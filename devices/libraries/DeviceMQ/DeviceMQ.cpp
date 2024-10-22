#include "DeviceMQ.h"

#include "ESPDevice.h"
#include <cstddef>         // std::size_t

namespace ART
{
	DeviceMQ::DeviceMQ(ESPDevice* espDevice)
	{
		_espDevice = espDevice;

		this->_mqqt = new PubSubClient(this->_espClient);
	}

	DeviceMQ::~DeviceMQ()
	{
		delete (_espDevice);
		delete (_host);
		delete (_user);
		delete (_password);
		delete (_clientId);
		delete (_deviceTopic);
	}

	void DeviceMQ::load(JsonObject& jsonObject)
	{
		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		deviceDebug->print("DeviceMQ", "load", "begin...\n");

		char* host = strdup(jsonObject["host"]);
		_host = new char(sizeof(strlen(host)));
		_host = host;

		_port = jsonObject["port"];

		char* user = strdup(jsonObject["user"]);
		_user = new char(sizeof(strlen(user)));
		_user = user;

		char* password = strdup(jsonObject["password"]);
		_password = new char(sizeof(strlen(password)));
		_password = password;

		char* clientId = strdup(jsonObject["clientId"]);
		_clientId = new char(sizeof(strlen(clientId)));
		_clientId = clientId;

		char* deviceTopic = strdup(jsonObject["deviceTopic"]);
		_deviceTopic = new char(sizeof(strlen(deviceTopic)));
		_deviceTopic = deviceTopic;

		_mqqt->setServer(_host, _port);

		_mqqt->setCallback([=](char* topic, uint8_t* payload, unsigned int length) {
			return onMQQTCallback(topic, payload, length);
		});

		_loaded = true;

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) {

			deviceDebug->printf("DeviceMQ", "load", "user: %s\n", _user);
			deviceDebug->printf("DeviceMQ", "load", "password: %s\n", _password);
			deviceDebug->printf("DeviceMQ", "load", "clientId: %s\n", _clientId);
			deviceDebug->printf("DeviceMQ", "load", "deviceTopic: %s\n", _deviceTopic);

			deviceDebug->print("DeviceMQ", "load", "end\n");
		}
	}

	void DeviceMQ::loop()
	{
		_mqqt->loop();
	}

	char* DeviceMQ::getHost() const
	{
		return (_host);
	}

	int DeviceMQ::getPort()
	{
		return _port;
	}

	char* DeviceMQ::getUser() const
	{
		return (_user);
	}

	char* DeviceMQ::getPassword() const
	{
		return (_password);
	}

	char* DeviceMQ::getClientId() const
	{
		return (_clientId);
	}

	char* DeviceMQ::getDeviceTopic() const
	{
		return (_deviceTopic);
	}

	void DeviceMQ::begin()
	{
		_espDevice->getDeviceInApplication()->addInsertCallback([=]() { return onDeviceInApplicationInsert(); });
		_espDevice->getDeviceInApplication()->addRemoveCallback([=]() { return onDeviceInApplicationRemove(); });
	}

	bool DeviceMQ::autoConnect()
	{
		if (!_espDevice->getDeviceWiFi()->isConnected()) {
			return false;
		}

		if (!_loaded) {
			Serial.println(F("[DeviceMQ::autoConnect] Not initialized !"));
			return false;
		}

		if (_mqqt->connected()) {
			return true;
		}
		else {

			Serial.print(F("[DeviceMQ] Tentando se conectar ao Broker MQTT: "));
			Serial.println(_host);

			Serial.print(F("[DeviceMQ] ClientId: "));
			Serial.println(_clientId);

			Serial.print(F("[DeviceMQ] User: "));
			Serial.println(_user);

			Serial.print(F("[DeviceMQ] Password: "));
			Serial.println(_password);

			byte willQoS = 0;
			const char* willTopic = "willTopic";
			const char* willMessage = "My Will Message";
			boolean willRetain = false;

			//if (this->_mqqt->connect(clientId, user, password, willTopic, willQoS, willRetain, willMessage)) 
			if (_mqqt->connect(_clientId, _user, _password))
			{
				Serial.println(F("[DeviceMQ] Conectado com sucesso ao broker MQTT!"));

				if (_espDevice->getDeviceInApplication()->inApplication()) {
					Serial.println(F("[DeviceMQ] Begin subscribeDeviceInApplicationCallbacks"));
					for (auto && fn : _subscribeDeviceInApplicationCallbacks) fn();
					Serial.println(F("[DeviceMQ] End subscribeDeviceInApplicationCallbacks"));
				}
				else {
					Serial.println(F("[DeviceMQ] Begin subscribeDeviceCallbacks"));
					for (auto && fn : _subscribeDeviceCallbacks) fn();
					Serial.println(F("[DeviceMQ] End subscribeDeviceCallbacks"));
				}

				return true;
			}
			else
			{
				Serial.println(F("[DeviceMQ] Falha ao reconectar no broker."));
				Serial.println(F("[DeviceMQ] Haver� nova tentatica de conexao em 2s"));
				delay(2000);

				return false;
			}
		}
	}

	void DeviceMQ::onMQQTCallback(char* topic, uint8_t* payload, unsigned int length)
	{
		Serial.print(F("[DeviceMQ::onMQQTCallback] topic:"));
		Serial.println(topic);

		char* topicKey = strdup(getTopicKey(topic));

		Serial.print(F("[DeviceMQ::onMQQTCallback] topicKey:"));
		Serial.println(topicKey);
		
		for (auto && fn : _subscriptionCallbacks) {
			if (fn(topicKey, (char*)payload)) {
				Serial.print(F("[DeviceMQ::onMQQTCallback] find: "));
				Serial.println(topicKey);
				return;
			}
		}		

		Serial.print(F("[DeviceMQ::onMQQTCallback] not find: "));
		Serial.println(topicKey);
	}

	bool DeviceMQ::connected()
	{
		return _mqqt->connected();
	}

	void DeviceMQ::getByKeyPub()
	{
		Serial.println(F("[DeviceMQ::getByKeyPub] begin"));
		_espDevice->getDeviceMQ()->publishInApplication(DEVICE_MQ_GET_BY_KEY_TOPIC_PUB, _espDevice->getDeviceKeyAsJson());
		Serial.println(F("[DeviceMQ::getByKeyPub] end"));
	}

	void DeviceMQ::getByKeySub(const char * json)
	{
		Serial.println(F("[DeviceMQ::getByKeySub] Aqui !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"));
	}

	void DeviceMQ::publishInApplication(const char* topic, const char* payload)
	{
		String routingKey = getApplicationRoutingKey(topic);
		Serial.print(F("[DeviceMQ::publishInApplication] routingKey: "));
		Serial.println(routingKey);
		_mqqt->publish(routingKey.c_str(), payload);
	}

	void DeviceMQ::subscribeDeviceInApplication(const char* topic)
	{
		String routingKey = getApplicationRoutingKey(topic);
		_mqqt->subscribe(routingKey.c_str());
		_mqqt->loop();

		Serial.print(F("[DeviceMQ::subscribeDeviceInApplication] Subscribe device in application with success routingKey: "));
		Serial.println(routingKey);
	}

	void DeviceMQ::unSubscribeDeviceInApplication(const char* topic)
	{
		String routingKey = getApplicationRoutingKey(topic);
		_mqqt->unsubscribe(routingKey.c_str());
		_mqqt->loop();

		Serial.print(F("[DeviceMQ::unSubscribeDeviceInApplication] UnSubscribe device in application with success routingKey: "));
		Serial.println(routingKey);
	}

	void DeviceMQ::subscribeDevice(const char* topic)
	{
		String routingKey = getDeviceRoutingKey(topic);
		_mqqt->subscribe(routingKey.c_str());
		_mqqt->loop();

		Serial.print(F("[DeviceMQ::subscribeDevice] Subscribe device with success routingKey: "));
		Serial.println(routingKey);
	}

	void DeviceMQ::unSubscribeDevice(const char* topic)
	{
		String routingKey = getDeviceRoutingKey(topic);
		_mqqt->unsubscribe(routingKey.c_str());
		_mqqt->loop();

		Serial.print(F("[DeviceMQ::unSubscribeDevice] UnSubscribe device with success routingKey: "));
		Serial.println(routingKey);
	}

	char * DeviceMQ::getTopicKey(const char * routingKey)
	{
		const char * pchFunc = strrchr(routingKey, '/');
		int lastIndexOfFunc = pchFunc - routingKey;

		char restString[lastIndexOfFunc];
		strncpy(restString, routingKey, lastIndexOfFunc);
		restString[lastIndexOfFunc] = '\0';

		const char * pchClass = strrchr(restString, '/');
		int lastIndexOfClass = pchClass - restString + 1;

		char result[strlen(pchClass) + strlen(pchFunc) - 1];
		strncpy(result, pchClass + 1, strlen(pchClass));
		strcat(result, pchFunc);
		
		return result;
	}

	String DeviceMQ::getApplicationRoutingKey(const char* topic)
	{
		DeviceInApplication* deviceInApplication = _espDevice->getDeviceInApplication();

		String routingKey = String("ART/Application/");
		routingKey.concat(deviceInApplication->getApplicationTopic());
		routingKey.concat("/Device/");
		routingKey.concat(getDeviceTopic());
		routingKey.concat("/");
		routingKey.concat(topic);

		return routingKey;
	}

	String DeviceMQ::getDeviceRoutingKey(const char* topic)
	{
		String routingKey = String("ART/Device/");
		routingKey.concat(getDeviceTopic());
		routingKey.concat("/");
		routingKey.concat(topic);

		return routingKey;
	}

	void DeviceMQ::onDeviceInApplicationInsert()
	{
		for (auto && fn : _unSubscribeDeviceCallbacks) fn();
		onDeviceMQSubscribeDeviceInApplication();
		for (auto && fn : _subscribeDeviceInApplicationCallbacks) fn();
	}

	void DeviceMQ::onDeviceInApplicationRemove()
	{
		onDeviceMQUnSubscribeDeviceInApplication();
		for (auto && fn : _unSubscribeDeviceInApplicationCallbacks) fn();
		for (auto && fn : _subscribeDeviceCallbacks) fn();
	}

	void DeviceMQ::onDeviceMQSubscribeDeviceInApplication()
	{
		subscribeDeviceInApplication(DEVICE_MQ_GET_BY_KEY_COMPLETED_TOPIC_SUB);
	}

	void DeviceMQ::onDeviceMQUnSubscribeDeviceInApplication()
	{
		unSubscribeDeviceInApplication(DEVICE_MQ_GET_BY_KEY_COMPLETED_TOPIC_SUB);
	}
}

