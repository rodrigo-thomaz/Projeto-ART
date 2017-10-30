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

void DisplayTemperatureSensorManager::printSensors()
{
	// variáveis
	
	int marginTop = 20;
	int marginLeft = 4;
	int marginRight = 4;
	int marginBotton = 0;
	
	int barWidth = 10;
	int barHeight = 15;
	
	// ---- engine
	
	int header 	 = 16;
	
	int screenX1 = marginLeft;
	int screenY1 = header + marginTop;
	
	int screenX2 = 128 - marginRight;
	int screenY2 = 64 - marginBotton;
	
	int screenWidth = screenX2 - screenX1;
	int screenHeight = screenY2 - screenY1;	
 
	TemperatureSensor* sensors = this->_temperatureSensorManager->getSensors();
	
	int sensorsCount = sizeof(sensors);
    
	int barChunk = screenWidth / (sensorsCount + 1);
   
	this->_displayManager->display.drawRect(screenX1, screenY1, screenWidth, screenHeight, WHITE);
 
	for(int i = 0; i < sensorsCount; ++i){	
		int barX = barChunk * (i + 1);
		int barY = screenY2 - barHeight;
		this->printSensor(sensors[i], barX, barY, barWidth, barHeight);	        
	}	
}

void DisplayTemperatureSensorManager::printSensor(TemperatureSensor& temperatureSensor, int x, int y, int width, int height)
{
	this->_displayManager->display.setFont();
    this->_displayManager->display.setTextSize(2);
    this->_displayManager->display.setTextColor(WHITE);
    this->_displayManager->display.setCursor(20, 20);       
	this->_displayManager->display.print(temperatureSensor.tempCelsius);
    this->_displayManager->display.println(" C");
      
	this->_displayManager->display.drawRect(x, y, width, height, WHITE);
	  
    // this->_displayManager->display.print(temperatureSensor.tempFahrenheit);
    // this->_displayManager->display.println(" F");
}
