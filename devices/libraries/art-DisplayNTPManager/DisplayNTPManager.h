#ifndef DisplayNTPManager_h
#define DisplayNTPManager_h

#include "Arduino.h"
#include "DisplayManager.h"
#include "ESPDevice.h"

class DisplayNTPManager
{
public:
	DisplayNTPManager(DisplayManager& displayManager, ART::ESPDevice& espDevice);
	~DisplayNTPManager();
	
private:

	DisplayManager*       							_displayManager;	
	ART::ESPDevice*          						_espDevice;
						
	void											printTime();
	void											printUpdate(bool on);
							
	void											updateCallback(bool update, bool forceUpdate);	
	
	DEVICE_NTP_SET_UPDATE_CALLBACK_SIGNATURE		_updateCallback;
	
};

#endif