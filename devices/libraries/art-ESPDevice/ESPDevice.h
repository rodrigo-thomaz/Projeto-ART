#ifndef ESPDevice_h
#define ESPDevice_h

#include "DeviceInApplication.h"
#include "DeviceMQ.h"
#include "DeviceNTP.h"
#include "DeviceSensors.h"

#include "WiFiManager.h"

#include "Arduino.h"
#include "ArduinoJson.h"
#include <ESP8266WiFi.h>
#include "ESP8266HTTPClient.h"
#include "RemoteDebug.h"        //https://github.com/JoaoLopesF/RemoteDebug

class DeviceMQ;
class DeviceNTP;
class DeviceSensors;

class ESPDevice
{
	public:
		
		ESPDevice(WiFiManager& wifiManager, char* webApiHost, uint16_t webApiPort, char* webApiUri = "/");
		~ESPDevice();
		
		void						begin();
		void						loop();
				
		bool						loaded();
		
		char *						getDeviceId();
		short						getDeviceDatasheetId();
		
		int							getChipId();
		int							getFlashChipId();
		long						getChipSize();
		
		char *						getStationMacAddress();
		char *						getSoftAPMacAddress();		

		char *						getLabel();
		void						setLabel(char * value);
		
		char *						getWebApiHost();
		uint16_t					getWebApiPort();
		char * 						getWebApiUri();
		
		DeviceInApplication*		getDeviceInApplication();
		DeviceMQ*					getDeviceMQ();
		DeviceNTP*					getDeviceNTP();
		DeviceSensors*				getDeviceSensors();
		
		RemoteDebug*				getDebug();			
	
	private:	

		char *						_deviceId;
		short						_deviceDatasheetId;
		
		int							_chipId;
		int							_flashChipId;
		long						_chipSize;
		
		char *						_stationMacAddress;
		char *						_softAPMacAddress;		
		
		char *						_label;
		
		char *						_webApiHost;
		uint16_t					_webApiPort;
		char * 						_webApiUri;
	
		DeviceInApplication*		_deviceInApplication;
		DeviceMQ*					_deviceMQ;
		DeviceNTP*					_deviceNTP;
		DeviceSensors*				_deviceSensors;
		
		RemoteDebug* 				_debug;
		
		void						autoLoad();
		void						load(String json);
		bool 						_loaded = false;
		
		WiFiManager*          		_wifiManager;		
};

#endif