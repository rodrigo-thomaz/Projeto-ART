#ifndef ConfigurationManager_h
#define ConfigurationManager_h

#include "Arduino.h"

#include "ESPDevice.h"

class ConfigurationManager
{
  public:
  
    ConfigurationManager(ESPDevice& espDevice);
	
	bool								loaded();
	
	ESPDevice*							getESPDevice();	
	
  private:			
			
	ESPDevice*							_espDevice;		
};

#endif
