#ifndef ConfigurationManager_h
#define ConfigurationManager_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "WiFiManager.h"
#include "ESP8266HTTPClient.h"

class BrokerSettings {
  public:

    BrokerSettings(String host, int port, String user, String pwd, String clientId, String applicationTopic, String deviceTopic);

    String								getHost();
	int									getPort();
	String								getUser();
	String								getPwd();	
	String								getClientId();	
	
	String								getApplicationTopic();	
	void								setApplicationTopic(String value);	
	
	String								getDeviceTopic();	
	
  private:
    
	String								_host;
	int									_port;
	String								_user;
	String								_pwd;
	String								_clientId;
	String								_applicationTopic;
	String								_deviceTopic;

    friend class ConfigurationManager;
};

class NTPSettings {
  public:

    NTPSettings(String host, int port, int updateInterval, int timeOffset);

    String								getHost();
	int									getPort();
	int									getUpdateInterval();
	int									getTimeOffset();
	
  private:
    
	String								_host;
	int									_port;
	int									_updateInterval;
	int									_timeOffset;

    friend class ConfigurationManager;
};

class DeviceSettings {
  public:

    DeviceSettings(String deviceId, String deviceInApplicationId);

    String								getDeviceId();	
	
	String								getDeviceInApplicationId();	
	void								setDeviceInApplicationId(String value);	
	
  private:
    
	String								_deviceId;
	String								_deviceInApplicationId;

    friend class ConfigurationManager;
};

class ConfigurationManager
{
  public:
  
    ConfigurationManager(DebugManager& debugManager, WiFiManager& wifiManager, String host, uint16_t port, String uri = "/");
		
	void								begin();
	
	void								autoInitialize();
	
	bool								initialized();
	
	BrokerSettings*						getBrokerSettings();
	NTPSettings*						getNTPSettings();
	DeviceSettings*						getDeviceSettings();
	
	int									getPublishMessageInterval();

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
	NTPSettings*						_ntpSettings;	
	DeviceSettings*						_deviceSettings;	
	
	int									_publishMessageInterval;
	
};

#endif
