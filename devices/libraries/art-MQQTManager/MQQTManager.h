#ifndef MQQTManager_h
#define MQQTManager_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "ConfigurationManager.h"
#include "WiFiManager.h"
#include "PubSubClient.h"

#define MQTTMANAGER_SUB_CALLBACK_SIGNATURE std::function<void(char*, uint8_t*, unsigned int)>
#define MQTTMANAGER_CONNECTED_CALLBACK_SIGNATURE std::function<void(PubSubClient*)>

class MQQTManager
{
  public:
  
    MQQTManager(DebugManager& debugManager, ConfigurationManager& configurationManager, WiFiManager& wifiManager);
	
	bool												begin();
	
	MQQTManager& 										setSubCallback(MQTTMANAGER_SUB_CALLBACK_SIGNATURE callback);
	MQQTManager& 										setConnectedCallback(MQTTMANAGER_CONNECTED_CALLBACK_SIGNATURE callback);
	
	bool												autoConnect();	
	
	PubSubClient*										getMQQT();
	
	void												publish(const char* topic, const char* payload);
	void												subscribeInApplication(const char* topic);	
	void												subscribeInDevice(const char* topic);	
	
	String 												getTopicKey(char* routingKey);
	
	
  private:			
			
	DebugManager*          								_debugManager;	
	ConfigurationManager*          						_configurationManager;	
	WiFiManager* 										_wifiManager;
	
	bool												_begin;
	
	String		 										_clientId;
	
	WiFiClient	 										_espClient;
	PubSubClient* 										_mqqt;
	
	MQTTMANAGER_SUB_CALLBACK_SIGNATURE					_subCallback;
	MQTTMANAGER_SUB_CALLBACK_SIGNATURE					_onSubCallback;
	
	MQTTMANAGER_CONNECTED_CALLBACK_SIGNATURE			_connectedCallback;
	
	void												onSubCallback(char* topic, byte* payload, unsigned int length);
	
	String 												getApplicationRoutingKey(const char* topic);
	String 												getDeviceRoutingKey(const char* topic);
};

#endif
