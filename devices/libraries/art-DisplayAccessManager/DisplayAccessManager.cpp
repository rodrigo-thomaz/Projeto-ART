#include "DisplayAccessManager.h"
#include "Arduino.h"
#include "DebugManager.h"

DisplayAccessManager::DisplayAccessManager(DebugManager& debugManager, DisplayManager& displayManager)
{ 
	this->_debugManager = &debugManager;
	this->_displayManager = &displayManager;
}

void DisplayAccessManager::updatePin(String payloadContract)
{	
	Serial.println("******************** Update PIN ********************");

	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(payloadContract);

	if (!root.success()) {
		Serial.println("parse updatePin failed");
		return;
	}

	String hardwareId = root["hardwareId"];
	String flashChipId = root["flashChipId"];
	
	String pin = root["pin"];	
	double nextFireTimeInSeconds = root["nextFireTimeInSeconds"];	
	
	this->_pin = pin;
	this->_nextFireTimeInSeconds = trunc(nextFireTimeInSeconds);
}

void DisplayAccessManager::loop()
{	
	uint64_t now = millis();   
	
	if(now - _messageTimestamp > MESSAGE_INTERVAL) {		
	
		this->_displayManager->display.clearDisplay();
		this->_displayManager->display.setTextSize(2);
		this->_displayManager->display.setTextColor(WHITE);
		this->_displayManager->display.setCursor(0, 1); 
		this->_displayManager->display.println("ART Device");
				
		if(this->_nextFireTimeInSeconds >= 0){
			this->_displayManager->display.setCursor(0, 16); 
			this->_displayManager->display.print("PIN ");
			this->_displayManager->display.println(_pin);
			this->_displayManager->display.print(this->_nextFireTimeInSeconds);
			if(this->_nextFireTimeInSeconds > 0){
				this->_displayManager->display.println("segundos");
				this->_displayManager->display.println("restantes");				
			}
			else{
				 this->_displayManager->display.println("segundo");
				 this->_displayManager->display.println("restante");
			}
			this->_nextFireTimeInSeconds--;	
		}		
		else{			
			this->_displayManager->display.setCursor(0, 22); 
			this->_displayManager->display.println("aguardando");
			this->_displayManager->display.println("pin...");
		}	
		
		this->_displayManager->display.display();
		
		_messageTimestamp = now;
    } 
}

