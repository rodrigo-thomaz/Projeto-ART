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

#define ESP_DEVICE_UPDATE_PIN_TOPIC_SUB "ESPDevice/UpdatePinIoT"
#define ESP_DEVICE_SET_LABEL_TOPIC_SUB "ESPDevice/SetLabelIoT"

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

		char *						getDeviceId() const;
		char *						getDeviceDatasheetId() const;

		int							getChipId();
		int							getFlashChipId();
		long						getChipSize();

		char *						getLabel() const;
		
		char *						getWebApiHost() const;
		uint16_t					getWebApiPort();
		char * 						getWebApiUri() const;

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
		char *						_deviceDatasheetId;

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

		void						setLabel(char* json);

		void						onDeviceMQSubscribeNotInApplication();
		void						onDeviceMQSubscribeInApplication();
		void						onDeviceMQUnSubscribeNotInApplication();
		void						onDeviceMQUnSubscribeInApplication();
		void						onDeviceMQSubscription(char* topicKey, char* json);
	};
}

#endif