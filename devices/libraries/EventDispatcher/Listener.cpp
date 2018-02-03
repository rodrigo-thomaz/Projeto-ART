#include "Listener.h"

namespace ART
{
	Listener::Listener()
	{
	}

	Listener::~Listener()
	{
	}

	Listener& Listener::setCallback(LISTENER_CALLBACK_SIGNATURE callback)
	{
		_callback = callback;
		return *this;
	}
}