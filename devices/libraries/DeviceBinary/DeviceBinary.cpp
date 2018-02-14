#include "DeviceBinary.h"
#include "ESPDevice.h"

namespace ART
{
	DeviceBinary::DeviceBinary(ESPDevice* espDevice)
	{
		_espDevice = espDevice;
	}

	DeviceBinary::~DeviceBinary()
	{

	}

	void DeviceBinary::loop()
	{
		if (!_espDevice->getDeviceWiFi()->isConnected()) {
			return;
		}

		uint64_t now = millis();

		if (now - _checkForUpdatesTimestamp > CHECKFORUPDATES_INTERVAL) {
			_checkForUpdatesTimestamp = now;
			update();
		}
	}

	void DeviceBinary::getByKeyPub()
	{
		Serial.println(F("[DeviceBinary::getByKeyPub] begin"));
		_espDevice->getDeviceMQ()->publishInApplication(DEVICE_BINARY_GET_BY_KEY_TOPIC_PUB, _espDevice->getDeviceKeyAsJson());
		Serial.println(F("[DeviceBinary::getByKeyPub] end"));
	}

	void DeviceBinary::getByKeySub(const char * json)
	{
		Serial.println(F("[DeviceBinary::getByKeySub] Aqui !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"));
	}

	void DeviceBinary::update()
	{
		std::string uri;
		uri.append(_espDevice->getWebApiUri());
		uri.append("api/espDevice/checkForUpdates");

		t_httpUpdate_return ret = ESPhttpUpdate.update(_espDevice->getWebApiHost(), _espDevice->getWebApiPort(), uri.c_str());

		switch (ret) {
		case HTTP_UPDATE_FAILED:
			Serial.printf("HTTP_UPDATE_FAILD Error (%d): %s", ESPhttpUpdate.getLastError(), ESPhttpUpdate.getLastErrorString().c_str());
			break;

		case HTTP_UPDATE_NO_UPDATES:
			Serial.println(F("HTTP_UPDATE_NO_UPDATES"));
			break;

		case HTTP_UPDATE_OK:
			Serial.println(F("HTTP_UPDATE_OK"));
			break;
		}

		Serial.println();
	}
}