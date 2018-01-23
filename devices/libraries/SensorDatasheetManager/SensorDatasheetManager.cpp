#include "SensorDatasheetManager.h"

namespace ART
{
	SensorDatasheetManager::SensorDatasheetManager()
	{
		Serial.println("[SensorDatasheetManager constructor]");
	}

	SensorDatasheetManager::~SensorDatasheetManager()
	{
		Serial.println("[SensorDatasheetManager destructor]");
	}	
}