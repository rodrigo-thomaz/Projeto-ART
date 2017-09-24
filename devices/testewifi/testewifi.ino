#include <ESP8266WiFi.h>    

//needed for library
#include <DNSServer.h>
#include <ESP8266WebServer.h>
#include <WiFiManager.h>        

//defines - mapeamento de pinos do NodeMCU
#define D0    16
#define D1    5
#define D2    4
#define D3    0
#define D4    2
#define D5    14
#define D6    12
#define D7    13
#define D8    15
#define D9    3
#define D10   1

void configModeCallback (String ssid, String pwd, String ip) {
  Serial.println();
  Serial.print("Modo de configuração: { SSID: ");
  Serial.print(ssid);  
  Serial.print(", PWD: ");
  Serial.print(pwd);    
  Serial.print(", IP: ");
  Serial.print(ip);    
  Serial.println(" }");
}

void configSaveCallback () {
  Serial.println();
  Serial.println("Configurações alteradas");
}

void configFailedToConnectCallback () {
  Serial.println();
  Serial.println("Não foi possível conectar na rede Wifi");
}

void setup() {

    Serial.begin(115200);

    WiFiManager wifiManager(D5);   

    wifiManager.setAPCallback(configModeCallback);
    wifiManager.setSaveConfigCallback(configSaveCallback);
    wifiManager.setFailedToConnectCallback(configFailedToConnectCallback);    

    wifiManager.autoConnect("ART");
}

void loop() {
  
}
