#include "Sensor.h"
#include "SensorInDevice.h"
#include "DeviceSensor.h"
#include "SensorTrigger.h"

#include "../DallasTemperature/DallasTemperature.h"

namespace ART
{
	Sensor::Sensor(SensorInDevice* sensorInDevice, JsonObject& jsonObject)
	{
		Serial.println(F("[Sensor constructor]"));

		_sensorInDevice = sensorInDevice;

		_deviceAddress = new uint8_t[8];
		for (uint8_t i = 0; i < 8; i++) _deviceAddress[i] = jsonObject["deviceAddress"][i];
			
		setSensorId(jsonObject["sensorId"]);

		_sensorDatasheetId = static_cast<SensorDatasheetEnum>(jsonObject["sensorDatasheetId"].as<short>());
		_sensorTypeId = static_cast<SensorTypeEnum>(jsonObject["sensorTypeId"].as<short>());

		setLabel(jsonObject["label"]);

		DeviceSensor* deviceSensor = _sensorInDevice->getDeviceSensor();
		_sensorDatasheet = deviceSensor->getSensorDatasheetByKey(_sensorDatasheetId, _sensorTypeId);

		_sensorTempDSFamily = new SensorTempDSFamily(this, jsonObject["sensorTempDSFamily"]);
		_sensorUnitMeasurementScale = new SensorUnitMeasurementScale(this, jsonObject["sensorUnitMeasurementScale"]);

		//SensorTriggers
		
		JsonArray& sensorTriggersJA = jsonObject["sensorTriggers"];

		for (JsonArray::iterator it = sensorTriggersJA.begin(); it != sensorTriggersJA.end(); ++it)
		{
			JsonObject& sensorTriggerJO = it->as<JsonObject>();
			_sensorTriggers.push_back(new SensorTrigger(this, sensorTriggerJO));
		}
	}

	Sensor::~Sensor()
	{
		Serial.println(F("[Sensor destructor]"));

		delete[] _sensorId;
		delete[] _label;
		delete[] _deviceAddress;

		delete (_sensorTempDSFamily);
		delete (_sensorUnitMeasurementScale);
	}	

	char* Sensor::getSensorId() const
	{
		return (_sensorId);
	}

	void Sensor::setSensorId(const char * value)
	{
		_sensorId = new char[strlen(value) + 1];
		strcpy(_sensorId, value);
		_sensorId[strlen(value) + 1] = '\0';
	}

	SensorTypeEnum Sensor::getSensorTypeId()
	{
		return _sensorTypeId;
	}

	SensorDatasheetEnum Sensor::getSensorDatasheetId()
	{
		return _sensorDatasheetId;
	}

	uint8_t* Sensor::getDeviceAddress() const
	{
		return (_deviceAddress);
	}

	char * Sensor::getLabel() const
	{
		return (_label);
	}

	void Sensor::setLabel(const char* value)
	{
		_label = new char[strlen(value) + 1];
		strcpy(_label, value);
		_label[strlen(value) + 1] = '\0';
	}	

	bool Sensor::getConnected()
	{
		return _connected;
	}

	void Sensor::setConnected(bool value)
	{
		_connected = value;
	}

	float Sensor::getValue()
	{
		return _value;
	}

	void Sensor::setValue(float value)
	{
		_value = value;
	}

	bool Sensor::hasAlarm()
	{
		for (int i = 0; i < _sensorTriggers.size(); ++i) {
			if (_sensorTriggers[i]->hasAlarm()) {
				return true;
			}
		}
		return false;
	}

	bool Sensor::hasAlarmBuzzer()
	{
		for (int i = 0; i < _sensorTriggers.size(); ++i) {
			if (_sensorTriggers[i]->hasAlarmBuzzer()) {
				return true;
			}
		}
		return false;
	}

	void Sensor::insertTrigger(JsonObject& root)
	{
		Serial.println(F("[Sensor::insertTrigger] "));
		_sensorTriggers.push_back(new SensorTrigger(this, root));
	}

	bool Sensor::deleteTrigger(const char* sensorTriggerId)
	{
		Serial.println(F("[Sensor::deleteTrigger] begin"));
		for (int i = 0; i < _sensorTriggers.size(); ++i) {			
			if (strcmp(_sensorTriggers[i]->getSensorTriggerId(), sensorTriggerId) == 0) {
				SensorTrigger* sensorTrigger = _sensorTriggers[i];
				_sensorTriggers.erase(_sensorTriggers.begin() + i);
				delete(sensorTrigger);
				Serial.println(F("[Sensor::deleteTrigger] deleted"));
				return true;
			}
		}		
		Serial.println(F("[Sensor::deleteTrigger] not deleted !!!"));
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

	SensorTrigger ** Sensor::getSensorTriggers()
	{
		SensorTrigger** array = _sensorTriggers.data();
		return array;
	}
}