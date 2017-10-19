#include "Arduino.h"
#include "DebugManager.h"
#include "TemperatureSensorManager.h"
#include "NTPManager.h"
#include "DisplayManager.h"
#include "WiFiManager.h"
#include "PubSubClient.h"
#include "WiFiClient.h"
#include "ArduinoJson.h"
#include "Fonts/FreeSans9pt7b.h"
#include "Fonts/FreeSansBold9pt7b.h"

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

#define ID_MQTT  "294700e3-b9a7-e711-9bef-707781d470bc" // HardwareBaseId

#define TOPIC_SUB_SET_RESOLUTION "DSFamilyTempSensor.SetResolution"
#define TOPIC_SUB_SET_HIGH_ALARM "DSFamilyTempSensor.SetHighAlarm"
#define TOPIC_SUB_SET_LOW_ALARM "DSFamilyTempSensor.SetLowAlarm"

#define TOPICO_PUBLISH   "ARTPUBTEMP"    //tópico MQTT de envio de informações para Broker

#define MESSAGE_INTERVAL 4000
uint64_t messageTimestamp = 0;

#define READTEMP_INTERVAL 2000
uint64_t readTempTimestamp = 0;

DebugManager debugManager(D6);
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

  // Buzzer
  pinMode(D6,OUTPUT);

  pinMode(D4, INPUT);
  pinMode(D5, INPUT);  

	debugManager.update();

	displayManager.begin();

	temperatureSensorManager.begin();

	if (debugManager.isDebug()) Serial.println("Iniciando...");

	// text display tests
	displayManager.display.clearDisplay();
	displayManager.display.setTextSize(1);
	displayManager.display.setTextColor(WHITE);
	displayManager.display.setCursor(0, 0);	
	displayManager.display.display();

	wifiManager.setStartConfigPortalCallback(startConfigPortalCallback);
  wifiManager.setCaptivePortalCallback(captivePortalCallback);
  wifiManager.setSuccessConfigPortalCallback(successConfigPortalCallback);    
  wifiManager.setFailedConfigPortalCallback(failedConfigPortalCallback);    
  wifiManager.setSuccessToConnectCallback(configSuccessToConnectCallback);    
  wifiManager.setFailedToConnectCallback(configFailedToConnectCallback);    
  wifiManager.autoConnect();
  
  initMQTT();

	ntpManager.begin();

	displayManager.display.println("Wifi conectado !!!");
	displayManager.display.display();
	delay(2000);

  Serial.println();
  Serial.print("MAC: ");
  Serial.println(WiFi.macAddress());
}

void initMQTT() 
{
    MQTT.setServer(BROKER_MQTT, BROKER_PORT);   //informa qual broker e porta deve ser conectado
    MQTT.setCallback(mqtt_callback);            //atribui função de callback (função chamada quando qualquer informação de um dos tópicos subescritos chega)
}
 
