#ifndef AccessManager_h
#define AccessManager_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "WiFiManager.h"
#include "ESP8266HTTPClient.h"

class AccessManager
{
  public:
  
    AccessManager(DebugManager& debugManager, WiFiManager& wifiManager, String host, uint16_t port, String uri = "/");
		
	void								begin();
	
	void								autoInitialize();
	
	bool								initialized();
	
	String								getBrokerHost();
	int									getBrokerPort();
	String								getBrokerUser();
	String								getBrokerPwd();	
	
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
	
	String								_brokerHost;
	int									_brokerPort;
	String								_brokerUser;
	String								_brokerPwd;
	
	String								_ntpServerName;
	int									_ntpServerPort;
	int									_ntpUpdateInterval;
	int									_ntpDisplayTimeOffset;
	
	String								_hardwareId = "";
	String								_hardwareInApplicationId = "";
	
};

#endif
