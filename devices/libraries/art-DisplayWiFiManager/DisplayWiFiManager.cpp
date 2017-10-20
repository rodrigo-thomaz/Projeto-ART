#include "DisplayWifiManager.h"
#include "Arduino.h"
#include "DebugManager.h"
#include "DisplayManager.h"
#include "WiFiManager.h"

DisplayWiFiManager::DisplayWiFiManager(DisplayManager& displayManager, WiFiManager& wifiManager, DebugManager& debugManager)
{
	this->_displayManager = &displayManager;
	this->_wifiManager = &wifiManager;
	this->_debugManager = &debugManager;
}

DisplayWiFiManager::~DisplayWiFiManager()
{
}

void DisplayWiFiManager::printPortalHeaderInDisplay(String title)
{  
  this->_displayManager->display.setFont();
  this->_displayManager->display.setTextSize(2);  
  this->_displayManager->display.setCursor(0, 0);       
  this->_displayManager->display.setTextWrap(false);  
  this->_displayManager->display.setTextColor(BLACK, WHITE);
  this->_displayManager->display.println(title);
  this->_displayManager->display.display();
  this->_displayManager->display.setTextColor(WHITE);
  this->_displayManager->display.setTextSize(1);  
}

void DisplayWiFiManager::showEnteringSetup()
{
	this->_displayManager->display.stopscroll();

	this->_displayManager->display.clearDisplay();
	this->_displayManager->display.setTextSize(2);
	this->_displayManager->display.setTextColor(WHITE);
	this->_displayManager->display.setCursor(0, 0);       

	this->_displayManager->display.setFont();

	this->_displayManager->display.println(" entrando");
	this->_displayManager->display.println(" no setup");
	this->_displayManager->display.println(" do  wifi");

	this->_displayManager->display.display();

	delay(400);

	this->_displayManager->display.print(" ");  
	for (int i=0; i <= 6; i++) {
		this->_displayManager->display.print(".");  
		this->_displayManager->display.display();
		delay(400);
	} 
}

void DisplayWiFiManager::showWiFiConect()
{
	String configPortalSSID = this->_wifiManager->getConfigPortalSSID();
	String configPortalPwd = this->_wifiManager->getConfigPortalPwd();

	this->_displayManager->display.clearDisplay();

	printPortalHeaderInDisplay("  Conecte  ");

	this->_displayManager->display.println();
	this->_displayManager->display.println();
	this->_displayManager->display.setFont(&FreeSansBold9pt7b);
	this->_displayManager->display.setTextSize(1);  
	this->_displayManager->display.print("ssid:  ");
	this->_displayManager->display.println(configPortalSSID);  
	this->_displayManager->display.print("pwd: ");  
	this->_displayManager->display.setTextWrap(false);
	this->_displayManager->display.print(configPortalPwd);    

	this->_displayManager->display.display();
}