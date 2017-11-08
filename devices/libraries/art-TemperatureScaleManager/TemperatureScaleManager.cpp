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

	String hardwareInApplicationId = this->_configurationManager->getHardwareSettings()->getHardwareInApplicationId();      

	StaticJsonBuffer<100> JSONbuffer;
	JsonObject& root = JSONbuffer.createObject();
	root["hardwareInApplicationId"] = hardwareInApplicationId;

	int len = root.measureLength();
	char result[len + 1]; 
	root.printTo(result, sizeof(result));
	Serial.print("[MQQT] ");
	Serial.println("TOPIC_PUB_GET_ALL_TEMPERATURE_SCALE_FOR_DEVICE");
	//MQTT.publish(TOPIC_PUB_GET_ALL_TEMPERATURE_SCALE_FOR_DEVICE, result);    
}