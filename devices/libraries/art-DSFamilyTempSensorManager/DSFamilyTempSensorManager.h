#ifndef DSFamilyTempSensorManager_h
#define DSFamilyTempSensorManager_h

#include "Arduino.h"
#include "vector"
#include "ArduinoJson.h"
#include "ESPDevice.h"
#include "OneWire.h"
#include "DallasTemperature.h"
#include "DeviceSensors.h"

#define DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_REQUEST_JSON_SIZE 			200
#define DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_RESPONSE_JSON_SIZE 			4096

#define TOPIC_PUB_DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID   					"Sensor/GetAllByDeviceInApplicationIdIoT" 
#define TOPIC_SUB_DS_FAMILY_TEMP_SENSOR_GET_ALL_BY_DEVICE_IN_APPLICATION_ID_COMPLETED		   	"Sensor/GetAllByDeviceInApplicationIdCompletedIoT"

using namespace ART; 

class DSFamilyTempSensorManager
{
  public:
  
    DSFamilyTempSensorManager(ESPDevice& espDevice);
	
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
				
  private:			
			
	ESPDevice*							_espDevice;	
	
	bool								_initialized;
	bool								_initializing;					
				
	Sensor&								getSensorById(char* sensorId);
	String 								getFamily(byte deviceAddress[8]);
	void								createSensorJsonNestedObject(Sensor sensor, JsonArray& root);
	String 								convertDeviceAddressToString(const uint8_t* deviceAddress);
	
	std::vector<Sensor> 				_sensors;
};

#endif
