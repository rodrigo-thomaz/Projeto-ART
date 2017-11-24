#include "ConfigurationManager.h"

// BrokerSettings

BrokerSettings::BrokerSettings(String host, int port, String user, String pwd, String clientId, String applicationTopic, String deviceTopic) {
  _host = host;
  _port = port;
  _user = user;
  _pwd = pwd;
  _clientId = clientId;
  _applicationTopic = applicationTopic;
  _deviceTopic = deviceTopic;
}

String BrokerSettings::getHost()
{	
	return this->_host;
}

int BrokerSettings::getPort()
{	
	return this->_port;
}

String BrokerSettings::getUser()
{	
	return this->_user;
}

String BrokerSettings::getPwd()
{	
	return this->_pwd;
}

String BrokerSettings::getClientId()
{	
	return this->_clientId;
}

String BrokerSettings::getApplicationTopic()
{	
	return this->_applicationTopic;
}

void BrokerSettings::setApplicationTopic(String value)
{	
	this->_applicationTopic = value;
}

String BrokerSettings::getDeviceTopic()
{	
	return this->_deviceTopic;
}

// NTPSettings

NTPSettings::NTPSettings(String host, int port, int timeOffsetInSecond, int updateIntervalInMilliSecond) {
  _host = host;
  _port = port;  
  _timeOffsetInSecond = timeOffsetInSecond;
  _updateIntervalInMilliSecond = updateIntervalInMilliSecond;
}

String NTPSettings::getHost()
{	
	return this->_host;
}

int NTPSettings::getPort()
{	
	return this->_port;
}

int NTPSettings::getTimeOffsetInSecond()
{	
	return this->_timeOffsetInSecond;
}

void NTPSettings::setTimeOffsetInSecond(int value)
{	
	this->_timeOffsetInSecond = value;
}

int NTPSettings::getUpdateIntervalInMilliSecond()
{	
	return this->_updateIntervalInMilliSecond;
}

void NTPSettings::setUpdateIntervalInMilliSecond(int value)
{	
	this->_updateIntervalInMilliSecond = value;
}

// DeviceSettings

DeviceSettings::DeviceSettings(String deviceId, String deviceInApplicationId) {	
  _deviceId = deviceId;
  _deviceInApplicationId = deviceInApplicationId == "null" ? "" : deviceInApplicationId;				
}

String DeviceSettings::getDeviceId()
{	
	return this->_deviceId;
}

String DeviceSettings::getDeviceInApplicationId()
{	
	return this->_deviceInApplicationId;
}

void DeviceSettings::setDeviceInApplicationId(String value)
{	
	this->_deviceInApplicationId = value;
}

// ConfigurationManager

ConfigurationManager::ConfigurationManager(DebugManager& debugManager, WiFiManager& wifiManager, String host, uint16_t port, String uri)
{ 
	this->_debugManager = &debugManager;
	this->_wifiManager = &wifiManager;
	
	this->_host = host;
	this->_port = port;
	this->_uri = uri;
	
	this->_brokerSettings = NULL;
	this->_ntpSettings = NULL;
	this->_deviceSettings = NULL;
}

void ConfigurationManager::begin()
{	
	this->_chipId = ESP.getChipId();
	this->_flashChipId = ESP.getFlashChipId();
	this->_macAddress = WiFi.macAddress();
	
	this->autoInitialize();
}

bool ConfigurationManager::initialized()
{	
	return this->_initialized;
}

BrokerSettings* ConfigurationManager::getBrokerSettings()
{	
	return this->_brokerSettings;
}

NTPSettings* ConfigurationManager::getNTPSettings()
{	
	return this->_ntpSettings;
}

DeviceSettings* ConfigurationManager::getDeviceSettings()
{	
	return this->_deviceSettings;
}

int ConfigurationManager::getPublishMessageInterval()
{	
	return this->_publishMessageInterval;
}

