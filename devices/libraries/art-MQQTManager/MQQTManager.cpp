#include "MQQTManager.h"

// MQQTManager

MQQTManager::MQQTManager(DebugManager& debugManager, ConfigurationManager& configurationManager)
{ 
	this->_debugManager = &debugManager;
	this->_configurationManager = &configurationManager;
}

bool MQQTManager::begin()
{ 
	
}