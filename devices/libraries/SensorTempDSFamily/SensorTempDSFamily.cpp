#include "SensorTempDSFamily.h"
#include "Sensor.h"

namespace ART
{	
	SensorTempDSFamily::SensorTempDSFamily(Sensor* sensor, JsonObject& jsonObject)
	{
		Serial.println("[SensorTempDSFamily constructor]");

		_sensor = sensor;

		_resolution = int(jsonObject["resolutionBits"]);
	}

	SensorTempDSFamily::~SensorTempDSFamily()
	{
		Serial.println("[SensorTempDSFamily destructor]");
	}

	void SensorTempDSFamily::create(SensorTempDSFamily *(&sensorTempDSFamily), Sensor * sensor, JsonObject & jsonObject)
	{
		sensorTempDSFamily = new SensorTempDSFamily(sensor, jsonObject);
	}

	int SensorTempDSFamily::getResolution()
	{
		return _resolution;
	}

	void SensorTempDSFamily::setResolution(int value)
	{
		_resolution = value;
	}

	void SensorTempDSFamily::onDeviceMQSubscribeDeviceInApplication()
	{

	}

	void SensorTempDSFamily::onDeviceMQUnSubscribeDeviceInApplication()
	{

	}

	void SensorTempDSFamily::onDeviceMQSubscription(char* topicKey, char* json)
	{

	}
}