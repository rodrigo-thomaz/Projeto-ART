#ifndef EventDispatcher1_h
#define EventDispatcher1_h

#include "vector"

#include "Listener1.h"

namespace ART
{
	template<class T>
	class EventDispatcher1
	{

	public:

		EventDispatcher1()
		{

		}

		~EventDispatcher1()
		{

		}

		void addListener(Listener1<T>* listener)
		{
			_listeners.push_back(listener);
		}

		bool removeListener(Listener1<T>* listener)
		{
			for (int i = 0; i < this->_listeners.size(); ++i) {
				if (_listeners[i] == listener)
				{
					_listeners.erase(_listeners.begin() + i);
					return true;
				}
			}
			return false;
		}

		bool throwEvent(void* params)
		{
			for (int i = 0; i < this->_listeners.size(); ++i) {
				//if (_listeners[i]->_callback) {
				//	_listeners[i]->_callback(params);
				//}
			}
		}

	private:

		std::vector<Listener1<T>*> _listeners;

	};	
}

#endif