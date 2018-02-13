#ifndef DeviceSensor_h
#define DeviceSensor_h

#include "vector"
#include "tuple"

#include "../OneWire/OneWire.h"
#include "../DallasTemperature/DallasTemperature.h"

#include "Arduino.h"
#include "../ArduinoJson/ArduinoJson.h"

#include "SensorDatasheet.h"
#include "SensorInDevice.h"

// Data wire is plugged into port 0
#define ONE_WIRE_BUS 0

#define DEVICE_SENSOR_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_RESPONSE_JSON_SIZE 			4096

#define DEVICE_SENSOR_GET_ALL_BY_KEY_TOPIC_PUB   										"DeviceSensor/GetAllByKeyIoT" 
#define DEVICE_SENSOR_GET_ALL_BY_KEY_COMPLETED_TOPIC_SUB								"DeviceSensor/GetAllByKeyCompletedIoT"

#define DEVICE_SENSOR_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_TOPIC_PUB   					"DeviceSensor/GetFullByDeviceInApplicationIdIoT" 
#define DEVICE_SENSOR_GET_FULL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED_TOPIC_SUB		   	"DeviceSensor/GetFullByDeviceInApplicationIdCompletedIoT"

#define DEVICE_SENSOR_SET_READ_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB						"DeviceSensor/SetReadIntervalInMilliSecondsIoT"
#define DEVICE_SENSOR_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB					"DeviceSensor/SetPublishIntervalInMilliSecondsIoT"
#define DEVICE_SENSOR_MESSAGE_TOPIC_PUB													"DeviceSensor/MessageIoT"

#define SENSOR_IN_DEVICE_SET_ORDINATION_TOPIC_SUB										"SensorInDevice/SetOrdinationIoT"

#define SENSOR_SET_LABEL_TOPIC_SUB														"Sensor/SetLabelIoT"

#define SENSOR_TEMP_DS_FAMILY_SET_RESOLUTION_TOPIC_SUB									"SensorTempDSFamily/SetResolutionIoT"

#define SENSOR_TRIGGER_INSERT_TOPIC_SUB													"SensorTrigger/InsertIoT"
#define SENSOR_TRIGGER_DELETE_TOPIC_SUB													"SensorTrigger/DeleteIoT"
#define SENSOR_TRIGGER_SET_TRIGGER_ON_TOPIC_SUB											"SensorTrigger/SetTriggerOnIoT"
#define SENSOR_TRIGGER_SET_BUZZER_ON_TOPIC_SUB											"SensorTrigger/SetBuzzerOnIoT"
#define SENSOR_TRIGGER_SET_TRIGGER_VALUE_TOPIC_SUB										"SensorTrigger/SetTriggerValueIoT"

#define SENSOR_UNIT_MEASUREMENT_SCALE_SET_DATASHEET_UNIT_MEASUREMENT_SCALE_TOPIC_SUB	"SensorUnitMeasurementScale/SetDatasheetUnitMeasurementScaleIoT"
#define SENSOR_UNIT_MEASUREMENT_SCALE_RANGE_SET_VALUE_TOPIC_SUB							"SensorUnitMeasurementScale/SetRangeIoT"
#define SENSOR_UNIT_MEASUREMENT_SCALE_CHART_LIMITER_SET_VALUE_TOPIC_SUB					"SensorUnitMeasurementScale/SetChartLimiterIoT"

namespace ART
{
	class ESPDevice;		

	class DeviceSensor
	{

	public:

		DeviceSensor(ESPDevice* espDevice);
		~DeviceSensor();

		void 								begin();

		bool								initialized();
		void 								setSensorsByMQQTCallback(const char* json);

		bool 								read();
		bool 								publish();

		ESPDevice *							getESPDevice();

		std::tuple<SensorInDevice**, short> getSensorsInDevice();

		void 								createSensorsJsonNestedArray(JsonObject& jsonObject);		

		long								getReadIntervalInMilliSeconds();
		
		long								getPublishIntervalInMilliSeconds();

		SensorDatasheet *					getSensorDatasheetByKey(SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId);		

	private:

		ESPDevice *							_espDevice;

		OneWire *							_oneWire;
		DallasTemperature *					_dallas;

		bool								_initialized;
		bool								_initializing;
		
		SensorInDevice *					getSensorInDeviceBySensorId(const char* sensorId);
		Sensor *							getSensorById(const char* sensorId);
		SensorTrigger *						getSensorTriggerByKey(const char* sensorId, const char* sensorTriggerId);

		void								createSensorJsonNestedObject(Sensor* sensor, JsonArray& root);
		String 								convertDeviceAddressToString(const uint8_t* deviceAddress);

		std::vector<SensorDatasheet*>		_sensorDatasheets;
		std::vector<SensorInDevice*>		_sensorsInDevice;
		
		uint64_t							_readIntervalTimestamp;
		long								_readIntervalInMilliSeconds;

		uint64_t							_publishIntervalTimestamp;
		long								_publishIntervalInMilliSeconds;

		void 								setLabel(const char* json);

		void 								setDatasheetUnitMeasurementScale(const char* json);
		void 								setResolution(const char* json);

		void 								setOrdination(const char* json);

		bool								compareSensorInDevice(SensorInDevice* a, SensorInDevice* b);

		void 								insertTrigger(const char* json);
		bool 								deleteTrigger(const char* json);

		void 								setTriggerOn(const char* json);
		void 								setBuzzerOn(const char* json);
		void 								setTriggerValue(const char* json);

		void 								setRange(const char* json);
		void 								setChartLimiter(const char* json);

		void								setReadIntervalInMilliSeconds(const char* json);
		void								setPublishIntervalInMilliSeconds(const char* json);

		void								onDeviceMQSubscribeDeviceInApplication();
		void								onDeviceMQUnSubscribeDeviceInApplication();
		bool								onDeviceMQSubscription(const char* topicKey, const char* json);
	};
}

#endif