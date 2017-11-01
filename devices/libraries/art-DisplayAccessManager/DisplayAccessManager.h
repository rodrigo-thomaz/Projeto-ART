#ifndef DisplayAccessManager_h
#define DisplayAccessManager_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "DisplayManager.h"

#define MESSAGE_INTERVAL 1000

class DisplayAccessManager
{
  public:
  
    DisplayAccessManager(DebugManager& debugManager, DisplayManager& displayManager);
	
	void								updatePin(String payloadContract);
	void								loop();
				
  private:			
			
	DebugManager*          				_debugManager;	
	DisplayManager*       				_displayManager;

	String 								_pin;
	int 								_nextFireTimeInSeconds = -1;	
		
	uint64_t 							_messageTimestamp = 0;
};

#endif
