#ifndef Listener1_h
#define Listener1_h

#include "functional"

//#define LISTENER_CALLBACK_SIGNATURE std::function<void(void* params)>

namespace ART
{	
	//typedef std::function<void(void* params)> callbackSignature;
	
	template<typename T>
	class Listener1
	{

	friend class EventDispatcher;

	public:

		Listener1();
		~Listener1();				
		
		Listener1<T>& 			setCallback(T callback);

	private:

		T						_callback;

	};	
}

#endif