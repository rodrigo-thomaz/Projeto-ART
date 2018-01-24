#include "DeviceSensors.h"
#include "ESPDevice.h"

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

	void DeviceSensors::load(JsonObject& jsonObject)
	{
		_publishIntervalInMilliSeconds = jsonObject["publishIntervalInMilliSeconds"];
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

		JsonArray& jsonArray = sensorInDeviceJO["sensorInDevice"];

		if (!jsonArray.success()) {
			Serial.print("[DeviceSensors::setSensorsByMQQTCallback] parse failed: ");
			Serial.println(json);
			return;
		}

		for (JsonArray::iterator it = jsonArray.begin(); it != jsonArray.end(); ++it)
		{
			Serial.println("[DeviceSensors::setSensorsByMQQTCallback]  SensorInDevice foreach begin");

			JsonObject& sensorInDeviceJsonObject = it->as<JsonObject>();
			JsonObject& sensorJsonObject = sensorInDeviceJsonObject["sensor"].as<JsonObject>();			

			// SensorInDevice
			_sensorsInDevice.push_back(SensorInDevice::create(this, sensorInDeviceJsonObject));
			Serial.println("[DeviceSensors::setSensorsByMQQTCallback] SensorInDevice created");

			// DeviceAddress
			DeviceAddress 	deviceAddress;
			for (uint8_t i = 0; i < 8; i++) deviceAddress[i] = sensorJsonObject["deviceAddress"][i];			

			int resolution = int(sensorJsonObject["resolutionBits"]);			

			_dallas.setResolution(deviceAddress, resolution);
			_dallas.resetAlarmSearch();

			Serial.println("[DeviceSensors::setSensorsByMQQTCallback]  SensorInDevice foreach end");
		}

		Serial.println("[DeviceSensors::setSensorsByMQQTCallback] initialized with success !");
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

		Sensor* sensor = getSensorInDeviceById(sensorId).getSensor();

		sensor->setLabel(label);

		Serial.print("setLabel=");
		Serial.println(label);
	}

	void DeviceSensors::setDatasheetUnitMeasurementScale(char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceSensors", "setDatasheetUnitMeasurementScale", "Parse failed: %s\n", json);
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		UnitMeasurementEnum value = static_cast<UnitMeasurementEnum>(root["unitMeasurementId"].as<int>());

		Sensor* sensor = getSensorInDeviceById(sensorId).getSensor();

		sensor->getSensorUnitMeasurementScale()->setUnitMeasurementId(value);

		Serial.print("setUnitOfMeasurement=");
		Serial.println(value);
	}

	void DeviceSensors::setResolution(String json)
	{
		StaticJsonBuffer<200> jsonBuffer;

		JsonObject& root = jsonBuffer.parseObject(json);

		if (!root.success()) {
			Serial.print("parse setResolution failed: ");
			Serial.println(json);
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		int value = root["dsFamilyTempSensorResolutionId"];

		Sensor* sensor = getSensorInDeviceById(sensorId).getSensor();

		sensor->getSensorTempDSFamily()->setResolution(value);
		_dallas.setResolution(sensor->getDeviceAddress(), value);

		Serial.print("setResolution=");
		Serial.println(json);
	}

	void DeviceSensors::setAlarmOn(String json)
	{
		StaticJsonBuffer<300> jsonBuffer;

		JsonObject& root = jsonBuffer.parseObject(json);

		if (!root.success()) {
			Serial.println("parse setAlarmOn failed");
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		bool alarmOn = root["alarmOn"];
		TempSensorAlarmPosition position = static_cast<TempSensorAlarmPosition>(root["position"].as<int>());

		Sensor* sensor = getSensorInDeviceById(sensorId).getSensor();

		if (position == Max)
			sensor->getHighAlarm().setAlarmOn(alarmOn);
		else if (position == Min)
			sensor->getLowAlarm().setAlarmOn(alarmOn);

		Serial.print("[DeviceSensors::setAlarmOn] ");
		Serial.println(json);
	}

	void DeviceSensors::setAlarmCelsius(String json)
	{
		StaticJsonBuffer<300> jsonBuffer;

		JsonObject& root = jsonBuffer.parseObject(json);

		if (!root.success()) {
			Serial.println("parse setAlarmCelsius failed");
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		float alarmCelsius = root["alarmCelsius"];
		TempSensorAlarmPosition position = static_cast<TempSensorAlarmPosition>(root["position"].as<int>());

		Sensor* sensor = getSensorInDeviceById(sensorId).getSensor();

		if (position == Max)
			sensor->getHighAlarm().setAlarmCelsius(alarmCelsius);
		else if (position == Min)
			sensor->getLowAlarm().setAlarmCelsius(alarmCelsius);

		Serial.print("[DeviceSensors::setAlarmCelsius] ");
		Serial.println(json);
	}

	void DeviceSensors::setAlarmBuzzerOn(String json)
	{
		StaticJsonBuffer<300> jsonBuffer;

		JsonObject& root = jsonBuffer.parseObject(json);

		if (!root.success()) {
			Serial.println("parse setAlarmBuzzerOn failed");
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		bool alarmBuzzerOn = root["alarmBuzzerOn"];
		TempSensorAlarmPosition position = static_cast<TempSensorAlarmPosition>(root["position"].as<int>());

		Sensor* sensor = getSensorInDeviceById(sensorId).getSensor();

		if (position == Max)
			sensor->getHighAlarm().setAlarmBuzzerOn(alarmBuzzerOn);
		else if (position == Min)
			sensor->getLowAlarm().setAlarmBuzzerOn(alarmBuzzerOn);

		Serial.print("[DeviceSensors::setAlarmBuzzerOn] ");
		Serial.println(json);
	}

	void DeviceSensors::setRange(String json)
	{
		StaticJsonBuffer<300> jsonBuffer;

		JsonObject& root = jsonBuffer.parseObject(json);

		if (!root.success()) {
			Serial.println("parse setRange failed");
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		float chartLimiterCelsius = root["value"];
		TempSensorAlarmPosition position = static_cast<TempSensorAlarmPosition>(root["position"].as<int>());

		Sensor* sensor = getSensorInDeviceById(sensorId).getSensor();

		if (position == Max)
			sensor->getSensorUnitMeasurementScale()->setRangeMax(chartLimiterCelsius);
		else if (position == Min)
			sensor->getSensorUnitMeasurementScale()->setRangeMin(chartLimiterCelsius);

		Serial.print("[DeviceSensors::setRange] ");
		Serial.println(json);
	}

	void DeviceSensors::setChartLimiter(String json)
	{
		StaticJsonBuffer<300> jsonBuffer;

		JsonObject& root = jsonBuffer.parseObject(json);

		if (!root.success()) {
			Serial.println("parse setChartLimiter failed");
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		float chartLimiterCelsius = root["value"];
		TempSensorAlarmPosition position = static_cast<TempSensorAlarmPosition>(root["position"].as<int>());

		Sensor* sensor = getSensorInDeviceById(sensorId).getSensor();

		if (position == Max)
			sensor->getSensorUnitMeasurementScale()->setChartLimiterMax(chartLimiterCelsius);
		else if (position == Min)
			sensor->getSensorUnitMeasurementScale()->setChartLimiterMin(chartLimiterCelsius);

		Serial.print("[DeviceSensors::setChartLimiter] ");
		Serial.println(json);
	}

	SensorInDevice& DeviceSensors::getSensorInDeviceById(char* sensorId) {
		for (int i = 0; i < this->_sensorsInDevice.size(); ++i) {
			if (this->_sensorsInDevice[i].getSensor()->getSensorId() == sensorId) {
				return this->_sensorsInDevice[i];
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
