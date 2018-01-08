#ifndef NTPManager_h
#define NTPManager_h

#include "Arduino.h"
#include "ESPDevice.h"
#include "Udp.h"

#define SEVENZYYEARS 2208988800UL
#define NTP_PACKET_SIZE 48

#define NTP_MANAGER_SET_UPDATE_CALLBACK_SIGNATURE std::function<void(bool, bool)>

class NTPManager {
	
  public:
  
	NTPManager(ESPDevice& espDevice);
    NTPManager(UDP& udp);

    /**
     * Starts the underlying UDP client with the default local port
     */
    bool begin();
	
    /**
     * This should be called in the main loop of your application. By default an update from the NTP Server is only
     * made every 60 seconds. This can be configured in the NTPManager constructor.
     *
     * @return true on success, false on failure
     */
    bool update();

    /**
     * This will force the update from the NTP Server.
     *
     * @return true on success, false on failure
     */
    bool forceUpdate();

    int getDay();
    int getHours();
    int getMinutes();
    int getSeconds();   

    /**
     * @return time formatted like `hh:mm:ss`
     */
    String getFormattedTimeOld();
	
	String getFormattedTime();

    /**
     * @return time in seconds since Jan. 1, 1970
     */
    unsigned long getEpochTime();
	unsigned long getEpochTimeUTC();

    /**
     * Stops the underlying UDP client
     */
    void end();
	
	NTPManager& setUpdateCallback(NTP_MANAGER_SET_UPDATE_CALLBACK_SIGNATURE callback);
	
private:
  
    UDP*          									_udp;
    bool          									_udpSetup       = false;
									
    unsigned long 									_currentEpoc    = 0;      // In s
    unsigned long 									_lastUpdate     = 0;      // In ms
									
    byte          									_packetBuffer[NTP_PACKET_SIZE];
									
    void          									sendNTPPacket();

	NTP_MANAGER_SET_UPDATE_CALLBACK_SIGNATURE		_updateCallback;
	
	bool 											_initialized = false;
				
	ESPDevice*  									_espDevice;
	
};

#endif