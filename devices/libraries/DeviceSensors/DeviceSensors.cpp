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
	// SensorOld 

	SensorOld::SensorOld(char* sensorId, DeviceAddress deviceAddress, char* family, char* label, int resolution, byte unitOfMeasurementId, TempSensorAlarm lowAlarm, TempSensorAlarm highAlarm, float lowChartLimiterCelsius, float highChartLimiterCelsius)
	{
		Serial.println("[SensorOld constructor]");

		_sensorId = new char(sizeof(strlen(sensorId)));
		_sensorId = sensorId;

		_family = new char(sizeof(strlen(family)));
		_family = family;

		this->_validFamily = true;

		_label = new char(sizeof(strlen(label)));
		_label = label;

		this->_resolution = resolution;
		this->_unitOfMeasurementId = unitOfMeasurementId;
		this->_lowChartLimiterCelsius = lowChartLimiterCelsius;
		this->_highChartLimiterCelsius = highChartLimiterCelsius;

		for (uint8_t i = 0; i < 8; i++) this->_deviceAddress.push_back(deviceAddress[i]);

		_alarms.push_back(lowAlarm);
		_alarms.push_back(highAlarm);
	}

	SensorOld::~SensorOld()
	{
		Serial.println("[SensorOld destructor]");
	}

	char* SensorOld::getSensorId()
	{
		return _sensorId;
	}

	const uint8_t* SensorOld::getDeviceAddress()
	{
		return _deviceAddress.data();
	}

	char * SensorOld::getFamily() const
	{
		return (_family);
	}

	bool SensorOld::getValidFamily()
	{
		return _validFamily;
	}

	char * SensorOld::getLabel() const
	{
		return (_label);
	}

	void SensorOld::setLabel(char* value)
	{
		_label = new char(sizeof(strlen(value)));
		_label = value;
	}

	int SensorOld::getResolution()
	{
		return _resolution;
	}

	void SensorOld::setResolution(int value)
	{
		_resolution = value;
	}

	byte SensorOld::getUnitOfMeasurementId()
	{
		return _unitOfMeasurementId;
	}

	void SensorOld::setUnitOfMeasurementId(int value)
	{
		_unitOfMeasurementId = value;
	}

	TempSensorAlarm& SensorOld::getLowAlarm()
	{
		return _alarms[0];
	}

	TempSensorAlarm& SensorOld::getHighAlarm()
	{
		return _alarms[1];
	}

	bool SensorOld::getConnected()
	{
		return _connected;
	}

	void SensorOld::setConnected(bool value)
	{
		_connected = value;
	}

	float SensorOld::getTempCelsius()
	{
		return _tempCelsius;
	}

	void SensorOld::setTempCelsius(float value)
	{
		_tempCelsius = value;
		_alarms[0].setTempCelsius(value);
		_alarms[1].setTempCelsius(value);
	}

	bool SensorOld::hasAlarm()
	{
		return _alarms[0].hasAlarm() || _alarms[1].hasAlarm();
	}

	bool SensorOld::hasAlarmBuzzer()
	{
		return _alarms[0].hasAlarmBuzzer() || _alarms[1].hasAlarmBuzzer();
	}

	float SensorOld::getLowChartLimiterCelsius()
	{
		return _lowChartLimiterCelsius;
	}

	void SensorOld::setLowChartLimiterCelsius(float value)
	{
		_lowChartLimiterCelsius = value;
	}

	float SensorOld::getHighChartLimiterCelsius()
	{
		return _highChartLimiterCelsius;
	}

	void SensorOld::setHighChartLimiterCelsius(float value)
	{
		_highChartLimiterCelsius = value;
	}

	// DeviceSensors

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

		StaticJsonBuffer<DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_REQUEST_JSON_SIZE> JSONbuffer;
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

		_espDevice->getDeviceMQ()->publish(TOPIC_PUB_DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID, result);

		return true;
	}

	void DeviceSensors::setSensorsByMQQTCallback(String json)
	{
		Serial.println("[DeviceSensors::setSensorsByMQQTCallback] Enter");

		this->_initialized = true;
		this->_initializing = false;

		DynamicJsonBuffer jsonBuffer;
		//StaticJsonBuffer<DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_RESPONSE_JSON_SIZE> jsonBuffer;

		JsonArray& jsonArray = jsonBuffer.parseArray(json);

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

			char* 			sensorId = strdup(sensorJsonObject["sensorId"]);
			char* 			family = strdup(getFamily(deviceAddress).c_str());
			char* 			label = strdup(sensorJsonObject["label"]);
			int 			resolution = int(sensorJsonObject["resolutionBits"]);
			byte 			unitOfMeasurementId = byte(sensorJsonObject["unitOfMeasurementId"]);

			float 			lowChartLimiterCelsius = float(sensorJsonObject["lowChartLimiterCelsius"]);
			float 			highChartLimiterCelsius = float(sensorJsonObject["highChartLimiterCelsius"]);

			JsonObject& 	lowAlarmJsonObject = sensorJsonObject["lowAlarm"].as<JsonObject>();
			JsonObject& 	highAlarmJsonObject = sensorJsonObject["highAlarm"].as<JsonObject>();

			bool 			lowAlarmOn = bool(lowAlarmJsonObject["alarmOn"]);
			float 			lowAlarmCelsius = float(lowAlarmJsonObject["alarmCelsius"]);
			bool 			lowAlarmBuzzerOn = bool(lowAlarmJsonObject["buzzerOn"]);

			bool 			highAlarmOn = bool(highAlarmJsonObject["alarmOn"]);
			float 			highAlarmCelsius = float(highAlarmJsonObject["alarmCelsius"]);
			bool 			highAlarmBuzzerOn = bool(highAlarmJsonObject["alarmBuzzerOn"]);

			TempSensorAlarm highAlarm = TempSensorAlarm(highAlarmOn, highAlarmCelsius, highAlarmBuzzerOn, TempSensorAlarmPosition::Max);
			TempSensorAlarm lowAlarm = TempSensorAlarm(lowAlarmOn, lowAlarmCelsius, lowAlarmBuzzerOn, TempSensorAlarmPosition::Min);

			this->_sensors.push_back(SensorOld(
				sensorId,
				deviceAddress,
				family,
				label,
				resolution,
				unitOfMeasurementId,
				lowAlarm,
				highAlarm,
				lowChartLimiterCelsius,
				highChartLimiterCelsius));

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
		for (int i = 0; i < this->_sensors.size(); ++i) {
			this->_sensors[i].setConnected(_dallas.isConnected(this->_sensors[i].getDeviceAddress()));
			this->_sensors[i].setResolution(_dallas.getResolution(this->_sensors[i].getDeviceAddress()));
			this->_sensors[i].setTempCelsius(_dallas.getTempC(this->_sensors[i].getDeviceAddress()));

			if (this->_sensors[i].hasAlarm()) 		hasAlarm = true;
			if (this->_sensors[i].hasAlarmBuzzer()) 	hasAlarmBuzzer = true;
		}
		if (hasAlarmBuzzer) {
			_espDevice->getDeviceBuzzer()->test();
		}
	}

	SensorOld *DeviceSensors::getSensors()
	{
		SensorOld* array = this->_sensors.data();
		return array;
	}

	void DeviceSensors::createSensorsJsonNestedArray(JsonObject& jsonObject)
	{
		JsonArray& jsonArray = jsonObject.createNestedArray("dsFamilyTempSensors");
		for (int i = 0; i < this->_sensors.size(); ++i) {
			createSensorJsonNestedObject(this->_sensors[i], jsonArray);
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

		SensorOld& sensor = this->getSensorById(sensorId);
		sensor.setLabel(label);

		Serial.print("setLabel=");
		Serial.println(label);
	}

	void DeviceSensors::setUnitOfMeasurement(String json)
	{
		StaticJsonBuffer<200> jsonBuffer;

		JsonObject& root = jsonBuffer.parseObject(json);

		if (!root.success()) {
			Serial.print("parse setUnitOfMeasurement failed: ");
			Serial.println(json);
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		int value = root["unitOfMeasurementId"];

		SensorOld& sensor = getSensorById(sensorId);
		sensor.setUnitOfMeasurementId(value);

		Serial.print("setUnitOfMeasurement=");
		Serial.println(json);
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

		SensorOld& sensor = getSensorById(sensorId);
		sensor.setResolution(value);
		_dallas.setResolution(sensor.getDeviceAddress(), value);

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

		SensorOld& sensor = getSensorById(sensorId);

		if (position == Max)
			sensor.getHighAlarm().setAlarmOn(alarmOn);
		else if (position == Min)
			sensor.getLowAlarm().setAlarmOn(alarmOn);

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

		SensorOld& sensor = getSensorById(sensorId);

		if (position == Max)
			sensor.getHighAlarm().setAlarmCelsius(alarmCelsius);
		else if (position == Min)
			sensor.getLowAlarm().setAlarmCelsius(alarmCelsius);

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

		SensorOld& sensor = getSensorById(sensorId);

		if (position == Max)
			sensor.getHighAlarm().setAlarmBuzzerOn(alarmBuzzerOn);
		else if (position == Min)
			sensor.getLowAlarm().setAlarmBuzzerOn(alarmBuzzerOn);

		Serial.print("[DeviceSensors::setAlarmBuzzerOn] ");
		Serial.println(json);
	}

	void DeviceSensors::setChartLimiterCelsius(String json)
	{
		StaticJsonBuffer<300> jsonBuffer;

		JsonObject& root = jsonBuffer.parseObject(json);

		if (!root.success()) {
			Serial.println("parse setChartLimiterCelsius failed");
			return;
		}

		char* sensorId = strdup(root["sensorId"]);
		float chartLimiterCelsius = root["value"];
		TempSensorAlarmPosition position = static_cast<TempSensorAlarmPosition>(root["position"].as<int>());

		SensorOld& sensor = getSensorById(sensorId);

		if (position == Max)
			sensor.setHighChartLimiterCelsius(chartLimiterCelsius);
		else if (position == Min)
			sensor.setLowChartLimiterCelsius(chartLimiterCelsius);

		Serial.print("[DeviceSensors::setChartLimiterCelsius] ");
		Serial.println(json);
	}

	SensorOld& DeviceSensors::getSensorById(char* sensorId) {
		for (int i = 0; i < this->_sensors.size(); ++i) {
			if (this->_sensors[i].getSensorId() == sensorId) {
				return this->_sensors[i];
			}
		}
	}

	String DeviceSensors::getFamily(byte deviceAddress[8]) {
		switch (deviceAddress[0]) {
		case DS18S20MODEL:
			return "DS18S20";
		case DS18B20MODEL:
			return "DS18B20";
		case DS1822MODEL:
			return "DS1822";
		case DS1825MODEL:
			return "DS1825";
		case DS28EA00MODEL:
			return "DS28EA00";
		default:
			return "";
		}
	}

	void DeviceSensors::createSensorJsonNestedObject(SensorOld sensor, JsonArray& root)
	{
		JsonObject& JSONencoder = root.createNestedObject();

		JSONencoder["sensorId"] = sensor.getSensorId();
		JSONencoder["isConnected"] = sensor.getConnected();
		JSONencoder["resolution"] = sensor.getResolution();
		JSONencoder["tempCelsius"] = sensor.getTempCelsius();
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
