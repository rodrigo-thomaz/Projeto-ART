#ifndef MQQTManager_h
#define MQQTManager_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "ConfigurationManager.h"
#include "PubSubClient.h"

class MQQTManager
{
  public:
  
    MQQTManager(DebugManager& debugManager, ConfigurationManager& configurationManager);
	
	bool								begin();
	
  private:			
			
	DebugManager*          				_debugManager;	
	ConfigurationManager*          		_configurationManager;	
	
	bool								_initialized;
	
	WiFiClient* 						_espClient;
	PubSubClient* 						_mqqt;
	
};

#endif
