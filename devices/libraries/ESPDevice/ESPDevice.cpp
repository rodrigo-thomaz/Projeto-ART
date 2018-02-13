#include "ESPDevice.h"

namespace ART
{
	ESPDevice::ESPDevice(const char* webApiHost, uint16_t webApiPort, const char* webApiUri)
	{
		Serial.println(F("[ESPDevice::ESPDevice]] constructor begin"));

		_chipId = ESP.getChipId();
		_flashChipId = ESP.getFlashChipId();
		_chipSize = ESP.getFlashChipSize();

		_webApiHost = (char*)webApiHost;
		_webApiPort = webApiPort;
		_webApiUri = (char*)webApiUri;

		_hasDeviceSerial = false;
		_hasDeviceSensor = false;

		_deviceDebug = new DeviceDebug(this);
		_deviceWiFi = new DeviceWiFi(this);
		_deviceNTP = new DeviceNTP(this);
		_deviceMQ = new DeviceMQ(this);
		_deviceBinary = new DeviceBinary(this);
		_deviceDisplay = new DeviceDisplay(this);

		DeviceBuzzer::create(_deviceBuzzer, this);		
		DeviceInApplication::create(_deviceInApplication, this);				

		Serial.println(F("[ESPDevice::ESPDevice]] constructor end"));
	}

	ESPDevice::~ESPDevice()
	{
		Serial.println(F("[ESPDevice::ESPDevice]] destructor begin"));

		delete (_deviceId);
		delete (_deviceDatasheetId);
		delete (_label);

		delete (_deviceInApplication);
		delete (_deviceBuzzer);

		if(_hasDeviceSerial) delete (_deviceSerial);
		if (_hasDeviceSensor) delete (_deviceSensor);
		if (_hasDeviceWiFi) delete (_deviceWiFi);
		if (_hasDeviceNTP) delete (_deviceNTP);
		if (_hasDeviceMQ) delete (_deviceMQ);
		if (_hasDeviceDebug) delete (_deviceDebug);
		if (_hasDeviceDisplay) delete (_deviceDisplay);
		if (_hasDeviceBinary) delete (_deviceBinary);		

		Serial.println(F("[ESPDevice::ESPDevice]] destructor begin"));
	}

