#include <DebugManager.h>

DebugManager::DebugManager()
{
	_isDebug = false;
}

DebugManager::~DebugManager()
{
}

void DebugManager::update()
{
	
}

bool DebugManager::isDebug()
{
	return _isDebug;
}