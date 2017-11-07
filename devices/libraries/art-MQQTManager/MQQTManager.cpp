#include "MQQTManager.h"

// MQQTManager

MQQTManager::MQQTManager(DebugManager& debugManager, ConfigurationManager& configurationManager, WiFiManager& wifiManager)
{ 
	this->_debugManager = &debugManager;
	this->_configurationManager = &configurationManager;
	this->_wifiManager = &wifiManager;
	
	this->_mqqt = new PubSubClient(this->_espClient);
	
	_onCallback = [=](char* topic, byte* payload, unsigned int length) {
		this->onCallback(topic, payload, length);
	};
}

void MQQTManager::teste1() 
{
    std::function<void(void)> f = std::bind(&MQQTManager::teste1, this);
}

void MQQTManager::onCallback(char* topic, byte* payload, unsigned int length) 
{
    
}

bool MQQTManager::begin()
{ 
	if(this->_begin) return true;
	
	if(this->_wifiManager->isConnected() && this->_configurationManager->initialized()){

		BrokerSettings* brokerSettings = this->_configurationManager->getBrokerSettings();

		char* const host = strdup(brokerSettings->getHost().c_str());
		int port = brokerSettings->getPort();

		this->_mqqt->setServer(host, port);           //informa qual broker e porta deve ser conectado			
		this->_mqqt->setCallback(_onCallback);      //atribui função de callback (função chamada quando qualquer informação de um dos tópicos subescritos chega) 

		this->_begin = true;

		Serial.println("[MQQT] Initialized with success !");
    }
    else{
		this->_begin = false;

		Serial.println("[MQQT] Not initialized !");
    }    	
}

MQQTManager& MQQTManager::setCallback(MQTTMANAGER_CALLBACK_SIGNATURE callback) {
    this->_callback = callback;
    return *this;
}

PubSubClient* MQQTManager::getMQQT() {    
    this->_mqqt;
}


