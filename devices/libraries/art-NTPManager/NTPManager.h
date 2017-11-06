#pragma once

#include "Arduino.h"
#include "DebugManager.h"
#include "ConfigurationManager.h"

#include <Udp.h>

#define SEVENZYYEARS 2208988800UL
#define NTP_PACKET_SIZE 48
#define NTP_DEFAULT_LOCAL_PORT 1337

class NTPManager {
  private:
    UDP*          _udp;
    bool          _udpSetup       = false;

    int           _port           = NTP_DEFAULT_LOCAL_PORT;
    int           _timeOffset     = 0;

    unsigned long _currentEpoc    = 0;      // In s
    unsigned long _lastUpdate     = 0;      // In ms

    byte          _packetBuffer[NTP_PACKET_SIZE];

    void          sendNTPPacket();

	void 		  (*_updateCallback)(bool, bool) = NULL;
	
	DebugManager*          	_debugManager;
	ConfigurationManager*  	_configurationManager;
	
  public:
  
	NTPManager(DebugManager& debugManager, ConfigurationManager& configurationManager);
    NTPManager(UDP& udp);
    NTPManager(UDP& udp, int timeOffset);

    /**
     * Starts the underlying UDP client with the default local port
     */
    void begin();

    /**
     * Starts the underlying UDP client with the specified local port
     */
    void begin(int port);

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
     * Changes the time offset. Useful for changing timezones dynamically
     */
    void setTimeOffset(int timeOffset);

    /**
     * Set the update interval to another frequency. E.g. useful when the
     * timeOffset should not be set in the constructor
     */
    void setUpdateInterval(int updateInterval);

    /**
     * @return time formatted like `hh:mm:ss`
     */
    String getFormattedTimeOld();
	
	String getFormattedTime();

    /**
     * @return time in seconds since Jan. 1, 1970
     */
    unsigned long getEpochTime();

    /**
     * Stops the underlying UDP client
     */
    void end();
	
	void setUpdateCallback( void (*func)(bool update, bool forceUpdate) );
};
