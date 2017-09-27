#include <WiFiManager.h>        

#include "DebugManager.h"

DebugManager debugManager(D0);

WiFiManager wifiManager(D5, debugManager);

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

void configSuccessToConnectCallback (String ssid, int quality) {  
  Serial.print("Conectado na rede Wifi: ");
  Serial.print(ssid);
  Serial.print(" qualidade: ");    
  Serial.println(quality);
}

void configFailedToConnectCallback (String ssid, int connectionResult, String message) {  
  Serial.print("Não foi possível conectar na rede Wifi ");
  Serial.println(ssid);
  Serial.print("Code: ");
  Serial.print(connectionResult);
  Serial.print(" Mensagem: ");
  Serial.println(message);
}

void VerificaConexoesWiFIEMQTT(void)
{    
     wifiManager.autoConnect();
}

void setup() {

    Serial.begin(115200);       

    wifiManager.setAPCallback(configModeCallback);
    wifiManager.setSuccessToConnectCallback(configSuccessToConnectCallback);    
    wifiManager.setFailedToConnectCallback(configFailedToConnectCallback);    
    wifiManager.autoConnect();
}

void loop() {
  VerificaConexoesWiFIEMQTT(); 
}
