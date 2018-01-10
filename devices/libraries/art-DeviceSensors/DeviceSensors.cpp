#include "DeviceSensors.h"
#include "ESPDevice.h"

namespace ART
{
	// TempSensorAlarm

	TempSensorAlarm::TempSensorAlarm(bool alarmOn, float alarmCelsius, bool alarmBuzzerOn, TempSensorAlarmPosition alarmPosition)
	{
		this->_alarmOn = alarmOn;
		this->_alarmCelsius = alarmCelsius;
		this->_alarmBuzzerOn = alarmBuzzerOn;
		this->_alarmPosition = alarmPosition;
	}

	bool TempSensorAlarm::getAlarmOn()	
	{
		return this->_alarmOn;
	}

	void TempSensorAlarm::setAlarmOn(bool value)
	{
		this->_alarmOn = value;
	}

	float TempSensorAlarm::getAlarmCelsius()
	{
		return this->_alarmCelsius;
	}

	void TempSensorAlarm::setAlarmCelsius(float value)
	{
		this->_alarmCelsius = value;
	}

	bool TempSensorAlarm::getAlarmBuzzerOn()
	{
		return this->_alarmBuzzerOn;
	}

	void TempSensorAlarm::setAlarmBuzzerOn(bool value)
	{
		this->_alarmBuzzerOn = value;
	}

	bool TempSensorAlarm::hasAlarm()
	{
		if(!this->_alarmOn) return false;
			
		switch(this->_alarmPosition)
		{
			case Max : return this->_tempCelsius > this->_alarmCelsius;
			case Min : return this->_tempCelsius < this->_alarmCelsius;		
		}
	}

	bool TempSensorAlarm::hasAlarmBuzzer()
	{
		return this->hasAlarm() && this->_alarmBuzzerOn;
	}

	void TempSensorAlarm::setTempCelsius(float value)
	{
		this->_tempCelsius = value;
	}

	// Sensor 

	Sensor::Sensor(char* sensorId, DeviceAddress deviceAddress, char* family, char* label, int resolution, byte unitOfMeasurementId, TempSensorAlarm lowAlarm, TempSensorAlarm highAlarm, float lowChartLimiterCelsius, float highChartLimiterCelsius)
	{
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

	char* Sensor::getSensorId()
	{
		return _sensorId;
	}

	const uint8_t* Sensor::getDeviceAddress()
	{
		return _deviceAddress.data();
	}

	char* Sensor::getFamily()
	{
		return _family;
	}

	bool Sensor::getValidFamily()
	{
		return _validFamily;
	}

	char* Sensor::getLabel()
	{
		return _label;
	}

	void Sensor::setLabel(char* value)
	{
		_label = new char(sizeof(strlen(value)));
		_label = value;
	}

	int Sensor::getResolution()
	{
		return _resolution;
	}

	void Sensor::setResolution(int value)
	{
		_resolution = value;
	}

	byte Sensor::getUnitOfMeasurementId()
	{
		return _unitOfMeasurementId;
	}

	void Sensor::setUnitOfMeasurementId(int value)
	{
		_unitOfMeasurementId = value;
	}

	TempSensorAlarm& Sensor::getLowAlarm()
	{
		return _alarms[0];
	}

	TempSensorAlarm& Sensor::getHighAlarm()
	{
		return _alarms[1];
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
		_alarms[0].setTempCelsius(value);
		_alarms[1].setTempCelsius(value);	
	}

	bool Sensor::hasAlarm()
	{
		return _alarms[0].hasAlarm() || _alarms[1].hasAlarm();
	}

	bool Sensor::hasAlarmBuzzer()
	{
		return _alarms[0].hasAlarmBuzzer() || _alarms[1].hasAlarmBuzzer();
	}

	float Sensor::getLowChartLimiterCelsius()
	{
		return _lowChartLimiterCelsius;
	}

	void Sensor::setLowChartLimiterCelsius(float value)
	{
		_lowChartLimiterCelsius = value;
	}

	float Sensor::getHighChartLimiterCelsius()
	{
		return _highChartLimiterCelsius;
	}

	void Sensor::setHighChartLimiterCelsius(float value)
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
