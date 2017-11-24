#include "ConfigurationManager.h"

// DeviceMQ

DeviceMQ::DeviceMQ(String host, int port, String user, String password, String clientId, String applicationTopic, String deviceTopic) {
  _host = host;
  _port = port;
  _user = user;
  _password = password;
  _clientId = clientId;
  _applicationTopic = applicationTopic;
  _deviceTopic = deviceTopic;
}

String DeviceMQ::getHost()
{	
	return this->_host;
}

int DeviceMQ::getPort()
{	
	return this->_port;
}

String DeviceMQ::getUser()
{	
	return this->_user;
}

String DeviceMQ::getPassword()
{	
	return this->_password;
}

String DeviceMQ::getClientId()
{	
	return this->_clientId;
}

String DeviceMQ::getApplicationTopic()
{	
	return this->_applicationTopic;
}

void DeviceMQ::setApplicationTopic(String value)
{	
	this->_applicationTopic = value;
}

String DeviceMQ::getDeviceTopic()
{	
	return this->_deviceTopic;
}

// DeviceNTP

DeviceNTP::DeviceNTP(String host, int port, int timeOffsetInSecond, int updateIntervalInMilliSecond) {
  _host = host;
  _port = port;  
  _timeOffsetInSecond = timeOffsetInSecond;
  _updateIntervalInMilliSecond = updateIntervalInMilliSecond;
}

String DeviceNTP::getHost()
{	
	return this->_host;
}

int DeviceNTP::getPort()
{	
	return this->_port;
}

int DeviceNTP::getTimeOffsetInSecond()
{	
	return this->_timeOffsetInSecond;
}

void DeviceNTP::setTimeOffsetInSecond(int value)
{	
	this->_timeOffsetInSecond = value;
}

int DeviceNTP::getUpdateIntervalInMilliSecond()
{	
	return this->_updateIntervalInMilliSecond;
}

void DeviceNTP::setUpdateIntervalInMilliSecond(int value)
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
	
	this->_deviceMQ = NULL;
	this->_deviceNTP = NULL;
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

DeviceMQ* ConfigurationManager::getDeviceMQ()
{	
	return this->_deviceMQ;
}

DeviceNTP* ConfigurationManager::getDeviceNTP()
{	
	return this->_deviceNTP;
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
			JsonObject& deviceNTP = jsonObjectResponse["deviceNTP"];
			
			this->_deviceMQ = new DeviceMQ(
				deviceMQ["host"], 
				deviceMQ["port"], 
				deviceMQ["user"], 
				deviceMQ["password"],
				deviceMQ["clientId"],
				deviceMQ["applicationTopic"],
				deviceMQ["deviceTopic"]);
			
			this->_deviceNTP = new DeviceNTP(
				deviceNTP["host"], 
				deviceNTP["port"], 
				deviceNTP["timeOffsetInSecond"],
				deviceNTP["updateIntervalInMilliSecond"]);			
			
			this->_deviceSettings = new DeviceSettings(
				jsonObjectResponse["deviceId"], 
				jsonObjectResponse["deviceInApplicationId"]);					
			
			int publishMessageInterval = jsonObjectResponse["publishMessageInterval"];	
			this->_publishMessageInterval = publishMessageInterval;
			
			Serial.println("ConfigurationManager initialized with success !");
			
			Serial.print("DeviceMQ Host: ");
			Serial.println(this->_deviceMQ->getHost());
			Serial.print("DeviceMQ Port: ");
			Serial.println(this->_deviceMQ->getPort());
			Serial.print("DeviceMQ User: ");
			Serial.println(this->_deviceMQ->getUser());
			Serial.print("DeviceMQ Password: ");
			Serial.println(this->_deviceMQ->getPassword());
			Serial.print("DeviceMQ ClientId: ");
			Serial.println(this->_deviceMQ->getClientId());
			Serial.print("DeviceMQ Application Topic: ");
			Serial.println(this->_deviceMQ->getApplicationTopic());
			Serial.print("DeviceMQ Device Topic: ");
			Serial.println(this->_deviceMQ->getDeviceTopic());
			
			Serial.print("DeviceNTP Host: ");
			Serial.println(this->_deviceNTP->getHost());
			Serial.print("DeviceNTP Port: ");
			Serial.println(this->_deviceNTP->getPort());			
			Serial.print("DeviceNTP Time Offset: ");
			Serial.println(this->_deviceNTP->getTimeOffsetInSecond());
			Serial.print("DeviceNTP Update Interval: ");
			Serial.println(this->_deviceNTP->getUpdateIntervalInMilliSecond());
			
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
	this->_deviceMQ->setApplicationTopic(brokerApplicationTopic);
	
	Serial.println("[ConfigurationManager::insertInApplication] ");
	Serial.print("deviceInApplicationId: ");
	Serial.println(deviceInApplicationId);
	Serial.print("brokerApplicationTopic: ");
	Serial.println(brokerApplicationTopic);
}

void ConfigurationManager::deleteFromApplication()
{	
	this->_deviceSettings->setDeviceInApplicationId("");	
	this->_deviceMQ->setApplicationTopic("");
	
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
	
	this->_deviceNTP->setTimeOffsetInSecond(timeOffsetInSecond);
	
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
	
	this->_deviceNTP->setUpdateIntervalInMilliSecond(updateIntervalInMilliSecond);
	
	Serial.println("[ConfigurationManager::setUpdateIntervalInMilliSecond] ");
	Serial.print("updateIntervalInMilliSecond: ");
	Serial.println(updateIntervalInMilliSecond);
}
	