#include "DeviceDisplayWiFiAccess.h"
#include "DeviceDisplay.h"
#include "ESPDevice.h"
#include "DeviceMQ.h"

namespace ART
{
	DeviceDisplayWiFiAccess::DeviceDisplayWiFiAccess(DeviceDisplay* deviceDisplay)
	{
		_deviceDisplay = deviceDisplay;
	}

	DeviceDisplayWiFiAccess::~DeviceDisplayWiFiAccess()
	{

	}

	void DeviceDisplayWiFiAccess::create(DeviceDisplayWiFiAccess *(&deviceDisplayWiFiAccess), DeviceDisplay * deviceDisplay)
	{
		deviceDisplayWiFiAccess = new DeviceDisplayWiFiAccess(deviceDisplay);
	}

	void DeviceDisplayWiFiAccess::begin()
	{
		_deviceDisplay->getESPDevice()->getDeviceMQ()->addSubscriptionCallback([=](const char* topicKey, const char* json) { return onDeviceMQSubscription(topicKey, json); });
	}

	void DeviceDisplayWiFiAccess::updatePin(const char* json)
	{
		Serial.println(F("******************** Update PIN ********************"));

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);

		if (!root.success()) {
			Serial.print(F("parse updatePin failed: "));
			Serial.println(json);
			return;
		}

		String deviceId = root["deviceId"];
		String deviceDatasheetId = root["deviceDatasheetId"];
		String flashChipId = root["flashChipId"];

		String pin = root["pin"];
		double nextFireTimeInSeconds = root["nextFireTimeInSeconds"];

		this->_pin = pin;
		this->_nextFireTimeInSeconds = trunc(nextFireTimeInSeconds);
	}

	void DeviceDisplayWiFiAccess::loop()
	{
		uint64_t now = millis();

		if (now - _messageTimestamp > MESSAGE_INTERVAL) {

			this->_deviceDisplay->display.clearDisplay();
			this->_deviceDisplay->display.setTextSize(2);
			this->_deviceDisplay->display.setTextColor(WHITE);
			this->_deviceDisplay->display.setCursor(0, 1);
			this->_deviceDisplay->display.println("ART Device");

			if (this->_nextFireTimeInSeconds >= 0) {
				this->_deviceDisplay->display.setCursor(0, 16);
				this->_deviceDisplay->display.print("PIN ");
				this->_deviceDisplay->display.println(_pin);
				this->_deviceDisplay->display.print(this->_nextFireTimeInSeconds);
				this->_deviceDisplay->display.println(" s");
				if (this->_nextFireTimeInSeconds > 0) {
					this->_deviceDisplay->display.println("restantes");
				}
				else {
					this->_deviceDisplay->display.println("restante");
				}
				this->_nextFireTimeInSeconds--;
			}
			else {
				this->_deviceDisplay->display.setCursor(0, 22);
				this->_deviceDisplay->display.println("aguardando");
				this->_deviceDisplay->display.println("pin...");
			}

			this->_deviceDisplay->display.display();

			_messageTimestamp = now;
		}
	}

	bool DeviceDisplayWiFiAccess::onDeviceMQSubscription(const char * topicKey, const char * json)
	{
		if (strcmp(topicKey, ESP_DEVICE_UPDATE_PIN_TOPIC_SUB) == 0) {
			updatePin(json);
			return true;
		}
		else {
			return false;
		}
	}
}