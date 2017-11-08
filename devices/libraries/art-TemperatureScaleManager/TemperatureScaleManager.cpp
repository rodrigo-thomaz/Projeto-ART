#include "TemperatureScaleManager.h"

// TemperatureScale

TemperatureScale::TemperatureScale(int id, String name, String simbol) {
  _id = id;
  _name = name;
  _simbol = simbol;
}

int TemperatureScale::getId()
{	
	return this->_id;
}

String TemperatureScale::getName()
{	
	return this->_name;
}

String TemperatureScale::getSimbol()
{	
	return this->_simbol;
}

// TemperatureScaleManager

TemperatureScaleManager::TemperatureScaleManager(DebugManager& debugManager, ConfigurationManager& configurationManager, MQQTManager& mqqtManager)
{ 
	this->_debugManager = &debugManager;
	this->_configurationManager = &configurationManager;
	this->_mqqtManager = &mqqtManager;
}

bool TemperatureScaleManager::begin()
{ 
	if(!this->_configurationManager->initialized()) return false;

	PubSubClient* mqqt = this->_mqqtManager->getMQQT();
 
	if(!mqqt->connected()) return false;	
	
	String hardwareId = this->_configurationManager->getHardwareSettings()->getHardwareId();      

	StaticJsonBuffer<100> JSONbuffer;
	JsonObject& root = JSONbuffer.createObject();
	root["hardwareId"] = hardwareId;

	int len = root.measureLength();
	char result[len + 1]; 
	root.printTo(result, sizeof(result));
	
	Serial.print("[MQQT] ");
	Serial.println("MQQT_TOPIC_PUB_GET_ALL_TEMPERATURE_SCALE_FOR_DEVICE");
	
	mqqt->publish(MQQT_TOPIC_PUB_GET_ALL_TEMPERATURE_SCALE_FOR_DEVICE, result);    
}