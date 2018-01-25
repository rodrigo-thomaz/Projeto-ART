#include "DeviceSensors.h"
#include "ESPDevice.h"
#include "SensorUnitMeasurementScalePositionEnum.h"

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
	}

	DeviceSensors::~DeviceSensors()
	{
		delete (_espDevice);
	}

	void DeviceSensors::begin()
	{
		_dallas.begin();
		initialized();
	}

	bool DeviceSensors::initialized()
	{
		if (this->_initialized) return true;

		if (!this->_espDevice->loaded()) return false;

		if (this->_initializing) return false;

		PubSubClient* mqqt = _espDevice->getDeviceMQ()->getMQQT();

		if (!mqqt->connected()) return false;

		// initializing

		this->_initializing = true;

		Serial.println("[DeviceSensors::initialized] initializing...]");

		char* deviceId = this->_espDevice->getDeviceId();
		short deviceDatasheetId = this->_espDevice->getDeviceDatasheetId();
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
					Serial.print("Não foi possível encontrar um endereço para o Device: ");
					Serial.println(i);
				}
			}
		}

		int len = root.measureLength();
		char result[len + 1];
		root.printTo(result, sizeof(result));

		_espDevice->getDeviceMQ()->publish(TOPIC_PUB_DEVICE_SENSORS_GET_FULL_BY_DEVICE_IN_APPLICATION_ID, result);

		return true;
	}

	void DeviceSensors::setSensorsByMQQTCallback(String json)
	{
		Serial.println("[DeviceSensors::setSensorsByMQQTCallback] Enter");

		this->_initialized = true;
		this->_initializing = false;

		DynamicJsonBuffer jsonBuffer;
		//StaticJsonBuffer<DEVICE_SENSORS_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_RESPONSE_JSON_SIZE> jsonBuffer;

		JsonObject& sensorInDeviceJO = jsonBuffer.parseObject(json);
		
		if (!sensorInDeviceJO.success()) {
			Serial.print("[DeviceSensors::setSensorsByMQQTCallback] parse failed: ");
			Serial.println(json);
			return;
		}

		JsonArray& sensorDatasheetsJA = sensorInDeviceJO["sensorDatasheets"];
		JsonArray& sensorsInDeviceJA = sensorInDeviceJO["sensorsInDevice"];

		_publishIntervalInMilliSeconds = sensorInDeviceJO["publishIntervalInMilliSeconds"];

		// sensorDatasheets

		for (JsonArray::iterator it = sensorDatasheetsJA.begin(); it != sensorDatasheetsJA.end(); ++it)
		{
			JsonObject& sensorDatasheetJO = it->as<JsonObject>();
			_sensorDatasheets.push_back(SensorDatasheet::create(this, sensorDatasheetJO));
		}

		//sensorsInDevice

		for (JsonArray::iterator it = sensorsInDeviceJA.begin(); it != sensorsInDeviceJA.end(); ++it)
		{
			JsonObject& sensorInDeviceJsonObject = it->as<JsonObject>();
			JsonObject& sensorJsonObject = sensorInDeviceJsonObject["sensor"].as<JsonObject>();			

			// SensorInDevice
			_sensorsInDevice.push_back(SensorInDevice::create(this, sensorInDeviceJsonObject));

			// DeviceAddress
			DeviceAddress 	deviceAddress;
			for (uint8_t i = 0; i < 8; i++) deviceAddress[i] = sensorJsonObject["deviceAddress"][i];			

			int resolution = int(sensorJsonObject["resolutionBits"]);			

			_dallas.setResolution(deviceAddress, resolution);
			_dallas.resetAlarmSearch();
		}
	}

	void DeviceSensors::refresh()
	{
		bool hasAlarm = false;
		bool hasAlarmBuzzer = false;

		_dallas.requestTemperatures();
		for (int i = 0; i < this->_sensorsInDevice.size(); ++i) {
			Sensor* sensor = _sensorsInDevice[i].getSensor();
			sensor->setConnected(_dallas.isConnected(sensor->getDeviceAddress()));
			sensor->getSensorTempDSFamily()->setResolution(_dallas.getResolution(sensor->getDeviceAddress()));
			sensor->setTempCelsius(_dallas.getTempC(sensor->getDeviceAddress()));

			if (sensor->hasAlarm()) 		hasAlarm = true;
			if (sensor->hasAlarmBuzzer()) 	hasAlarmBuzzer = true;
		}
		if (hasAlarmBuzzer) {
			_espDevice->getDeviceBuzzer()->test();
		}
	}

	SensorInDevice *DeviceSensors::getSensorsInDevice()
	{
		SensorInDevice* array = this->_sensorsInDevice.data();
		return array;
	}

	SensorDatasheet& DeviceSensors::getSensorDatasheetByKey(SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId)
	{
		for (int i = 0; i < _sensorDatasheets.size(); ++i) {

			if (_sensorDatasheets[i].getSensorDatasheetId() == sensorDatasheetId && _sensorDatasheets[i].getSensorTypeId() == sensorTypeId) {
				return _sensorDatasheets[i];
			}
		}
	}

	void DeviceSensors::createSensorsJsonNestedArray(JsonObject& jsonObject)
	{
		JsonArray& jsonArray = jsonObject.createNestedArray("dsFamilyTempSensors");
		for (int i = 0; i < this->_sensorsInDevice.size(); ++i) {
			createSensorJsonNestedObject(_sensorsInDevice[i].getSensor(), jsonArray);
		}
	}

	void DeviceSensors::setLabel(char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);

		if (!root.success()) {
			Serial.print("parse setLabel failed: ");
			Serial.println(json);
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		char* label = strdup(root["label"]);

		Sensor* sensor = getSensorInDeviceBySensorKey(sensorId).getSensor();

		sensor->setLabel(label);

		Serial.print("setLabel=");
		Serial.println(label);
	}

	void DeviceSensors::setDatasheetUnitMeasurementScale(char* json)
	{
		Serial.println("[DeviceSensors] setUnitOfMeasurement");

		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setDatasheetUnitMeasurementScale", "Parse failed: %s\n", json);
			return;
		}		

		char* sensorId = strdup(root["sensorId"]);
		UnitMeasurementEnum unitMeasurementId = static_cast<UnitMeasurementEnum>(root["unitMeasurementId"].as<int>());

		Sensor* sensor = getSensorInDeviceBySensorKey(sensorId).getSensor();
		SensorUnitMeasurementScale* sensorUnitMeasurementScale = sensor->getSensorUnitMeasurementScale();

		sensorUnitMeasurementScale->setUnitMeasurementId(unitMeasurementId);		
	}

	void DeviceSensors::setResolution(char* json)
	{
		Serial.println("[DeviceSensors] setResolution");
		Serial.println(json);

		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setResolution", "Parse failed: %s\n", json);
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		int value = root["dsFamilyTempSensorResolutionId"];

		Sensor* sensor = getSensorInDeviceBySensorKey(sensorId).getSensor();

		sensor->getSensorTempDSFamily()->setResolution(value);
		_dallas.setResolution(sensor->getDeviceAddress(), value);		
	}

	void DeviceSensors::setTriggerOn(char* json)
	{
		Serial.print("[DeviceSensors::setAlarmOn] ");

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setTriggerOn", "Parse failed: %s\n", json);
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		bool alarmOn = root["alarmOn"];
		SensorUnitMeasurementScalePositionEnum position = static_cast<SensorUnitMeasurementScalePositionEnum>(root["position"].as<int>());

		Sensor* sensor = getSensorInDeviceBySensorKey(sensorId).getSensor();

		if (position == Max)
			sensor->getHighAlarm().setAlarmOn(alarmOn);
		else if (position == Min)
			sensor->getLowAlarm().setAlarmOn(alarmOn);
	}

	void DeviceSensors::setBuzzerOn(char* json)
	{
		Serial.print("[DeviceSensors::setAlarmBuzzerOn] ");

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setBuzzerOn", "Parse failed: %s\n", json);
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		bool alarmBuzzerOn = root["alarmBuzzerOn"];
		SensorUnitMeasurementScalePositionEnum position = static_cast<SensorUnitMeasurementScalePositionEnum>(root["position"].as<int>());

		Sensor* sensor = getSensorInDeviceBySensorKey(sensorId).getSensor();

		if (position == Max)
			sensor->getHighAlarm().setAlarmBuzzerOn(alarmBuzzerOn);
		else if (position == Min)
			sensor->getLowAlarm().setAlarmBuzzerOn(alarmBuzzerOn);
	}

	void DeviceSensors::setTriggerValue(char* json)
	{
		Serial.print("[DeviceSensors::setAlarmCelsius] ");

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setTriggerValue", "Parse failed: %s\n", json);
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		float alarmCelsius = root["alarmCelsius"];
		SensorUnitMeasurementScalePositionEnum position = static_cast<SensorUnitMeasurementScalePositionEnum>(root["position"].as<int>());

		Sensor* sensor = getSensorInDeviceBySensorKey(sensorId).getSensor();

		if (position == Max)
			sensor->getHighAlarm().setAlarmCelsius(alarmCelsius);
		else if (position == Min)
			sensor->getLowAlarm().setAlarmCelsius(alarmCelsius);
	}	

	void DeviceSensors::setRange(char* json)
	{
		Serial.print("[DeviceSensors::setRange] ");

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setRange", "Parse failed: %s\n", json);
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		float chartLimiterCelsius = root["value"];
		SensorUnitMeasurementScalePositionEnum position = static_cast<SensorUnitMeasurementScalePositionEnum>(root["position"].as<int>());

		Sensor* sensor = getSensorInDeviceBySensorKey(sensorId).getSensor();

		if (position == Max)
			sensor->getSensorUnitMeasurementScale()->setRangeMax(chartLimiterCelsius);
		else if (position == Min)
			sensor->getSensorUnitMeasurementScale()->setRangeMin(chartLimiterCelsius);
	}

	void DeviceSensors::setChartLimiter(char* json)
	{
		Serial.print("[DeviceSensors::setChartLimiter] ");

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setChartLimiter", "Parse failed: %s\n", json);
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		float chartLimiterCelsius = root["value"];
		SensorUnitMeasurementScalePositionEnum position = static_cast<SensorUnitMeasurementScalePositionEnum>(root["position"].as<int>());

		Sensor* sensor = getSensorInDeviceBySensorKey(sensorId).getSensor();

		if (position == Max)
			sensor->getSensorUnitMeasurementScale()->setChartLimiterMax(chartLimiterCelsius);
		else if (position == Min)
			sensor->getSensorUnitMeasurementScale()->setChartLimiterMin(chartLimiterCelsius);
	}	

	SensorInDevice& DeviceSensors::getSensorInDeviceBySensorKey(char* sensorId) {
		for (int i = 0; i < _sensorsInDevice.size(); ++i) {
			if (stricmp(_sensorsInDevice[i].getSensor()->getSensorId(), sensorId) == 0) {
				return _sensorsInDevice[i];
			}
		}
	}	

	void DeviceSensors::createSensorJsonNestedObject(Sensor* sensor, JsonArray& root)
	{
		JsonObject& JSONencoder = root.createNestedObject();

		JSONencoder["sensorId"] = sensor->getSensorId();
		JSONencoder["isConnected"] = sensor->getConnected();
		JSONencoder["resolution"] = sensor->getSensorTempDSFamily()->getResolution();
		JSONencoder["tempCelsius"] = sensor->getTempCelsius();
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

	int DeviceSensors::getPublishIntervalInMilliSeconds()
	{
		return _publishIntervalInMilliSeconds;
	}

	void DeviceSensors::setPublishIntervalInMilliSeconds(char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setPublishIntervalInMilliSeconds", "Parse failed: %s\n", json);
			return;
		}
		_publishIntervalInMilliSeconds = root["value"].as<int>();
	}
}
