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

DebugManager debugManager(D6);
DisplayManager displayManager(debugManager);

Ultrasonic ultrasonic(D7, D8); // Instância chamada ultrasonic com parâmetros (trig,echo)

void setup() { 
  
  Serial.begin(9600); // Inicio da comunicação serial

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
  displayManager.display.setCursor(0, 20); 
  displayManager.display.print(ultrasonic.distanceRead());
  displayManager.display.print("cm");
  displayManager.display.display();
  
  delay(1000); // aguarda 1s 
}
