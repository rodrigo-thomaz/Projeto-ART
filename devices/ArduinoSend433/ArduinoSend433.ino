#include <VirtualWire.h>

const int pushButtonPin = 2; //PINO DIGITAL UTILIZADO PELO PUSH BUTTON
const int txPin = 3; //PINO DIGITAL UTILIZADO PELO PUSH BUTTON
const int ledPin = 4;  

int currentValue = 0;

void setup() {

  Serial.begin(9600);     // opens serial port, sets data rate to 9600 bps
  
  pinMode(pushButtonPin, INPUT); //DEFINE A PORTA COMO ENTRADA
  pinMode(ledPin, OUTPUT);      // configura pino digital como saída
  
  vw_set_tx_pin(txPin);
  //Velocidade de comunicacao (bits por segundo)
  vw_setup(5000);
}

void loop() {

  int leitura = !digitalRead(pushButtonPin); //LÊ O VALOR NA PORTA DIGITAL E ARMAZENA NA VARIÁVEL

  if(leitura == 1){

   if(currentValue == 1){
    currentValue = 0;
    digitalWrite(ledPin, LOW);
   }
   else{
    currentValue = 1;
    digitalWrite(ledPin, HIGH);
   }
    
  char Valor_CharMsg[4]; 
  itoa(currentValue, Valor_CharMsg, 10);
  vw_send((uint8_t *)Valor_CharMsg, strlen(Valor_CharMsg));
    //Aguarda envio dos dados
  vw_wait_tx();
  Serial.print("Valor enviado: ");
  Serial.println(Valor_CharMsg);   

    delay(500);
  }  
  
}
