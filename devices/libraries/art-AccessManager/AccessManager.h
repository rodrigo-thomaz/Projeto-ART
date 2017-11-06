#ifndef AccessManager_h
#define AccessManager_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "WiFiManager.h"
#include "ESP8266HTTPClient.h"

class BrokerSettings {
  public:

    BrokerSettings(String host, int port, String user, String pwd);

    String								getHost();
	int									getPort();
	String								getUser();
	String								getPwd();	
	
  private:
    
	String								_host;
	int									_port;
	String								_user;
	String								_pwd;

    friend class AccessManager;
};

class AccessManager
{
  public:
  
    AccessManager(DebugManager& debugManager, WiFiManager& wifiManager, String host, uint16_t port, String uri = "/");
		
	void								begin();
	
	void								autoInitialize();
	
	bool								initialized();
	
	BrokerSettings*						getBrokerSettings();
	
	String								getNTPServerName();
	int									getNTPServerPort();
	int									getNTPUpdateInterval();
	int									getNTPDisplayTimeOffset();
	
	String								getHardwareId();	
	String								getHardwareInApplicationId();	
	
	void								insertInApplication(String json);	
	void								deleteFromApplication();	
	
  private:			
			
	DebugManager*          				_debugManager;	
	WiFiManager*          				_wifiManager;

	bool 								_initialized = false;
	
	String 								_host;
	uint16_t 							_port;
	String 								_uri;
	
	int									_chipId;
	int									_flashChipId;
	String								_macAddress;

	BrokerSettings*						_brokerSettings;
	
	String								_ntpServerName;
	int									_ntpServerPort;
	int									_ntpUpdateInterval;
	int									_ntpDisplayTimeOffset;
	
	String								_hardwareId = "";
	String								_hardwareInApplicationId = "";
	
};

#endif
