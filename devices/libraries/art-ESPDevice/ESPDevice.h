#ifndef ESPDevice_h
#define ESPDevice_h

#include "Arduino.h"
#include "DeviceSensors.h"

class DeviceSensors;

class ESPDevice
{
	public:
		
		ESPDevice(char* espDeviceId, short deviceDatasheetId, int chipId, int flashChipId, char* stationMacAddress, char* softAPMacAddress, char* sdkVersion, long chipSize, char* label);
		~ESPDevice();
		
		char *						getESPDeviceId();
		short						getDeviceDatasheetId();
		int							getChipId();
		int							getFlashChipId();
		char *						getStationMacAddress();
		char *						getSoftAPMacAddress();
		char *						getSDKVersion();
		long						getChipSize();

		char *						getLabel();
		void						setLabel(char * value);
		
		DeviceSensors*				getDeviceSensors();
	
	private:	

		char *						_espDeviceId;
		short						_deviceDatasheetId;
		int							_chipId;
		int							_flashChipId;
		char *						_stationMacAddress;
		char *						_softAPMacAddress;
		char *						_sdkVersion;
		long						_chipSize;
		char *						_label;
		
		DeviceSensors*				_deviceSensors;
		
};

#endif