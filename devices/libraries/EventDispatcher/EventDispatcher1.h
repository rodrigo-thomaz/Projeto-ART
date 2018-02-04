#ifndef EventDispatcher1_h
#define EventDispatcher1_h

#include "functional"
#include "vector"

namespace ART
{
	class EventDispatcher1
	{

	public:

		template<typename Function>
		void addListener(Function && fn)
		{
			_functions.push_back(std::forward<Function>(fn));
		}

		void invoke_all()
		{
			for (auto && fn : _functions)
				fn();
		}

	private:

		std::vector<std::function<void()>> _functions;

	};
}

#endif