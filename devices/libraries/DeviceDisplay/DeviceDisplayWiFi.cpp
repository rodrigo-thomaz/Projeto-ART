#include "DeviceDisplayWiFi.h"

#include "../DeviceDisplay/DeviceDisplay.h"
#include "../ESPDevice/ESPDevice.h"
#include "../DeviceWiFi/DeviceWiFi.h"

#include "../Adafruit-GFX-Library/Fonts/FreeSans9pt7b.h"
#include "../Adafruit-GFX-Library/Fonts/FreeSansBold9pt7b.h"

namespace ART
{
	DeviceDisplayWiFi::DeviceDisplayWiFi(DeviceDisplay* deviceDisplay)
	{
		_deviceDisplay = deviceDisplay;

		this->_startConfigPortalCallback = std::bind(&DeviceDisplayWiFi::startConfigPortalCallback, this);
		this->_captivePortalCallback = [=](String ip) { this->captivePortalCallback(ip); };
		this->_successConfigPortalCallback = std::bind(&DeviceDisplayWiFi::successConfigPortalCallback, this);
		this->_failedConfigPortalCallback = [=](int connectionResult) { this->failedConfigPortalCallback(connectionResult); };
		this->_connectingConfigPortalCallback = std::bind(&DeviceDisplayWiFi::connectingConfigPortalCallback, this);

		_deviceDisplay->getESPDevice()->getDeviceWiFi()->setStartConfigPortalCallback(this->_startConfigPortalCallback);
		_deviceDisplay->getESPDevice()->getDeviceWiFi()->setCaptivePortalCallback(this->_captivePortalCallback);
		_deviceDisplay->getESPDevice()->getDeviceWiFi()->setSuccessConfigPortalCallback(this->_successConfigPortalCallback);
		_deviceDisplay->getESPDevice()->getDeviceWiFi()->setFailedConfigPortalCallback(this->_failedConfigPortalCallback);
		_deviceDisplay->getESPDevice()->getDeviceWiFi()->setConnectingConfigPortalCallback(this->_connectingConfigPortalCallback);
	}

	DeviceDisplayWiFi::~DeviceDisplayWiFi()
	{
	}

	void DeviceDisplayWiFi::create(DeviceDisplayWiFi *(&deviceDisplayWiFi), DeviceDisplay * deviceDisplay)
	{
		deviceDisplayWiFi = new DeviceDisplayWiFi(deviceDisplay);
	}

	void DeviceDisplayWiFi::printSignal() {

		int quality = _deviceDisplay->getESPDevice()->getDeviceWiFi()->getQuality();
		int barSignal = _deviceDisplay->getESPDevice()->getDeviceWiFi()->convertQualitytToBarsSignal(quality);

		if (_deviceDisplay->getESPDevice()->getDeviceWiFi()->isConnected())
			this->printConnectedSignal(106, 0, 4, 2, barSignal);
		else
			this->printNoConnectedSignal(106, 0, 4, 2);
	}

	void DeviceDisplayWiFi::printConnectedSignal(int x, int y, int barWidth, int margin, int barSignal) {

		int barsCount = 4;
		int barHeight = 4;

		for (int i = 0; i < 4; i++)
		{
			int currentX = x + (barWidth * i) + (margin * i);
			int currentY = (y + (barsCount - i - 1) * barHeight);
			int currentHeight = 0;

			currentHeight = barHeight * (i + 1);

			if (barSignal <= i)
			{
				_deviceDisplay->display.drawRect(currentX, currentY, barWidth, currentHeight, WHITE);
			}
			else
			{
				_deviceDisplay->display.fillRect(currentX, currentY, barWidth, currentHeight, WHITE);
			}
		}
	}

	void DeviceDisplayWiFi::printNoConnectedSignal(int x, int y, int barWidth, int margin) {

		int barsCount = 4;
		int barHeight = 4;

		for (int i = 0; i < 4; i++)
		{
			int currentX = x + (barWidth * i) + (margin * i);
			int currentY = (y + (barsCount - i - 1) * barHeight);
			int currentHeight = 0;

			currentHeight = barHeight * (i + 1);

			_deviceDisplay->display.drawRect(currentX, currentY, barWidth, currentHeight, WHITE);
		}

		_deviceDisplay->display.setFont();
		_deviceDisplay->display.setTextSize(1);
		_deviceDisplay->display.setTextColor(BLACK, WHITE);
		_deviceDisplay->display.setCursor(x + 15, y + 8);
		_deviceDisplay->display.setTextWrap(false);
		_deviceDisplay->display.println("X");
	}

	void DeviceDisplayWiFi::printPortalHeaderInDisplay(String title)
	{
		_deviceDisplay->display.setFont();
		_deviceDisplay->display.setTextSize(2);
		_deviceDisplay->display.setCursor(0, 0);
		_deviceDisplay->display.setTextWrap(false);
		_deviceDisplay->display.setTextColor(BLACK, WHITE);
		_deviceDisplay->display.println(title);
		_deviceDisplay->display.display();
		_deviceDisplay->display.setTextColor(WHITE);
		_deviceDisplay->display.setTextSize(1);
	}

