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

	StaticJsonBuffer<TEMPERATURE_SCALE_GET_ALL_FOR_DEVICE_REQUEST_JSON_SIZE> JSONbuffer;
	JsonObject& root = JSONbuffer.createObject();
	root["hardwareId"] = hardwareId;

	int len = root.measureLength();
	char result[len + 1]; 
	root.printTo(result, sizeof(result));
	
	Serial.println("[TemperatureScaleManager::begin] beginning...]");
	
	mqqt->publish(TEMPERATURE_SCALE_GET_ALL_FOR_DEVICE_MQQT_TOPIC_PUB, result);    
}

void TemperatureScaleManager::update(String json)
{ 
	this->_begin = true;
	this->_beginning = false;
				
	StaticJsonBuffer<TEMPERATURE_SCALE_GET_ALL_FOR_DEVICE_RESPONSE_JSON_SIZE> jsonBuffer;

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
				
	Serial.println("[TemperatureScaleManager:: begin]");
}