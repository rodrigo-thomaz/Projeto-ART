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

		DeviceDebug::createDeviceDebug(_deviceDebug, this);
		DeviceWiFi::createDeviceWiFi(_deviceWiFi, this);
		DeviceNTP::createDeviceNTP(_deviceNTP, this);
		DeviceMQ::createDeviceMQ(_deviceMQ, this);
		DeviceBinary::createDeviceBinary(_deviceBinary, this);
		DeviceBuzzer::createDeviceBuzzer(_deviceBuzzer, this);
		DeviceSensors::createDeviceSensors(_deviceSensors, this);
	}

	ESPDevice::~ESPDevice()
	{
		delete (_deviceId);
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
		_deviceWiFi->autoConnect();
		autoLoad();
		_deviceNTP->begin();
	}

	void ESPDevice::loop()
	{
		_deviceWiFi->autoConnect(); //se não há conexão com o WiFI, a conexão é refeita

		autoLoad();

		_deviceDebug->loop();

		// Give a time for ESP8266
		yield();
	}

	char* ESPDevice::getDeviceId()
	{
		return _deviceId;
	}

	short ESPDevice::getDeviceDatasheetId()
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

	char* ESPDevice::getLabel()
	{
		return _label;
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

	char* ESPDevice::getWebApiHost()
	{
		return _webApiHost;
	}

	uint16_t ESPDevice::getWebApiPort()
	{
		return _webApiPort;
	}

	char* ESPDevice::getWebApiUri()
	{
		return _webApiUri;
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

				String payload = http.getString();

				load(payload);

				if (_deviceDebug->isActive(DeviceDebug::DEBUG)) {

					_deviceDebug->print("ESPDevice", "autoLoad", "Initialized with success !\n");

					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceId: %s\n", _deviceId);
					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceDatasheetId: %d\n", (char*)_deviceDatasheetId);

					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceWiFi HostName: %s\n", getDeviceWiFi()->getHostName());
					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceWiFi StationMacAddress: %s\n", getDeviceWiFi()->getStationMacAddress());
					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceWiFi SoftAPMacAddress: %s\n", getDeviceWiFi()->getSoftAPMacAddress());

					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceMQ Host: %s\n", getDeviceMQ()->getHost());
					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceMQ Port: %d\n", (char*)getDeviceMQ()->getPort());
					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceMQ User: %s\n", getDeviceMQ()->getUser());
					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceMQ Password: %s\n", getDeviceMQ()->getPassword());
					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceMQ ClientId: %s\n", getDeviceMQ()->getClientId());
					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceMQ Device Topic: %s\n", getDeviceMQ()->getDeviceTopic());

					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceNTP Host: %s\n", getDeviceNTP()->getHost());
					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceNTP Port: %d\n", (char*)getDeviceNTP()->getPort());
					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceNTP UtcTimeOffsetInSecond: %d\n", (char*)getDeviceNTP()->getUtcTimeOffsetInSecond());
					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceNTP UpdateIntervalInMilliSecond: %d\n", (char*)getDeviceNTP()->getUpdateIntervalInMilliSecond());

					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceInApplication ApplicationId: %s\n", getDeviceInApplication()->getApplicationId());
					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceInApplication ApplicationTopic: %s\n", getDeviceInApplication()->getApplicationTopic());

					_deviceDebug->printf("ESPDevice", "autoLoad", "DeviceSensors PublishIntervalInMilliSeconds: %d\n", (char*)getDeviceSensors()->getPublishIntervalInMilliSeconds());
				}
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
		DynamicJsonBuffer 				  jsonBuffer;
		JsonObject& jsonObject = jsonBuffer.parseObject(json);

		_deviceId = strdup(jsonObject["deviceId"]);
		_deviceDatasheetId = jsonObject["deviceDatasheetId"];

		_label = strdup(jsonObject["label"]);

		_deviceDebug->load(jsonObject);
		_deviceWiFi->load(jsonObject["deviceWiFi"]);
		_deviceNTP->load(jsonObject["deviceNTP"]);
		_deviceMQ->load(jsonObject["deviceMQ"]);
		_deviceSensors->load(jsonObject["deviceSensors"]);

		DeviceInApplication::createDeviceInApplication(_deviceInApplication, this, jsonObject["deviceInApplication"]);

		_loaded = true;
	}
}