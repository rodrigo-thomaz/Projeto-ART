#include "WifiManager.h"
#include "Arduino.h"
#include "ESP8266WiFi.h"
#include "DebugManager.h"

const char* host = "Termometro";
const char* ssid = "RThomaz";
const char* password = "2919517400";

WifiManager::WifiManager(DebugManager& debugManager)
{
	this->_debugManager = &debugManager;
}

void WifiManager::begin()
{
	delay(10);

	if (this->_debugManager->isDebug()) {
		Serial.println("------Conexao WI-FI------");
		Serial.print("Conectando-se na rede: ");
		Serial.println(ssid);
		Serial.println("Aguarde");
	}

	connect();
}

void WifiManager::connect()
{
	//se já está conectado a rede WI-FI, nada é feito. 
	//Caso contrário, são efetuadas tentativas de conexão
	if (WiFi.status() == WL_CONNECTED)
		return;

	WiFi.mode(WIFI_AP_STA);
	WiFi.begin(ssid, password);  // Conecta na rede WI-FI

	while (WiFi.status() != WL_CONNECTED)
	{
		delay(100);
		Serial.print(".");
	}

	if (this->_debugManager->isDebug()) {
		Serial.println();
		Serial.print("Conectado com sucesso na rede ");
		Serial.println(ssid);
		Serial.print("IP obtido: ");
		Serial.println(WiFi.localIP());
		Serial.print("Mac Address: ");
		Serial.println(WiFi.macAddress());
	}	
}