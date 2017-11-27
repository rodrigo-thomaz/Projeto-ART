#include "DebugManager.h"
#include "DisplayManager.h"

/*=========================================================================================
                        Baú da Eletrônica Componentes Eletrônicos
                              www.baudaeletronica.com.br
                        Integrando sensor ultrassônico ao NodeMcu
===========================================================================================
 
 Pinagem
 * ---------------------    
 * | HC-SC04 | NodeMCU |   
 * ---------------------   
 * |   Vcc   |   5V    |     
 * |   Trig  |   D7    | 
 * |   Echo  |   D8    |   
 * |   Gnd   |   GND   |   */

#include <Ultrasonic.h> // Declaração de biblioteca

#define D5    14 // LM393 

const int sampleWindow = 50; // Sample window width in mS (50 mS = 20Hz)
unsigned int sample;




DebugManager debugManager(D6);
DisplayManager displayManager(debugManager);

Ultrasonic ultrasonic(D7, D8); // Instância chamada ultrasonic com parâmetros (trig,echo)

void setup() { 
  
  Serial.begin(9600); // Inicio da comunicação serial

  pinMode (A0, INPUT);
  pinMode(D5, INPUT);

  debugManager.update();  

  displayManager.begin();

  // text display tests
  displayManager.display.clearDisplay();
  displayManager.display.setTextSize(1);
  displayManager.display.setTextColor(WHITE);
  displayManager.display.setCursor(0, 0); 
  displayManager.display.display();
}

void loop() {
  Serial.print("Distancia: "); // Escreve texto na tela
  Serial.print(ultrasonic.distanceRead());// distância medida em cm
  Serial.println("cm"); // escreve texto na tela e pula uma linha

  displayManager.display.clearDisplay();
  displayManager.display.setTextSize(3);
  displayManager.display.setTextColor(WHITE);
  displayManager.display.setCursor(0, 10); 
  displayManager.display.print(ultrasonic.distanceRead());
  displayManager.display.println("cm");

  int valor_D = digitalRead(D5) * (5.0 / 1023.0); 
  float valor_A = analogRead(A0);
  
  displayManager.display.print(valor_A);
  displayManager.display.print(" - ");

  displayManager.display.println(valor_D);
  
  displayManager.display.display();
  
  //delay(1000); // aguarda 1s 
  //Som();
}

void Som(){
  unsigned long startMillis= millis();  // Start of sample window
   unsigned int peakToPeak = 0;   // peak-to-peak level
 
   unsigned int signalMax = 0;
   unsigned int signalMin = 1024;
 
   // collect data for 50 mS
   while (millis() - startMillis < sampleWindow)
   {
      sample = analogRead(0);
      if (sample < 1024)  // toss out spurious readings
      {
         if (sample > signalMax)
         {
            signalMax = sample;  // save just the max levels
         }
         else if (sample < signalMin)
         {
            signalMin = sample;  // save just the min levels
         }
      }
   }
   peakToPeak = signalMax - signalMin;  // max - min = peak-peak amplitude
   double volts = (peakToPeak * 5.0) / 1024;  // convert to volts
 
   Serial.println(volts);
}


