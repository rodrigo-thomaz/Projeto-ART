#ifndef ESPDevice_h
#define ESPDevice_h

#include "DeviceMQ.h"
#include "DeviceSensors.h"

#include "Arduino.h"
#include "ArduinoJson.h"
#include <ESP8266WiFi.h>

class DeviceMQ;
class DeviceSensors;

class ESPDevice
{
	public:
		
		ESPDevice(String json);
		~ESPDevice();
		
		char *						getDeviceId();
		short						getDeviceDatasheetId();
		
		int							getChipId();
		int							getFlashChipId();
		long						getChipSize();
		
		char *						getStationMacAddress();
		char *						getSoftAPMacAddress();		

		char *						getLabel();
		void						setLabel(char * value);
		
		DeviceMQ*					getDeviceMQ();
		DeviceSensors*				getDeviceSensors();
	
	private:	

		char *						_deviceId;
		short						_deviceDatasheetId;
		
		int							_chipId;
		int							_flashChipId;
		long						_chipSize;
		
		char *						_stationMacAddress;
		char *						_softAPMacAddress;		
		
		char *						_label;
		
		DeviceMQ*					_deviceMQ;
		DeviceSensors*				_deviceSensors;
		
};

#endif