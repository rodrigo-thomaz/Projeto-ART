#include "DisplayMQTTManager.h"
#include "Arduino.h"
#include "DebugManager.h"
#include "DisplayManager.h"

DisplayMQTTManager::DisplayMQTTManager(DisplayManager& displayManager, DebugManager& debugManager)
{
	this->_displayManager = &displayManager;
	this->_debugManager = &debugManager;
	
	this->_x = 80;
	this->_y = 0;
}

DisplayMQTTManager::~DisplayMQTTManager()
{
}

void DisplayMQTTManager::printConnected()
{
	int y = this->_y + 9;
	
	this->_displayManager->display.setTextSize(1);
	this->_displayManager->display.setTextColor(WHITE, BLACK);  
	this->_displayManager->display.setCursor(this->_x, y);
	this->_displayManager->display.println("ART");  
}

void DisplayMQTTManager::printSent(bool on)
{
	if(on) 
		this->_displayManager->display.setTextColor(BLACK, WHITE);  		
	else 
		this->_displayManager->display.setTextColor(WHITE, BLACK);  
	
	this->_displayManager->display.setTextSize(1);	
	this->_displayManager->display.setCursor(this->_x, this->_y);
	this->_displayManager->display.write(24); // ↑
}

void DisplayMQTTManager::printReceived(bool on)
{
	int x = this->_x + 12;
	
	if(on) 
		this->_displayManager->display.setTextColor(BLACK, WHITE);  		
	else 
		this->_displayManager->display.setTextColor(WHITE, BLACK);  
	
	this->_displayManager->display.setTextSize(1);
	this->_displayManager->display.setCursor(x, this->_y);
	this->_displayManager->display.write(25); // ↓
}

