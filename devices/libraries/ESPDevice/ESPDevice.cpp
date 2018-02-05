#include "ESPDevice.h"

namespace ART
{
	ESPDevice::ESPDevice(char* webApiHost, uint16_t webApiPort, char* webApiUri)
	{
		_chipId = ESP.getChipId();
		_flashChipId = ESP.getFlashChipId();
		_chipSize = ESP.getFlashChipSize();

		_webApiHost = new char(sizeof(strlen(webApiHost)));
		_webApiHost = webApiHost;

		_webApiPort = webApiPort;

		_webApiUri = new char(sizeof(strlen(webApiUri)));
		_webApiUri = webApiUri;

		DeviceDebug::create(_deviceDebug, this);
		DeviceWiFi::create(_deviceWiFi, this);
		DeviceNTP::create(_deviceNTP, this);
		DeviceMQ::create(_deviceMQ, this);
		DeviceBinary::create(_deviceBinary, this);
		DeviceBuzzer::create(_deviceBuzzer, this);
		DeviceSensors::create(_deviceSensors, this);
		DeviceInApplication::create(_deviceInApplication, this);		

		_deviceMQ->addSubscribeNotInApplicationCallback([=]() { return this->onDeviceMQSubscribeNotInApplication(); });
		_deviceMQ->addSubscribeInApplicationCallback([=]() { return this->onDeviceMQSubscribeInApplication(); });
		_deviceMQ->addUnSubscribeNotInApplicationCallback([=]() { return this->onDeviceMQUnSubscribeNotInApplication(); });
		_deviceMQ->addUnSubscribeInApplicationCallback([=]() { return this->onDeviceMQUnSubscribeInApplication(); });
		_deviceMQ->addSubscriptionCallback([=](char* topicKey, char* json) { return this->onDeviceMQSubscription(topicKey, json); });
	}

	ESPDevice::~ESPDevice()
	{
		delete (_deviceId);
		delete (_deviceDatasheetId);
		delete (_label);

		delete (_deviceInApplication);
		delete (_deviceDebug);
		delete (_deviceWiFi);
		delete (_deviceNTP);
		delete (_deviceMQ);
		delete (_deviceBinary);
		delete (_deviceBuzzer);
		delete (_deviceSensors);
	}

	void ESPDevice::begin()
	{
		_deviceInApplication->begin();
		_deviceMQ->beginNew();
		_deviceWiFi->autoConnect();
		autoLoad();
		_deviceNTP->begin();
	}

	void ESPDevice::loop()
	{
		_deviceWiFi->autoConnect(); //se não há conexão com o WiFI, a conexão é refeita

		autoLoad();

		_deviceDebug->loop();
		_deviceMQ->autoConnect(); //se não há conexão com o Broker, a conexão é refeita

		_deviceBinary->loop();

		// Give a time for ESP8266
		yield();
	}

	char* ESPDevice::getDeviceId() const
	{
		return (_deviceId);
	}

	char* ESPDevice::getDeviceDatasheetId() const
	{
		return _deviceDatasheetId;
	}

	int	ESPDevice::getChipId()
	{
		return _chipId;
	}

	int ESPDevice::getFlashChipId()
	{
		return _flashChipId;
	}

	long ESPDevice::getChipSize()
	{
		return _chipSize;
	}

	char* ESPDevice::getLabel() const
	{
		return (_label);
	}

