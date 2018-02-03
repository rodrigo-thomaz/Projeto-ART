#ifndef EventDispatcher_h
#define EventDispatcher_h

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

		std::vector<Listener*>			_listeners;

	};
}

#endif