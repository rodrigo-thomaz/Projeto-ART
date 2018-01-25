#include "Sensor.h"
#include "SensorInDevice.h"
#include "DeviceSensors.h"

namespace ART
{
	Sensor::Sensor(SensorInDevice* sensorInDevice, JsonObject& jsonObject)
	{
		Serial.println("[Sensor constructor]");

		_sensorInDevice = sensorInDevice;	

		DeviceAddress 	deviceAddress;
		for (uint8_t i = 0; i < 8; i++) deviceAddress[i] = jsonObject["deviceAddress"][i];
		for (uint8_t i = 0; i < 8; i++) this->_deviceAddress.push_back(deviceAddress[i]);

		char* sensorId = strdup(jsonObject["sensorId"]);
		_sensorId = new char(sizeof(strlen(sensorId)));
		_sensorId = sensorId;

		_sensorDatasheetId = static_cast<SensorDatasheetEnum>(jsonObject["sensorDatasheetId"].as<short>());
		_sensorTypeId = static_cast<SensorTypeEnum>(jsonObject["sensorTypeId"].as<short>());
		
		char* family = strdup(SensorTempDSFamily::getFamily(deviceAddress).c_str());
		_family = new char(sizeof(strlen(family)));
		_family = family;

		_validFamily = true;

		char* label = strdup(jsonObject["label"]);
		_label = new char(sizeof(strlen(label)));
		_label = label;		

		DeviceSensors* deviceSensors = _sensorInDevice->getDeviceSensors();
		_sensorDatasheet = &deviceSensors->getSensorDatasheetByKey(_sensorDatasheetId, _sensorTypeId);

		SensorTempDSFamily::create(_sensorTempDSFamily, this, jsonObject["sensorTempDSFamily"]);
		SensorUnitMeasurementScale::create(_sensorUnitMeasurementScale, this, jsonObject["sensorUnitMeasurementScale"]);				

		//SensorTriggers
		
		JsonArray& sensorTriggersJA = jsonObject["sensorTriggers"];

		for (JsonArray::iterator it = sensorTriggersJA.begin(); it != sensorTriggersJA.end(); ++it)
		{
			JsonObject& sensorTriggerJO = it->as<JsonObject>();
			_sensorTriggers.push_back(SensorTrigger::create(this, sensorTriggerJO));
		}
	}

	Sensor::~Sensor()
	{
		Serial.println("[Sensor destructor]");
	}

	char* Sensor::getSensorId()
	{
		return _sensorId;
	}

	SensorTypeEnum Sensor::getSensorTypeId()
	{
		return _sensorTypeId;
	}

	SensorDatasheetEnum Sensor::getSensorDatasheetId()
	{
		return _sensorDatasheetId;
	}

	const uint8_t* Sensor::getDeviceAddress()
	{
		return _deviceAddress.data();
	}

	char * Sensor::getFamily() const
	{
		return (_family);
	}

	bool Sensor::getValidFamily()
	{
		return _validFamily;
	}

	char * Sensor::getLabel() const
	{
		return (_label);
	}

	void Sensor::setLabel(char* value)
	{
		_label = new char(sizeof(strlen(value)));
		_label = value;
	}	

	bool Sensor::getConnected()
	{
		return _connected;
	}

	void Sensor::setConnected(bool value)
	{
		_connected = value;
	}

	float Sensor::getTempCelsius()
	{
		return _tempCelsius;
	}

	void Sensor::setTempCelsius(float value)
	{
		_tempCelsius = value;
	}

	bool Sensor::hasAlarm()
	{
		for (int i = 0; i < _sensorTriggers.size(); ++i) {
			if (_sensorTriggers[i].hasAlarm()) {
				return true;
			}
		}
		return false;
	}

	bool Sensor::hasAlarmBuzzer()
	{
		for (int i = 0; i < _sensorTriggers.size(); ++i) {
			if (_sensorTriggers[i].hasAlarmBuzzer()) {
				return true;
			}
		}
		return false;
	}	

	SensorTempDSFamily * Sensor::getSensorTempDSFamily()
	{
		return _sensorTempDSFamily;
	}

	SensorUnitMeasurementScale * Sensor::getSensorUnitMeasurementScale()
	{
		return _sensorUnitMeasurementScale;
	}

	SensorTrigger * Sensor::getSensorTriggers()
	{
		SensorTrigger* array = this->_sensorTriggers.data();
		return array;
	}
}