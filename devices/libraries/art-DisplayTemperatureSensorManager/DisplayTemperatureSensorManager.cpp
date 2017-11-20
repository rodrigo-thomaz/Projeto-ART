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
	if(!this->_temperatureScaleManager->begin()) return;
	
	// variáveis
	
	int marginTop = 2;
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
    
	int boxChunk = round(screenWidth / sensorsCount);
		
	//this->_displayManager->display.drawRect(screenX1, screenY1, screenWidth, screenHeight, WHITE);
 
	for(int i = 0; i < sensorsCount; ++i){	
		int boxX = screenX1 + (boxChunk * i);
		this->printBar(sensors[i], boxX, screenY1, boxChunk, screenHeight);	
		this->printText(sensors[i], boxX, screenY1);	        
	}	
}

void DisplayTemperatureSensorManager::printBar(DSFamilyTempSensor& dsFamilyTempSensor, int x, int y, int width, int height)
{
	int barMarginTop = 10;
	int barMarginLeft = 5;
	int barMarginRight = 5;
	int barMarginBotton = 0;
	
	int barX1 = x + barMarginLeft;
	int barY1 = y + barMarginTop;
	
	int barX2 = x + width - barMarginRight;
	int barY2 = y + height - barMarginBotton;
	
	int barWidth = barX2 - barX1;
	int barHeight = barY2 - barY1;	
	
	// Box
	//this->_displayManager->display.drawRect(x, y, width, height, WHITE);	
	// Bar
	this->printBarValue(dsFamilyTempSensor, barX1, barY1, barWidth, barHeight);	        
}

void DisplayTemperatureSensorManager::printBarValue(DSFamilyTempSensor& dsFamilyTempSensor, int x, int y, int width, int height)
{	
	float highChartLimiterCelsius = dsFamilyTempSensor.getHighChartLimiterCelsius();
	float lowChartLimiterCelsius = dsFamilyTempSensor.getLowChartLimiterCelsius();	
 
 
	Serial.println("lowChartLimiterCelsius lowChartLimiterCelsius lowChartLimiterCelsius lowChartLimiterCelsius lowChartLimiterCelsius ");
	Serial.println(lowChartLimiterCelsius);
 
	float range = highChartLimiterCelsius - lowChartLimiterCelsius;
	float value = dsFamilyTempSensor.getTempCelsius() - lowChartLimiterCelsius;
	float percent = (value * 100) / range;
	
	int tempHeight = dsFamilyTempSensor.getTempCelsius() - lowChartLimiterCelsius;
	
	if(tempHeight > height) tempHeight = height;
	else if(tempHeight < height) tempHeight = 0;
	
	//int tempHeight = round((width * percent) / 100);
	int tempRectY = y + height - tempHeight;
	
	this->_displayManager->display.drawRect(x, y, width, height, WHITE);	
	this->_displayManager->display.fillRect(x, tempRectY, width, tempHeight, WHITE);
}

void DisplayTemperatureSensorManager::printText(DSFamilyTempSensor& dsFamilyTempSensor, int x, int y)
{
	int temperatureScaleId = dsFamilyTempSensor.getTemperatureScaleId();
	
	float tempConverted = this->_temperatureScaleConverter->convertFromCelsius(temperatureScaleId, dsFamilyTempSensor.getTempCelsius());	
	
	TemperatureScale& temperatureScale = this->_temperatureScaleManager->getById(temperatureScaleId);
		
	this->_displayManager->display.setFont();
    this->_displayManager->display.setTextSize(1);
    this->_displayManager->display.setTextColor(WHITE);
    this->_displayManager->display.setCursor(x, y);   
	this->_displayManager->display.setTextWrap(false);
	this->_displayManager->display.print(tempConverted, 1);
    this->_displayManager->display.println(temperatureScale.getSymbol());
}