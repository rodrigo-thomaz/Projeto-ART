#include "ConfigurationManager.h"

// ConfigurationManager

ConfigurationManager::ConfigurationManager(ESPDevice& espDevice)
{ 
	_espDevice = &espDevice;
}

bool ConfigurationManager::loaded()
{	
	return _espDevice->loaded();
}

ESPDevice* ConfigurationManager::getESPDevice()
{	
	return _espDevice;
}

