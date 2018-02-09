#ifndef DeviceBuzzer_h
#define DeviceBuzzer_h

#include "Arduino.h"
#include "../ArduinoJson/ArduinoJson.h"

#define BUZZER_PIN    13 // D7 => 13

namespace ART
{
	class ESPDevice;

	class DeviceBuzzer
	{

	public:
		DeviceBuzzer(ESPDevice* espDevice);
		~DeviceBuzzer();

		static void				create(DeviceBuzzer* (&deviceBuzzer), ESPDevice* espDevice);

		void					test();

	private:

		ESPDevice * _espDevice;

	};
}

#endif