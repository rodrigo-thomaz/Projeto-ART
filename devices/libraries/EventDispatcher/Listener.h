#ifndef Listener_h
#define Listener_h

namespace ART
{
	class Listener
	{

	public:

		Listener() {}
		virtual ~Listener() {}
		virtual void onEvent(char event, void* params) = 0;
	};
}

#endif