#include "DisplayNTPManager.h"
#include "Arduino.h"
#include "DebugManager.h"
#include "DisplayManager.h"
#include "NTPManager.h"

DisplayNTPManager::DisplayNTPManager(DisplayManager& displayManager, NTPManager& ntpManager, DebugManager& debugManager, WiFiManager& wifiManager)
{
	this->_displayManager = &displayManager;
	this->_ntpManager = &ntpManager;
	this->_debugManager = &debugManager;
	this->_wifiManager = &wifiManager;
}

DisplayNTPManager::~DisplayNTPManager()
{
}

void DisplayNTPManager::printTime()
{
	this->_displayManager->display.setFont();
	this->_displayManager->display.setTextSize(2);
	this->_displayManager->display.setTextColor(WHITE);
	this->_displayManager->display.setCursor(0, 3);       
		
	if(this->_wifiManager->isConnected()){
		String formattedTime = this->_ntpManager->getFormattedTime();
		this->_displayManager->display.println(formattedTime);	
	}	
}
