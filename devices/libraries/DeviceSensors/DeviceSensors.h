#ifndef DeviceSensors_h
#define DeviceSensors_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "vector"
#include "OneWire.h"
#include "DallasTemperature.h"

#define DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_REQUEST_JSON_SIZE 			200
#define DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_RESPONSE_JSON_SIZE 			4096

#define TOPIC_PUB_DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID   					"Sensor/GetAllByDeviceInApplicationIdIoT" 
#define TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED		   	"Sensor/GetAllByDeviceInApplicationIdCompletedIoT"

namespace ART
{
	class ESPDevice;

	enum TempSensorAlarmPosition
	{
		Max = 0,
		Min = 1,
	};

	class TempSensorAlarm
	{
	public:

		TempSensorAlarm(bool alarmOn, float alarmCelsius, bool alarmBuzzerOn, TempSensorAlarmPosition alarmPosition);

		bool 								getAlarmOn();
		void 								setAlarmOn(bool value);

		float 								getAlarmCelsius();
		void 								setAlarmCelsius(float value);

		bool 								getAlarmBuzzerOn();
		void 								setAlarmBuzzerOn(bool value);

		bool 								hasAlarm();

		bool 								hasAlarmBuzzer();

		void 								setTempCelsius(float value);

	private:

		bool 								_alarmOn;
		float 								_alarmCelsius;
		bool 								_alarmBuzzerOn;
		TempSensorAlarmPosition				_alarmPosition;

		float 								_tempCelsius;
	};

	class Sensor
	{

	public:

		Sensor(char* sensorId, DeviceAddress deviceAddress, char* family, char* label, int resolution, byte unitOfMeasurementId, TempSensorAlarm lowAlarm, TempSensorAlarm highAlarm, float lowChartLimiterCelsius, float highChartLimiterCelsius);

		char*								getSensorId();

		const uint8_t*		 				getDeviceAddress();

		char*								getFamily();
		bool								getValidFamily();

		char* 								getLabel();
		void 								setLabel(char* value);

		int 								getResolution();
		void 								setResolution(int value);

		byte 								getUnitOfMeasurementId();
		void 								setUnitOfMeasurementId(int value);

		TempSensorAlarm& 					getLowAlarm();
		TempSensorAlarm& 					getHighAlarm();

		bool 								getConnected();
		void 								setConnected(bool value);

		float 								getTempCelsius();
		void 								setTempCelsius(float value);

		bool 								hasAlarm();
		bool 								hasAlarmBuzzer();

		float 								getLowChartLimiterCelsius();
		void 								setLowChartLimiterCelsius(float value);

		float 								getHighChartLimiterCelsius();
		void 								setHighChartLimiterCelsius(float value);

	private:

		char* 								_sensorId;

		std::vector<uint8_t> 				_deviceAddress;

		char* 								_family;
		bool 								_validFamily;

		char* 								_label;

		int 								_resolution;

		byte								_unitOfMeasurementId;

		std::vector<TempSensorAlarm> 		_alarms;

		bool 								_connected;

		float 								_tempCelsius;

		long 								_epochTimeUtc;

		float 								_lowChartLimiterCelsius;
		float 								_highChartLimiterCelsius;

		//friend class 						DeviceSensors;

	};

	class DeviceSensors
	{

	public:

		DeviceSensors(ESPDevice* espDevice);
		~DeviceSensors();

		void								load(JsonObject& jsonObject);

		void 								begin();

		bool								initialized();
		void 								setSensorsByMQQTCallback(String json);

		void 								refresh();

		Sensor 								*getSensors();

		void 								createSensorsJsonNestedArray(JsonObject& jsonObject);

		void 								setLabel(char* json);
		void 								setUnitOfMeasurement(String json);
		void 								setResolution(String json);

		void 								setAlarmOn(String json);
		void 								setAlarmCelsius(String json);
		void 								setAlarmBuzzerOn(String json);
		void 								setChartLimiterCelsius(String json);

		int									getPublishIntervalInMilliSeconds();
		void								setPublishIntervalInMilliSeconds(char* json);

		static void createDeviceSensors(DeviceSensors* (&deviceSensors), ESPDevice* espDevice)
		{
			deviceSensors = new DeviceSensors(espDevice);
		}

	private:

		ESPDevice * _espDevice;

		bool								_initialized;
		bool								_initializing;

		Sensor&								getSensorById(char* sensorId);
		String 								getFamily(byte deviceAddress[8]);
		void								createSensorJsonNestedObject(Sensor sensor, JsonArray& root);
		String 								convertDeviceAddressToString(const uint8_t* deviceAddress);

		std::vector<Sensor> 				_sensors;

		int									_publishIntervalInMilliSeconds;

	};
}

#endif