#include "DisplayDeviceWiFiAccess.h"
#include "DisplayDevice.h"
#include "ESPDevice.h"
#include "DeviceMQ.h"

namespace ART
{
	DisplayDeviceWiFiAccess::DisplayDeviceWiFiAccess(DisplayDevice* displayDevice)
	{
		_displayDevice = displayDevice;
	}

	DisplayDeviceWiFiAccess::~DisplayDeviceWiFiAccess()
	{

	}

	void DisplayDeviceWiFiAccess::create(DisplayDeviceWiFiAccess *(&displayDeviceWiFiAccess), DisplayDevice * displayDevice)
	{
		displayDeviceWiFiAccess = new DisplayDeviceWiFiAccess(displayDevice);
	}

	void DisplayDeviceWiFiAccess::begin()
	{
		_displayDevice->getESPDevice()->getDeviceMQ()->addSubscriptionCallback([=](const char* topicKey, const char* json) { return onDeviceMQSubscription(topicKey, json); });
	}

	void DisplayDeviceWiFiAccess::updatePin(const char* json)
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

	void DisplayDeviceWiFiAccess::loop()
	{
		uint64_t now = millis();

		if (now - _messageTimestamp > MESSAGE_INTERVAL) {

			this->_displayDevice->display.clearDisplay();
			this->_displayDevice->display.setTextSize(2);
			this->_displayDevice->display.setTextColor(WHITE);
			this->_displayDevice->display.setCursor(0, 1);
			this->_displayDevice->display.println("ART Device");

			if (this->_nextFireTimeInSeconds >= 0) {
				this->_displayDevice->display.setCursor(0, 16);
				this->_displayDevice->display.print("PIN ");
				this->_displayDevice->display.println(_pin);
				this->_displayDevice->display.print(this->_nextFireTimeInSeconds);
				this->_displayDevice->display.println(" s");
				if (this->_nextFireTimeInSeconds > 0) {
					this->_displayDevice->display.println("restantes");
				}
				else {
					this->_displayDevice->display.println("restante");
				}
				this->_nextFireTimeInSeconds--;
			}
			else {
				this->_displayDevice->display.setCursor(0, 22);
				this->_displayDevice->display.println("aguardando");
				this->_displayDevice->display.println("pin...");
			}

			this->_displayDevice->display.display();

			_messageTimestamp = now;
		}
	}

	bool DisplayDeviceWiFiAccess::onDeviceMQSubscription(const char * topicKey, const char * json)
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