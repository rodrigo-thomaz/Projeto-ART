#include "DisplayDeviceWiFiAccess.h"
#include "DisplayDevice.h"

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

	void DisplayDeviceWiFiAccess::updatePin(char* json)
	{
		Serial.println("******************** Update PIN ********************");

		StaticJsonBuffer<300> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);

		if (!root.success()) {
			Serial.print("parse updatePin failed: ");
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
}