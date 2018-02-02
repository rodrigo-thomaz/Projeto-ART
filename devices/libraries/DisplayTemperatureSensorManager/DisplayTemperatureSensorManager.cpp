#include "DisplayTemperatureSensorManager.h"

DisplayTemperatureSensorManager::DisplayTemperatureSensorManager(DisplayManager& displayManager, ESPDevice& espDevice, UnitMeasurementConverter& unitMeasurementConverter)
{
	this->_displayManager = &displayManager;
	this->_espDevice = &espDevice;
	this->_unitMeasurementConverter = &unitMeasurementConverter;
}

DisplayTemperatureSensorManager::~DisplayTemperatureSensorManager()
{
}

void DisplayTemperatureSensorManager::printUpdate(bool on)
{
	this->_displayManager->display.setFont();
	this->_displayManager->display.setTextSize(1);
	if (on) {
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

	int header = 16;

	int screenX1 = marginLeft;
	int screenY1 = header + marginTop;

	int screenX2 = 128 - marginRight;
	int screenY2 = 64 - marginBotton;

	int screenWidth = screenX2 - screenX1;
	int screenHeight = screenY2 - screenY1;

	SensorInDevice* sensorsInDevice = _espDevice->getDeviceSensors()->getSensorsInDevice();	

	int sensorsCount = sizeof(sensorsInDevice);

	int boxChunk = round(screenWidth / sensorsCount);

	//this->_displayManager->display.drawRect(screenX1, screenY1, screenWidth, screenHeight, WHITE);

	for (int i = 0; i < sensorsCount; ++i) {
		Sensor* sensor = sensorsInDevice[i].getSensor();
		int boxX = screenX1 + (boxChunk * i);
		this->printBar(sensor, boxX, screenY1, boxChunk, screenHeight);
		this->printText(sensor, boxX, screenY1);
	}
}

void DisplayTemperatureSensorManager::printBar(Sensor* sensor, int x, int y, int width, int height)
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
	this->printBarValue(sensor, barX1, barY1, barWidth, barHeight);
}

void DisplayTemperatureSensorManager::printBarValue(Sensor* sensor, int x, int y, int width, int height)
{
	float chartLimiterMax = sensor->getSensorUnitMeasurementScale()->getChartLimiterMax();
	float chartLimiterMin = sensor->getSensorUnitMeasurementScale()->getChartLimiterMin();

	float range = chartLimiterMax - chartLimiterMin;
	float value = sensor->getValue() - chartLimiterMin;
	float percent = (value * 100) / range;

	int tempHeight = round((height * percent) / 100);

	if (tempHeight > height) tempHeight = height;
	else if (tempHeight < 0) tempHeight = 0;

	int tempRectY = y + height - tempHeight;

	this->_displayManager->display.fillRect(x, tempRectY, width, tempHeight, WHITE);
}

void DisplayTemperatureSensorManager::printText(Sensor* sensor, int x, int y)
{
	UnitMeasurementEnum unitMeasurementId = sensor->getSensorUnitMeasurementScale()->getUnitMeasurementId();

	float tempConverted = this->_unitMeasurementConverter->convertFromCelsius(unitMeasurementId, sensor->getValue());

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