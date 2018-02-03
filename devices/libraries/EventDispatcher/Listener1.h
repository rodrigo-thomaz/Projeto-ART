#ifndef Listener1_h
#define Listener1_h

#include "functional"

#define LISTENER_CALLBACK_SIGNATURE std::function<void(void* params)>

namespace ART
{
	
	typedef std::function<void(void* params)> callbackSignature;
	
	class Listener1
	{

	friend class EventDispatcher;

	public:

		Listener1();
		~Listener1();
				
		Listener1& 								setCallback(callbackSignature callback);

	private:

		callbackSignature						_callback;

	};
}

#endif