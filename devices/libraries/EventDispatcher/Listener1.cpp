#include "Listener1.h"

namespace ART
{
	template<typename T>
	Listener1<T>::Listener1()
	{
	}

	template<typename T>
	Listener1<T>::~Listener1()
	{
	}

	template<typename T>
	Listener1<T> & Listener1<T>::setCallback(T callback)
	{
		_callback = callback;
		return *this;
	}

}