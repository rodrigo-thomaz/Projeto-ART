#include "ConfigurationManager.h"

// BrokerSettings

BrokerSettings::BrokerSettings(String host, int port, String user, String pwd) {
  _host = host;
  _port = port;
  _user = user;
  _pwd = pwd;
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

// NTPSettings

NTPSettings::NTPSettings(String host, int port, int updateInterval, int displayTimeOffset) {
  _host = host;
  _port = port;
  _updateInterval = updateInterval;
  _displayTimeOffset = displayTimeOffset;
}

String NTPSettings::getHost()
{	
	return this->_host;
}

int NTPSettings::getPort()
{	
	return this->_port;
}

int NTPSettings::getUpdateInterval()
{	
	return this->_updateInterval;
}

int NTPSettings::getDisplayTimeOffset()
{	
	return this->_displayTimeOffset;
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

String ConfigurationManager::getHardwareId()
{	
	return this->_hardwareId;
}

String ConfigurationManager::getHardwareInApplicationId()
{	
	return this->_hardwareInApplicationId;
}

void ConfigurationManager::insertInApplication(String json)
{	
	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.parseObject(json);
	
	if (!root.success()) {
		Serial.print("parse setHardwareInApplicationId failed: ");
		Serial.println(json);
		return;
	}	

	String hardwareInApplicationId = root["hardwareInApplicationId"];

	Serial.print("[ConfigurationManager] insertInApplication: ");
	Serial.println(hardwareInApplicationId);
	
	this->_hardwareInApplicationId = hardwareInApplicationId;
}

void ConfigurationManager::deleteFromApplication()
{	
	Serial.println("[ConfigurationManager] deleteFromApplication");
	this->_hardwareInApplicationId = "";
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
			
			StaticJsonBuffer<500> jsonBufferResponse;
			JsonObject& jsonObjectResponse = jsonBufferResponse.parseObject(payload);			
			
			this->_brokerSettings = new BrokerSettings(
				jsonObjectResponse["brokerHost"], 
				jsonObjectResponse["brokerPort"], 
				jsonObjectResponse["brokerUser"], 
				jsonObjectResponse["brokerPassword"]);
			
			this->_ntpSettings = new NTPSettings(
				jsonObjectResponse["ntpHost"], 
				jsonObjectResponse["ntpPort"], 
				jsonObjectResponse["ntpUpdateInterval"], 
				jsonObjectResponse["ntpDisplayTimeOffset"]);			
			
			String hardwareId = jsonObjectResponse["hardwareId"];	
			String hardwareInApplicationId = jsonObjectResponse["hardwareInApplicationId"];	
			
			this->_hardwareId = hardwareId;	
			this->_hardwareInApplicationId = hardwareInApplicationId == "null" ? "" : hardwareInApplicationId;				
			
			Serial.println("ConfigurationManager initialized with success !");
			
			Serial.print("Broker Host: ");
			Serial.println(this->_brokerSettings->getHost());
			Serial.print("Broker Port: ");
			Serial.println(this->_brokerSettings->getPort());
			Serial.print("Broker User: ");
			Serial.println(this->_brokerSettings->getUser());
			Serial.print("Broker Pwd: ");
			Serial.println(this->_brokerSettings->getPwd());
			
			Serial.print("NTP Host: ");
			Serial.println(this->_ntpSettings->getHost());
			Serial.print("NTP Port: ");
			Serial.println(this->_ntpSettings->getPort());
			Serial.print("NTP Update Interval: ");
			Serial.println(this->_ntpSettings->getUpdateInterval());
			Serial.print("NTP Display Time Offset: ");
			Serial.println(this->_ntpSettings->getDisplayTimeOffset());
			
			Serial.print("HardwareId: ");
			Serial.println(this->_hardwareId);
			Serial.print("HardwareInApplicationId: ");
			Serial.println(this->_hardwareInApplicationId);
			
			this->_initialized = true;
		}
	} else {
		Serial.print("[HTTP] GET... failed, error: ");
		Serial.println(http.errorToString(httpCode).c_str());		
	}
  
  http.end();
}

