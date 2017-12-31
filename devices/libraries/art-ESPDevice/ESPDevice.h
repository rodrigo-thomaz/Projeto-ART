#ifndef ESPDevice_h
#define ESPDevice_h

#include "DeviceSensors.h"

#include "Arduino.h"
#include "ArduinoJson.h"

class DeviceSensors;

class ESPDevice
{
	public:
		
		ESPDevice(char* deviceId, short deviceDatasheetId, int chipId, int flashChipId, char* stationMacAddress, char* softAPMacAddress, long chipSize, char* label, JsonObject& jsonObject);
		~ESPDevice();
		
		char *						getDeviceId();
		short						getDeviceDatasheetId();
		int							getChipId();
		int							getFlashChipId();
		char *						getStationMacAddress();
		char *						getSoftAPMacAddress();
		long						getChipSize();

		char *						getLabel();
		void						setLabel(char * value);
		
		DeviceSensors*				getDeviceSensors();
	
	private:	

		char *						_deviceId;
		short						_deviceDatasheetId;
		int							_chipId;
		int							_flashChipId;
		char *						_stationMacAddress;
		char *						_softAPMacAddress;
		long						_chipSize;
		char *						_label;
		
		DeviceSensors*				_deviceSensors;
		
};

#endif