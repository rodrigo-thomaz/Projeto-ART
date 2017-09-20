#include <NTPClient.h>
// change next line to use with another board/shield
#include <ESP8266WiFi.h>
#include <WiFiClient.h>
//#include <WiFi.h> // for WiFi shield
//#include <WiFi101.h> // for WiFi 101 shield or MKR1000
#include <WiFiUdp.h>

const char* host = "Termometro";
const char* ssid = "RThomaz";
const char* password = "2919517400";

IPAddress ip(192, 168, 1, 177);
IPAddress gateway(192,168,1,1); 
IPAddress subnet(255,255,255,0); 

WiFiUDP ntpUDP;

int ntpUpdateInterval = 60000;
int16_t utc = 0; //UTC
NTPClient timeClient(ntpUDP, "a.st1.ntp.br", utc*3600, ntpUpdateInterval);

void setup(){
 
  Serial.begin(9600);

  WiFi.config(ip, gateway, subnet);
  
  WiFi.mode(WIFI_AP_STA);
  WiFi.begin(ssid, password);
  
  Serial.println("Iniciando...");

  if(WiFi.waitForConnectResult() == WL_CONNECTED){
    
    timeClient.begin();
    timeClient.update();
  }   
}

void loop() {  

  printf("Time Epoch: %d: ", timeClient.getEpochTime());
  Serial.println(timeClient.getFormattedTime());  
  
  delay(1000);
}
