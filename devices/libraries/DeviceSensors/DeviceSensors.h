#ifndef DeviceSensors_h
#define DeviceSensors_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "vector"
#include "OneWire.h"
#include "DallasTemperature.h"

#include "SensorDatasheet.h"
#include "SensorInDevice.h"

#define DEVICE_SENSORS_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_REQUEST_JSON_SIZE 			200
#define DEVICE_SENSORS_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_RESPONSE_JSON_SIZE 			4096

#define DEVICE_SENSORS_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_TOPIC_PUB   				"DeviceSensors/GetFullByDeviceInApplicationIdIoT" 
#define DEVICE_SENSORS_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED_TOPIC_SUB		   	"DeviceSensors/GetFullByDeviceInApplicationIdCompletedIoT"

namespace ART
{
	class ESPDevice;		

	class DeviceSensors
	{

	public:

		DeviceSensors(ESPDevice* espDevice);
		~DeviceSensors();

		static void							create(DeviceSensors* (&deviceSensors), ESPDevice* espDevice);

		void 								begin();

		bool								initialized();
		void 								setSensorsByMQQTCallback(String json);

		void								loop();
		void 								refresh();

		ESPDevice *							getESPDevice();

		SensorInDevice						*getSensorsInDevice();		

		void 								createSensorsJsonNestedArray(JsonObject& jsonObject);

		void 								setLabel(char* json);

		void 								setDatasheetUnitMeasurementScale(char* json);
		void 								setResolution(char* json);

		void 								setOrdination(char* json);

		void 								insertTrigger(char* json);
		void 								deleteTrigger(char* json);
		void 								setTriggerOn(char* json);
		void 								setBuzzerOn(char* json);
		void 								setTriggerValue(char* json);

		void 								setRange(char* json);
		void 								setChartLimiter(char* json);

		int									getReadIntervalInMilliSeconds();
		void								setReadIntervalInMilliSeconds(char* json);

		int									getPublishIntervalInMilliSeconds();
		void								setPublishIntervalInMilliSeconds(char* json);

		SensorDatasheet&					getSensorDatasheetByKey(SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId);		

	private:

		ESPDevice *							_espDevice;

		bool								_initialized;
		bool								_initializing;
		
		SensorInDevice&						getSensorInDeviceBySensorId(char* sensorId);
		Sensor*								getSensorById(char* sensorId);
		SensorTrigger&						getSensorTriggerByKey(char* sensorId, char* sensorTriggerId);

		void								createSensorJsonNestedObject(Sensor* sensor, JsonArray& root);
		String 								convertDeviceAddressToString(const uint8_t* deviceAddress);

		std::vector<SensorDatasheet>		_sensorDatasheets;
		std::vector<SensorInDevice>			_sensorsInDevice;

		uint64_t							_readIntervalTimestamp;
		int									_readIntervalInMilliSeconds;
		int									_publishIntervalInMilliSeconds;

	};
}

#endif