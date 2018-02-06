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
		_espDevice->getDeviceInApplication()->setInsertCallback([=]() { return onDeviceInApplicationInsert(); });
		_espDevice->getDeviceInApplication()->setRemoveCallback([=]() { return onDeviceInApplicationRemove(); });
	}

	bool DeviceMQ::autoConnect()
	{
		if (!_espDevice->getDeviceWiFi()->isConnected()) {
			return false;
		}

		if (!_loaded) {
			Serial.println("[DeviceMQ::autoConnect] Not initialized !");
			return false;
		}

		if (_mqqt->connected()) {
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
			if (_mqqt->connect(_clientId, _user, _password))				
			{
				Serial.println("[DeviceMQ] Conectado com sucesso ao broker MQTT!");

				if (_espDevice->getDeviceInApplication()->inApplication()) {
					Serial.println("[DeviceMQ] Begin subscribeDeviceInApplicationCallbacks");
					for (auto && fn : _subscribeDeviceInApplicationCallbacks) fn();
					Serial.println("[DeviceMQ] End subscribeDeviceInApplicationCallbacks");
				}
				else {
					Serial.println("[DeviceMQ] Begin subscribeDeviceCallbacks");
					for (auto && fn : _subscribeDeviceCallbacks) fn();
					Serial.println("[DeviceMQ] End subscribeDeviceCallbacks");
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
		
		String json;

		for (int i = 0; i < length; i++)
		{
			char c = (char)payload[i];
			json += c;
		}
		
		// TODO: Não funcionou !!!?@#$%?
		/*for (auto && fn : _subscriptionCallbacks) {
			if (fn(topicKey, strdup(json.c_str()))) {
				Serial.print("Achou em ");
				Serial.print(topicKey);
				Serial.println(" !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
				break;
			}
		}*/

		for (auto && fn : _subscriptionCallbacks) fn(topicKey, strdup(json.c_str()));		
	}

	bool DeviceMQ::connected()
	{
		return _mqqt->connected();
	}

	void DeviceMQ::publishInApplication(const char* topic, const char* payload)
	{
		String routingKey = getApplicationRoutingKey(topic);
		Serial.print("[DeviceMQ::publishInApplication] routingKey: ");
		Serial.println(routingKey);
		_mqqt->publish(routingKey.c_str(), payload);
	}

	void DeviceMQ::subscribeDeviceInApplication(const char* topic)
	{
		String routingKey = getApplicationRoutingKey(topic);
		_mqqt->subscribe(routingKey.c_str());
		_mqqt->loop();

		Serial.print("[DeviceMQ::subscribeDeviceInApplication] Subscribe device in application with success routingKey: ");
		Serial.println(routingKey);
	}

	void DeviceMQ::unSubscribeDeviceInApplication(const char* topic)
	{
		String routingKey = getApplicationRoutingKey(topic);
		_mqqt->unsubscribe(routingKey.c_str());
		_mqqt->loop();

		Serial.print("[DeviceMQ::unSubscribeDeviceInApplication] UnSubscribe device in application with success routingKey: ");
		Serial.println(routingKey);
	}

	void DeviceMQ::subscribeDevice(const char* topic)
	{
		String routingKey = getDeviceRoutingKey(topic);
		_mqqt->subscribe(routingKey.c_str());
		_mqqt->loop();

		Serial.print("[DeviceMQ::subscribeDevice] Subscribe device with success routingKey: ");
		Serial.println(routingKey);
	}

	void DeviceMQ::unSubscribeDevice(const char* topic)
	{
		String routingKey = getDeviceRoutingKey(topic);
		_mqqt->unsubscribe(routingKey.c_str());
		_mqqt->loop();

		Serial.print("[DeviceMQ::unSubscribeDevice] UnSubscribe device with success routingKey: ");
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
		for (auto && fn : _subscribeDeviceInApplicationCallbacks) fn();
	}

	void DeviceMQ::onDeviceInApplicationRemove()
	{
		for (auto && fn : _unSubscribeDeviceInApplicationCallbacks) fn();
		for (auto && fn : _subscribeDeviceCallbacks) fn();
	}
}

