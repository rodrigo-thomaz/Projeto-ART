#include "DisplayDeviceWiFi.h"

namespace ART
{
	DisplayDeviceWiFi::DisplayDeviceWiFi(DisplayDevice& displayDevice, ESPDevice& espDevice)
	{
		this->_displayDevice = &displayDevice;
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
				this->_displayDevice->display.drawRect(currentX, currentY, barWidth, currentHeight, WHITE);
			}
			else
			{
				this->_displayDevice->display.fillRect(currentX, currentY, barWidth, currentHeight, WHITE);
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

			this->_displayDevice->display.drawRect(currentX, currentY, barWidth, currentHeight, WHITE);
		}

		this->_displayDevice->display.setFont();
		this->_displayDevice->display.setTextSize(1);
		this->_displayDevice->display.setTextColor(BLACK, WHITE);
		this->_displayDevice->display.setCursor(x + 15, y + 8);
		this->_displayDevice->display.setTextWrap(false);
		this->_displayDevice->display.println("X");
	}

	void DisplayDeviceWiFi::printPortalHeaderInDisplay(String title)
	{
		this->_displayDevice->display.setFont();
		this->_displayDevice->display.setTextSize(2);
		this->_displayDevice->display.setCursor(0, 0);
		this->_displayDevice->display.setTextWrap(false);
		this->_displayDevice->display.setTextColor(BLACK, WHITE);
		this->_displayDevice->display.println(title);
		this->_displayDevice->display.display();
		this->_displayDevice->display.setTextColor(WHITE);
		this->_displayDevice->display.setTextSize(1);
	}

	void DisplayDeviceWiFi::showEnteringSetup()
	{
		this->_displayDevice->display.stopscroll();

		this->_displayDevice->display.clearDisplay();
		this->_displayDevice->display.setTextSize(2);
		this->_displayDevice->display.setTextColor(WHITE);
		this->_displayDevice->display.setCursor(0, 0);

		this->_displayDevice->display.setFont();

		this->_displayDevice->display.println(" entrando");
		this->_displayDevice->display.println(" no setup");
		this->_displayDevice->display.println(" do  wifi");

		this->_displayDevice->display.display();

		delay(400);

		this->_displayDevice->display.print(" ");
		for (int i = 0; i <= 6; i++) {
			this->_displayDevice->display.print(".");
			this->_displayDevice->display.display();
			delay(400);
		}
	}

	void DisplayDeviceWiFi::showWiFiConect()
	{
		String configPortalSSID = this->_espDevice->getDeviceWiFi()->getConfigPortalSSID();
		String configPortalPwd = this->_espDevice->getDeviceWiFi()->getConfigPortalPwd();

		this->_displayDevice->display.clearDisplay();

		printPortalHeaderInDisplay("  Conecte  ");

		this->_displayDevice->display.println();
		this->_displayDevice->display.println();
		this->_displayDevice->display.setFont(&FreeSansBold9pt7b);
		this->_displayDevice->display.setTextSize(1);
		this->_displayDevice->display.print("ssid:  ");
		this->_displayDevice->display.println(configPortalSSID);
		this->_displayDevice->display.print("pwd: ");
		this->_displayDevice->display.setTextWrap(false);
		this->_displayDevice->display.print(configPortalPwd);

		this->_displayDevice->display.display();
	}

	void DisplayDeviceWiFi::startConfigPortalCallback() {
		this->_firstTimecaptivePortalCallback = true;
		this->showEnteringSetup();
		this->showWiFiConect();
	}

	void DisplayDeviceWiFi::captivePortalCallback(String ip) {

		this->_displayDevice->display.stopscroll();

		if (!this->_firstTimecaptivePortalCallback) {
			return;
		}

		this->_firstTimecaptivePortalCallback = false;

		this->_displayDevice->display.clearDisplay();

		this->printPortalHeaderInDisplay("  Acesse    ");

		this->_displayDevice->display.println();
		this->_displayDevice->display.println();
		this->_displayDevice->display.println();
		this->_displayDevice->display.setFont(&FreeSansBold9pt7b);
		this->_displayDevice->display.setTextSize(1);
		this->_displayDevice->display.setTextWrap(false);
		this->_displayDevice->display.print("  http://");
		this->_displayDevice->display.println(ip);

		this->_displayDevice->display.display();
	}

	void DisplayDeviceWiFi::successConfigPortalCallback() {

		this->_displayDevice->display.stopscroll();

		String ssid = this->_espDevice->getDeviceWiFi()->getSSID();

		this->_displayDevice->display.clearDisplay();

		this->printPortalHeaderInDisplay("  Acesso    ");

		this->_displayDevice->display.println();
		this->_displayDevice->display.println();
		this->_displayDevice->display.setFont(&FreeSansBold9pt7b);
		this->_displayDevice->display.setTextSize(1);
		this->_displayDevice->display.setTextWrap(false);
		this->_displayDevice->display.println("Conectado a");
		this->_displayDevice->display.print(ssid);
		this->_displayDevice->display.print("!");
		this->_displayDevice->display.display();

		delay(4000);
	}

	void DisplayDeviceWiFi::failedConfigPortalCallback(int connectionResult) {

		this->_displayDevice->display.stopscroll();

		this->_displayDevice->display.clearDisplay();

		this->printPortalHeaderInDisplay("  Acesso    ");

		if (connectionResult == WL_CONNECT_FAILED) {
			this->_displayDevice->display.println();
			this->_displayDevice->display.println();
			this->_displayDevice->display.setFont(&FreeSansBold9pt7b);
			this->_displayDevice->display.setTextSize(1);
			this->_displayDevice->display.setTextWrap(false);
			this->_displayDevice->display.println("   Ops! falha");
			this->_displayDevice->display.println("  na tentativa");
		}

		this->_displayDevice->display.display();

		bool invertDisplay = false;
		for (int i = 0; i <= 10; i++) {
			this->_displayDevice->display.invertDisplay(invertDisplay);
			invertDisplay = !invertDisplay;
			delay(500);
		}

		this->_firstTimecaptivePortalCallback = true;

		this->showWiFiConect();

	}

	void DisplayDeviceWiFi::connectingConfigPortalCallback() {

		this->_displayDevice->display.stopscroll();

		String ssid = this->_espDevice->getDeviceWiFi()->getSSID();

		this->_displayDevice->display.clearDisplay();

		this->printPortalHeaderInDisplay("  Acesso    ");

		this->_displayDevice->display.setCursor(0, 27);

		this->_displayDevice->display.setFont(&FreeSansBold9pt7b);
		this->_displayDevice->display.setTextSize(1);
		this->_displayDevice->display.setTextWrap(false);
		this->_displayDevice->display.println(" Conectando a");

		this->_displayDevice->display.print(" ");
		this->_displayDevice->display.println(ssid);

		this->_displayDevice->display.display();

		// progress  
		this->_displayDevice->display.setCursor(0, 63);
		this->_displayDevice->display.setTextWrap(false);
		this->_displayDevice->display.println(".... .... .... .... .... .... .... .... .... .... .... ....");
		this->_displayDevice->display.display();
		this->_displayDevice->display.startscrollleft(0x07, 0x0F);
	}
}