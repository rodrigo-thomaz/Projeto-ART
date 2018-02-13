#include "DisplayDeviceSensor.h"
#include "../DisplayDevice/DisplayDevice.h"
#include "../ESPDevice/ESPDevice.h"
#include "../DeviceSensor/SensorInDevice.h"
#include "../UnitMeasurement/UnitMeasurementEnum.h"

namespace ART
{
	DisplayDeviceSensor::DisplayDeviceSensor(DisplayDevice* displayDevice)
	{
		_displayDevice = displayDevice;
	}

	DisplayDeviceSensor::~DisplayDeviceSensor()
	{
	}

	void DisplayDeviceSensor::create(DisplayDeviceSensor *(&displayDeviceSensor), DisplayDevice * displayDevice)
	{
		displayDeviceSensor = new DisplayDeviceSensor(displayDevice);
	}

	void DisplayDeviceSensor::printUpdate(bool on)
	{
		_displayDevice->display.setFont();
		_displayDevice->display.setTextSize(1);
		if (on) {
			_displayDevice->display.setTextColor(BLACK, WHITE);
			_displayDevice->display.setCursor(66, 8);
		}
		else {
			_displayDevice->display.setTextColor(WHITE, BLACK);
			_displayDevice->display.setCursor(66, 9);
		}
		_displayDevice->display.println("S");
	}

	void DisplayDeviceSensor::printSensors()
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

		std::tuple<SensorInDevice**, short> tpl = _displayDevice->getESPDevice()->getDeviceSensor()->getSensorsInDevice();

		SensorInDevice** sensorsInDevice = std::get<0>(tpl);
		short sensorsCount = std::get<1>(tpl);

		int boxChunk = round(screenWidth / sensorsCount);

		//_displayDevice->display.drawRect(screenX1, screenY1, screenWidth, screenHeight, WHITE);

		for (int i = 0; i < sensorsCount; ++i) {
			Sensor* sensor = sensorsInDevice[i]->getSensor();
			int boxX = screenX1 + (boxChunk * i);
			this->printBar(sensor, boxX, screenY1, boxChunk, screenHeight);
			this->printText(sensor, boxX, screenY1);
		}
	}

	void DisplayDeviceSensor::printBar(Sensor* sensor, int x, int y, int width, int height)
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
		//_displayDevice->display.drawRect(x, y, width, height, WHITE);	
		// Bar
		_displayDevice->display.drawRect(barX1, barY1, barWidth, barHeight, WHITE);
		/// Bar Value
		this->printBarValue(sensor, barX1, barY1, barWidth, barHeight);
	}

	void DisplayDeviceSensor::printBarValue(Sensor* sensor, int x, int y, int width, int height)
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

		_displayDevice->display.fillRect(x, tempRectY, width, tempHeight, WHITE);
	}

	void DisplayDeviceSensor::printText(Sensor* sensor, int x, int y)
	{
		UnitMeasurementEnum unitMeasurementId = sensor->getSensorUnitMeasurementScale()->getUnitMeasurementId();

		//float tempConverted = UnitMeasurementConverter::convertFromCelsius(unitMeasurementId, sensor->getValue());

		//Temporario
		String symbol = "C";

		_displayDevice->display.setFont();
		_displayDevice->display.setTextSize(1);
		_displayDevice->display.setTextColor(WHITE);
		_displayDevice->display.setCursor(x, y);
		_displayDevice->display.setTextWrap(false);
		_displayDevice->display.print(sensor->getValue(), 1);
		_displayDevice->display.println(symbol);
	}
}