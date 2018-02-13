#ifndef DeviceDisplayWiFi_h
#define DeviceDisplayWiFi_h

#include "functional"
#include "vector"
#include "Arduino.h"
#include "../DeviceWiFi/DeviceWiFi.h"

namespace ART
{
	class DeviceDisplay;

	class DeviceDisplayWiFi
	{
	public:
		DeviceDisplayWiFi(DeviceDisplay* deviceDisplay);
		~DeviceDisplayWiFi();

		static void														create(DeviceDisplayWiFi* (&deviceDisplayWiFi), DeviceDisplay* deviceDisplay);

		void 															printSignal();

	private:

		DeviceDisplay *													_deviceDisplay;

		bool 															_firstTimecaptivePortalCallback = true;

		void															printPortalHeaderInDisplay(String title);
		void 															showEnteringSetup();
		void 															showWiFiConect();

		void															printConnectedSignal(int x, int y, int barWidth, int margin, int barSignal);
		void															printNoConnectedSignal(int x, int y, int barWidth, int margin);

		void 															startConfigPortalCallback();
		void 															captivePortalCallback(String ip);
		void 															successConfigPortalCallback();
		void 															failedConfigPortalCallback(int connectionResult);
		void 															connectingConfigPortalCallback();

		DEVICE_WIFI_SET_START_CONFIG_PORTAL_CALLBACK_SIGNATURE 			_startConfigPortalCallback;
		DEVICE_WIFI_SET_CAPTIVE_PORTAL_CALLBACK_SIGNATURE				_captivePortalCallback;
		DEVICE_WIFI_SET_SUCCESS_CONFIG_PORTAL_CALLBACK_SIGNATURE 		_successConfigPortalCallback;
		DEVICE_WIFI_SET_FAILED_CONFIG_PORTAL_CALLBACK_SIGNATURE			_failedConfigPortalCallback;
		DEVICE_WIFI_SET_CONNECTING_CONFIG_PORTAL_CALLBACK_SIGNATURE 	_connectingConfigPortalCallback;

	};
}

#endif