#ifndef SimpleListener_h
#define SimpleListener_h

#include "Arduino.h"

#include "Listener.h"

namespace ART
{
	class SimpleListener : public Listener
	{

	public:

		void onEvent(void* params);

	};
}

#endif