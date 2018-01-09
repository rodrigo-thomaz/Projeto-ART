#include "DeviceBuzzer.h"

DeviceBuzzer::DeviceBuzzer(int pin)
{
	this->_pin = pin;
}

DeviceBuzzer::~DeviceBuzzer()
{
}

void DeviceBuzzer::test()
{	
  tone(this->_pin,900,300); //aqui sai o som   
  /*   
   o número D7 indica que o pino positivo do buzzer está na porta 10   
   o número 300 é a frequência que será tocado   
   o número 300 é a duração do som   
  */    
}
