#ifndef SensorInDevice_h
#define SensorInDevice_h

namespace ART
{
	class DeviceSensors;

	class SensorInDevice
	{

	public:
		SensorInDevice(DeviceSensors* deviceSensors);
		~SensorInDevice();

		static void create(SensorInDevice* (&sensorInDevice), DeviceSensors* deviceSensors)
		{
			sensorInDevice = new SensorInDevice(deviceSensors);
		}

	private:

		DeviceSensors * _deviceSensors;

	};
}

#endif