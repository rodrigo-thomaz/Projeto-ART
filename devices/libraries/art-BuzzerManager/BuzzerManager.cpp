#include "BuzzerManager.h"
#include "Arduino.h"
#include "DebugManager.h"

BuzzerManager::BuzzerManager(int pin, DebugManager& debugManager)
{
	this->_pin = pin;
	this->_debugManager = &debugManager;
}

BuzzerManager::~BuzzerManager()
{
}

void BuzzerManager::test()
{	
  tone(this->_pin,900,300); //aqui sai o som   
  /*   
   o número D7 indica que o pino positivo do buzzer está na porta 10   
   o número 300 é a frequência que será tocado   
   o número 300 é a duração do som   
  */    
}
