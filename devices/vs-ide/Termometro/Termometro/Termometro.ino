#include <Arduino.h>

#include <WebSocketsClient.h>
#include <Hash.h>

#include <DebugManager.h>
#include <TemperatureSensorManager.h>
#include <NTPManager.h>
#include <DisplayManager.h>
#include <WifiManager.h>

DebugManager debugManager(16);
NTPManager ntpManager;

//Display
#include <SPI.h>
#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>
#define OLED_RESET 0
Adafruit_SSD1306 display(OLED_RESET);
DisplayManager displayManager(display);

#if (SSD1306_LCDHEIGHT != 64)
#error("Height incorrect, please fix Adafruit_SSD1306.h!");
#endif

//display

WifiManager wifiManager;
TemperatureSensorManager temperatureSensorManager(ntpManager);

WebSocketsClient webSocket;

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

	//display
	displayManager.begin();
	display.begin(SSD1306_SWITCHCAPVCC, 0x3C);  // initialize with the I2C addr 0x3D (for the 128x64)
														// init done

														// Show image buffer on the display hardware.
														// Since the buffer is intialized with an Adafruit splashscreen
														// internally, this will display the splashscreen.
	display.display();
	delay(2000);

	// Clear the buffer.
	display.clearDisplay();
	//display

	temperatureSensorManager.begin();

	if (debugManager.isDebug()) Serial.println("Iniciando...");

	// text display tests
	display.clearDisplay();
	display.setTextSize(1);
	display.setTextColor(WHITE);
	display.setCursor(0, 0);

	display.println("Conectando Wifi...");
	display.display();

	if (wifiManager.connect()) {

		if (debugManager.isDebug()) Serial.println("conectou wifi!");

		display.println("Wifi conectado !!!");
		display.display();
		delay(2000);

		webSocket.beginSocketIO("192.168.1.12", 3000);
		//webSocket.setAuthorization("user", "Password"); // HTTP Basic Authorization
		webSocket.onEvent(webSocketEvent);

		ntpManager.begin();
	}
	else {
		display.println("Conexão com a rede WiFi falou!");
		display.display();
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
		if (deviceAddress[i] < 16) display.print("0");
		display.print(deviceAddress[i], HEX);
	}
}

void printDataDisplay(TemperatureSensor temperatureSensor)
{
	display.print("Address=");
	printAddressDisplay(temperatureSensor.deviceAddress);
	display.println();
	display.setTextSize(2);
	display.print(temperatureSensor.tempCelsius);
	display.println(" C ");
	display.print(temperatureSensor.tempFahrenheit);
	display.println(" F");
}

void loop() {	

	debugManager.update();

	webSocket.loop();

	if (isConnected) {		

		uint64_t now = millis();

		if (now - messageTimestamp > MESSAGE_INTERVAL) {

			// text display tests
			display.clearDisplay();
			display.setTextSize(1);
			display.setTextColor(WHITE);
			display.setCursor(0, 0);

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

			display.display();
		}
		if ((now - heartbeatTimestamp) > HEARTBEAT_INTERVAL) {
			heartbeatTimestamp = now;
			// socket.io heartbeat message
			webSocket.sendTXT("2");
		}		
	}
}