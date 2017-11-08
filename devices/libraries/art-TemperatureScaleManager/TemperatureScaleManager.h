#ifndef TemperatureScaleManager_h
#define TemperatureScaleManager_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "vector"
#include "DebugManager.h"
#include "ConfigurationManager.h"
#include "MQQTManager.h"

#define MQQT_TOPIC_PUB_GET_ALL_TEMPERATURE_SCALE_FOR_DEVICE   				"TemperatureScale.GetAllForDevice" 
#define MQQT_TOPIC_SUB_GET_ALL_TEMPERATURE_SCALE_FOR_DEVICE_COMPLETED   	"TemperatureScale.GetAllForDeviceCompleted"

class TemperatureScale {
  public:

    TemperatureScale(int id, String name, String symbol);

    int									getId();
	String								getName();
	String								getSymbol();
	
  private:
    
	int									_id;
	String								_name;
	String								_symbol;

    friend class TemperatureScaleManager;
};

class TemperatureScaleManager
{
  public:
  
    TemperatureScaleManager(DebugManager& debugManager, ConfigurationManager& configurationManager, MQQTManager& mqqtManager);
	
	bool								begin();
	void								update(String json);
	
  private:			
			
	DebugManager*          				_debugManager;	
	ConfigurationManager*          		_configurationManager;	
	MQQTManager* 						_mqqtManager;
	
	bool								_begin;
	bool								_beginning;
	
	std::vector<TemperatureScale> 		_scales;
	
};

#endif
