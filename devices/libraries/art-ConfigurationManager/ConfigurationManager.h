#ifndef ConfigurationManager_h
#define ConfigurationManager_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "DebugManager.h"
#include "WiFiManager.h"
#include "ESP8266HTTPClient.h"

class DeviceMQ {
  public:

    DeviceMQ(String host, int port, String user, String password, String clientId, String applicationTopic, String deviceTopic);

    String								getHost();
	int									getPort();
	String								getUser();
	String								getPassword();	
	String								getClientId();	
	
	String								getApplicationTopic();			
	String								getDeviceTopic();	
	
  private:
    
	String								_host;
	int									_port;
	String								_user;
	String								_password;
	String								_clientId;
	String								_applicationTopic;
	String								_deviceTopic;

	void								setApplicationTopic(String value);	
	
    friend class ConfigurationManager;
};

class NTPSettings {
  public:

    NTPSettings(String host, int port, int timeOffsetInSecond, int updateIntervalInMilliSecond);

    String								getHost();
	int									getPort();
	int									getTimeOffsetInSecond();
	int									getUpdateIntervalInMilliSecond();	
	
  private:
    
	String								_host;
	int									_port;
	int									_timeOffsetInSecond;
	int									_updateIntervalInMilliSecond;	

	void								setTimeOffsetInSecond(int value);
	void								setUpdateIntervalInMilliSecond(int value);	
	
    friend class ConfigurationManager;
};

class DeviceSettings {
  public:

    DeviceSettings(String deviceId, String deviceInApplicationId);

    String								getDeviceId();	
	
	String								getDeviceInApplicationId();	
		
  private:
    
	String								_deviceId;
	String								_deviceInApplicationId;

	void								setDeviceInApplicationId(String value);	
	
    friend class ConfigurationManager;
};

class ConfigurationManager
{
  public:
  
    ConfigurationManager(DebugManager& debugManager, WiFiManager& wifiManager, String host, uint16_t port, String uri = "/");
		
	void								begin();
	
	void								autoInitialize();
	
	bool								initialized();
	
	DeviceMQ*							getDeviceMQ();
	NTPSettings*						getNTPSettings();
	DeviceSettings*						getDeviceSettings();
	
	int									getPublishMessageInterval();

	void								insertInApplication(String json);
	void								deleteFromApplication();		
	
	void								setTimeOffsetInSecond(String json);
	void								setUpdateIntervalInMilliSecond(String json);
	
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

	DeviceMQ*							_deviceMQ;
	NTPSettings*						_ntpSettings;	
	DeviceSettings*						_deviceSettings;	
	
	int									_publishMessageInterval;
	
};

#endif
