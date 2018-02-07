#include "DisplayDeviceWiFi.h"

namespace ART
{
	DisplayDeviceWiFi::DisplayDeviceWiFi(ESPDevice& espDevice)
	{
		this->_espDevice = &espDevice;

		this->_startConfigPortalCallback = std::bind(&DisplayDeviceWiFi::startConfigPortalCallback, this);
		this->_captivePortalCallback = [=](String ip) { this->captivePortalCallback(ip); };
		this->_successConfigPortalCallback = std::bind(&DisplayDeviceWiFi::successConfigPortalCallback, this);
		this->_failedConfigPortalCallback = [=](int connectionResult) { this->failedConfigPortalCallback(connectionResult); };
		this->_connectingConfigPortalCallback = std::bind(&DisplayDeviceWiFi::connectingConfigPortalCallback, this);

		this->_espDevice->getDeviceWiFi()->setStartConfigPortalCallback(this->_startConfigPortalCallback);
		this->_espDevice->getDeviceWiFi()->setCaptivePortalCallback(this->_captivePortalCallback);
		this->_espDevice->getDeviceWiFi()->setSuccessConfigPortalCallback(this->_successConfigPortalCallback);
		this->_espDevice->getDeviceWiFi()->setFailedConfigPortalCallback(this->_failedConfigPortalCallback);
		this->_espDevice->getDeviceWiFi()->setConnectingConfigPortalCallback(this->_connectingConfigPortalCallback);
	}

	DisplayDeviceWiFi::~DisplayDeviceWiFi()
	{
	}

	void DisplayDeviceWiFi::printSignal() {

		int quality = this->_espDevice->getDeviceWiFi()->getQuality();
		int barSignal = this->_espDevice->getDeviceWiFi()->convertQualitytToBarsSignal(quality);

		if (this->_espDevice->getDeviceWiFi()->isConnected())
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
				_espDevice->getDisplayDevice()->display.drawRect(currentX, currentY, barWidth, currentHeight, WHITE);
			}
			else
			{
				_espDevice->getDisplayDevice()->display.fillRect(currentX, currentY, barWidth, currentHeight, WHITE);
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

			_espDevice->getDisplayDevice()->display.drawRect(currentX, currentY, barWidth, currentHeight, WHITE);
		}

