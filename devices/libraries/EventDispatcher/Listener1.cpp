//#include "Listener1.h"
//
//namespace ART
//{
//	template<class T>
//	Listener1<T>::Listener1()
//	{
//	}
//
//	template<class T>
//	Listener1<T>::~Listener1()
//	{
//	}
//
//	template<class T>
//	Listener1<T> & Listener1<T>::setCallback(T callback)
//	{
//		_callback = callback;
//		return *this;
//	}
//
//	template class Listener1<std::function<void(char*, uint8_t*, unsigned int)>>;
//}