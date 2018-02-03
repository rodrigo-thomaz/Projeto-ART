#include "EventDispatcher1.h"

namespace ART
{
	template<typename T>
	EventDispatcher1<T>::EventDispatcher1()
	{
		
	}

	template<typename T>
	EventDispatcher1<T>::~EventDispatcher1() 
	{

	}	

	template<typename T>
	void EventDispatcher1<T>::addListener(Listener1<T> *listener)
	{
		_listeners.push_back(listener);
	}

	template<typename T>
	bool EventDispatcher1<T>::removeListener(Listener1<T>* listener)
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

	template<typename T>
	bool EventDispatcher1<T>::throwEvent(void* params)
	{
		for (int i = 0; i < this->_listeners.size(); ++i) {
			if (_listeners[i]->_callback) {
				_listeners[i]->_callback(params);
			}
		}
	}
}