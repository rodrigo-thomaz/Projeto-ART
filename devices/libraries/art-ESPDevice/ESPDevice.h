#ifndef ESPDevice_h
#define ESPDevice_h

#include "DeviceInApplication.h"
#include "DeviceDebug.h"
#include "DeviceWiFi.h"
#include "DeviceMQ.h"
#include "DeviceNTP.h"
#include "DeviceBinary.h"
#include "DeviceBuzzer.h"
#include "DeviceSensors.h"

#include "Arduino.h"
#include "ArduinoJson.h"
#include "ESP8266HTTPClient.h"

namespace ART
{
	class ESPDevice
	{
		public:
			
			ESPDevice(char* webApiHost, uint16_t webApiPort, char* webApiUri = "/");
			~ESPDevice();
			
			void						begin();
			void						loop();
					
			bool						loaded();
			
			char *						getDeviceId();
			short						getDeviceDatasheetId();
			
			int							getChipId();
			int							getFlashChipId();
			long						getChipSize();

			char *						getLabel();
			void						setLabel(char* json);
			
			char *						getWebApiHost();
			uint16_t					getWebApiPort();
			char * 						getWebApiUri();
			
			DeviceInApplication*		getDeviceInApplication();
			DeviceDebug*				getDeviceDebug();
			DeviceWiFi*					getDeviceWiFi();
			DeviceMQ*					getDeviceMQ();
			DeviceNTP*					getDeviceNTP();
			DeviceBinary*				getDeviceBinary();
			DeviceBuzzer*				getDeviceBuzzer();
			DeviceSensors*				getDeviceSensors();
		
		private:	

			char *						_deviceId;
			short						_deviceDatasheetId;
			
			int							_chipId;
			int							_flashChipId;
			long						_chipSize;
			
			char *						_label;
			
			char *						_webApiHost;
			uint16_t					_webApiPort;
			char * 						_webApiUri;
		
			DeviceInApplication*		_deviceInApplication;
			DeviceDebug*				_deviceDebug;
			DeviceWiFi*					_deviceWiFi;
			DeviceMQ*					_deviceMQ;
			DeviceNTP*					_deviceNTP;
			DeviceBinary*				_deviceBinary;
			DeviceBuzzer*				_deviceBuzzer;
			DeviceSensors*				_deviceSensors;		
			
			void						autoLoad();
			void						load(String json);
			bool 						_loaded = false;	
	};
}

#endif