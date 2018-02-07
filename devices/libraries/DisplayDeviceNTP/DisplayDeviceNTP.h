#ifndef DisplayDeviceNTP_h
#define DisplayDeviceNTP_h

#include "Arduino.h"
#include "DisplayManager.h"
#include "ESPDevice.h"

using namespace ART;

class DisplayDeviceNTP
{
public:
	DisplayDeviceNTP(DisplayManager& displayManager, ESPDevice& espDevice);
	~DisplayDeviceNTP();

private:

	DisplayManager * _displayManager;
	ESPDevice*          							_espDevice;

	void											printTime();
	void											printUpdate(bool on);

	void											updateCallback(bool update, bool forceUpdate);

	DEVICE_NTP_SET_UPDATE_CALLBACK_SIGNATURE		_updateCallback;

};

#endif