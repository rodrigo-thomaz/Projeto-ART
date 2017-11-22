#include "MQQTManager.h"
#include <cstddef>         // std::size_t

// MQQTManager

MQQTManager::MQQTManager(DebugManager& debugManager, ConfigurationManager& configurationManager, WiFiManager& wifiManager)
{ 
	this->_debugManager = &debugManager;
	this->_configurationManager = &configurationManager;
	this->_wifiManager = &wifiManager;
	
	this->_mqqt = new PubSubClient(this->_espClient);
	
	_onSubCallback = [=](char* topic, byte* payload, unsigned int length) {
		this->onSubCallback(topic, payload, length);
	};
}

bool MQQTManager::begin()
{ 
	if(this->_begin) return true;
	
	if(this->_wifiManager->isConnected() && this->_configurationManager->initialized()){

		BrokerSettings* brokerSettings = this->_configurationManager->getBrokerSettings();

		char* const host = strdup(brokerSettings->getHost().c_str());
		int port = brokerSettings->getPort();

		this->_mqqt->setServer(host, port);         //informa qual broker e porta deve ser conectado			
		this->_mqqt->setCallback(_onSubCallback);      //atribui função de callback (função chamada quando qualquer informação de um dos tópicos subescritos chega) 

		this->_begin = true;

		Serial.println("[MQQT] Initialized with success !");
    }
    else{
		this->_begin = false;

		Serial.println("[MQQT] Not initialized !");
    }    	
}

bool MQQTManager::autoConnect()
{ 
	if(!this->_wifiManager->isConnected() || !this->_configurationManager->initialized()){
      return false;
    }
    
    if(!this->begin()){
      return false;
    }
    
    if (this->_mqqt->connected()) {
        return true;
    }
	else {
		
		BrokerSettings* brokerSettings = this->_configurationManager->getBrokerSettings();        
      
        char* const host 		= strdup(brokerSettings->getHost().c_str());
        char* const user 		= strdup(brokerSettings->getUser().c_str());
        char* const pwd  		= strdup(brokerSettings->getPwd().c_str());
		char* const clientId  	= strdup(brokerSettings->getClientId().c_str());
        		
        Serial.print("[MQQT] Tentando se conectar ao Broker MQTT: ");
        Serial.println(host);

        Serial.print("[MQQT] ClientId: ");
        Serial.println(clientId);        
        
        Serial.print("[MQQT] User: ");
        Serial.println(user);        

        Serial.print("[MQQT] Pwd: ");
        Serial.println(pwd);        

        byte willQoS = 0;
        const char* willTopic = "willTopic";
        const char* willMessage = "My Will Message";
        boolean willRetain = false;
        
        if (this->_mqqt->connect(clientId, user, pwd)) 
        //if (this->_mqqt->connect(clientId, user, pwd, willTopic, willQoS, willRetain, willMessage)) 
        {
            Serial.println("[MQQT] Conectado com sucesso ao broker MQTT!");

            if (this->_connectedCallback) {
				this->_connectedCallback(this->_mqqt);
			}     
			
			return true;
        } 
        else 
        {
            Serial.println("[MQQT] Falha ao reconectar no broker.");
            Serial.println("[MQQT] Haverá nova tentatica de conexao em 2s");
            delay(2000);
			
			return false;
        }
	}
}

MQQTManager& MQQTManager::setSubCallback(MQTTMANAGER_SUB_CALLBACK_SIGNATURE callback) {
    this->_subCallback = callback;
    return *this;
}

void MQQTManager::onSubCallback(char* topic, byte* payload, unsigned int length) 
{
    if (this->_subCallback) {
		this->_subCallback(topic, payload, length);
	}
}

MQQTManager& MQQTManager::setConnectedCallback(MQTTMANAGER_CONNECTED_CALLBACK_SIGNATURE callback) {
    this->_connectedCallback = callback;
    return *this;
}

PubSubClient* MQQTManager::getMQQT() {    
    return this->_mqqt;
}

void MQQTManager::publish(const char* topic, const char* payload)
{
	String routingKey = this->getApplicationRoutingKey(topic);	
	this->_mqqt->publish(routingKey.c_str(), payload); 
}

void MQQTManager::subscribeInApplication(const char* topic)
{
	String routingKey = this->getApplicationRoutingKey(topic);	
	this->_mqqt->subscribe(routingKey.c_str());
	this->_mqqt->loop();  
	
	Serial.print("[MQQTManager::subscribe] Subscribe in application with success routingKey: ");
    Serial.println(routingKey);
}

void MQQTManager::unSubscribeInApplication(const char* topic)
{
	String routingKey = this->getApplicationRoutingKey(topic);	
	this->_mqqt->unsubscribe(routingKey.c_str());
	this->_mqqt->loop();  
	
	Serial.print("[MQQTManager::unSubscribeInApplication] UnSubscribeInApplication in application with success routingKey: ");
    Serial.println(routingKey);
}

void MQQTManager::subscribeInDevice(const char* topic)
{
	String routingKey = this->getDeviceRoutingKey(topic);	
	this->_mqqt->subscribe(routingKey.c_str());
	this->_mqqt->loop();  
	
	Serial.print("[MQQTManager::subscribe] Subscribe in application with success routingKey: ");
    Serial.println(routingKey);
}

void MQQTManager::unSubscribeInDevice(const char* topic)
{
	String routingKey = this->getDeviceRoutingKey(topic);	
	this->_mqqt->unsubscribe(routingKey.c_str());
	this->_mqqt->loop();  
	
	Serial.print("[MQQTManager::unSubscribeInDevice] UnSubscribeInDevice in application with success routingKey: ");
    Serial.println(routingKey);
}

String MQQTManager::getTopicKey(char* routingKey)
{
	String routingKeyStr = String(routingKey);
	int lastIndexOf = routingKeyStr.lastIndexOf('/');
	
	String restString = routingKeyStr.substring(0, lastIndexOf);
	int restLastIndexOf = restString.lastIndexOf('/');
	int restSize = sizeof(routingKeyStr) - restLastIndexOf;
	
	String result = routingKeyStr.substring(restLastIndexOf + 1, restSize);
		
	return result;
}

String MQQTManager::getApplicationRoutingKey(const char* topic)
{
	BrokerSettings* brokerSettings = this->_configurationManager->getBrokerSettings();        
	String applicationTopic = brokerSettings->getApplicationTopic();
	String deviceTopic = brokerSettings->getDeviceTopic();
	
	String routingKey = String("ART/Application/");
	routingKey.concat(applicationTopic);
	routingKey.concat("/Device/");
	routingKey.concat(deviceTopic);
	routingKey.concat("/");
	routingKey.concat(topic);
	
	return routingKey;
}

String MQQTManager::getDeviceRoutingKey(const char* topic)
{
	BrokerSettings* brokerSettings = this->_configurationManager->getBrokerSettings();        
	String deviceTopic = brokerSettings->getDeviceTopic();
	
	String routingKey = String("ART/Device/");
	routingKey.concat(deviceTopic);
	routingKey.concat("/");
	routingKey.concat(topic);
	
	return routingKey;
}