void mqtt_callback(char* topic, byte* payload, unsigned int length) 
{
    String json;
    
    //obtem a string do payload recebido
    for(int i = 0; i < length; i++) 
    {
       char c = (char)payload[i];
       json += c;
    }

    Serial.print("payload: ");
    Serial.println(json);

    StaticJsonBuffer<300> jsonBuffer;
    
    JsonObject& root = jsonBuffer.parseObject(json);
  
    if (!root.success()) {
      Serial.print("parse payload failed: ");
      Serial.println(json);
      return;
    }

    String payloadTopic = root["topic"];
    String payloadContract = root["contract"];

    Serial.print("payloadTopic: ");
    Serial.println(payloadTopic);

    Serial.print("payloadContract: ");
    Serial.println(payloadContract);

    if(payloadTopic == String(TOPIC_SUB_SET_RESOLUTION)){
      temperatureSensorManager.setResolution(payloadContract);
    }
    if(payloadTopic == String(TOPIC_SUB_SET_HIGH_ALARM)){
      temperatureSensorManager.setHighAlarm(payloadContract);
    }
    if(payloadTopic == String(TOPIC_SUB_SET_LOW_ALARM)){
      temperatureSensorManager.setLowAlarm(payloadContract);
    }
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

            MQTT.subscribe(TOPIC_SUB_SET_RESOLUTION); 
            MQTT.subscribe(TOPIC_SUB_SET_HIGH_ALARM); 
            MQTT.subscribe(TOPIC_SUB_SET_LOW_ALARM);         
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

void printDataDisplay()
{
    displayManager.display.clearDisplay();
    displayManager.display.setFont();
    displayManager.display.setTextSize(1);
    displayManager.display.setTextColor(WHITE);
    displayManager.display.setCursor(0, 0);       

    // Sensor
    if(sizeof(temperatureSensorManager.Sensors)/sizeof(int) > 0){
      displayManager.display.setTextSize(2);
      displayManager.display.setTextColor(WHITE);
      displayManager.display.setCursor(0, 0);    
      
      displayManager.display.print(temperatureSensorManager.Sensors[0].tempCelsius);
      displayManager.display.println(" C");
      
      //displayManager.display.print(temperatureSensorManager.Sensors[0].tempFahrenheit);
      //displayManager.display.println(" F");
    }

    // Formatted Time
    String formattedTime = ntpManager.getFormattedTime();
    displayManager.display.println(formattedTime);


    // Wifi
    int quality = wifiManager.getQuality();
    int bars = wifiManager.convertQualitytToBarsSignal(quality);
    for (int b=0; b <= bars; b++) {
      // display.fillRect(59 + (b*5),33 - (b*5),3,b*5,WHITE); 
      displayManager.display.fillRect(10 + (b*5),48 - (b*5),3,b*5,WHITE); 
    }
  
    displayManager.display.display();
}

bool firstTimecaptivePortalCallback = true;

void startConfigPortalCallback (String ssid, String pwd) {

  firstTimecaptivePortalCallback = true;
  
  displayManager.display.clearDisplay();
  displayManager.display.setTextSize(2);
  displayManager.display.setTextColor(WHITE);
  displayManager.display.setCursor(0, 0);       

  displayManager.display.setFont();

  displayManager.display.println(" entrando");
  displayManager.display.println(" no setup");
  displayManager.display.println(" do  wifi");

  displayManager.display.display();

  delay(400);
  
  displayManager.display.print(" ");  
  for (int i=0; i <= 6; i++) {
    displayManager.display.print(".");  
    displayManager.display.display();
    delay(400);
  } 

  displayManager.display.clearDisplay();
  
  printPortalHeaderInDisplay("  Conecte  ");
  
  displayManager.display.println();
  displayManager.display.println();
  displayManager.display.setFont(&FreeSansBold9pt7b);
  displayManager.display.setTextSize(1);  
  displayManager.display.print("ssid:  ");
  displayManager.display.println(ssid);  
  displayManager.display.print("pwd: ");  
  displayManager.display.setTextWrap(false);
  displayManager.display.print(pwd);    
    
  displayManager.display.display();
}

void captivePortalCallback (String ip) {

  if(!firstTimecaptivePortalCallback){
    return;
  }

  firstTimecaptivePortalCallback = false;

  displayManager.display.clearDisplay();
  
  printPortalHeaderInDisplay("  Acesse    ");
  
  displayManager.display.println();
  displayManager.display.println();
  displayManager.display.println();
  displayManager.display.setFont(&FreeSansBold9pt7b);
  displayManager.display.setTextSize(1);  
  displayManager.display.setTextWrap(false);
  displayManager.display.print("  http://");
  displayManager.display.println(ip);    
    
  displayManager.display.display();  
}

void successConfigPortalCallback () {  

  String ssid = wifiManager.getSSID();
  
  displayManager.display.clearDisplay();
  
  printPortalHeaderInDisplay("  Acesso    ");
    
  displayManager.display.println();
  displayManager.display.println();
  displayManager.display.setFont(&FreeSansBold9pt7b);
  displayManager.display.setTextSize(1);  
  displayManager.display.setTextWrap(false);
  displayManager.display.println("Conectado a");
  displayManager.display.print(ssid);
  displayManager.display.print("!");
  displayManager.display.display();  

  delay(4000);
}

void failedConfigPortalCallback (String ssid, int connectionResult, String message) {  

  displayManager.display.clearDisplay();
  
  printPortalHeaderInDisplay("  Acesso    ");

  if(connectionResult == WL_CONNECT_FAILED){
    displayManager.display.println();
    displayManager.display.println();
    displayManager.display.setFont(&FreeSansBold9pt7b);
    displayManager.display.setTextSize(1);  
    displayManager.display.setTextWrap(false);
    displayManager.display.println("   Ops! falha");
    displayManager.display.println("  na tentativa");
  }
    
  displayManager.display.display();    

  bool invertDisplay = false;
  for (int i=0; i <= 30; i++) {
    displayManager.display.invertDisplay(invertDisplay);
    invertDisplay = !invertDisplay;
    delay(500);
  }

  firstTimecaptivePortalCallback = true;
  
}

void printPortalHeaderInDisplay(String title)
{  
  displayManager.display.setFont();
  displayManager.display.setTextSize(2);  
  displayManager.display.setCursor(0, 0);       
  displayManager.display.setTextWrap(false);  
  displayManager.display.setTextColor(BLACK, WHITE);
  displayManager.display.println(title);
  displayManager.display.display();
  displayManager.display.setTextColor(WHITE);
  displayManager.display.setTextSize(1);  
}

void configSuccessToConnectCallback (String ssid, int quality, int bars) {  
  Serial.print("Conectado na rede Wifi: ");
  Serial.print(ssid);
  Serial.print(" qualidade: ");    
  Serial.print(quality);
  Serial.print(" bars: ");    
  Serial.println(bars);

  for (int b=0; b <= bars; b++) {
    // display.fillRect(59 + (b*5),33 - (b*5),3,b*5,WHITE); 
    displayManager.display.fillRect(10 + (b*5),48 - (b*5),3,b*5,WHITE); 
  }
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

  //garante funcionamento das conexões WiFi e ao broker MQTT
  VerificaConexoesWiFIEMQTT(); 

  uint64_t now = millis(); 

  if(now - readTempTimestamp > READTEMP_INTERVAL) {
    readTempTimestamp = now;
    temperatureSensorManager.refresh();
  }

  if(now - messageTimestamp > MESSAGE_INTERVAL) {
    messageTimestamp = now;
    char *sensorsJson = temperatureSensorManager.convertSensorsToJson();
    Serial.print("enviando para o servidor => ");
    Serial.println(sensorsJson);
    MQTT.publish(TOPICO_PUBLISH, sensorsJson);      
  }      

  printDataDisplay(); 
  
  // Buzzer
  //tone(D7,900,300); //aqui sai o som   
  /*   
   o número D7 indica que o pino positivo do buzzer está na porta 10   
   o número 300 é a frequência que será tocado   
   o número 300 é a duração do som   
  */    
  
  //keep-alive da comunicação com broker MQTT
  MQTT.loop();
}
