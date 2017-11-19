#include "DisplayTemperatureSensorManager.h"
#include "Arduino.h"
#include "DebugManager.h"
#include "DisplayManager.h"
#include "DSFamilyTempSensorManager.h"

DisplayTemperatureSensorManager::DisplayTemperatureSensorManager(DisplayManager& displayManager, DSFamilyTempSensorManager& dsFamilyTempSensorManager, DebugManager& debugManager, TemperatureScaleManager& temperatureScaleManager, TemperatureScaleConverter& temperatureScaleConverter)
{
	this->_displayManager = &displayManager;
	this->_dsFamilyTempSensorManager = &dsFamilyTempSensorManager;
	this->_debugManager = &debugManager;
	this->_temperatureScaleManager = &temperatureScaleManager;
	this->_temperatureScaleConverter = &temperatureScaleConverter;
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
	
	int marginTop = 0;
	int marginLeft = 0;
	int marginRight = 0;
	int marginBotton = 0;
	
	int barWidth = 20;
	int barHeight = 35;
	
	// ---- engine
	
	int header 	 = 16;
	
	int screenX1 = marginLeft;
	int screenY1 = header + marginTop;
	
	int screenX2 = 128 - marginRight;
	int screenY2 = 64 - marginBotton;
	
	int screenWidth = screenX2 - screenX1;
	int screenHeight = screenY2 - screenY1;	
 
	DSFamilyTempSensor* sensors = this->_dsFamilyTempSensorManager->getSensors();
	
	int sensorsCount = sizeof(sensors);
    
	int spaceChunk = round((screenWidth - (barWidth * sensorsCount)) / (sensorsCount - 1));
		
	this->_displayManager->display.drawRect(screenX1, screenY1, screenWidth, screenHeight, WHITE);
 
	for(int i = 0; i < sensorsCount; ++i){	
		int barX = screenX1 + (i * barWidth) + (i * spaceChunk);
		int barY = screenY2 - barHeight;
		this->printSensor(sensors[i], barX, barY, barWidth, barHeight);	        
	}	
}

void DisplayTemperatureSensorManager::printSensor(DSFamilyTempSensor& dsFamilyTempSensor, int x, int y, int width, int height)
{
	if(!this->_temperatureScaleManager->begin()) return;
	
	int temperatureScaleId = dsFamilyTempSensor.getTemperatureScaleId();
	
	float tempConverted = this->_temperatureScaleConverter->convertFromCelsius(temperatureScaleId, dsFamilyTempSensor.getTempCelsius());	
	
	TemperatureScale& temperatureScale = this->_temperatureScaleManager->getById(temperatureScaleId);
		
	this->_displayManager->display.setFont();
    this->_displayManager->display.setTextSize(1);
    this->_displayManager->display.setTextColor(WHITE);
    this->_displayManager->display.setCursor(x, y - 10);   
	this->_displayManager->display.setTextWrap(false);
	this->_displayManager->display.print(tempConverted, 1);
    this->_displayManager->display.println(temperatureScale.getSymbol());
 	
	float lowChartLimiterCelsius = dsFamilyTempSensor.getLowChartLimiterCelsius();
	float highChartLimiterCelsius = dsFamilyTempSensor.getHighChartLimiterCelsius();
 
	float range = highChartLimiterCelsius - lowChartLimiterCelsius;
	float value = dsFamilyTempSensor.getTempCelsius() - lowChartLimiterCelsius;
	float percent = (value * 100) / range;
	int tempHeight = round((width * percent) / 100);
	int tempRectY = y + height - tempHeight;
	
	this->_displayManager->display.drawRect(x, y, width, height, WHITE);	
	this->_displayManager->display.fillRect(x, tempRectY, width, tempHeight, WHITE);
}
