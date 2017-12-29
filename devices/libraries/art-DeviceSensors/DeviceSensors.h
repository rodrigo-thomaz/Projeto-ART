#ifndef DeviceSensors_h
#define DeviceSensors_h

#include "Arduino.h"

class ESPDevice;

class DeviceSensors
{

public:

	DeviceSensors(ESPDevice& espDevice);
	~DeviceSensors();
	
	ESPDevice*          				getESPDevice();	
	
	int									getPublishIntervalInSeconds();
	void								setPublishIntervalInSeconds(int value);
	
private:	

	ESPDevice*          				_espDevice;	
	
	int									_publishIntervalInSeconds;
	
};

#endif