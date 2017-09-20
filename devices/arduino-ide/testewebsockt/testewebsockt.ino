/*
 * WebSocketClient.ino
 *
 *  Created on: 24.05.2015
 *
 */

#include <Arduino.h>

#include <ESP8266WiFi.h>
#include <WiFiClient.h>

#include <WebSocketsClient.h>

#include <Hash.h>

#include <TemperatureSensorManager.h>

const char* host = "Termometro";
const char* ssid = "RThomaz";
const char* password = "2919517400";

IPAddress ip(192, 168, 1, 177);
IPAddress gateway(192,168,1,1); 
IPAddress subnet(255,255,255,0); 

WebSocketsClient webSocket;

#define MESSAGE_INTERVAL 3000
#define HEARTBEAT_INTERVAL 25000

uint64_t messageTimestamp = 0;
uint64_t heartbeatTimestamp = 0;
bool isConnected = false;

//NTP inicio

#include <NTPClient.h>
#include <WiFiUdp.h>

WiFiUDP ntpUDP;

int ntpUpdateInterval = 60000;
int16_t utc = 0; //UTC

NTPClient timeClient(ntpUDP, "a.st1.ntp.br", utc*3600, ntpUpdateInterval);

//NTP fim

TemperatureSensorManager temperatureSensorManager(timeClient);  

void webSocketEvent(WStype_t type, uint8_t * payload, size_t length) {

    Serial.printf("webSocketEvent!!!");

    switch(type) {
        case WStype_DISCONNECTED:
            Serial.printf("[WSc] Disconnected!\n");
            isConnected = false;
            break;
        case WStype_CONNECTED:
            {
                Serial.printf("[WSc] Connected to url: %s\n",  payload);
                isConnected = true;

          // send message to server when Connected
                // socket.io upgrade confirmation message (required)
        webSocket.sendTXT("5");
            }
            break;
        case WStype_TEXT:
            Serial.printf("[WSc] get text: %s\n", payload);

      // send message to server
      // webSocket.sendTXT("message here");
            break;
        case WStype_BIN:
            Serial.printf("[WSc] get binary length: %u\n", length);
            hexdump(payload, length);

            // send data to server
            // webSocket.sendBIN(payload, length);
            break;
    }

}

void setup() {

  Serial.begin(9600);

  WiFi.config(ip, gateway, subnet);
  
  WiFi.mode(WIFI_AP_STA);
  WiFi.begin(ssid, password);

  temperatureSensorManager.begin();  
  
  Serial.println("Iniciando...");

  if(WiFi.waitForConnectResult() == WL_CONNECTED){
    
    Serial.println("conectou wifi!");

    webSocket.beginSocketIO("192.168.1.12", 3000);
    //webSocket.setAuthorization("user", "Password"); // HTTP Basic Authorization
    webSocket.onEvent(webSocketEvent);    

    //NTP Clint
    timeClient.begin();
    timeClient.update();
  } 
}

void loop() {
  
    webSocket.loop();

    //printf("Time Epoch: %d: ", timeClient.getEpochTime());
    //Serial.println(timeClient.getFormattedTime());  
  
    if(isConnected) {

        uint64_t now = millis();

        if(now - messageTimestamp > MESSAGE_INTERVAL) {
            
            messageTimestamp = now;

            TemperatureSensor *arr = temperatureSensorManager.getSensors();  

            String json = "";                       
            for(int i = 0; i < sizeof(arr)/sizeof(int); ++i){              
              json += arr[i].json;
            }  
            json += "";
  
            String data = "42[\"sendTemp\"," + json + "]";
            webSocket.sendTXT(data);
            
        }
        if((now - heartbeatTimestamp) > HEARTBEAT_INTERVAL) {
            heartbeatTimestamp = now;
            // socket.io heartbeat message
            webSocket.sendTXT("2");
        }
    }
}
