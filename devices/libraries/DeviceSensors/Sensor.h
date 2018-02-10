#ifndef Sensor_h
#define Sensor_h

#include "vector"

#include "../ArduinoJson/ArduinoJson.h"

#include "SensorDatasheet.h"
#include "SensorTempDSFamily.h"
#include "SensorUnitMeasurementScale.h"
#include "SensorTrigger.h"
#include "PositionEnum.h"

namespace ART
{
	class SensorInDevice;	

	class Sensor
	{

	public:
		Sensor(SensorInDevice* sensorInDevice, JsonObject& jsonObject);
		~Sensor();

		static void							create(Sensor* (&sensor), SensorInDevice* sensorInDevice, JsonObject& jsonObject);

		char*								getSensorId() const;
		SensorTypeEnum						getSensorTypeId();
		SensorDatasheetEnum					getSensorDatasheetId();
		
		uint8_t*			 				getDeviceAddress() const;
		
		char* 								getLabel() const;		
		
		bool 								getConnected();
		
		float 								getValue();		

		bool 								hasAlarm();
		bool 								hasAlarmBuzzer();		

		void								insertTrigger(JsonObject& root);
		bool								deleteTrigger(const char* sensorTriggerId);

		SensorTempDSFamily *				getSensorTempDSFamily();
		SensorUnitMeasurementScale *		getSensorUnitMeasurementScale();
		SensorTrigger **					getSensorTriggers();

	private:

		SensorInDevice *					_sensorInDevice;
		SensorDatasheet *					_sensorDatasheet;

		SensorTempDSFamily *				_sensorTempDSFamily;
		SensorUnitMeasurementScale *		_sensorUnitMeasurementScale;

		char* 								_sensorId;
		SensorTypeEnum						_sensorTypeId;
		SensorDatasheetEnum					_sensorDatasheetId;

		uint8_t* 				 			_deviceAddress;

		char* 								_label;	

		std::vector<SensorTrigger*> 		_sensorTriggers;

		bool 								_connected;

		float 								_value;

		long 								_epochTimeUtc;	

		void 								setSensorId(const char* value);
		void 								setLabel(const char* value);
		void 								setConnected(bool value);
		void 								setValue(float value);

		friend class						DeviceSensors;
	};
}

#endif