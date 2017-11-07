#ifndef MQQTManager_h
#define MQQTManager_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "ConfigurationManager.h"
#include "WiFiManager.h"
#include "PubSubClient.h"

#define MQTTMANAGER_CALLBACK_SIGNATURE std::function<void(char*, uint8_t*, unsigned int)>

class MQQTManager
{
  public:
  
    MQQTManager(DebugManager& debugManager, ConfigurationManager& configurationManager, WiFiManager& wifiManager);
	
	bool												begin();
	
	MQQTManager& 										setCallback(MQTTMANAGER_CALLBACK_SIGNATURE callback);
	
	PubSubClient*										getMQQT();
	
  private:			
			
	DebugManager*          								_debugManager;	
	ConfigurationManager*          						_configurationManager;	
	WiFiManager* 										_wifiManager;
	
	bool												_begin;
				
	WiFiClient	 										_espClient;
	PubSubClient* 										_mqqt;
	
	MQTTMANAGER_CALLBACK_SIGNATURE						_callback;
	MQTTMANAGER_CALLBACK_SIGNATURE						_onCallback;
	
	void												onCallback(char* topic, byte* payload, unsigned int length);
	
	void teste1();
	
};

#endif
