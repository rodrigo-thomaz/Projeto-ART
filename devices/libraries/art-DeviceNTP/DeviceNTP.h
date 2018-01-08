#ifndef DeviceNTP_h
#define DeviceNTP_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "RemoteDebug.h"
#include "Udp.h"
#include "WiFiUdp.h"

#define SEVENZYYEARS 2208988800UL
#define NTP_PACKET_SIZE 48

#define DEVICE_NTP_SET_UPDATE_CALLBACK_SIGNATURE std::function<void(bool, bool)>

class ESPDevice;

class DeviceNTP
{

public:

	DeviceNTP(ESPDevice* espDevice);
	~DeviceNTP();
	
	void											load(JsonObject& jsonObject);
	
	char*											getHost();
	int												getPort();

	int												getUtcTimeOffsetInSecond();
	void											setUtcTimeOffsetInSecond(String json);
	
	int												getUpdateIntervalInMilliSecond();	
	void											setUpdateIntervalInMilliSecond(String json);		
		
	static void createDeviceNTP(DeviceNTP* (&deviceNTP), ESPDevice* espDevice)
    {
		deviceNTP = new DeviceNTP(espDevice);
    }
	
private:	

	ESPDevice*          							_espDevice;	
	
	char*											_host;
	int												_port;
	int												_utcTimeOffsetInSecond;
	int												_updateIntervalInMilliSecond;	
	
	UDP*          									_udp;
    bool          									_udpSetup       = false;
									
    unsigned long 									_currentEpoc    = 0;      // In s
    unsigned long 									_lastUpdate     = 0;      // In ms
									
    byte          									_packetBuffer[NTP_PACKET_SIZE];
									
    void          									sendNTPPacket();

	DEVICE_NTP_SET_UPDATE_CALLBACK_SIGNATURE		_updateCallback;
	
	bool 											_initialized = false;
};

#endif