#include "DisplayAccessManager.h"

DisplayAccessManager::DisplayAccessManager(DisplayManager& displayManager)
{
	this->_displayManager = &displayManager;
}

void DisplayAccessManager::updatePin(char* json)
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

void DisplayAccessManager::loop()
{
	uint64_t now = millis();

	if (now - _messageTimestamp > MESSAGE_INTERVAL) {

		this->_displayManager->display.clearDisplay();
		this->_displayManager->display.setTextSize(2);
		this->_displayManager->display.setTextColor(WHITE);
		this->_displayManager->display.setCursor(0, 1);
		this->_displayManager->display.println("ART Device");

		if (this->_nextFireTimeInSeconds >= 0) {
			this->_displayManager->display.setCursor(0, 16);
			this->_displayManager->display.print("PIN ");
			this->_displayManager->display.println(_pin);
			this->_displayManager->display.print(this->_nextFireTimeInSeconds);
			this->_displayManager->display.println(" s");
			if (this->_nextFireTimeInSeconds > 0) {
				this->_displayManager->display.println("restantes");
			}
			else {
				this->_displayManager->display.println("restante");
			}
			this->_nextFireTimeInSeconds--;
		}
		else {
			this->_displayManager->display.setCursor(0, 22);
			this->_displayManager->display.println("aguardando");
			this->_displayManager->display.println("pin...");
		}

		this->_displayManager->display.display();

		_messageTimestamp = now;
	}
}

