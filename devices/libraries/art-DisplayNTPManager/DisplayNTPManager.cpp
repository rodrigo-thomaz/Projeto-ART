#include "DisplayNTPManager.h"
#include "Arduino.h"
#include "DebugManager.h"
#include "DisplayManager.h"
#include "NTPManager.h"

DisplayNTPManager::DisplayNTPManager(DisplayManager& displayManager, NTPManager& ntpManager, DebugManager& debugManager)
{
	this->_displayManager = &displayManager;
	this->_ntpManager = &ntpManager;
	this->_debugManager = &debugManager;	
}

DisplayNTPManager::~DisplayNTPManager()
{	
}

void DisplayNTPManager::updateCallback(bool update, bool forceUpdate)
{
	if(update){
		this->printTime();
		this->printUpdate(forceUpdate);   
	}  
}

void DisplayNTPManager::printTime()
{
	this->_displayManager->display.setFont();
	this->_displayManager->display.setTextSize(2);
	this->_displayManager->display.setTextColor(WHITE);
	this->_displayManager->display.setCursor(0, 1);       
	String formattedTime = this->_ntpManager->getFormattedTime();
	this->_displayManager->display.println(formattedTime);	
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
}
