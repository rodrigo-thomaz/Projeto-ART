#include "EventDispatcher.h"

namespace ART
{
	EventDispatcher::EventDispatcher()
	{
		
	}

	EventDispatcher::~EventDispatcher() {}	

	void EventDispatcher::addListener(Listener *listener)
	{
		_listeners.push_back(listener);
	}

	bool EventDispatcher::removeListener(Listener* listener)
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

	bool EventDispatcher::throwEvent(void* params)
	{
		for (int i = 0; i < this->_listeners.size(); ++i) {
			
			_listeners[i]->onEvent(params);
			
			if (_listeners[i]->_callback) {
				_listeners[i]->_callback(params);
			}
		}
	}
}