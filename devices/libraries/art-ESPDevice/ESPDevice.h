#ifndef ESPDevice_h
#define ESPDevice_h

#include "Arduino.h"
#include "DeviceSensors.h"

class ESPDevice
{
	public:
		
		ESPDevice(String espDeviceId, short deviceDatasheetId, String label);
		~ESPDevice();
		
		String						getESPDeviceId();
		short						getDeviceDatasheetId();
		int							getChipId();
		int							getFlashChipId();
		String						getStationMacAddress();
		String						getSoftAPMacAddress();
		String						getSDKVersion();
		long						getChipSize();

		String						getLabel();
		void						setLabel(String value);
		
		DeviceSensors*				getDeviceSensors();
	
	private:	

		String						_espDeviceId;
		short						_deviceDatasheetId;
		int							_chipId;
		int							_flashChipId;
		String						_stationMacAddress;
		String						_softAPMacAddress;
		String						_sdkVersion;
		long						_chipSize;
		String						_label;
		
		DeviceSensors*				_deviceSensors;
		
};

#endif