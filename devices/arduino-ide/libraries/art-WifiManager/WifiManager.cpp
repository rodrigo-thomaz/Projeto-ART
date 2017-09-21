#include "WifiManager.h"
#include "Arduino.h"
#include "ESP8266WiFi.h"
#include "WiFiClient.h"

const char* host = "Termometro";
const char* ssid = "RThomaz";
const char* password = "2919517400";

IPAddress ip(192, 168, 1, 177);
IPAddress gateway(192, 168, 1, 1);
IPAddress subnet(255, 255, 255, 0);

WifiManager::WifiManager(DebugManager& debugManager)
{
	this->_debugManager = &debugManager;
}

bool WifiManager::connect()
{
	WiFi.config(ip, gateway, subnet);

	WiFi.mode(WIFI_AP_STA);
	WiFi.begin(ssid, password);

	return WiFi.waitForConnectResult() == WL_CONNECTED;
}