	void ESPDevice::setLabel(char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("ESPDevice", "setLabel", "Parse failed: %s\n", json);
			return;
		}
		char* value = strdup(root["value"]);
		_label = new char(sizeof(strlen(value)));
		_label = value;
	}	

	char* ESPDevice::getWebApiHost() const
	{
		return (_webApiHost);
	}

	uint16_t ESPDevice::getWebApiPort()
	{
		return _webApiPort;
	}

	char* ESPDevice::getWebApiUri() const
	{
		return (_webApiUri);
	}

	DeviceInApplication* ESPDevice::getDeviceInApplication()
	{
		return _deviceInApplication;
	}

	DeviceDebug* ESPDevice::getDeviceDebug()
	{
		return _deviceDebug;
	}

	DeviceWiFi* ESPDevice::getDeviceWiFi()
	{
		return _deviceWiFi;
	}

	DeviceMQ* ESPDevice::getDeviceMQ()
	{
		return _deviceMQ;
	}

	DeviceNTP* ESPDevice::getDeviceNTP()
	{
		return _deviceNTP;
	}

	DeviceBinary* ESPDevice::getDeviceBinary()
	{
		return _deviceBinary;
	}

	DeviceBuzzer* ESPDevice::getDeviceBuzzer()
	{
		return _deviceBuzzer;
	}

	DeviceSensors* ESPDevice::getDeviceSensors()
	{
		return _deviceSensors;
	}

	bool ESPDevice::loaded()
	{
		return _loaded;
	}

	void ESPDevice::autoLoad()
	{
		if (!_deviceWiFi->isConnected() || _loaded) {
			return;
		}

		HTTPClient http;

		String apiUri = String(_webApiUri) + "api/espDevice/getConfigurations";
		http.begin(_webApiHost, _webApiPort, apiUri);

		StaticJsonBuffer<200> jsonBufferRequest;
		JsonObject& jsonObjectRequest = jsonBufferRequest.createObject();

		jsonObjectRequest["chipId"] = _chipId;
		jsonObjectRequest["flashChipId"] = _flashChipId;
		jsonObjectRequest["stationMacAddress"] = _deviceWiFi->getStationMacAddress();
		jsonObjectRequest["softAPMacAddress"] = _deviceWiFi->getSoftAPMacAddress();

		int lenRequest = jsonObjectRequest.measureLength();
		char dataRequest[lenRequest + 1];
		jsonObjectRequest.printTo(dataRequest, sizeof(dataRequest));

		Serial.print("[ESPDevice] getConfigurations request: ");
		jsonObjectRequest.printTo(Serial);
		Serial.println();

		// start connection and send HTTP header
		http.addHeader("access-control-allow-credentials", "true");
		http.addHeader("content-length", String(lenRequest));
		http.addHeader("Content-Type", "application/json");

		int httpCode = http.POST(dataRequest);

		// httpCode will be negative on error
		if (httpCode > 0) {
			// file found at server
			if (httpCode == HTTP_CODE_OK) {

				Serial.println("[HTTP_CODE_OK] !!! ");
				load(http.getString());
			}
		}
		else {
			Serial.print("[HTTP] GET... failed, error: ");
			Serial.println(http.errorToString(httpCode).c_str());
		}

		http.end();

	}

	void ESPDevice::load(String json)
	{
		// TODO: O DeviceDebug começa a funcionar aqui

		_deviceDebug->print("ESPDevice", "load", "begin\n");

		DynamicJsonBuffer jsonBuffer;
		JsonObject& jsonObject = jsonBuffer.parseObject(json);

		_deviceId = strdup(jsonObject["deviceId"]);
		_deviceDatasheetId = strdup(jsonObject["deviceDatasheetId"]);

		_label = strdup(jsonObject["label"]);

		_deviceDebug->load(jsonObject);
		_deviceWiFi->load(jsonObject["deviceWiFi"]);
		_deviceNTP->load(jsonObject["deviceNTP"]);
		_deviceMQ->load(jsonObject["deviceMQ"]);

		JsonObject& insertInApplicationJO = jsonObject["deviceInApplication"];

		if (insertInApplicationJO.success()) {
			_deviceInApplication->load(insertInApplicationJO);
		}		

		_loaded = true;

		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) {

			_deviceDebug->printf("ESPDevice", "load", "deviceId: %s\n", _deviceId);
			_deviceDebug->printf("ESPDevice", "load", "deviceDatasheetId: %s\n", _deviceDatasheetId);
			_deviceDebug->printf("ESPDevice", "load", "label: %s\n", _label);

			_deviceDebug->print("ESPDevice", "load", "end\n");
		}
	}	

	void ESPDevice::onDeviceMQSubscribeNotInApplication()
	{
		_deviceMQ->subscribeInDevice(ESP_DEVICE_UPDATE_PIN_TOPIC_SUB);
	}

	void ESPDevice::onDeviceMQSubscribeInApplication()
	{
		_deviceMQ->subscribeInApplication(ESP_DEVICE_SET_LABEL_TOPIC_SUB);
	}

	void ESPDevice::onDeviceMQUnSubscribeNotInApplication()
	{
		_deviceMQ->unSubscribeInDevice(ESP_DEVICE_UPDATE_PIN_TOPIC_SUB);
	}

	void ESPDevice::onDeviceMQUnSubscribeInApplication()
	{
		_deviceMQ->unSubscribeInApplication(ESP_DEVICE_SET_LABEL_TOPIC_SUB);
	}

	void ESPDevice::onDeviceMQSubscription(char* topicKey, char* json)
	{
		if (strcmp(topicKey, ESP_DEVICE_SET_LABEL_TOPIC_SUB) == 0) {
			setLabel(json);
		}
	}
}