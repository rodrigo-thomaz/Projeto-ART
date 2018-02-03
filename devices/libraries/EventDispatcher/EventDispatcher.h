#ifndef EventDispatcher_h
#define EventDispatcher_h

#ifndef LISTENER_LIST_SIZE
#define LISTENER_LIST_SIZE 30
#endif

#include "vector"

#include "Listener.h"

namespace ART
{
	class EventDispatcher
	{

	public:

		EventDispatcher();
		~EventDispatcher();
				
		void							addListener(Listener* listener);
		bool							removeListener(Listener* listener);
		bool							throwEvent(void* params);		

	private:

		EventDispatcher(EventDispatcher const&);

		std::vector<Listener*>			_listeners;
				
		void operator=(EventDispatcher const&);

	};
}

#endif