#include "DeviceBuzzer.h"
#include "ESPDevice.h"

namespace ART
{
	DeviceBuzzer::DeviceBuzzer(ESPDevice* espDevice)
	{
		_espDevice = espDevice;
	}

	DeviceBuzzer::~DeviceBuzzer()
	{
		delete (_espDevice);
	}

	void DeviceBuzzer::test()
	{
		tone(BUZZER_PIN, 900, 300); //aqui sai o som   
									/*
									o n�mero D7 indica que o pino positivo do buzzer est� na porta 10
									o n�mero 300 � a frequ�ncia que ser� tocado
									o n�mero 300 � a dura��o do som
									*/
	}
}