#include "DeviceSensor.h"

#include "algorithm"    // std::sort

#include "../ESPDevice/ESPDevice.h"
#include "SensorInDevice.h"
#include "PositionEnum.h"
#include "../ESPDevice/DeviceTypeEnum.h"

namespace ART
{
	DeviceSensor::DeviceSensor(ESPDevice* espDevice)
	{
		Serial.println(F("[DeviceSensor constructor]"));

		_espDevice = espDevice;
		
		_oneWire = new OneWire(ONE_WIRE_BUS);
		_dallas = new DallasTemperature(_oneWire);

		_initializing = false;
		_initialized = false;

		_readIntervalTimestamp = 0;
		_readIntervalInMilliSeconds = 0;

		_publishIntervalTimestamp = 0;
		_publishIntervalInMilliSeconds = 0;
	}

	DeviceSensor::~DeviceSensor()
	{
		Serial.println(F("[DeviceSensor destructor]"));

		delete (_espDevice);
	}

	void DeviceSensor::begin()
	{
		Serial.println(F("[DeviceSensor::begin] begin"));

		_espDevice->getDeviceMQ()->addSubscriptionCallback([=](const char* topicKey, const char* json) { return onDeviceMQSubscription(topicKey, json); });
		_espDevice->getDeviceMQ()->addSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQSubscribeDeviceInApplication(); });
		_espDevice->getDeviceMQ()->addUnSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQUnSubscribeDeviceInApplication(); });
		
		_dallas->begin();
		initialized();

		Serial.println(F("[DeviceSensor::begin] end"));
	}

	bool DeviceSensor::initialized()
	{
		if (_initialized) return true;

		if (!_espDevice->loaded()) {
			Serial.println(F("[DeviceSensor::initialized] espDevice not loaded"));
			return false;
		}

		if (!_espDevice->getDeviceMQ()->connected()) {
			Serial.println(F("[DeviceSensor::initialized] deviceMQ not connected"));
			return false;
		}

		if (_initializing) {
			Serial.println(F("[DeviceSensor::initialized] initializing: true"));
			return false;
		}

		// initializing

		_initializing = true;

		Serial.println(F("[DeviceSensor::initialized] begin]"));		

		/*
		//device addresses prepare	
		uint8_t deviceCount = _dallas->getDeviceCount();
		if (deviceCount > 0) {
			JsonArray& deviceAddressJsonArray = root.createNestedArray("deviceAddresses");
			for (int i = 0; i < deviceCount; ++i) {
				DeviceAddress deviceAddress;
				if (_dallas->getAddress(deviceAddress, i))
				{
					deviceAddressJsonArray.add(this->convertDeviceAddressToString(deviceAddress));
				}
				else {
					Serial.print(F("Não foi possível encontrar um endereço para o Device: "));
					Serial.println(i);
				}
			}
		}
		*/		

		_espDevice->getDeviceMQ()->publishInApplication(DEVICE_SENSOR_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_TOPIC_PUB, _espDevice->getDeviceKeyAsJson());

		Serial.println(F("[DeviceSensor::initialized] end"));

		return true;
	}

	void DeviceSensor::setSensorsByMQQTCallback(const char* json)
	{
		Serial.println(F("[DeviceSensor::setSensorsByMQQTCallback] begin"));

		Serial.print(F("FreeHeap pre buffer deviceSensorJO: "));
		Serial.println(ESP.getFreeHeap());

		this->_initialized = true;
		this->_initializing = false;

		DynamicJsonBuffer jsonBuffer;
		//StaticJsonBuffer<DEVICE_SENSOR_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_RESPONSE_JSON_SIZE> jsonBuffer;

		JsonObject& deviceSensorJO = jsonBuffer.parseObject(json);
		
		Serial.print(F("Depois deste ponto o uso de memoria do ArduinoJson permanece o mesmo."));
		Serial.print(F("FreeHeap pos buffer deviceSensorJO: "));
		Serial.println(ESP.getFreeHeap());
		
		if (!deviceSensorJO.success()) {
			Serial.print(F("[DeviceSensor::setSensorsByMQQTCallback] parse failed: "));
			Serial.println(json);
			return;
		}

		Serial.print(F("FreeHeap pre buffer sensorDatasheetsJA: "));
		Serial.println(ESP.getFreeHeap());
		JsonArray& sensorDatasheetsJA = deviceSensorJO["sensorDatasheets"];
		Serial.print(F("FreeHeap pos buffer sensorDatasheetsJA: "));
		Serial.println(ESP.getFreeHeap());

		Serial.print(F("FreeHeap pre buffer sensorsInDeviceJA: "));
		Serial.println(ESP.getFreeHeap());
		JsonArray& sensorsInDeviceJA = deviceSensorJO["sensorsInDevice"];
		Serial.print(F("FreeHeap pos buffer sensorsInDeviceJA: "));
		Serial.println(ESP.getFreeHeap());

		_readIntervalInMilliSeconds = deviceSensorJO["readIntervalInMilliSeconds"];
		_publishIntervalInMilliSeconds = deviceSensorJO["publishIntervalInMilliSeconds"];

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

			_dallas->setResolution(deviceAddress, resolution);
			_dallas->resetAlarmSearch();

			Serial.print(F("FreeHeap pos _sensorsInDevice.push_back: "));
			Serial.println(ESP.getFreeHeap());
		}

		Serial.print(F("FreeHeap Final: "));
		Serial.println(ESP.getFreeHeap());
	}

	bool DeviceSensor::read()
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

		_dallas->requestTemperatures();

		for (int i = 0; i < this->_sensorsInDevice.size(); ++i) {
			Sensor* sensor = _sensorsInDevice[i]->getSensor();
			sensor->setConnected(_dallas->isConnected(sensor->getDeviceAddress()));
			sensor->getSensorTempDSFamily()->setResolution(_dallas->getResolution(sensor->getDeviceAddress()));
			sensor->setValue(_dallas->getTempC(sensor->getDeviceAddress()));

			if (sensor->hasAlarm()) 		hasAlarm = true;
			if (sensor->hasAlarmBuzzer()) 	hasAlarmBuzzer = true;
		}
		if (hasAlarmBuzzer) {
			_espDevice->getDeviceBuzzer()->test();
		}

		return true;
	}

	bool DeviceSensor::publish()
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

		root["deviceTypeId"] = DeviceTypeEnumConverter::convertToString(_espDevice->getDeviceTypeId());
		root["deviceDatasheetId"] = _espDevice->getDeviceDatasheetId();
		root["deviceId"] = _espDevice->getDeviceId();		
		root["epochTimeUtc"] = _espDevice->getDeviceNTP()->getEpochTimeUTC();

		createSensorsJsonNestedArray(root);

		int messageJsonLen = root.measureLength();
		char messageJson[messageJsonLen + 1];

		root.printTo(messageJson, sizeof(messageJson));

		Serial.print(F("DeviceSensor enviando para o servidor (Char Len)=> "));
		Serial.println(messageJsonLen);

		_espDevice->getDeviceMQ()->publishInApplication(DEVICE_SENSOR_MESSAGE_TOPIC_PUB, messageJson);

		return true;
	}

	ESPDevice * DeviceSensor::getESPDevice()
	{
		return _espDevice;
	}

	std::tuple<SensorInDevice**, short> DeviceSensor::getSensorsInDevice()
	{
		SensorInDevice** array = this->_sensorsInDevice.data();
		return std::make_tuple(array, _sensorsInDevice.size());
	}

	SensorDatasheet * DeviceSensor::getSensorDatasheetByKey(SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId)
	{
		for (int i = 0; i < _sensorDatasheets.size(); ++i) {
			if (_sensorDatasheets[i]->getSensorDatasheetId() == sensorDatasheetId && _sensorDatasheets[i]->getSensorTypeId() == sensorTypeId) {
				return _sensorDatasheets[i];
			}
		}
	}

	void DeviceSensor::createSensorsJsonNestedArray(JsonObject& jsonObject)
	{
		JsonArray& jsonArray = jsonObject.createNestedArray("sensorsInDevice");
		for (int i = 0; i < this->_sensorsInDevice.size(); ++i) {
			createSensorJsonNestedObject(_sensorsInDevice[i]->getSensor(), jsonArray);
		}
	}

	void DeviceSensor::setLabel(const char* json)
	{
		Serial.println(F("[DeviceSensor::setLabel] begin"));

		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);

		if (!root.success()) {
			Serial.print(F("parse setLabel failed: "));
			Serial.println(json);
			return;
		}

		Sensor* sensor = getSensorById(root["sensorId"]);

		sensor->setLabel(root["label"]);

		Serial.println(F("[DeviceSensor::setLabel] end"));
	}

	void DeviceSensor::setDatasheetUnitMeasurementScale(const char* json)
	{
		Serial.println(F("[DeviceSensor::setUnitOfMeasurement] begin"));

		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensor", "setDatasheetUnitMeasurementScale", "Parse failed: %s\n", json);
			return;
		}		

		UnitMeasurementEnum unitMeasurementId = static_cast<UnitMeasurementEnum>(root["unitMeasurementId"].as<int>());

		Sensor* sensor = getSensorById(root["sensorId"]);
		SensorUnitMeasurementScale* sensorUnitMeasurementScale = sensor->getSensorUnitMeasurementScale();

		sensorUnitMeasurementScale->setUnitMeasurementId(unitMeasurementId);

		Serial.println(F("[DeviceSensor::setUnitOfMeasurement] end"));
	}

	void DeviceSensor::setResolution(const char* json)
	{
		Serial.println(F("[DeviceSensor::setResolution] begin"));		

		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensor", "setResolution", "Parse failed: %s\n", json);
			return;
		}

		int value = root["dsFamilyTempSensorResolutionId"];

		Sensor* sensor = getSensorById(root["sensorId"]);

		sensor->getSensorTempDSFamily()->setResolution(value);
		_dallas->setResolution(sensor->getDeviceAddress(), value);

		Serial.println(F("[DeviceSensor::setResolution] end"));
	}

	void DeviceSensor::setOrdination(const char * json)
	{
		Serial.print(F("[DeviceSensor::setOrdination] begin\n"));

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensor", "setOrdination", "Parse failed: %s\n", json);
			return;
		}

		short ordination = root["ordination"];

		short orderedExceptCurrentSize = _sensorsInDevice.size() - 1;

		SensorInDevice** orderedExceptCurrent = new SensorInDevice*[orderedExceptCurrentSize];

		short index = 0;

		for (int i = 0; i < _sensorsInDevice.size(); ++i) {
			if (stricmp(_sensorsInDevice[i]->getSensor()->getSensorId(), root["sensorId"]) == 0) {
				_sensorsInDevice[i]->setOrdination(ordination);
				Serial.print(F("[DeviceSensor::setOrdination] sensorId: "));
				Serial.print(_sensorsInDevice[i]->getSensor()->getSensorId());
				Serial.print(F(" ordination:"));
				Serial.println(ordination);
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
			Serial.print(F("[DeviceSensor::setOrdination] sensorId: "));
			Serial.print(orderedExceptCurrent[i]->getSensor()->getSensorId());
			Serial.print(F(" ordination:"));
			Serial.println(counter);
			counter++;
		}				

		std::sort(_sensorsInDevice.begin(), _sensorsInDevice.end(), [=](SensorInDevice * a, SensorInDevice * b) { return compareSensorInDevice(a, b); });

		Serial.print(F("[DeviceSensor::setOrdination] end\n"));
	}

	bool DeviceSensor::compareSensorInDevice(SensorInDevice * a, SensorInDevice * b)
	{
		return a->getOrdination() < b->getOrdination();
	}

	void DeviceSensor::insertTrigger(const char * json)
	{
		Serial.println(F("[DeviceSensor::insertTrigger] begin"));

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensor", "insertTrigger", "Parse failed: %s\n", json);
			return;
		}

		Sensor* sensor = getSensorById(root["sensorId"]);

		sensor->insertTrigger(root);

		Serial.println(F("[DeviceSensor::insertTrigger] end"));
	}

	bool DeviceSensor::deleteTrigger(const char * json)
	{
		Serial.println(F("[DeviceSensor::deleteTrigger] begin"));

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensor", "deleteTrigger", "Parse failed: %s\n", json);
			return false;
		}

		Sensor* sensor = getSensorById(root["sensorId"]);

		return sensor->deleteTrigger(root["sensorTriggerId"]);
	}

	void DeviceSensor::setTriggerOn(const char* json)
	{
		Serial.println(F("[DeviceSensor::setTriggerOn] begin"));

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensor", "setTriggerOn", "Parse failed: %s\n", json);
			return;
		}

		SensorTrigger* sensorTrigger = getSensorTriggerByKey(root["sensorId"], root["sensorTriggerId"]);

		sensorTrigger->setTriggerOn(root["triggerOn"]);

		Serial.println(F("[DeviceSensor::setTriggerOn] end"));
	}

	void DeviceSensor::setBuzzerOn(const char* json)
	{
		Serial.println(F("[DeviceSensor::setBuzzerOn] begin"));

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensor", "setBuzzerOn", "Parse failed: %s\n", json);
			return;
		}

		SensorTrigger* sensorTrigger = getSensorTriggerByKey(root["sensorId"], root["sensorTriggerId"]);

		sensorTrigger->setBuzzerOn(root["buzzerOn"]);

		Serial.println(F("[DeviceSensor::setBuzzerOn] end"));
	}

	void DeviceSensor::setTriggerValue(const char* json)
	{
		Serial.println(F("[DeviceSensor::setTriggerValue] begin"));

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensor", "setTriggerValue", "Parse failed: %s\n", json);
			return;
		}

		float triggerValue = root["triggerValue"];
		PositionEnum position = static_cast<PositionEnum>(root["position"].as<int>());

		SensorTrigger* sensorTrigger = getSensorTriggerByKey(root["sensorId"], root["sensorTriggerId"]);

		if (position == Max)
			sensorTrigger->setMax(triggerValue);
		else if (position == Min)
			sensorTrigger->setMin(triggerValue);

		Serial.println(F("[DeviceSensor::setTriggerValue] end"));
	}	

	void DeviceSensor::setRange(const char* json)
	{
		Serial.println(F("[DeviceSensor::setRange] begin"));

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensor", "setRange", "Parse failed: %s\n", json);
			return;
		}

		float chartLimiterCelsius = root["value"];
		PositionEnum position = static_cast<PositionEnum>(root["position"].as<int>());

		Sensor* sensor = getSensorById(root["sensorId"]);

		if (position == Max)
			sensor->getSensorUnitMeasurementScale()->setRangeMax(chartLimiterCelsius);
		else if (position == Min)
			sensor->getSensorUnitMeasurementScale()->setRangeMin(chartLimiterCelsius);

		Serial.println(F("[DeviceSensor::setRange] end"));
	}

	void DeviceSensor::setChartLimiter(const char* json)
	{
		Serial.println(F("[DeviceSensor::setChartLimiter] begin"));

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensor", "setChartLimiter", "Parse failed: %s\n", json);
			return;
		}

		float value = root["value"];
		PositionEnum position = static_cast<PositionEnum>(root["position"].as<int>());

		Sensor* sensor = getSensorById(root["sensorId"]);

		if (position == Max)
			sensor->getSensorUnitMeasurementScale()->setChartLimiterMax(value);
		else if (position == Min)
			sensor->getSensorUnitMeasurementScale()->setChartLimiterMin(value);

		Serial.println(F("[DeviceSensor::setChartLimiter] end"));
	}	

	SensorInDevice* DeviceSensor::getSensorInDeviceBySensorId(const char* sensorId) {
		for (int i = 0; i < _sensorsInDevice.size(); ++i) {
			if (stricmp(_sensorsInDevice[i]->getSensor()->getSensorId(), sensorId) == 0) {
				return _sensorsInDevice[i];
			}
		}
	}

	Sensor * DeviceSensor::getSensorById(const char * sensorId)
	{
		return getSensorInDeviceBySensorId(sensorId)->getSensor();
	}

	SensorTrigger* DeviceSensor::getSensorTriggerByKey(const char * sensorId, const char * sensorTriggerId)
	{
		SensorTrigger** sensorTriggers = getSensorById(sensorId)->getSensorTriggers();
		int count = sizeof(SensorTrigger);
		for (int i = 0; i < count; ++i) {
			if (stricmp(sensorTriggers[i]->getSensorTriggerId(), sensorTriggerId) == 0) {
				return sensorTriggers[i];
			}
		}
	}

	void DeviceSensor::createSensorJsonNestedObject(Sensor* sensor, JsonArray& root)
	{
		JsonObject& JSONencoder = root.createNestedObject();

		JSONencoder["sensorTypeId"] = SensorTypeEnumConverter::convertToString(sensor->getSensorTypeId());
		JSONencoder["sensorDatasheetId"] = SensorDatasheetEnumConverter::convertToString(sensor->getSensorDatasheetId());
		JSONencoder["sensorId"] = sensor->getSensorId();
		JSONencoder["isConnected"] = sensor->getConnected();
		JSONencoder["resolution"] = sensor->getSensorTempDSFamily()->getResolution();
		JSONencoder["value"] = sensor->getValue();
	}

	String DeviceSensor::convertDeviceAddressToString(const uint8_t* deviceAddress)
	{
		String result;
		for (uint8_t i = 0; i < 8; i++)
		{
			result += String(deviceAddress[i]);
			if (i < 7) result += ":";
		}
		return result;
	}

	long DeviceSensor::getPublishIntervalInMilliSeconds()
	{
		return _publishIntervalInMilliSeconds;
	}

	void DeviceSensor::setPublishIntervalInMilliSeconds(const char* json)
	{
		Serial.println(F("[DeviceSensor::setPublishIntervalInMilliSeconds] begin"));

		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensor", "setPublishIntervalInMilliSeconds", "Parse failed: %s\n", json);
			return;
		}
		
		_publishIntervalInMilliSeconds = root["value"].as<int>();

		Serial.println(F("[DeviceSensor::setPublishIntervalInMilliSeconds] end"));
	}

	long DeviceSensor::getReadIntervalInMilliSeconds()
	{
		return _readIntervalInMilliSeconds;
	}

	void DeviceSensor::setReadIntervalInMilliSeconds(const char* json)
	{
		Serial.println(F("[DeviceSensor::setReadIntervalInMilliSeconds] begin"));

		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensor", "setReadIntervalInMilliSeconds", "Parse failed: %s\n", json);
			return;
		}
		_readIntervalInMilliSeconds = root["value"].as<int>();

		Serial.println(F("[DeviceSensor::setReadIntervalInMilliSeconds] end"));
	}

	void DeviceSensor::onDeviceMQSubscribeDeviceInApplication()
	{
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_SENSOR_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_SENSOR_SET_READ_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_SENSOR_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB);

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

	void DeviceSensor::onDeviceMQUnSubscribeDeviceInApplication()
	{
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_SENSOR_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_SENSOR_SET_READ_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_SENSOR_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB);

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

	bool DeviceSensor::onDeviceMQSubscription(const char* topicKey, const char* json)
	{
		if (strcmp(topicKey, DEVICE_SENSOR_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED_TOPIC_SUB) == 0) {
			setSensorsByMQQTCallback(json);
			return true;
		}
		else if (strcmp(topicKey, DEVICE_SENSOR_SET_READ_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB) == 0) {
			setReadIntervalInMilliSeconds(json);
			return true;
		}
		else if (strcmp(topicKey, DEVICE_SENSOR_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB) == 0) {
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