void ConfigurationManager::autoInitialize()
{	
	if(!this->_wifiManager->isConnected() || this->_initialized){
		return;
	}
	
	HTTPClient http; 

	String apiUri = this->_uri + "api/espDevice/getConfigurations";
	http.begin(this->_host, this->_port, apiUri); 

	StaticJsonBuffer<200> jsonBufferRequest;
	JsonObject& jsonObjectRequest = jsonBufferRequest.createObject();
	
	jsonObjectRequest["chipId"] = this->_chipId;
	jsonObjectRequest["flashChipId"] = this->_flashChipId;
	jsonObjectRequest["macAddress"] = this->_macAddress;

	int lenRequest = jsonObjectRequest.measureLength();
	char dataRequest[lenRequest + 1];
	jsonObjectRequest.printTo(dataRequest, sizeof(dataRequest));

	Serial.print("[ConfigurationManager] getConfigurations request: ");
	jsonObjectRequest.printTo(Serial);
	Serial.println();
	
	// start connection and send HTTP header
	http.addHeader("access-control-allow-credentials", "true");
	http.addHeader("content-length", String(lenRequest));
	http.addHeader("Content-Type", "application/json");

	int httpCode = http.POST(dataRequest);

	// httpCode will be negative on error
	if(httpCode > 0) {
		// file found at server
		if(httpCode == HTTP_CODE_OK) {
			
			String payload = http.getString();
			
			DynamicJsonBuffer jsonBufferResponse;
			JsonObject& jsonObjectResponse = jsonBufferResponse.parseObject(payload);			
			
			JsonObject& deviceMQ = jsonObjectResponse["deviceMQ"];
			
			this->_brokerSettings = new BrokerSettings(
				deviceMQ["host"], 
				deviceMQ["port"], 
				deviceMQ["user"], 
				deviceMQ["password"],
				deviceMQ["clientId"],
				deviceMQ["applicationTopic"],
				deviceMQ["deviceTopic"]);
			
			this->_ntpSettings = new NTPSettings(
				jsonObjectResponse["ntpHost"], 
				jsonObjectResponse["ntpPort"], 
				jsonObjectResponse["timeOffset"],
				jsonObjectResponse["ntpUpdateInterval"]);			
			
			this->_deviceSettings = new DeviceSettings(
				jsonObjectResponse["deviceId"], 
				jsonObjectResponse["deviceInApplicationId"]);					
			
			int publishMessageInterval = jsonObjectResponse["publishMessageInterval"];	
			this->_publishMessageInterval = publishMessageInterval;
			
			Serial.println("ConfigurationManager initialized with success !");
			
			Serial.print("Broker Host: ");
			Serial.println(this->_brokerSettings->getHost());
			Serial.print("Broker Port: ");
			Serial.println(this->_brokerSettings->getPort());
			Serial.print("Broker User: ");
			Serial.println(this->_brokerSettings->getUser());
			Serial.print("Broker Pwd: ");
			Serial.println(this->_brokerSettings->getPwd());
			Serial.print("Broker ClientId: ");
			Serial.println(this->_brokerSettings->getClientId());
			Serial.print("Broker Application Topic: ");
			Serial.println(this->_brokerSettings->getApplicationTopic());
			Serial.print("Broker Device Topic: ");
			Serial.println(this->_brokerSettings->getDeviceTopic());
			
			Serial.print("NTP Host: ");
			Serial.println(this->_ntpSettings->getHost());
			Serial.print("NTP Port: ");
			Serial.println(this->_ntpSettings->getPort());			
			Serial.print("NTP Time Offset: ");
			Serial.println(this->_ntpSettings->getTimeOffsetInSecond());
			Serial.print("NTP Update Interval: ");
			Serial.println(this->_ntpSettings->getUpdateIntervalInMilliSecond());
			
			Serial.print("DeviceId: ");
			Serial.println(this->_deviceSettings->getDeviceId());
			Serial.print("DeviceInApplicationId: ");
			Serial.println(this->_deviceSettings->getDeviceInApplicationId());
			
			Serial.print("PublishMessageInterval: ");
			Serial.println(this->_publishMessageInterval);
			
			this->_initialized = true;
		}
	} else {
		Serial.print("[HTTP] GET... failed, error: ");
		Serial.println(http.errorToString(httpCode).c_str());		
	}
  
  http.end();
}

void ConfigurationManager::insertInApplication(String json)
{	
	StaticJsonBuffer<300> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		Serial.print("[ConfigurationManager::insertInApplication] parse failed: ");
		Serial.println(json);
		return;
	}	

	String deviceInApplicationId = root["deviceInApplicationId"];
	String brokerApplicationTopic = root["brokerApplicationTopic"];	
	
	this->_deviceSettings->setDeviceInApplicationId(deviceInApplicationId);
	this->_brokerSettings->setApplicationTopic(brokerApplicationTopic);
	
	Serial.println("[ConfigurationManager::insertInApplication] ");
	Serial.print("deviceInApplicationId: ");
	Serial.println(deviceInApplicationId);
	Serial.print("brokerApplicationTopic: ");
	Serial.println(brokerApplicationTopic);
}

void ConfigurationManager::deleteFromApplication()
{	
	this->_deviceSettings->setDeviceInApplicationId("");	
	this->_brokerSettings->setApplicationTopic("");
	
	Serial.println("[ConfigurationManager::deleteFromApplication] delete from Application with success !");
}

void ConfigurationManager::setTimeOffsetInSecond(String json)
{
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		Serial.print("[ConfigurationManager::setTimeOffsetInSecond] parse failed: ");
		Serial.println(json);
		return;
	}	

	int timeOffsetInSecond = root["timeOffsetInSecond"];
	
	this->_ntpSettings->setTimeOffsetInSecond(timeOffsetInSecond);
	
	Serial.println("[ConfigurationManager::setTimeOffsetInSecond] ");
	Serial.print("timeOffsetInSecond: ");
	Serial.println(timeOffsetInSecond);
}

void ConfigurationManager::setUpdateIntervalInMilliSecond(String json)
{
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		Serial.print("[ConfigurationManager::setUpdateIntervalInMilliSecond] parse failed: ");
		Serial.println(json);
		return;
	}	

	int updateIntervalInMilliSecond = root["updateIntervalInMilliSecond"];
	
	this->_ntpSettings->setUpdateIntervalInMilliSecond(updateIntervalInMilliSecond);
	
	Serial.println("[ConfigurationManager::setUpdateIntervalInMilliSecond] ");
	Serial.print("updateIntervalInMilliSecond: ");
	Serial.println(updateIntervalInMilliSecond);
}
	