	void ESPDevice::begin()
	{
		_deviceMQ->addSubscribeDeviceCallback([=]() { return onDeviceMQSubscribeDevice(); });
		_deviceMQ->addUnSubscribeDeviceCallback([=]() { return onDeviceMQUnSubscribeDevice(); });
		_deviceMQ->addSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQSubscribeDeviceInApplication(); });
		_deviceMQ->addUnSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQUnSubscribeDeviceInApplication(); });
		_deviceMQ->addSubscriptionCallback([=](const char* topicKey, const char* json) { return onDeviceMQSubscription(topicKey, json); });

		_deviceDisplay->begin();
		_deviceInApplication->begin();
		_deviceMQ->begin();		
		_deviceWiFi->begin();
		_deviceWiFi->autoConnect();
		autoLoad();
		_deviceNTP->begin();
		_deviceDebug->begin();		
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

	DeviceTypeEnum ESPDevice::getDeviceTypeId()
	{
		return _deviceTypeId;
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

	void ESPDevice::setLabel(const char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("ESPDevice", "setLabel", "Parse failed: %s\n", json);
			return;
		}
		const char* value = root["value"];
		_label = new char[strlen(value) + 1];
		strcpy(_label, value);
		_label[strlen(value) + 1] = '\0';
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

	DeviceSerial* ESPDevice::getDeviceSerial()
	{
		return _deviceSerial;
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

	bool ESPDevice::hasDeviceSensor()
	{
		return _hasDeviceSensor;
	}

	bool ESPDevice::hasDeviceSerial()
	{
		return _hasDeviceSerial;
	}

	bool ESPDevice::hasDeviceWiFi()
	{
		return _hasDeviceWiFi;
	}

	bool ESPDevice::hasDeviceNTP()
	{
		return _hasDeviceNTP;
	}

	bool ESPDevice::hasDeviceMQ()
	{
		return _hasDeviceMQ;
	}

	bool ESPDevice::hasDeviceDebug()
	{
		return _hasDeviceDebug;
	}

	bool ESPDevice::hasDeviceDisplay()
	{
		return _hasDeviceDisplay;
	}

	bool ESPDevice::hasDeviceBinary()
	{
		return _hasDeviceBinary;
	}

	DeviceSensor* ESPDevice::getDeviceSensor()
	{
		return _deviceSensor;
	}

	DeviceDisplay * ESPDevice::getDeviceDisplay()
	{
		return _deviceDisplay;
	}

	char * ESPDevice::getDeviceKeyAsJson()
	{
		StaticJsonBuffer<300> JSONbuffer;
		JsonObject& root = JSONbuffer.createObject();

		root["deviceTypeId"] = _deviceTypeId;
		root["deviceDatasheetId"] = _deviceDatasheetId;
		root["deviceId"] = _deviceId;

		int len = root.measureLength() + 1;
		char* result = new char[len];

		root.printTo(result, len);

		result[len] = '\0';

		return result;
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

		Serial.print(F("[ESPDevice] getConfigurations request: "));
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

				Serial.println(F("[HTTP_CODE_OK] !!! "));
				load(http.getString());
			}
		}
		else {
			Serial.print(F("[HTTP] GET... failed, error: "));
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

		_deviceTypeId = static_cast<DeviceTypeEnum>(jsonObject["deviceTypeId"].as<int>());

		const char* deviceId = jsonObject["deviceId"];
		_deviceId = new char[strlen(deviceId) + 1];
		strcpy(_deviceId, deviceId);
		_deviceId[strlen(deviceId) + 1] = '\0';
			
		const char* deviceDatasheetId = jsonObject["deviceDatasheetId"];
		_deviceDatasheetId = new char[strlen(deviceDatasheetId) + 1];
		strcpy(_deviceDatasheetId, deviceDatasheetId);
		_deviceDatasheetId[strlen(deviceDatasheetId) + 1] = '\0';

		const char* label = jsonObject["label"];
		_label = new char[strlen(label) + 1];
		strcpy(_label, label);
		_label[strlen(label) + 1] = '\0';

		_hasDeviceSerial = jsonObject["hasDeviceSerial"];
		_hasDeviceSensor = jsonObject["hasDeviceSensor"];
		_hasDeviceWiFi = jsonObject["hasDeviceWiFi"];
		_hasDeviceNTP = jsonObject["hasDeviceNTP"];
		_hasDeviceMQ = jsonObject["hasDeviceMQ"];
		_hasDeviceDebug = jsonObject["hasDeviceDebug"];
		_hasDeviceDisplay = jsonObject["hasDeviceDisplay"];
		_hasDeviceBinary = jsonObject["hasDeviceBinary"];

		_deviceDebug->load(jsonObject);
		_deviceWiFi->load(jsonObject["deviceWiFi"]);
		_deviceNTP->load(jsonObject["deviceNTP"]);
		_deviceMQ->load(jsonObject["deviceMQ"]);

		JsonObject& deviceInApplicationJO = jsonObject["deviceInApplication"];

		if (deviceInApplicationJO.success()) {
			_deviceInApplication->load(deviceInApplicationJO);
		}		

		Serial.printf("hasDeviceSerial: %s\n", _hasDeviceSerial ? "true" : "false");
		if (_hasDeviceSerial) {
			_deviceSerial = new DeviceSerial(this);
			_deviceSerial->begin();
		}

		Serial.printf("hasDeviceSensor: %s\n", _hasDeviceSensor ? "true" : "false");
		if (_hasDeviceSensor) {
			_deviceSensor = new DeviceSensor(this);
			_deviceSensor->begin();
		}

		Serial.printf("hasDeviceWiFi: %s\n", _hasDeviceWiFi ? "true" : "false");
		if (_hasDeviceWiFi) {
			//_deviceWiFi = new DeviceWiFi(this);
			//_deviceWiFi->begin();
		}

		Serial.printf("hasDeviceNTP: %s\n", _hasDeviceNTP ? "true" : "false");
		if (_hasDeviceNTP) {
			//_deviceNTP = new DeviceNTP(this);
			//_deviceNTP->begin();
		}

		Serial.printf("hasDeviceMQ: %s\n", _hasDeviceMQ ? "true" : "false");
		if (_hasDeviceMQ) {
			//_deviceMQ = new DeviceMQ(this);
			//_deviceMQ->begin();
		}

		Serial.printf("hasDeviceDebug: %s\n", _hasDeviceDebug ? "true" : "false");
		if (_hasDeviceDebug) {
			//_deviceDebug = new DeviceDebug(this);
			//_deviceDebug->begin();
		}

		Serial.printf("hasDeviceDisplay: %s\n", _hasDeviceDisplay ? "true" : "false");
		if (_hasDeviceDisplay) {
			//_deviceDisplay = new DeviceDisplay(this);
			//_deviceDisplay->begin();
		}

		Serial.printf("hasDeviceBinary: %s\n", _hasDeviceBinary ? "true" : "false");
		if (_hasDeviceBinary) {
			//_deviceBinary = new DeviceBinary(this);
			//_deviceBinary->begin();
		}

		_loaded = true;

		if (_deviceDebug->isActive(DeviceDebug::DEBUG)) {
			Serial.printf("[ESPDevice:load] deviceTypeId: %d\n", _deviceTypeId);
			Serial.printf("[ESPDevice:load] deviceDatasheetId: %s\n", _deviceDatasheetId);
			Serial.printf("[ESPDevice:load] deviceId: %s\n", _deviceId);
			Serial.printf("[ESPDevice:load] label: %s\n", _label);
			Serial.print(F("[ESPDevice:load] end\n"));
		}
	}	

	void ESPDevice::onDeviceMQSubscribeDevice()
	{
		_deviceMQ->subscribeDevice(ESP_DEVICE_UPDATE_PIN_TOPIC_SUB);
	}

	void ESPDevice::onDeviceMQUnSubscribeDevice()
	{
		_deviceMQ->unSubscribeDevice(ESP_DEVICE_UPDATE_PIN_TOPIC_SUB);
	}

	void ESPDevice::onDeviceMQSubscribeDeviceInApplication()
	{
		_deviceMQ->subscribeDeviceInApplication(ESP_DEVICE_SET_LABEL_TOPIC_SUB);
	}	

	void ESPDevice::onDeviceMQUnSubscribeDeviceInApplication()
	{
		_deviceMQ->unSubscribeDeviceInApplication(ESP_DEVICE_SET_LABEL_TOPIC_SUB);
	}

	bool ESPDevice::onDeviceMQSubscription(const char* topicKey, const char* json)
	{
		if (strcmp(topicKey, ESP_DEVICE_SET_LABEL_TOPIC_SUB) == 0) {
			setLabel(json);
			return true;
		}
		else {
			return false;
		}
	}
}