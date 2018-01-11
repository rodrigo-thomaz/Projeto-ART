#ifndef DeviceSensors_h
#define DeviceSensors_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "vector"
#include "OneWire.h"
#include "DallasTemperature.h"

#include "SensorInDevice.h"
#include "Sensor.h" // Temp

#define DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_REQUEST_JSON_SIZE 			200
#define DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_RESPONSE_JSON_SIZE 			4096

#define TOPIC_PUB_DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID   					"SensorInDevice/GetAllByDeviceInApplicationIdIoT" 
#define TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED		   	"SensorInDevice/GetAllByDeviceInApplicationIdCompletedIoT"

namespace ART
{
	class ESPDevice;	

	class SensorOld
	{

	public:

		SensorOld(char* sensorId, DeviceAddress deviceAddress, char* family, char* label, int resolution, byte unitOfMeasurementId, TempSensorAlarm lowAlarm, TempSensorAlarm highAlarm, float lowChartLimiterCelsius, float highChartLimiterCelsius);
		~SensorOld();

		char*								getSensorId();

		const uint8_t*		 				getDeviceAddress();

		char*								getFamily() const;
		bool								getValidFamily();

		char* 								getLabel() const;
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

		SensorInDevice						*getSensorsInDevice();

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

		static void create(DeviceSensors* (&deviceSensors), ESPDevice* espDevice)
		{
			deviceSensors = new DeviceSensors(espDevice);
		}

	private:

		ESPDevice * _espDevice;

		bool								_initialized;
		bool								_initializing;

		SensorInDevice&						getSensorInDeviceById(char* sensorId);
		void								createSensorJsonNestedObject(SensorOld sensor, JsonArray& root);
		String 								convertDeviceAddressToString(const uint8_t* deviceAddress);

		std::vector<SensorOld> 				_sensors;
		std::vector<SensorInDevice>			_sensorsInDevice;

		int									_publishIntervalInMilliSeconds;

	};
}

#endif