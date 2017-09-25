#include "Arduino.h"
#include "DebugManager.h"
#include "TemperatureSensorManager.h"
#include "NTPManager.h"
#include "DisplayManager.h"
#include "WiFiManager.h"
#include "PubSubClient.h"
#include "WiFiClient.h"

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

#define ID_MQTT  "HomeAut"

#define TOPICO_SUBSCRIBE "ARTSUBTEMPMIN"     //tópico MQTT de escuta
#define TOPICO_PUBLISH   "ARTPUBTEMP"    //tópico MQTT de envio de informações para Broker

DebugManager debugManager(D0);
NTPManager ntpManager(debugManager);
DisplayManager displayManager(debugManager);
WiFiManager wifiManager(D5, debugManager);
TemperatureSensorManager temperatureSensorManager(debugManager, ntpManager);

//const char* BROKER_MQTT = "broker.hivemq.com"; //URL do broker MQTT que se deseja utilizar
const char* BROKER_MQTT = "file-server.rthomaz.local"; //URL do broker MQTT que se deseja utilizar
int BROKER_PORT = 1883; // Porta do Broker MQTT

WiFiClient espClient;
PubSubClient MQTT(espClient);

void setup() {
		
	Serial.begin(9600);

	debugManager.update();

	displayManager.begin();

	temperatureSensorManager.begin();
  temperatureSensorManager.setCallback(sensorCallback);

	if (debugManager.isDebug()) Serial.println("Iniciando...");

	// text display tests
	displayManager.display.clearDisplay();
	displayManager.display.setTextSize(1);
	displayManager.display.setTextColor(WHITE);
	displayManager.display.setCursor(0, 0);

	displayManager.display.println("Conectando Wifi...");
	displayManager.display.display();

	wifiManager.setAPCallback(configModeCallback);
  wifiManager.setSuccessToConnectCallback(configSuccessToConnectCallback);    
  wifiManager.setFailedToConnectCallback(configFailedToConnectCallback);    
  wifiManager.autoConnect();
  
  initMQTT();

	ntpManager.begin();

	displayManager.display.println("Wifi conectado !!!");
	displayManager.display.display();
	delay(2000);
}

void initMQTT() 
{
    MQTT.setServer(BROKER_MQTT, BROKER_PORT);   //informa qual broker e porta deve ser conectado
    MQTT.setCallback(mqtt_callback);            //atribui função de callback (função chamada quando qualquer informação de um dos tópicos subescritos chega)
}
 
void mqtt_callback(char* topic, byte* payload, unsigned int length) 
{
    String msg;

    //obtem a string do payload recebido
    for(int i = 0; i < length; i++) 
    {
       char c = (char)payload[i];
       msg += c;
    }
  
    Serial.print("RECEBIDO ");
    Serial.println(msg);
}
 
void reconnectMQTT() 
{
    if (!MQTT.connected()) 
    {
        Serial.print("* Tentando se conectar ao Broker MQTT: ");
        Serial.println(BROKER_MQTT);
        if (MQTT.connect(ID_MQTT, "test", "test")) 
        {
            Serial.println("Conectado com sucesso ao broker MQTT!");
            MQTT.subscribe(TOPICO_SUBSCRIBE); 
        } 
        else 
        {
            Serial.println("Falha ao reconectar no broker.");
            Serial.println("Havera nova tentatica de conexao em 2s");
            delay(2000);
        }
    }
}

void VerificaConexoesWiFIEMQTT(void)
{    
     reconnectMQTT(); //se não há conexão com o Broker, a conexão é refeita
     wifiManager.autoConnect(); //se não há conexão com o WiFI, a conexão é refeita
}

void printDataDisplay(TemperatureSensor temperatureSensor)
{
  displayManager.display.print("Address=");
  displayManager.display.println(temperatureSensor.deviceAddressStr);
  displayManager.display.print(temperatureSensor.tempCelsius);
  displayManager.display.print(" C | ");
  displayManager.display.print(temperatureSensor.tempFahrenheit);
  displayManager.display.println(" F");
}

void sensorCallback(TemperatureSensor sensor)
{
  printDataDisplay(sensor);       
}

void sendTemp(){
  char *sensorsJson = temperatureSensorManager.getSensorsJson();
  MQTT.publish(TOPICO_PUBLISH, sensorsJson);  
}

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

void loop() {	

  debugManager.update();
  
  // text display tests
  displayManager.display.clearDisplay();
  displayManager.display.setTextSize(1);
  displayManager.display.setTextColor(WHITE);
  displayManager.display.setCursor(0, 0);  	

  //garante funcionamento das conexões WiFi e ao broker MQTT
  VerificaConexoesWiFIEMQTT(); 

  sendTemp();

  displayManager.display.display();

  //keep-alive da comunicação com broker MQTT
  MQTT.loop();
}
