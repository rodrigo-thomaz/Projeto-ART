#include "DisplayDeviceWiFi.h"
#include "DisplayDevice.h"
#include "ESPDevice.h"
#include "DeviceWiFi.h"

#include "../Adafruit-GFX-Library/Fonts/FreeSans9pt7b.h"
#include "../Adafruit-GFX-Library/Fonts/FreeSansBold9pt7b.h"

namespace ART
{
	DisplayDeviceWiFi::DisplayDeviceWiFi(DisplayDevice* displayDevice)
	{
		_displayDevice = displayDevice;

		this->_startConfigPortalCallback = std::bind(&DisplayDeviceWiFi::startConfigPortalCallback, this);
		this->_captivePortalCallback = [=](String ip) { this->captivePortalCallback(ip); };
		this->_successConfigPortalCallback = std::bind(&DisplayDeviceWiFi::successConfigPortalCallback, this);
		this->_failedConfigPortalCallback = [=](int connectionResult) { this->failedConfigPortalCallback(connectionResult); };
		this->_connectingConfigPortalCallback = std::bind(&DisplayDeviceWiFi::connectingConfigPortalCallback, this);

		_displayDevice->getESPDevice()->getDeviceWiFi()->setStartConfigPortalCallback(this->_startConfigPortalCallback);
		_displayDevice->getESPDevice()->getDeviceWiFi()->setCaptivePortalCallback(this->_captivePortalCallback);
		_displayDevice->getESPDevice()->getDeviceWiFi()->setSuccessConfigPortalCallback(this->_successConfigPortalCallback);
		_displayDevice->getESPDevice()->getDeviceWiFi()->setFailedConfigPortalCallback(this->_failedConfigPortalCallback);
		_displayDevice->getESPDevice()->getDeviceWiFi()->setConnectingConfigPortalCallback(this->_connectingConfigPortalCallback);
	}

	DisplayDeviceWiFi::~DisplayDeviceWiFi()
	{
	}

	void DisplayDeviceWiFi::create(DisplayDeviceWiFi *(&displayDeviceWiFi), DisplayDevice * displayDevice)
	{
		displayDeviceWiFi = new DisplayDeviceWiFi(displayDevice);
	}

	void DisplayDeviceWiFi::printSignal() {

		int quality = _displayDevice->getESPDevice()->getDeviceWiFi()->getQuality();
		int barSignal = _displayDevice->getESPDevice()->getDeviceWiFi()->convertQualitytToBarsSignal(quality);

		if (_displayDevice->getESPDevice()->getDeviceWiFi()->isConnected())
			this->printConnectedSignal(106, 0, 4, 2, barSignal);
		else
			this->printNoConnectedSignal(106, 0, 4, 2);
	}

