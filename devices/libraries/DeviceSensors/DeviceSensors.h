#ifndef DeviceSensors_h
#define DeviceSensors_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "vector"
#include "OneWire.h"
#include "DallasTemperature.h"

#include "SensorInDevice.h"

#define DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_REQUEST_JSON_SIZE 			200
#define DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_RESPONSE_JSON_SIZE 			4096

#define TOPIC_PUB_DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID   					"SensorInDevice/GetAllByDeviceInApplicationIdIoT" 
#define TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED		   	"SensorInDevice/GetAllByDeviceInApplicationIdCompletedIoT"

namespace ART
{
	class ESPDevice;		

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

		void 								setRange(String json);
		void 								setChartLimiter(String json);

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
		void								createSensorJsonNestedObject(Sensor* sensor, JsonArray& root);
		String 								convertDeviceAddressToString(const uint8_t* deviceAddress);

		std::vector<SensorInDevice>			_sensorsInDevice;

		int									_publishIntervalInMilliSeconds;

	};
}

#endif