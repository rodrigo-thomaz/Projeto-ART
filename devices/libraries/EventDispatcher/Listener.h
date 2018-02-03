#ifndef Listener_h
#define Listener_h

#include "functional"

#define LISTENER_CALLBACK_SIGNATURE std::function<void(void* params)>

namespace ART
{
	class Listener
	{

	friend class EventDispatcher;

	public:

		Listener();
		~Listener();

		Listener& 										setCallback(LISTENER_CALLBACK_SIGNATURE callback);

	private:

		LISTENER_CALLBACK_SIGNATURE						_callback;		

	};
}

#endif