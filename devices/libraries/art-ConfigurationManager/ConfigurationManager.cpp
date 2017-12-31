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

DeviceNTP::DeviceNTP(String host, int port, int utcTimeOffsetInSecond, int updateIntervalInMilliSecond) {
  _host = host;
  _port = port;  
  _utcTimeOffsetInSecond = utcTimeOffsetInSecond;
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

int DeviceNTP::getUtcTimeOffsetInSecond()
{	
	return this->_utcTimeOffsetInSecond;
}

void DeviceNTP::setUtcTimeOffsetInSecond(int value)
{	
	this->_utcTimeOffsetInSecond = value;
}

int DeviceNTP::getUpdateIntervalInMilliSecond()
{	
	return this->_updateIntervalInMilliSecond;
}

void DeviceNTP::setUpdateIntervalInMilliSecond(int value)
{	
	this->_updateIntervalInMilliSecond = value;
}

// DeviceInApplication

DeviceInApplication::DeviceInApplication(String applicationId) {	
  _applicationId = applicationId == "null" ? "" : applicationId;				
}

String DeviceInApplication::getApplicationId()
{	
	return this->_applicationId;
}

void DeviceInApplication::setApplicationId(String value)
{	
	this->_applicationId = value;
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
	this->_deviceInApplication = NULL;
	
	this->_espDevice = NULL;
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
	return _initialized;
}

DeviceMQ* ConfigurationManager::getDeviceMQ()
{	
	return _deviceMQ;
}

DeviceNTP* ConfigurationManager::getDeviceNTP()
{	
	return _deviceNTP;
}

DeviceInApplication* ConfigurationManager::getDeviceInApplication()
{	
	return _deviceInApplication;
}

ESPDevice* ConfigurationManager::getESPDevice()
{	
	return _espDevice;
}

void ConfigurationManager::autoInitialize()
{	
	if(!_wifiManager->isConnected() || _initialized){
		return;
	}
	
	HTTPClient http; 

	String apiUri = _uri + "api/espDevice/getConfigurations";
	http.begin(_host, _port, apiUri); 

	StaticJsonBuffer<200> jsonBufferRequest;
	JsonObject& jsonObjectRequest = jsonBufferRequest.createObject();
	
	jsonObjectRequest["chipId"] = _chipId;
	jsonObjectRequest["flashChipId"] = _flashChipId;
	jsonObjectRequest["macAddress"] = _macAddress;

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
			JsonObject& deviceSensors = jsonObjectResponse["deviceSensors"];
			
			//////////////////////
			
			char* espDeviceId = strdup(jsonObjectResponse["deviceId"]);
			short deviceDatasheetId = jsonObjectResponse["deviceDatasheetId"];
			int chipId = ESP.getChipId();
			int flashChipId = ESP.getFlashChipId();
			char* stationMacAddress = strdup(WiFi.macAddress().c_str());
			char* softAPMacAddress = strdup(WiFi.softAPmacAddress().c_str());
			long chipSize = ESP.getFlashChipSize();
			char* label = strdup(jsonObjectResponse["label"]);
			
			_espDevice = new ESPDevice(espDeviceId, deviceDatasheetId, chipId, flashChipId, stationMacAddress, softAPMacAddress, chipSize, label, deviceSensors);
			
			//////////////////////
			
			
			
			
			_deviceMQ = new DeviceMQ(
				deviceMQ["host"], 
				deviceMQ["port"], 
				deviceMQ["user"], 
				deviceMQ["password"],
				deviceMQ["clientId"],
				deviceMQ["applicationTopic"],
				deviceMQ["deviceTopic"]);
			
			_deviceNTP = new DeviceNTP(
				deviceNTP["host"], 
				deviceNTP["port"], 
				deviceNTP["utcTimeOffsetInSecond"],
				deviceNTP["updateIntervalInMilliSecond"]);			
			
			_deviceInApplication = new DeviceInApplication(jsonObjectResponse["applicationId"].as<String>());					
			
			Serial.println("ConfigurationManager initialized with success !");
			
			Serial.print("DeviceId: ");
			Serial.println(_espDevice->getDeviceId());
			Serial.print("DeviceDatasheetId: ");
			Serial.println(_espDevice->getDeviceDatasheetId());
			
			Serial.print("DeviceMQ Host: ");
			Serial.println(_deviceMQ->getHost());
			Serial.print("DeviceMQ Port: ");
			Serial.println(_deviceMQ->getPort());
			Serial.print("DeviceMQ User: ");
			Serial.println(_deviceMQ->getUser());
			Serial.print("DeviceMQ Password: ");
			Serial.println(_deviceMQ->getPassword());
			Serial.print("DeviceMQ ClientId: ");
			Serial.println(_deviceMQ->getClientId());
			Serial.print("DeviceMQ Application Topic: ");
			Serial.println(_deviceMQ->getApplicationTopic());
			Serial.print("DeviceMQ Device Topic: ");
			Serial.println(_deviceMQ->getDeviceTopic());
			
			Serial.print("DeviceNTP Host: ");
			Serial.println(_deviceNTP->getHost());
			Serial.print("DeviceNTP Port: ");
			Serial.println(_deviceNTP->getPort());			
			Serial.print("DeviceNTP Utc Time Offset in second: ");
			Serial.println(_deviceNTP->getUtcTimeOffsetInSecond());
			Serial.print("DeviceNTP Update Interval: ");
			Serial.println(_deviceNTP->getUpdateIntervalInMilliSecond());
			
			Serial.print("ApplicationId: ");
			Serial.println(_deviceInApplication->getApplicationId());
			
			Serial.print("PublishIntervalInSeconds: ");
			Serial.println(_espDevice->getDeviceSensors()->getPublishIntervalInSeconds());
			
			_initialized = true;
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

	String applicationId = root["applicationId"];
	String brokerApplicationTopic = root["brokerApplicationTopic"];	
	
	this->_deviceInApplication->setApplicationId(applicationId);
	this->_deviceMQ->setApplicationTopic(brokerApplicationTopic);
	
	Serial.println("[ConfigurationManager::insertInApplication] ");
	Serial.print("applicationId: ");
	Serial.println(applicationId);
	Serial.print("brokerApplicationTopic: ");
	Serial.println(brokerApplicationTopic);
}

void ConfigurationManager::deleteFromApplication()
{	
	this->_deviceInApplication->setApplicationId("");	
	this->_deviceMQ->setApplicationTopic("");
	
	Serial.println("[ConfigurationManager::deleteFromApplication] delete from Application with success !");
}

void ConfigurationManager::setUtcTimeOffsetInSecond(String json)
{
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		Serial.print("[ConfigurationManager::setUtcTimeOffsetInSecond] parse failed: ");
		Serial.println(json);
		return;
	}	

	int utcTimeOffsetInSecond = root["utcTimeOffsetInSecond"];
	
	this->_deviceNTP->setUtcTimeOffsetInSecond(utcTimeOffsetInSecond);
	
	Serial.println("[ConfigurationManager::setUtcTimeOffsetInSecond] ");
	Serial.print("utcTimeOffsetInSecond: ");
	Serial.println(utcTimeOffsetInSecond);
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
	