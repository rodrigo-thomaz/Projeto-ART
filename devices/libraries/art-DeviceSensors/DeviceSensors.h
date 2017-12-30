#ifndef DeviceSensors_h
#define DeviceSensors_h

#include "Arduino.h"

class ESPDevice;

class DeviceSensors
{

public:

	DeviceSensors(ESPDevice* espDevice, int publishIntervalInSeconds);
	~DeviceSensors();
	
	ESPDevice*          				getESPDevice();	
	
	int									getPublishIntervalInSeconds();
	void								setPublishIntervalInSeconds(int value);
	
	static void createDeviceSensors(DeviceSensors* (&deviceSensors), ESPDevice* espDevice, int publishIntervalInSeconds)
    {
      deviceSensors = new DeviceSensors(espDevice, publishIntervalInSeconds); 
    }
	
private:	

	ESPDevice*          				_espDevice;	
	
	int									_publishIntervalInSeconds;
	
};

#endif