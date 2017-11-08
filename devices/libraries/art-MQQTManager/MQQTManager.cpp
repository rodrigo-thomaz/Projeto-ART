#include "MQQTManager.h"

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
        HardwareSettings* hardwareSettings = this->_configurationManager->getHardwareSettings();
      
        char* const host = strdup(brokerSettings->getHost().c_str());
        char* const user = strdup(brokerSettings->getUser().c_str());
        char* const pwd  = strdup(brokerSettings->getPwd().c_str());
        
        char* const clientId  = strdup(hardwareSettings->getHardwareId().c_str());
        
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
        
        //if (MQTT.connect(clientId, brokerUser, brokerPwd)) 
        if (this->_mqqt->connect(clientId, user, pwd, willTopic, willQoS, willRetain, willMessage)) 
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

