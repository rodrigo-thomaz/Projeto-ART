#ifndef TemperatureScaleManager_h
#define TemperatureScaleManager_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "vector"
#include "DebugManager.h"
#include "ConfigurationManager.h"
#include "MQQTManager.h"

#define TEMPERATURE_SCALE_GET_ALL_FOR_IOT_REQUEST_JSON_SIZE 				100
#define TEMPERATURE_SCALE_GET_ALL_FOR_IOT_RESPONSE_JSON_SIZE 				1000

#define TOPIC_PUB_TEMPERATURE_SCALE_GET_ALL_FOR_IOT		   					"TemperatureScale.GetAllForIoT" 
#define TOPIC_SUB_TEMPERATURE_SCALE_GET_ALL_FOR_IOT_COMPLETED		   		"TemperatureScale.GetAllForIoTCompleted"

class TemperatureScale {
  public:

    TemperatureScale(int id, String name, String symbol);

    int																		getId();
	String																	getName();
	String																	getSymbol();
										
  private:									
										
	int																		_id;
	String																	_name;
	String																	_symbol;

    friend class TemperatureScaleManager;
};

class TemperatureScaleManager
{
  public:
  
    TemperatureScaleManager(DebugManager& debugManager, ConfigurationManager& configurationManager, MQQTManager& mqqtManager);
	
	bool																	begin();
	void																	update(String json);
	TemperatureScale&														getById(int id);
										
  private:												
												
	DebugManager*          													_debugManager;	
	ConfigurationManager*          											_configurationManager;	
	MQQTManager* 															_mqqtManager;
										
	bool																	_begin;
	bool																	_beginning;
										
	std::vector<TemperatureScale> 											_scales;
	
};

#endif
