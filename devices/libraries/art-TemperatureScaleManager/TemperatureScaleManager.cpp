#include "TemperatureScaleManager.h"

// TemperatureScale

TemperatureScale::TemperatureScale(int id, String name, String symbol) {
  _id = id;
  _name = name;
  _symbol = symbol;
}

int TemperatureScale::getId()
{	
	return this->_id;
}

String TemperatureScale::getName()
{	
	return this->_name;
}

String TemperatureScale::getSymbol()
{	
	return this->_symbol;
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
	if(this->_begin) return true;	

	if(!this->_configurationManager->initialized()) return false;	
	
	if(this->_beginning) return false;	
	
	PubSubClient* mqqt = this->_mqqtManager->getMQQT();
 
	if(!mqqt->connected()) return false;	
	
	// Begin
	
	this->_beginning = true;
	
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

void TemperatureScaleManager::update(String json)
{ 
	this->_begin = true;
	this->_beginning = false;
				
	StaticJsonBuffer<1000> jsonBuffer;

	JsonArray& jsonArray = jsonBuffer.parseArray(json);
	
	if (!jsonArray.success()) {
		Serial.print("[TemperatureScaleManager::update] parse failed: ");
		Serial.println(json);
		return;
	}		

	this->_scales.clear();
	
	for(JsonArray::iterator it=jsonArray.begin(); it!=jsonArray.end(); ++it) 
	{
		JsonObject& root = it->as<JsonObject>();
		
		int id = int(root["id"]);
		String name = root["name"];			
		String symbol = root["symbol"];			
				
		this->_scales.push_back(TemperatureScale(
				id, 
				name, 				
				symbol));
	}
				
	Serial.println("****************************************** TOPIC_SUB_GET_IN_APPLICATION_FOR_DEVICE_COMPLETED ******************** !!!!!!!!!!!!!!!!!!!!!!!!!");
}