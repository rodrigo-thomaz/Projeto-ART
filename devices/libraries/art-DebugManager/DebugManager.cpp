#include "DebugManager.h"
#include "Arduino.h"

DebugManager::DebugManager(int pin)
{
	_pin = pin;
	_isDebug = false;
}

DebugManager::~DebugManager()
{
}

void DebugManager::update()
{	
	bool value = digitalRead(_pin);
	if (_isDebug != value) {
		_isDebug = value;
		Serial.print("[Debug Manager] Debug Mode ");
		Serial.println(_isDebug ? "On" : "Off");
	}	
}

bool DebugManager::isDebug()
{
	return _isDebug;
}