#ifndef DisplayNTPManager_h
#define DisplayNTPManager_h

#include "Arduino.h"
#include "DisplayManager.h"
#include "NTPManager.h"

class DisplayNTPManager
{
public:
	DisplayNTPManager(DisplayManager& displayManager, NTPManager& ntpManager);
	~DisplayNTPManager();
	
private:

	DisplayManager*       							_displayManager;	
	NTPManager*          							_ntpManager;
						
	void											printTime();
	void											printUpdate(bool on);
							
	void											updateCallback(bool update, bool forceUpdate);	
	
	NTP_MANAGER_SET_UPDATE_CALLBACK_SIGNATURE		_updateCallback;
	
};

#endif