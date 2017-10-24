#include "DisplayMQTTManager.h"
#include "Arduino.h"
#include "DebugManager.h"
#include "DisplayManager.h"

DisplayMQTTManager::DisplayMQTTManager(DisplayManager& displayManager, DebugManager& debugManager)
{
	this->_displayManager = &displayManager;
	this->_debugManager = &debugManager;
}

DisplayMQTTManager::~DisplayMQTTManager()
{
}

void DisplayMQTTManager::printConnected()
{
	int x = 74;
	int y = 9;
	
	this->_displayManager->display.setTextSize(1);
	this->_displayManager->display.setTextColor(WHITE, BLACK);  
	this->_displayManager->display.setCursor(x, y);
	this->_displayManager->display.println("ART");  
}

void DisplayMQTTManager::printSent()
{
	int x = 74;
	int y = 0;
	
	this->_displayManager->display.setTextSize(1);
	this->_displayManager->display.setTextColor(WHITE, BLACK);  
	this->_displayManager->display.setCursor(x, y);
	this->_displayManager->display.write(24); // ↑
}

void DisplayMQTTManager::printReceived()
{
	int x = 86;
	int y = 0;
	
	this->_displayManager->display.setTextSize(1);
	this->_displayManager->display.setTextColor(WHITE, BLACK);  
	this->_displayManager->display.setCursor(x, y);
	this->_displayManager->display.write(25); // ↓
}

