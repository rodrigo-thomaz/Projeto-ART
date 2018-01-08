#include "DisplayTemperatureSensorManager.h"

DisplayTemperatureSensorManager::DisplayTemperatureSensorManager(DisplayManager& displayManager, DSFamilyTempSensorManager& dsFamilyTempSensorManager, UnitOfMeasurementConverter& unitOfMeasurementConverter)
{
	this->_displayManager = &displayManager;
	this->_dsFamilyTempSensorManager = &dsFamilyTempSensorManager;
	this->_unitOfMeasurementConverter = &unitOfMeasurementConverter;
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
	this->_displayManager->display.drawRect(barX1, barY1, barWidth, barHeight, WHITE);	
	/// Bar Value
	this->printBarValue(dsFamilyTempSensor, barX1, barY1, barWidth, barHeight);	        
}

void DisplayTemperatureSensorManager::printBarValue(DSFamilyTempSensor& dsFamilyTempSensor, int x, int y, int width, int height)
{	
	float highChartLimiterCelsius = dsFamilyTempSensor.getHighChartLimiterCelsius();
	float lowChartLimiterCelsius = dsFamilyTempSensor.getLowChartLimiterCelsius();	
 
	float range = highChartLimiterCelsius - lowChartLimiterCelsius;
	float value = dsFamilyTempSensor.getTempCelsius() - lowChartLimiterCelsius;
	float percent = (value * 100) / range;
	
	int tempHeight = round((height * percent) / 100);

	if(tempHeight > height) tempHeight = height;
	else if(tempHeight < 0) tempHeight = 0;
		
	int tempRectY = y + height - tempHeight;
		
	this->_displayManager->display.fillRect(x, tempRectY, width, tempHeight, WHITE);
}

void DisplayTemperatureSensorManager::printText(DSFamilyTempSensor& dsFamilyTempSensor, int x, int y)
{
	int unitOfMeasurementId = dsFamilyTempSensor.getUnitOfMeasurementId();
	
	float tempConverted = this->_unitOfMeasurementConverter->convertFromCelsius(unitOfMeasurementId, dsFamilyTempSensor.getTempCelsius());	

    //Temporario
	String symbol = "C";
		
	this->_displayManager->display.setFont();
    this->_displayManager->display.setTextSize(1);
    this->_displayManager->display.setTextColor(WHITE);
    this->_displayManager->display.setCursor(x, y);   
	this->_displayManager->display.setTextWrap(false);
	this->_displayManager->display.print(tempConverted, 1);
    this->_displayManager->display.println(symbol);
}