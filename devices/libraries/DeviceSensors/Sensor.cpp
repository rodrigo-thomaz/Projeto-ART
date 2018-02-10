#include "Sensor.h"
#include "SensorInDevice.h"
#include "DeviceSensors.h"
#include "SensorTrigger.h"

#include "../DallasTemperature/DallasTemperature.h"

namespace ART
{
	Sensor::Sensor(SensorInDevice* sensorInDevice, JsonObject& jsonObject)
	{
		Serial.println(F("[Sensor constructor]"));

		_sensorInDevice = sensorInDevice;

		_deviceAddress = new uint8_t[8];
		for (uint8_t i = 0; i < 8; i++) {
			_deviceAddress[i] = jsonObject["deviceAddress"][i];
		}		
				
		_sensorId = new char[37];
		strcpy(_sensorId, jsonObject["sensorId"]);
		_sensorId[37] = '\0';

		_sensorDatasheetId = static_cast<SensorDatasheetEnum>(jsonObject["sensorDatasheetId"].as<short>());
		_sensorTypeId = static_cast<SensorTypeEnum>(jsonObject["sensorTypeId"].as<short>());
		
		char* family = strdup(SensorTempDSFamily::getFamily(_deviceAddress).c_str());
		_family = new char(sizeof(strlen(family)));
		_family = family;

		_validFamily = true;

		char* label = strdup(jsonObject["label"]);
		_label = new char(sizeof(strlen(label)));
		_label = label;		

		DeviceSensors* deviceSensors = _sensorInDevice->getDeviceSensors();
		_sensorDatasheet = deviceSensors->getSensorDatasheetByKey(_sensorDatasheetId, _sensorTypeId);

		SensorTempDSFamily::create(_sensorTempDSFamily, this, jsonObject["sensorTempDSFamily"]);
		SensorUnitMeasurementScale::create(_sensorUnitMeasurementScale, this, jsonObject["sensorUnitMeasurementScale"]);				

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
	}

	void Sensor::create(Sensor *(&sensor), SensorInDevice * sensorInDevice, JsonObject & jsonObject)
	{
		sensor = new Sensor(sensorInDevice, jsonObject);
	}

	char* Sensor::getSensorId() const
	{
		return (_sensorId);
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

	void Sensor::setLabel(const char* value)
	{
		_label = new char(sizeof(strlen(value)));
		_label = strdup(value);
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
				_sensorTriggers.erase(_sensorTriggers.begin() + i);
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