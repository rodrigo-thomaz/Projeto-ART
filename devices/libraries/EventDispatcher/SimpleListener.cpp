#include "SimpleListener.h"

namespace ART
{
	void SimpleListener::onEvent(void* params)
	{		
		char* p = (char*)params;  // cast it to an int

		Serial.print("[onEvent] params: ");
		Serial.print(p);

		Serial.println(" !!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
	}
}