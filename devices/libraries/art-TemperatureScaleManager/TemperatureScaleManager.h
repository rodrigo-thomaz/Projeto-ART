#ifndef TemperatureScaleManager_h
#define TemperatureScaleManager_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "ConfigurationManager.h"
#include "MQQTManager.h"

class TemperatureScale {
  public:

    TemperatureScale(int id, String name, String simbol);

    int									getId();
	String								getName();
	String								getSimbol();
	
  private:
    
	int									_id;
	String								_name;
	String								_simbol;

    friend class TemperatureScaleManager;
};

class TemperatureScaleManager
{
  public:
  
    TemperatureScaleManager(DebugManager& debugManager, ConfigurationManager& configurationManager, MQQTManager& mqqtManager);
	
	bool								begin();
	
  private:			
			
	DebugManager*          				_debugManager;	
	ConfigurationManager*          		_configurationManager;	
	MQQTManager* 						_mqqtManager;
	
	bool								_initialized;
	
};

#endif