		_espDevice->getDisplayDevice()->display.setFont();
		_espDevice->getDisplayDevice()->display.setTextSize(1);
		_espDevice->getDisplayDevice()->display.setTextColor(BLACK, WHITE);
		_espDevice->getDisplayDevice()->display.setCursor(x + 15, y + 8);
		_espDevice->getDisplayDevice()->display.setTextWrap(false);
		_espDevice->getDisplayDevice()->display.println("X");
	}

	void DisplayDeviceWiFi::printPortalHeaderInDisplay(String title)
	{
		_espDevice->getDisplayDevice()->display.setFont();
		_espDevice->getDisplayDevice()->display.setTextSize(2);
		_espDevice->getDisplayDevice()->display.setCursor(0, 0);
		_espDevice->getDisplayDevice()->display.setTextWrap(false);
		_espDevice->getDisplayDevice()->display.setTextColor(BLACK, WHITE);
		_espDevice->getDisplayDevice()->display.println(title);
		_espDevice->getDisplayDevice()->display.display();
		_espDevice->getDisplayDevice()->display.setTextColor(WHITE);
		_espDevice->getDisplayDevice()->display.setTextSize(1);
	}

	void DisplayDeviceWiFi::showEnteringSetup()
	{
		_espDevice->getDisplayDevice()->display.stopscroll();

		_espDevice->getDisplayDevice()->display.clearDisplay();
		_espDevice->getDisplayDevice()->display.setTextSize(2);
		_espDevice->getDisplayDevice()->display.setTextColor(WHITE);
		_espDevice->getDisplayDevice()->display.setCursor(0, 0);

		_espDevice->getDisplayDevice()->display.setFont();

		_espDevice->getDisplayDevice()->display.println(" entrando");
		_espDevice->getDisplayDevice()->display.println(" no setup");
		_espDevice->getDisplayDevice()->display.println(" do  wifi");

		_espDevice->getDisplayDevice()->display.display();

		delay(400);

		_espDevice->getDisplayDevice()->display.print(" ");
		for (int i = 0; i <= 6; i++) {
			_espDevice->getDisplayDevice()->display.print(".");
			_espDevice->getDisplayDevice()->display.display();
			delay(400);
		}
	}

	void DisplayDeviceWiFi::showWiFiConect()
	{
		String configPortalSSID = this->_espDevice->getDeviceWiFi()->getConfigPortalSSID();
		String configPortalPwd = this->_espDevice->getDeviceWiFi()->getConfigPortalPwd();

		_espDevice->getDisplayDevice()->display.clearDisplay();

		printPortalHeaderInDisplay("  Conecte  ");

		_espDevice->getDisplayDevice()->display.println();
		_espDevice->getDisplayDevice()->display.println();
		_espDevice->getDisplayDevice()->display.setFont(&FreeSansBold9pt7b);
		_espDevice->getDisplayDevice()->display.setTextSize(1);
		_espDevice->getDisplayDevice()->display.print("ssid:  ");
		_espDevice->getDisplayDevice()->display.println(configPortalSSID);
		_espDevice->getDisplayDevice()->display.print("pwd: ");
		_espDevice->getDisplayDevice()->display.setTextWrap(false);
		_espDevice->getDisplayDevice()->display.print(configPortalPwd);

		_espDevice->getDisplayDevice()->display.display();
	}

	void DisplayDeviceWiFi::startConfigPortalCallback() {
		this->_firstTimecaptivePortalCallback = true;
		this->showEnteringSetup();
		this->showWiFiConect();
	}

	void DisplayDeviceWiFi::captivePortalCallback(String ip) {

		_espDevice->getDisplayDevice()->display.stopscroll();

		if (!this->_firstTimecaptivePortalCallback) {
			return;
		}

		this->_firstTimecaptivePortalCallback = false;

		_espDevice->getDisplayDevice()->display.clearDisplay();

		this->printPortalHeaderInDisplay("  Acesse    ");

		_espDevice->getDisplayDevice()->display.println();
		_espDevice->getDisplayDevice()->display.println();
		_espDevice->getDisplayDevice()->display.println();
		_espDevice->getDisplayDevice()->display.setFont(&FreeSansBold9pt7b);
		_espDevice->getDisplayDevice()->display.setTextSize(1);
		_espDevice->getDisplayDevice()->display.setTextWrap(false);
		_espDevice->getDisplayDevice()->display.print("  http://");
		_espDevice->getDisplayDevice()->display.println(ip);

		_espDevice->getDisplayDevice()->display.display();
	}

	void DisplayDeviceWiFi::successConfigPortalCallback() {

		_espDevice->getDisplayDevice()->display.stopscroll();

		String ssid = this->_espDevice->getDeviceWiFi()->getSSID();

		_espDevice->getDisplayDevice()->display.clearDisplay();

		this->printPortalHeaderInDisplay("  Acesso    ");

		_espDevice->getDisplayDevice()->display.println();
		_espDevice->getDisplayDevice()->display.println();
		_espDevice->getDisplayDevice()->display.setFont(&FreeSansBold9pt7b);
		_espDevice->getDisplayDevice()->display.setTextSize(1);
		_espDevice->getDisplayDevice()->display.setTextWrap(false);
		_espDevice->getDisplayDevice()->display.println("Conectado a");
		_espDevice->getDisplayDevice()->display.print(ssid);
		_espDevice->getDisplayDevice()->display.print("!");
		_espDevice->getDisplayDevice()->display.display();

		delay(4000);
	}

	void DisplayDeviceWiFi::failedConfigPortalCallback(int connectionResult) {

		_espDevice->getDisplayDevice()->display.stopscroll();

		_espDevice->getDisplayDevice()->display.clearDisplay();

		this->printPortalHeaderInDisplay("  Acesso    ");

		if (connectionResult == WL_CONNECT_FAILED) {
			_espDevice->getDisplayDevice()->display.println();
			_espDevice->getDisplayDevice()->display.println();
			_espDevice->getDisplayDevice()->display.setFont(&FreeSansBold9pt7b);
			_espDevice->getDisplayDevice()->display.setTextSize(1);
			_espDevice->getDisplayDevice()->display.setTextWrap(false);
			_espDevice->getDisplayDevice()->display.println("   Ops! falha");
			_espDevice->getDisplayDevice()->display.println("  na tentativa");
		}

		_espDevice->getDisplayDevice()->display.display();

		bool invertDisplay = false;
		for (int i = 0; i <= 10; i++) {
			_espDevice->getDisplayDevice()->display.invertDisplay(invertDisplay);
			invertDisplay = !invertDisplay;
			delay(500);
		}

		this->_firstTimecaptivePortalCallback = true;

		this->showWiFiConect();

	}

	void DisplayDeviceWiFi::connectingConfigPortalCallback() {

		_espDevice->getDisplayDevice()->display.stopscroll();

		String ssid = this->_espDevice->getDeviceWiFi()->getSSID();

		_espDevice->getDisplayDevice()->display.clearDisplay();

		this->printPortalHeaderInDisplay("  Acesso    ");

		_espDevice->getDisplayDevice()->display.setCursor(0, 27);

		_espDevice->getDisplayDevice()->display.setFont(&FreeSansBold9pt7b);
		_espDevice->getDisplayDevice()->display.setTextSize(1);
		_espDevice->getDisplayDevice()->display.setTextWrap(false);
		_espDevice->getDisplayDevice()->display.println(" Conectando a");

		_espDevice->getDisplayDevice()->display.print(" ");
		_espDevice->getDisplayDevice()->display.println(ssid);

		_espDevice->getDisplayDevice()->display.display();

		// progress  
		_espDevice->getDisplayDevice()->display.setCursor(0, 63);
		_espDevice->getDisplayDevice()->display.setTextWrap(false);
		_espDevice->getDisplayDevice()->display.println(".... .... .... .... .... .... .... .... .... .... .... ....");
		_espDevice->getDisplayDevice()->display.display();
		_espDevice->getDisplayDevice()->display.startscrollleft(0x07, 0x0F);
	}
}