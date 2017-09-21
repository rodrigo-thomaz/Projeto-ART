#include "Arduino.h"
#include "DebugManager.h"
#include "TemperatureSensorManager.h"
#include "NTPManager.h"
#include "DisplayManager.h"
#include "WifiManager.h"

#include "WebSocketsClient.h"
#include "Hash.h"

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

DebugManager debugManager(D0);
NTPManager ntpManager(debugManager);
DisplayManager displayManager(debugManager);
WifiManager wifiManager(debugManager);
TemperatureSensorManager temperatureSensorManager(debugManager, ntpManager);

WebSocketsClient webSocket;

const String ART_DEVICE_ID = "C24F71CB-E705-4188-9A94-73EB05AD4F0B";

#define MESSAGE_INTERVAL 3000
#define HEARTBEAT_INTERVAL 25000

uint64_t messageTimestamp = 0;
uint64_t heartbeatTimestamp = 0;
bool isConnected = false;

void webSocketEvent(WStype_t type, uint8_t * payload, size_t length) {

	if(debugManager.isDebug()) Serial.printf("[WebSocket event]");

	switch (type) {
	case WStype_DISCONNECTED:
		if (debugManager.isDebug()) Serial.printf("[Websocket client] Disconnected!\n");
		isConnected = false;
		break;
	case WStype_CONNECTED:
	{
		if (debugManager.isDebug()) Serial.printf("[Websocket client] Connected to url: %s\n", payload);
		isConnected = true;

		// send message to server when Connected
		// socket.io upgrade confirmation message (required)
		webSocket.sendTXT("5");
	}
	break;
	case WStype_TEXT:
		if (debugManager.isDebug()) Serial.printf("[Websocket client] get text: %s\n", payload);

		// send message to server
		// webSocket.sendTXT("message here");
		break;
	case WStype_BIN:
		if (debugManager.isDebug()) Serial.printf("[Websocket client] get binary length: %u\n", length);
		hexdump(payload, length);

		// send data to server
		// webSocket.sendBIN(payload, length);
		break;
	}

}

void setup() {
		
	Serial.begin(9600);

	debugManager.update();

	displayManager.begin();

	temperatureSensorManager.begin();

	if (debugManager.isDebug()) Serial.println("Iniciando...");

	// text display tests
	displayManager.display.clearDisplay();
	displayManager.display.setTextSize(1);
	displayManager.display.setTextColor(WHITE);
	displayManager.display.setCursor(0, 0);

	displayManager.display.println("Conectando Wifi...");
	displayManager.display.display();

	if (wifiManager.connect()) {

		ntpManager.begin();

		if (debugManager.isDebug()) Serial.println("conectou wifi!");

		displayManager.display.println("Wifi conectado !!!");
		displayManager.display.display();
		delay(2000);

		webSocket.beginSocketIO("192.168.1.12", 3000);
		//webSocket.setAuthorization("user", "Password"); // HTTP Basic Authorization
		webSocket.onEvent(webSocketEvent);
		
		join();
	}
	else {
		displayManager.display.println("Conex�o com a rede WiFi falou!");
		displayManager.display.display();
		delay(2000);
 }
}

void printAddressSerial(byte deviceAddress[8])
{
	for (uint8_t i = 0; i < 8; i++)
	{
		// zero pad the address if necessary
		if (deviceAddress[i] < 16) Serial.print("0");
		Serial.print(deviceAddress[i], HEX);
	}
}

void printDataSerial(TemperatureSensor temperatureSensor)
{
	Serial.print("Address: ");
	printAddressSerial(temperatureSensor.deviceAddress);
	Serial.print(" ValidFamily: ");
	Serial.print(temperatureSensor.validFamily);
	Serial.print(" Family: ");
	Serial.print(temperatureSensor.family);
	Serial.print(" Connected: ");
	Serial.print(temperatureSensor.isConnected);
	Serial.print(" Resolution: ");
	Serial.print(temperatureSensor.resolution);
	Serial.print(" Temp C: ");
	Serial.print(temperatureSensor.tempCelsius);
	Serial.print(" Temp F: ");
	Serial.print(temperatureSensor.tempFahrenheit);
	Serial.print(" HasAlarm: ");
	Serial.print(temperatureSensor.hasAlarm);
	Serial.print(" LowAlarmTemp: ");
	Serial.print(temperatureSensor.lowAlarmTemp);
	Serial.print(" HighAlarmTemp: ");
	Serial.print(temperatureSensor.highAlarmTemp);
	Serial.println();
}

void printAddressDisplay(byte deviceAddress[8])
{
	for (uint8_t i = 0; i < 8; i++)
	{
		// zero pad the address if necessary
		if (deviceAddress[i] < 16) displayManager.display.print("0");
		displayManager.display.print(deviceAddress[i], HEX);
	}
}

void printDataDisplay(TemperatureSensor temperatureSensor)
{
	displayManager.display.print("Address=");
	printAddressDisplay(temperatureSensor.deviceAddress);
	displayManager.display.println();
	displayManager.display.setTextSize(2);
	displayManager.display.print(temperatureSensor.tempCelsius);
	displayManager.display.println(" C ");
	displayManager.display.print(temperatureSensor.tempFahrenheit);
	displayManager.display.println(" F");
}

void loop() {	

	debugManager.update();

	webSocket.loop();

	if (isConnected) {		

		uint64_t now = millis();

		if (now - messageTimestamp > MESSAGE_INTERVAL) {

			// text display tests
			displayManager.display.clearDisplay();
			displayManager.display.setTextSize(1);
			displayManager.display.setTextColor(WHITE);
			displayManager.display.setCursor(0, 0);

			messageTimestamp = now;

			TemperatureSensor *arr = temperatureSensorManager.getSensors();			

			String json = "";
			for (int i = 0; i < sizeof(arr) / sizeof(int); ++i) {
				json += arr[i].json;
				if (debugManager.isDebug()) printDataSerial(arr[i]);
				printDataDisplay(arr[i]);
			}
			json += "";

			String data = "42[\"sendTemp\"," + json + "]";
			webSocket.sendTXT(data);

			displayManager.display.display();
		}
		if ((now - heartbeatTimestamp) > HEARTBEAT_INTERVAL) {
			heartbeatTimestamp = now;
			// socket.io heartbeat message
			webSocket.sendTXT("2");
		}		
	}
}

void join() {
	if (debugManager.isDebug()) {
		Serial.print("[Websocket client] send join: ");
		Serial.println(ART_DEVICE_ID);
	}
	String data = "42[\"join\",{" + ART_DEVICE_ID + "}]";
	webSocket.sendTXT(data);
}