	void DeviceDisplayWiFi::showEnteringSetup()
	{
		_deviceDisplay->display.stopscroll();

		_deviceDisplay->display.clearDisplay();
		_deviceDisplay->display.setTextSize(2);
		_deviceDisplay->display.setTextColor(WHITE);
		_deviceDisplay->display.setCursor(0, 0);

		_deviceDisplay->display.setFont();

		_deviceDisplay->display.println(" entrando");
		_deviceDisplay->display.println(" no setup");
		_deviceDisplay->display.println(" do  wifi");

		_deviceDisplay->display.display();

		delay(400);

		_deviceDisplay->display.print(" ");
		for (int i = 0; i <= 6; i++) {
			_deviceDisplay->display.print(".");
			_deviceDisplay->display.display();
			delay(400);
		}
	}

	void DeviceDisplayWiFi::showWiFiConect()
	{
		String configPortalSSID = _deviceDisplay->getESPDevice()->getDeviceWiFi()->getConfigPortalSSID();
		String configPortalPwd = _deviceDisplay->getESPDevice()->getDeviceWiFi()->getConfigPortalPwd();

		_deviceDisplay->display.clearDisplay();

		printPortalHeaderInDisplay("  Conecte  ");

		_deviceDisplay->display.println();
		_deviceDisplay->display.println();
		_deviceDisplay->display.setFont(&FreeSansBold9pt7b);
		_deviceDisplay->display.setTextSize(1);
		_deviceDisplay->display.print("ssid:  ");
		_deviceDisplay->display.println(configPortalSSID);
		_deviceDisplay->display.print("pwd: ");
		_deviceDisplay->display.setTextWrap(false);
		_deviceDisplay->display.print(configPortalPwd);

		_deviceDisplay->display.display();
	}

	void DeviceDisplayWiFi::startConfigPortalCallback() {
		this->_firstTimecaptivePortalCallback = true;
		this->showEnteringSetup();
		this->showWiFiConect();
	}

	void DeviceDisplayWiFi::captivePortalCallback(String ip) {

		_deviceDisplay->display.stopscroll();

		if (!this->_firstTimecaptivePortalCallback) {
			return;
		}

		this->_firstTimecaptivePortalCallback = false;

		_deviceDisplay->display.clearDisplay();

		this->printPortalHeaderInDisplay("  Acesse    ");

		_deviceDisplay->display.println();
		_deviceDisplay->display.println();
		_deviceDisplay->display.println();
		_deviceDisplay->display.setFont(&FreeSansBold9pt7b);
		_deviceDisplay->display.setTextSize(1);
		_deviceDisplay->display.setTextWrap(false);
		_deviceDisplay->display.print("  http://");
		_deviceDisplay->display.println(ip);

		_deviceDisplay->display.display();
	}

	void DeviceDisplayWiFi::successConfigPortalCallback() {

		_deviceDisplay->display.stopscroll();

		String ssid = _deviceDisplay->getESPDevice()->getDeviceWiFi()->getSSID();

		_deviceDisplay->display.clearDisplay();

		this->printPortalHeaderInDisplay("  Acesso    ");

		_deviceDisplay->display.println();
		_deviceDisplay->display.println();
		_deviceDisplay->display.setFont(&FreeSansBold9pt7b);
		_deviceDisplay->display.setTextSize(1);
		_deviceDisplay->display.setTextWrap(false);
		_deviceDisplay->display.println("Conectado a");
		_deviceDisplay->display.print(ssid);
		_deviceDisplay->display.print("!");
		_deviceDisplay->display.display();

		delay(4000);
	}

	void DeviceDisplayWiFi::failedConfigPortalCallback(int connectionResult) {

		_deviceDisplay->display.stopscroll();

		_deviceDisplay->display.clearDisplay();

		this->printPortalHeaderInDisplay("  Acesso    ");

		if (connectionResult == WL_CONNECT_FAILED) {
			_deviceDisplay->display.println();
			_deviceDisplay->display.println();
			_deviceDisplay->display.setFont(&FreeSansBold9pt7b);
			_deviceDisplay->display.setTextSize(1);
			_deviceDisplay->display.setTextWrap(false);
			_deviceDisplay->display.println("   Ops! falha");
			_deviceDisplay->display.println("  na tentativa");
		}

		_deviceDisplay->display.display();

		bool invertDisplay = false;
		for (int i = 0; i <= 10; i++) {
			_deviceDisplay->display.invertDisplay(invertDisplay);
			invertDisplay = !invertDisplay;
			delay(500);
		}

		this->_firstTimecaptivePortalCallback = true;

		this->showWiFiConect();

	}

	void DeviceDisplayWiFi::connectingConfigPortalCallback() {

		_deviceDisplay->display.stopscroll();

		String ssid = _deviceDisplay->getESPDevice()->getDeviceWiFi()->getSSID();

		_deviceDisplay->display.clearDisplay();

		this->printPortalHeaderInDisplay("  Acesso    ");

		_deviceDisplay->display.setCursor(0, 27);

		_deviceDisplay->display.setFont(&FreeSansBold9pt7b);
		_deviceDisplay->display.setTextSize(1);
		_deviceDisplay->display.setTextWrap(false);
		_deviceDisplay->display.println(" Conectando a");

		_deviceDisplay->display.print(" ");
		_deviceDisplay->display.println(ssid);

		_deviceDisplay->display.display();

		// progress  
		_deviceDisplay->display.setCursor(0, 63);
		_deviceDisplay->display.setTextWrap(false);
		_deviceDisplay->display.println(".... .... .... .... .... .... .... .... .... .... .... ....");
		_deviceDisplay->display.display();
		_deviceDisplay->display.startscrollleft(0x07, 0x0F);
	}
}