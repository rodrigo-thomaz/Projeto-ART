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
	
	String deviceId = this->_configurationManager->getDeviceSettings()->getDeviceId();      

	StaticJsonBuffer<TEMPERATURE_SCALE_GET_ALL_FOR_IOT_REQUEST_JSON_SIZE> JSONbuffer;
	JsonObject& root = JSONbuffer.createObject();
	root["deviceId"] = deviceId;

	int len = root.measureLength();
	char result[len + 1]; 
	root.printTo(result, sizeof(result));
	
	Serial.println("[TemperatureScaleManager::begin] beginning...]");
	
	this->_mqqtManager->publish(TOPIC_PUB_TEMPERATURE_SCALE_GET_ALL_FOR_IOT, result); 
	
	return true;
}

void TemperatureScaleManager::update(String json)
{ 
	this->_begin = true;
	this->_beginning = false;
				
	StaticJsonBuffer<TEMPERATURE_SCALE_GET_ALL_FOR_IOT_RESPONSE_JSON_SIZE> jsonBuffer;

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
				
	Serial.println("[TemperatureScaleManager:: begin] Initialized with success !");
}

TemperatureScale& TemperatureScaleManager::getById(int id)
{
	for (int i = 0; i < this->_scales.size(); ++i) {
		if (this->_scales[i].getId() == id) {
			return this->_scales[i];
		}
	}
}