	void DisplayDeviceWiFi::printConnectedSignal(int x, int y, int barWidth, int margin, int barSignal) {

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
				_displayDevice->display.drawRect(currentX, currentY, barWidth, currentHeight, WHITE);
			}
			else
			{
				_displayDevice->display.fillRect(currentX, currentY, barWidth, currentHeight, WHITE);
			}
		}
	}

	void DisplayDeviceWiFi::printNoConnectedSignal(int x, int y, int barWidth, int margin) {

		int barsCount = 4;
		int barHeight = 4;

		for (int i = 0; i < 4; i++)
		{
			int currentX = x + (barWidth * i) + (margin * i);
			int currentY = (y + (barsCount - i - 1) * barHeight);
			int currentHeight = 0;

			currentHeight = barHeight * (i + 1);

			_displayDevice->display.drawRect(currentX, currentY, barWidth, currentHeight, WHITE);
		}

		_displayDevice->display.setFont();
		_displayDevice->display.setTextSize(1);
		_displayDevice->display.setTextColor(BLACK, WHITE);
		_displayDevice->display.setCursor(x + 15, y + 8);
		_displayDevice->display.setTextWrap(false);
		_displayDevice->display.println("X");
	}

	void DisplayDeviceWiFi::printPortalHeaderInDisplay(String title)
	{
		_displayDevice->display.setFont();
		_displayDevice->display.setTextSize(2);
		_displayDevice->display.setCursor(0, 0);
		_displayDevice->display.setTextWrap(false);
		_displayDevice->display.setTextColor(BLACK, WHITE);
		_displayDevice->display.println(title);
		_displayDevice->display.display();
		_displayDevice->display.setTextColor(WHITE);
		_displayDevice->display.setTextSize(1);
	}

	void DisplayDeviceWiFi::showEnteringSetup()
	{
		_displayDevice->display.stopscroll();

		_displayDevice->display.clearDisplay();
		_displayDevice->display.setTextSize(2);
		_displayDevice->display.setTextColor(WHITE);
		_displayDevice->display.setCursor(0, 0);

		_displayDevice->display.setFont();

		_displayDevice->display.println(" entrando");
		_displayDevice->display.println(" no setup");
		_displayDevice->display.println(" do  wifi");

		_displayDevice->display.display();

		delay(400);

		_displayDevice->display.print(" ");
		for (int i = 0; i <= 6; i++) {
			_displayDevice->display.print(".");
			_displayDevice->display.display();
			delay(400);
		}
	}

	void DisplayDeviceWiFi::showWiFiConect()
	{
		String configPortalSSID = _displayDevice->getESPDevice()->getDeviceWiFi()->getConfigPortalSSID();
		String configPortalPwd = _displayDevice->getESPDevice()->getDeviceWiFi()->getConfigPortalPwd();

		_displayDevice->display.clearDisplay();

		printPortalHeaderInDisplay("  Conecte  ");

		_displayDevice->display.println();
		_displayDevice->display.println();
		_displayDevice->display.setFont(&FreeSansBold9pt7b);
		_displayDevice->display.setTextSize(1);
		_displayDevice->display.print("ssid:  ");
		_displayDevice->display.println(configPortalSSID);
		_displayDevice->display.print("pwd: ");
		_displayDevice->display.setTextWrap(false);
		_displayDevice->display.print(configPortalPwd);

		_displayDevice->display.display();
	}

	void DisplayDeviceWiFi::startConfigPortalCallback() {
		this->_firstTimecaptivePortalCallback = true;
		this->showEnteringSetup();
		this->showWiFiConect();
	}

	void DisplayDeviceWiFi::captivePortalCallback(String ip) {

		_displayDevice->display.stopscroll();

		if (!this->_firstTimecaptivePortalCallback) {
			return;
		}

		this->_firstTimecaptivePortalCallback = false;

		_displayDevice->display.clearDisplay();

		this->printPortalHeaderInDisplay("  Acesse    ");

		_displayDevice->display.println();
		_displayDevice->display.println();
		_displayDevice->display.println();
		_displayDevice->display.setFont(&FreeSansBold9pt7b);
		_displayDevice->display.setTextSize(1);
		_displayDevice->display.setTextWrap(false);
		_displayDevice->display.print("  http://");
		_displayDevice->display.println(ip);

		_displayDevice->display.display();
	}

	void DisplayDeviceWiFi::successConfigPortalCallback() {

		_displayDevice->display.stopscroll();

		String ssid = _displayDevice->getESPDevice()->getDeviceWiFi()->getSSID();

		_displayDevice->display.clearDisplay();

		this->printPortalHeaderInDisplay("  Acesso    ");

		_displayDevice->display.println();
		_displayDevice->display.println();
		_displayDevice->display.setFont(&FreeSansBold9pt7b);
		_displayDevice->display.setTextSize(1);
		_displayDevice->display.setTextWrap(false);
		_displayDevice->display.println("Conectado a");
		_displayDevice->display.print(ssid);
		_displayDevice->display.print("!");
		_displayDevice->display.display();

		delay(4000);
	}

	void DisplayDeviceWiFi::failedConfigPortalCallback(int connectionResult) {

		_displayDevice->display.stopscroll();

		_displayDevice->display.clearDisplay();

		this->printPortalHeaderInDisplay("  Acesso    ");

		if (connectionResult == WL_CONNECT_FAILED) {
			_displayDevice->display.println();
			_displayDevice->display.println();
			_displayDevice->display.setFont(&FreeSansBold9pt7b);
			_displayDevice->display.setTextSize(1);
			_displayDevice->display.setTextWrap(false);
			_displayDevice->display.println("   Ops! falha");
			_displayDevice->display.println("  na tentativa");
		}

		_displayDevice->display.display();

		bool invertDisplay = false;
		for (int i = 0; i <= 10; i++) {
			_displayDevice->display.invertDisplay(invertDisplay);
			invertDisplay = !invertDisplay;
			delay(500);
		}

		this->_firstTimecaptivePortalCallback = true;

		this->showWiFiConect();

	}

	void DisplayDeviceWiFi::connectingConfigPortalCallback() {

		_displayDevice->display.stopscroll();

		String ssid = _displayDevice->getESPDevice()->getDeviceWiFi()->getSSID();

		_displayDevice->display.clearDisplay();

		this->printPortalHeaderInDisplay("  Acesso    ");

		_displayDevice->display.setCursor(0, 27);

		_displayDevice->display.setFont(&FreeSansBold9pt7b);
		_displayDevice->display.setTextSize(1);
		_displayDevice->display.setTextWrap(false);
		_displayDevice->display.println(" Conectando a");

		_displayDevice->display.print(" ");
		_displayDevice->display.println(ssid);

		_displayDevice->display.display();

		// progress  
		_displayDevice->display.setCursor(0, 63);
		_displayDevice->display.setTextWrap(false);
		_displayDevice->display.println(".... .... .... .... .... .... .... .... .... .... .... ....");
		_displayDevice->display.display();
		_displayDevice->display.startscrollleft(0x07, 0x0F);
	}
}