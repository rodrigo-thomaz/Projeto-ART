#include "DisplayTemperatureSensorManager.h"
#include "Arduino.h"
#include "DebugManager.h"
#include "DisplayManager.h"
#include "TemperatureSensorManager.h"

DisplayTemperatureSensorManager::DisplayTemperatureSensorManager(DisplayManager& displayManager, TemperatureSensorManager& temperatureSensorManager, DebugManager& debugManager)
{
	this->_displayManager = &displayManager;
	this->_temperatureSensorManager = &temperatureSensorManager;
	this->_debugManager = &debugManager;
}

DisplayTemperatureSensorManager::~DisplayTemperatureSensorManager()
{
}

void DisplayTemperatureSensorManager::printSensors()
{
	this->_displayManager->display.setFont();
    this->_displayManager->display.setTextSize(2);
    this->_displayManager->display.setTextColor(WHITE);
    this->_displayManager->display.setCursor(0, 16);       
        
	if(sizeof(this->_temperatureSensorManager->Sensors)/sizeof(int) > 0){
      
      this->_displayManager->display.print(this->_temperatureSensorManager->Sensors[0].tempCelsius);
      this->_displayManager->display.println(" C");
      
      //this->_displayManager->display.print(temperatureSensorManager.Sensors[0].tempFahrenheit);
      //this->_displayManager->display.println(" F");
    } 
}

void DisplayTemperatureSensorManager::printUpdate(bool on)
{
	this->_displayManager->display.setFont();
	this->_displayManager->display.setTextSize(1);
	if(on) {
		
		this->_displayManager->display.setTextColor(BLACK, WHITE);
		this->_displayManager->display.setCursor(66, 8);       
	}
	else {
		this->_displayManager->display.setTextColor(WHITE, BLACK);
		this->_displayManager->display.setCursor(66, 9);       
	}
	
	this->_displayManager->display.println("S");	
}
