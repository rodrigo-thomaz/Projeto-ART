#include "SimpleListener.h"

namespace ART
{
	void SimpleListener::onEvent(char event, void* params)
	{		
		char* p = (char*)params;  // cast it to an int

		Serial.print("[onEvent] event: ");
		Serial.print((uint8_t)event);
		Serial.print(" params: ");
		Serial.print(p);

		Serial.println(" !!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
	}
}