#ifndef Listener1_h
#define Listener1_h

#include "functional"

//#define LISTENER_CALLBACK_SIGNATURE std::function<void(char*, uint8_t*, unsigned int)>

namespace ART
{	
	//typedef std::function<void(void* params)> callbackSignature;
	
	template<class T>
	class Listener1
	{

	/*template<class T>
	friend class EventDispatcher1<T>; */

	public:

		Listener1()
		{
		}

		~Listener1()
		{
		}
		
		Listener1<T>& setCallback(T callback)
		{
			_callback = callback;
			return *this;
		}

		T _callback;

	private:

		//T _callback;

	};	
}

#endif