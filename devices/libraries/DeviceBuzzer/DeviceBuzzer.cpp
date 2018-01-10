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
									o número D7 indica que o pino positivo do buzzer está na porta 10
									o número 300 é a frequência que será tocado
									o número 300 é a duração do som
									*/
	}
}