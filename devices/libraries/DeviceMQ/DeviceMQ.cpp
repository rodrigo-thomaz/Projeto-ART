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

	void DeviceMQ::create(DeviceMQ *(&deviceMQ), ESPDevice * espDevice)
	{
		deviceMQ = new DeviceMQ(espDevice);
	}

	void DeviceMQ::load(JsonObject& jsonObject)
	{
		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		deviceDebug->print("DeviceMQ", "load", "begin\n");

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

	bool DeviceMQ::begin()
	{
		if (this->_begin) return true;

		if (this->_espDevice->getDeviceWiFi()->isConnected() && this->_espDevice->loaded()) {

			this->_mqqt->setServer(_host, _port);   
			
			this->_mqqt->setCallback([=](char* topic, uint8_t* payload, unsigned int length) {
				return this->onMQQTCallback(topic, payload, length);
			});

			this->_begin = true;

			Serial.println("[MQQT] Initialized with success !");
		}
		else {
			this->_begin = false;

			Serial.println("[MQQT] Not initialized !");
		}
	}

	void DeviceMQ::beginNew()
	{
		_espDevice->getDeviceInApplication()->addInsertCallback([=]() { return this->onDeviceInApplicationInsert(); });
		_espDevice->getDeviceInApplication()->addRemoveCallback([=]() { return this->onDeviceInApplicationRemove(); });
	}

	bool DeviceMQ::autoConnect()
	{
		if (!this->_espDevice->getDeviceWiFi()->isConnected() || !this->_espDevice->loaded()) {
			return false;
		}

		if (!this->begin()) {
			return false;
		}

		if (this->_mqqt->connected()) {
			return true;
		}
		else {

			Serial.print("[DeviceMQ] Tentando se conectar ao Broker MQTT: ");
			Serial.println(_host);

			Serial.print("[DeviceMQ] ClientId: ");
			Serial.println(_clientId);

			Serial.print("[DeviceMQ] User: ");
			Serial.println(_user);

			Serial.print("[DeviceMQ] Password: ");
			Serial.println(_password);

			byte willQoS = 0;
			const char* willTopic = "willTopic";
			const char* willMessage = "My Will Message";
			boolean willRetain = false;

			//if (this->_mqqt->connect(clientId, user, password, willTopic, willQoS, willRetain, willMessage)) 
			if (this->_mqqt->connect(_clientId, _user, _password))				
			{
				Serial.println("[DeviceMQ] Conectado com sucesso ao broker MQTT!");

				if (_espDevice->getDeviceInApplication()->getApplicationId() == NULL) {
					Serial.println("[DeviceMQ] Begin subscribeNotInApplicationCallbacks");
					for (auto && fn : _subscribeNotInApplicationCallbacks) fn();
					Serial.println("[DeviceMQ] End subscribeNotInApplicationCallbacks");
				}
				else {
					Serial.println("[DeviceMQ] Begin subscribeInApplicationCallbacks");
					for (auto && fn : _subscribeInApplicationCallbacks) fn();
					Serial.println("[DeviceMQ] End subscribeInApplicationCallbacks");
				}

				return true;
			}
			else
			{
				Serial.println("[DeviceMQ] Falha ao reconectar no broker.");
				Serial.println("[DeviceMQ] Haverá nova tentatica de conexao em 2s");
				delay(2000);

				return false;
			}
		}
	}

	void DeviceMQ::onMQQTCallback(char* topic, uint8_t* payload, unsigned int length)
	{
		char* topicKey = getTopicKey(topic);
		char* json = reinterpret_cast<char*>(payload);
		
		for (auto && fn : _subscriptionCallbacks) fn(topicKey, json);
	}

	bool DeviceMQ::connected()
	{
		return _mqqt->connected();
	}

	void DeviceMQ::publishInApplication(const char* topic, const char* payload)
	{
		String routingKey = this->getApplicationRoutingKey(topic);
		Serial.print("[DeviceMQ::publishInApplication] routingKey: ");
		Serial.println(routingKey);
		this->_mqqt->publish(routingKey.c_str(), payload);
	}

	void DeviceMQ::subscribeInApplication(const char* topic)
	{
		String routingKey = this->getApplicationRoutingKey(topic);
		this->_mqqt->subscribe(routingKey.c_str());
		this->_mqqt->loop();

		Serial.print("[DeviceMQ::subscribe] Subscribe in application with success routingKey: ");
		Serial.println(routingKey);
	}

	void DeviceMQ::unSubscribeInApplication(const char* topic)
	{
		String routingKey = this->getApplicationRoutingKey(topic);
		this->_mqqt->unsubscribe(routingKey.c_str());
		this->_mqqt->loop();

		Serial.print("[DeviceMQ::unSubscribeInApplication] UnSubscribe in application with success routingKey: ");
		Serial.println(routingKey);
	}

	void DeviceMQ::subscribeInDevice(const char* topic)
	{
		String routingKey = this->getDeviceRoutingKey(topic);
		this->_mqqt->subscribe(routingKey.c_str());
		this->_mqqt->loop();

		Serial.print("[DeviceMQ::subscribe] Subscribe in device with success routingKey: ");
		Serial.println(routingKey);
	}

	void DeviceMQ::unSubscribeInDevice(const char* topic)
	{
		String routingKey = this->getDeviceRoutingKey(topic);
		this->_mqqt->unsubscribe(routingKey.c_str());
		this->_mqqt->loop();

		Serial.print("[DeviceMQ::unSubscribeInDevice] UnSubscribe in device with success routingKey: ");
		Serial.println(routingKey);
	}

	char* DeviceMQ::getTopicKey(char* routingKey)
	{
		String routingKeyStr = String(routingKey);
		int lastIndexOf = routingKeyStr.lastIndexOf('/');

		String restString = routingKeyStr.substring(0, lastIndexOf);
		int restLastIndexOf = restString.lastIndexOf('/');
		int restSize = sizeof(routingKeyStr) - restLastIndexOf;

		String result = routingKeyStr.substring(restLastIndexOf + 1, restSize);

		return strdup(result.c_str());
	}

	String DeviceMQ::getApplicationRoutingKey(const char* topic)
	{
		DeviceInApplication* deviceInApplication = this->_espDevice->getDeviceInApplication();

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
		for (auto && fn : _unSubscribeNotInApplicationCallbacks) fn();
		for (auto && fn : _subscribeInApplicationCallbacks) fn();
	}

	void DeviceMQ::onDeviceInApplicationRemove()
	{
		for (auto && fn : _unSubscribeInApplicationCallbacks) fn();
		for (auto && fn : _subscribeNotInApplicationCallbacks) fn();
	}
}

