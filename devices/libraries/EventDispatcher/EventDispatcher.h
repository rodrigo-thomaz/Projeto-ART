#ifndef EventDispatcher_h
#define EventDispatcher_h

#ifndef LISTENER_LIST_SIZE
#define LISTENER_LIST_SIZE 30
#endif

#include "Listener.h"

namespace ART
{
	class EventDispatcher
	{

	public:

		EventDispatcher();
		~EventDispatcher();

		bool addListener(char event, Listener* listener);
		bool removeListener(Listener* listener);
		bool throwEvent(char event, void* params);		

	private:

		char events[LISTENER_LIST_SIZE];
		Listener *listeners[LISTENER_LIST_SIZE];
		EventDispatcher(EventDispatcher const&);
		void operator=(EventDispatcher const&);

	};
}

#endif