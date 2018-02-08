#include "DeviceSensors.h"

#include "../ESPDevice/ESPDevice.h"
#include "../SensorInDevice/SensorInDevice.h"
#include "PositionEnum.h"
#include <algorithm>    // std::sort

// Data wire is plugged into port 0
#define ONE_WIRE_BUS 0

// Setup a oneWire instance to communicate with any OneWire devices (not just Maxim/Dallas temperature ICs)
OneWire oneWire(ONE_WIRE_BUS);

// Pass our oneWire reference to Dallas Temperature. 
DallasTemperature _dallas(&oneWire);

namespace ART
{
	DeviceSensors::DeviceSensors(ESPDevice* espDevice)
	{
		_espDevice = espDevice;
		_readIntervalTimestamp = 0;
	}

	DeviceSensors::~DeviceSensors()
	{
		delete (_espDevice);
	}

	void DeviceSensors::create(DeviceSensors *(&deviceSensors), ESPDevice * espDevice)
	{
		deviceSensors = new DeviceSensors(espDevice);
	}

	void DeviceSensors::begin()
	{
		_espDevice->getDeviceMQ()->addSubscriptionCallback([=](char* topicKey, char* json) { return onDeviceMQSubscription(topicKey, json); });
		_espDevice->getDeviceMQ()->addSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQSubscribeDeviceInApplication(); });
		_espDevice->getDeviceMQ()->addUnSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQUnSubscribeDeviceInApplication(); });
		
		_dallas.begin();
		initialized();
	}

	bool DeviceSensors::initialized()
	{
		if (this->_initialized) return true;

		if (!this->_espDevice->loaded()) return false;

		if (this->_initializing) return false;

		if (!_espDevice->getDeviceMQ()->connected()) return false;

		// initializing

		//TODO:TESTE
		return false;

		this->_initializing = true;

		Serial.println(F("[DeviceSensors::initialized] initializing...]"));

		char* deviceId = this->_espDevice->getDeviceId();
		char* deviceDatasheetId = this->_espDevice->getDeviceDatasheetId();
		char* applicationId = this->_espDevice->getDeviceInApplication()->getApplicationId();

		StaticJsonBuffer<DEVICE_SENSORS_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_REQUEST_JSON_SIZE> JSONbuffer;
		JsonObject& root = JSONbuffer.createObject();

		root["deviceId"] = deviceId;
		root["deviceDatasheetId"] = deviceDatasheetId;
		root["applicationId"] = applicationId;

		// device addresses prepare	
		uint8_t deviceCount = _dallas.getDeviceCount();
		if (deviceCount > 0) {
			JsonArray& deviceAddressJsonArray = root.createNestedArray("deviceAddresses");
			for (int i = 0; i < deviceCount; ++i) {
				DeviceAddress deviceAddress;
				if (_dallas.getAddress(deviceAddress, i))
				{
					deviceAddressJsonArray.add(this->convertDeviceAddressToString(deviceAddress));
				}
				else {
					Serial.print(F("Não foi possível encontrar um endereço para o Device: "));
					Serial.println(i);
				}
			}
		}

		int len = root.measureLength();
		char result[len + 1];
		root.printTo(result, sizeof(result));

		_espDevice->getDeviceMQ()->publishInApplication(DEVICE_SENSORS_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_TOPIC_PUB, result);

		return true;
	}

	void DeviceSensors::setSensorsByMQQTCallback(char* json)
	{
		Serial.println(F("[DeviceSensors::setSensorsByMQQTCallback] Enter"));

		Serial.print(F("FreeHeap pre buffer deviceSensorsJO: "));
		Serial.println(ESP.getFreeHeap());

		this->_initialized = true;
		this->_initializing = false;

		DynamicJsonBuffer jsonBuffer;
		//StaticJsonBuffer<DEVICE_SENSORS_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_RESPONSE_JSON_SIZE> jsonBuffer;

		JsonObject& deviceSensorsJO = jsonBuffer.parseObject(json);
		
		Serial.print(F("Depois deste ponto o uso de memoria do ArduinoJson permanece o mesmo."));
		Serial.print(F("FreeHeap pos buffer deviceSensorsJO: "));
		Serial.println(ESP.getFreeHeap());
		
		if (!deviceSensorsJO.success()) {
			Serial.print(F("[DeviceSensors::setSensorsByMQQTCallback] parse failed: "));
			Serial.println(json);
			return;
		}

		Serial.print(F("FreeHeap pre buffer sensorDatasheetsJA: "));
		Serial.println(ESP.getFreeHeap());
		JsonArray& sensorDatasheetsJA = deviceSensorsJO["sensorDatasheets"];
		Serial.print(F("FreeHeap pos buffer sensorDatasheetsJA: "));
		Serial.println(ESP.getFreeHeap());

		Serial.print(F("FreeHeap pre buffer sensorsInDeviceJA: "));
		Serial.println(ESP.getFreeHeap());
		JsonArray& sensorsInDeviceJA = deviceSensorsJO["sensorsInDevice"];
		Serial.print(F("FreeHeap pos buffer sensorsInDeviceJA: "));
		Serial.println(ESP.getFreeHeap());

		_readIntervalInMilliSeconds = deviceSensorsJO["readIntervalInMilliSeconds"];
		_publishIntervalInMilliSeconds = deviceSensorsJO["publishIntervalInMilliSeconds"];

		// sensorDatasheets

		for (JsonArray::iterator it = sensorDatasheetsJA.begin(); it != sensorDatasheetsJA.end(); ++it)
		{
			Serial.print(F("FreeHeap pre _sensorDatasheets.push_back: "));
			Serial.println(ESP.getFreeHeap());

			JsonObject& sensorDatasheetJO = it->as<JsonObject>();
			_sensorDatasheets.push_back(new SensorDatasheet(this, sensorDatasheetJO));

			Serial.print(F("FreeHeap pos _sensorDatasheets.push_back: "));
			Serial.println(ESP.getFreeHeap());
		}

		//sensorsInDevice

		for (JsonArray::iterator it = sensorsInDeviceJA.begin(); it != sensorsInDeviceJA.end(); ++it)
		{
			Serial.print(F("FreeHeap pre _sensorsInDevice.push_back: "));
			Serial.println(ESP.getFreeHeap());

			JsonObject& sensorInDeviceJsonObject = it->as<JsonObject>();
			JsonObject& sensorJsonObject = sensorInDeviceJsonObject["sensor"].as<JsonObject>();			

			// SensorInDevice
			_sensorsInDevice.push_back(new SensorInDevice(this, sensorInDeviceJsonObject));

			// DeviceAddress
			DeviceAddress 	deviceAddress;
			for (uint8_t i = 0; i < 8; i++) deviceAddress[i] = sensorJsonObject["deviceAddress"][i];			

			int resolution = int(sensorJsonObject["resolutionBits"]);			

			_dallas.setResolution(deviceAddress, resolution);
			_dallas.resetAlarmSearch();

			Serial.print(F("FreeHeap pos _sensorsInDevice.push_back: "));
			Serial.println(ESP.getFreeHeap());
		}

		Serial.print(F("FreeHeap Final: "));
		Serial.println(ESP.getFreeHeap());
	}

	bool DeviceSensors::read()
	{
		if (!initialized()) return false;

		uint64_t now = millis();

		if (now - _readIntervalTimestamp > _readIntervalInMilliSeconds) {
			_readIntervalTimestamp = now;
		}
		else {
			return false;
		}

		bool hasAlarm = false;
		bool hasAlarmBuzzer = false;

		_dallas.requestTemperatures();

		for (int i = 0; i < this->_sensorsInDevice.size(); ++i) {
			Sensor* sensor = _sensorsInDevice[i]->getSensor();
			sensor->setConnected(_dallas.isConnected(sensor->getDeviceAddress()));
			sensor->getSensorTempDSFamily()->setResolution(_dallas.getResolution(sensor->getDeviceAddress()));
			sensor->setValue(_dallas.getTempC(sensor->getDeviceAddress()));

			if (sensor->hasAlarm()) 		hasAlarm = true;
			if (sensor->hasAlarmBuzzer()) 	hasAlarmBuzzer = true;
		}
		if (hasAlarmBuzzer) {
			_espDevice->getDeviceBuzzer()->test();
		}

		return true;
	}

	bool DeviceSensors::publish()
	{
		if (!initialized()) return false;

		uint64_t now = millis();

		if (now - _publishIntervalTimestamp > _publishIntervalInMilliSeconds) {
			_publishIntervalTimestamp = now;
		}
		else {
			return false;
		}

		DynamicJsonBuffer jsonBuffer;
		JsonObject& root = jsonBuffer.createObject();

		root["deviceId"] = _espDevice->getDeviceId();
		root["deviceDatasheetId"] = _espDevice->getDeviceDatasheetId();
		root["epochTimeUtc"] = _espDevice->getDeviceNTP()->getEpochTimeUTC();

		createSensorsJsonNestedArray(root);

		int messageJsonLen = root.measureLength();
		char messageJson[messageJsonLen + 1];

		root.printTo(messageJson, sizeof(messageJson));

		Serial.print(F("DeviceSensors enviando para o servidor (Char Len)=> "));
		Serial.println(messageJsonLen);

		_espDevice->getDeviceMQ()->publishInApplication(DEVICE_SENSORS_MESSAGE_TOPIC_PUB, messageJson);

		return true;
	}

	ESPDevice * DeviceSensors::getESPDevice()
	{
		return _espDevice;
	}

	SensorInDevice ** DeviceSensors::getSensorsInDevice()
	{
		SensorInDevice** array = this->_sensorsInDevice.data();
		return array;
	}

	SensorDatasheet * DeviceSensors::getSensorDatasheetByKey(SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId)
	{
		for (int i = 0; i < _sensorDatasheets.size(); ++i) {
			if (_sensorDatasheets[i]->getSensorDatasheetId() == sensorDatasheetId && _sensorDatasheets[i]->getSensorTypeId() == sensorTypeId) {
				return _sensorDatasheets[i];
			}
		}
	}

	void DeviceSensors::createSensorsJsonNestedArray(JsonObject& jsonObject)
	{
		JsonArray& jsonArray = jsonObject.createNestedArray("sensorsInDevice");
		for (int i = 0; i < this->_sensorsInDevice.size(); ++i) {
			createSensorJsonNestedObject(_sensorsInDevice[i]->getSensor(), jsonArray);
		}
	}

	void DeviceSensors::setLabel(const char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);

		if (!root.success()) {
			Serial.print(F("parse setLabel failed: "));
			Serial.println(json);
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		char* label = strdup(root["label"]);

		Sensor* sensor = getSensorById(sensorId);

		sensor->setLabel(label);

		Serial.print(F("setLabel="));
		Serial.println(label);
	}

	void DeviceSensors::setDatasheetUnitMeasurementScale(const char* json)
	{
		Serial.println(F("[DeviceSensors] setUnitOfMeasurement"));

		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setDatasheetUnitMeasurementScale", "Parse failed: %s\n", json);
			return;
		}		

		char* sensorId = strdup(root["sensorId"]);
		UnitMeasurementEnum unitMeasurementId = static_cast<UnitMeasurementEnum>(root["unitMeasurementId"].as<int>());

		Sensor* sensor = getSensorById(sensorId);
		SensorUnitMeasurementScale* sensorUnitMeasurementScale = sensor->getSensorUnitMeasurementScale();

		sensorUnitMeasurementScale->setUnitMeasurementId(unitMeasurementId);		
	}

	void DeviceSensors::setResolution(const char* json)
	{
		Serial.println(F("[DeviceSensors] setResolution"));
		Serial.println(json);

		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setResolution", "Parse failed: %s\n", json);
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		int value = root["dsFamilyTempSensorResolutionId"];

		Sensor* sensor = getSensorById(sensorId);

		sensor->getSensorTempDSFamily()->setResolution(value);
		_dallas.setResolution(sensor->getDeviceAddress(), value);		
	}

	void DeviceSensors::setOrdination(const char * json)
	{
		Serial.print(F("[DeviceSensors::setOrdination] "));

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setOrdination", "Parse failed: %s\n", json);
			return;
		}

		root.printTo(Serial);

		char* sensorId = strdup(root["sensorId"]);
		short ordination = root["ordination"];

		short orderedExceptCurrentSize = _sensorsInDevice.size() - 1;

		SensorInDevice** orderedExceptCurrent = new SensorInDevice*[orderedExceptCurrentSize];

		short index = 0;

		for (int i = 0; i < _sensorsInDevice.size(); ++i) {
			if (stricmp(_sensorsInDevice[i]->getSensor()->getSensorId(), sensorId) == 0) {
				_sensorsInDevice[i]->setOrdination(ordination);
			}
			else {
				orderedExceptCurrent[index] = _sensorsInDevice[i];
				index++;
			}
		}

		short counter = 0;

		for (short i = 0; i < orderedExceptCurrentSize; ++i) {
			if (i == ordination) {				
				counter++;		
			}
			orderedExceptCurrent[i]->setOrdination(counter);
			counter++;
		}		

		std::sort(_sensorsInDevice.begin(), _sensorsInDevice.end());
	}

	void DeviceSensors::insertTrigger(const char * json)
	{
		Serial.println(F("[DeviceSensors::insertTrigger] "));

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "insertTrigger", "Parse failed: %s\n", json);
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		Sensor* sensor = getSensorById(sensorId);
		sensor->insertTrigger(root);
	}

	void DeviceSensors::deleteTrigger(const char * json)
	{
		Serial.println(F("[DeviceSensors::deleteTrigger] "));

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "deleteTrigger", "Parse failed: %s\n", json);
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		char* sensorTriggerId = strdup(root["sensorTriggerId"]);

		Sensor* sensor = getSensorById(sensorId);

		sensor->deleteTrigger(sensorTriggerId);
	}

	void DeviceSensors::setTriggerOn(const char* json)
	{
		Serial.println(F("[DeviceSensors::setTriggerOn] "));

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setTriggerOn", "Parse failed: %s\n", json);
			return;
		}

		root.printTo(Serial);

		char* sensorId = strdup(root["sensorId"]);
		char* sensorTriggerId = strdup(root["sensorTriggerId"]);
		bool triggerOn = root["triggerOn"];

		SensorTrigger* sensorTrigger = getSensorTriggerByKey(sensorId, sensorTriggerId);

		sensorTrigger->setTriggerOn(triggerOn);
	}

	void DeviceSensors::setBuzzerOn(const char* json)
	{
		Serial.print(F("[DeviceSensors::setBuzzerOn] "));

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setBuzzerOn", "Parse failed: %s\n", json);
			return;
		}

		root.printTo(Serial);

		char* sensorId = strdup(root["sensorId"]);
		char* sensorTriggerId = strdup(root["sensorTriggerId"]);
		bool buzzerOn = root["buzzerOn"];

		SensorTrigger* sensorTrigger = getSensorTriggerByKey(sensorId, sensorTriggerId);

		sensorTrigger->setBuzzerOn(buzzerOn);
	}

	void DeviceSensors::setTriggerValue(const char* json)
	{
		Serial.print(F("[DeviceSensors::setTriggerValue] "));

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setTriggerValue", "Parse failed: %s\n", json);
			return;
		}

		root.printTo(Serial);

		char* sensorId = strdup(root["sensorId"]);
		char* sensorTriggerId = strdup(root["sensorTriggerId"]);
		float triggerValue = root["triggerValue"];
		PositionEnum position = static_cast<PositionEnum>(root["position"].as<int>());

		SensorTrigger* sensorTrigger = getSensorTriggerByKey(sensorId, sensorTriggerId);

		if (position == Max)
			sensorTrigger->setMax(triggerValue);
		else if (position == Min)
			sensorTrigger->setMin(triggerValue);
	}	

	void DeviceSensors::setRange(const char* json)
	{
		Serial.print(F("[DeviceSensors::setRange] "));

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setRange", "Parse failed: %s\n", json);
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		float chartLimiterCelsius = root["value"];
		PositionEnum position = static_cast<PositionEnum>(root["position"].as<int>());

		Sensor* sensor = getSensorById(sensorId);

		if (position == Max)
			sensor->getSensorUnitMeasurementScale()->setRangeMax(chartLimiterCelsius);
		else if (position == Min)
			sensor->getSensorUnitMeasurementScale()->setRangeMin(chartLimiterCelsius);
	}

	void DeviceSensors::setChartLimiter(const char* json)
	{
		Serial.print(F("[DeviceSensors::setChartLimiter] "));

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setChartLimiter", "Parse failed: %s\n", json);
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		float chartLimiterCelsius = root["value"];
		PositionEnum position = static_cast<PositionEnum>(root["position"].as<int>());

		Sensor* sensor = getSensorById(sensorId);

		if (position == Max)
			sensor->getSensorUnitMeasurementScale()->setChartLimiterMax(chartLimiterCelsius);
		else if (position == Min)
			sensor->getSensorUnitMeasurementScale()->setChartLimiterMin(chartLimiterCelsius);
	}	

	SensorInDevice* DeviceSensors::getSensorInDeviceBySensorId(char* sensorId) {
		for (int i = 0; i < _sensorsInDevice.size(); ++i) {
			if (stricmp(_sensorsInDevice[i]->getSensor()->getSensorId(), sensorId) == 0) {
				return _sensorsInDevice[i];
			}
		}
	}

	Sensor * DeviceSensors::getSensorById(char * sensorId)
	{
		return getSensorInDeviceBySensorId(sensorId)->getSensor();
	}

	SensorTrigger* DeviceSensors::getSensorTriggerByKey(char * sensorId, char * sensorTriggerId)
	{
		SensorTrigger** sensorTriggers = getSensorById(sensorId)->getSensorTriggers();
		int count = sizeof(SensorTrigger);
		for (int i = 0; i < count; ++i) {
			if (stricmp(sensorTriggers[i]->getSensorTriggerId(), sensorTriggerId) == 0) {
				return sensorTriggers[i];
			}
		}
	}

	void DeviceSensors::createSensorJsonNestedObject(Sensor* sensor, JsonArray& root)
	{
		JsonObject& JSONencoder = root.createNestedObject();

		JSONencoder["sensorId"] = sensor->getSensorId();
		JSONencoder["isConnected"] = sensor->getConnected();
		JSONencoder["resolution"] = sensor->getSensorTempDSFamily()->getResolution();
		JSONencoder["value"] = sensor->getValue();
	}

	String DeviceSensors::convertDeviceAddressToString(const uint8_t* deviceAddress)
	{
		String result;
		for (uint8_t i = 0; i < 8; i++)
		{
			result += String(deviceAddress[i]);
			if (i < 7) result += ":";
		}
		return result;
	}

	long DeviceSensors::getPublishIntervalInMilliSeconds()
	{
		return _publishIntervalInMilliSeconds;
	}

	void DeviceSensors::setPublishIntervalInMilliSeconds(const char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setPublishIntervalInMilliSeconds", "Parse failed: %s\n", json);
			return;
		}
		_publishIntervalInMilliSeconds = root["value"].as<int>();
	}

	long DeviceSensors::getReadIntervalInMilliSeconds()
	{
		return _readIntervalInMilliSeconds;
	}

	void DeviceSensors::setReadIntervalInMilliSeconds(const char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setReadIntervalInMilliSeconds", "Parse failed: %s\n", json);
			return;
		}
		_readIntervalInMilliSeconds = root["value"].as<int>();
	}

	void DeviceSensors::onDeviceMQSubscribeDeviceInApplication()
	{
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_SENSORS_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_SENSORS_SET_READ_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_SENSORS_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB);

		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(SENSOR_IN_DEVICE_SET_ORDINATION_TOPIC_SUB);

		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(SENSOR_SET_LABEL_TOPIC_SUB);

		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(SENSOR_TEMP_DS_FAMILY_SET_RESOLUTION_TOPIC_SUB);

		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(SENSOR_TRIGGER_INSERT_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(SENSOR_TRIGGER_DELETE_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(SENSOR_TRIGGER_SET_TRIGGER_ON_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(SENSOR_TRIGGER_SET_BUZZER_ON_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(SENSOR_TRIGGER_SET_TRIGGER_VALUE_TOPIC_SUB);

		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(SENSOR_UNIT_MEASUREMENT_SCALE_SET_DATASHEET_UNIT_MEASUREMENT_SCALE_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(SENSOR_UNIT_MEASUREMENT_SCALE_RANGE_SET_VALUE_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(SENSOR_UNIT_MEASUREMENT_SCALE_CHART_LIMITER_SET_VALUE_TOPIC_SUB);
	}

	void DeviceSensors::onDeviceMQUnSubscribeDeviceInApplication()
	{
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_SENSORS_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_SENSORS_SET_READ_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_SENSORS_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB);

		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(SENSOR_IN_DEVICE_SET_ORDINATION_TOPIC_SUB);

		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(SENSOR_SET_LABEL_TOPIC_SUB);

		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(SENSOR_TEMP_DS_FAMILY_SET_RESOLUTION_TOPIC_SUB);

		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(SENSOR_TRIGGER_INSERT_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(SENSOR_TRIGGER_DELETE_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(SENSOR_TRIGGER_SET_TRIGGER_ON_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(SENSOR_TRIGGER_SET_BUZZER_ON_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(SENSOR_TRIGGER_SET_TRIGGER_VALUE_TOPIC_SUB);

		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(SENSOR_UNIT_MEASUREMENT_SCALE_SET_DATASHEET_UNIT_MEASUREMENT_SCALE_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(SENSOR_UNIT_MEASUREMENT_SCALE_RANGE_SET_VALUE_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(SENSOR_UNIT_MEASUREMENT_SCALE_CHART_LIMITER_SET_VALUE_TOPIC_SUB);
	}

	bool DeviceSensors::onDeviceMQSubscription(char* topicKey, char* json)
	{
		if (strcmp(topicKey, DEVICE_SENSORS_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED_TOPIC_SUB) == 0) {
			setSensorsByMQQTCallback(json);
			return true;
		}
		else if (strcmp(topicKey, DEVICE_SENSORS_SET_READ_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB) == 0) {
			setReadIntervalInMilliSeconds(json);
			return true;
		}
		else if (strcmp(topicKey, DEVICE_SENSORS_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB) == 0) {
			setPublishIntervalInMilliSeconds(json);
			return true;
		}

		else if (strcmp(topicKey, SENSOR_IN_DEVICE_SET_ORDINATION_TOPIC_SUB) == 0) {
			setOrdination(json);
			return true;
		}

		else if (strcmp(topicKey, SENSOR_SET_LABEL_TOPIC_SUB) == 0) {
			setLabel(json);
			return true;
		}

		else if (strcmp(topicKey, SENSOR_TEMP_DS_FAMILY_SET_RESOLUTION_TOPIC_SUB) == 0) {
			setResolution(json);
			return true;
		}

		else if (strcmp(topicKey, SENSOR_TRIGGER_INSERT_TOPIC_SUB) == 0) {
			insertTrigger(json);
			return true;
		}
		else if (strcmp(topicKey, SENSOR_TRIGGER_DELETE_TOPIC_SUB) == 0) {
			deleteTrigger(json);
			return true;
		}
		else if (strcmp(topicKey, SENSOR_TRIGGER_SET_TRIGGER_ON_TOPIC_SUB) == 0) {
			setTriggerOn(json);
			return true;
		}
		else if (strcmp(topicKey, SENSOR_TRIGGER_SET_BUZZER_ON_TOPIC_SUB) == 0) {
			setBuzzerOn(json);
			return true;
		}
		else if (strcmp(topicKey, SENSOR_TRIGGER_SET_TRIGGER_VALUE_TOPIC_SUB) == 0) {
			setTriggerValue(json);
			return true;
		}

		else if (strcmp(topicKey, SENSOR_UNIT_MEASUREMENT_SCALE_SET_DATASHEET_UNIT_MEASUREMENT_SCALE_TOPIC_SUB) == 0) {
			setDatasheetUnitMeasurementScale(json);
			return true;
		}
		else if (strcmp(topicKey, SENSOR_UNIT_MEASUREMENT_SCALE_RANGE_SET_VALUE_TOPIC_SUB) == 0) {
			setRange(json);
			return true;
		}
		else if (strcmp(topicKey, SENSOR_UNIT_MEASUREMENT_SCALE_CHART_LIMITER_SET_VALUE_TOPIC_SUB) == 0) {
			setChartLimiter(json);
			return true;
		}
		else {
			return false;
		}
	}
}
