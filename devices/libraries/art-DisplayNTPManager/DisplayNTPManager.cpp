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
	this->_displayManager->display.setCursor(0, 1);       
		
	if(this->_wifiManager->isConnected()){
		String formattedTime = this->_ntpManager->getFormattedTime();
		this->_displayManager->display.println(formattedTime);	
	}	
}

void DisplayNTPManager::printUpdate(bool on)
{
	this->_displayManager->display.setFont();
	this->_displayManager->display.setTextSize(1);
	if(on)		
		this->_displayManager->display.setTextColor(BLACK, WHITE);
	else
		this->_displayManager->display.setTextColor(WHITE, BLACK);
  
	this->_displayManager->display.setCursor(66, 0);       
	this->_displayManager->display.println("T");	
	
	this->_displayManager->display.drawChar(66, 9, 'S', WHITE, BLACK, 1);	
	//this->_displayManager->display.drawChar(64, 8, 'S', BLACK, WHITE, 1);	
	
	this->_displayManager->display.setFont();
}
