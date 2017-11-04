#include "AccessManager.h"

AccessManager::AccessManager(DebugManager& debugManager, WiFiManager& wifiManager, String host, uint16_t port, String uri)
{ 
	this->_debugManager = &debugManager;
	this->_wifiManager = &wifiManager;
	
	this->_host = host;
	this->_port = port;
	this->_uri = uri;
}

void AccessManager::begin()
{	
	this->_chipId = ESP.getChipId();
	this->_flashChipId = ESP.getFlashChipId();
	this->_macAddress = WiFi.macAddress();
	
	this->autoInitialize();
}

bool AccessManager::initialized()
{	
	return this->_initialized;
}

String AccessManager::getBrokerHost()
{	
	return this->_brokerHost;
}

int AccessManager::getBrokerPort()
{	
	return this->_brokerPort;
}

String AccessManager::getBrokerUser()
{	
	return this->_brokerUser;
}

String AccessManager::getBrokerPwd()
{	
	return this->_brokerPwd;
}

String AccessManager::getHardwareId()
{	
	return this->_hardwareId;
}

String AccessManager::getHardwareInApplicationId()
{	
	return this->_hardwareInApplicationId;
}

void AccessManager::autoInitialize()
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

	Serial.print("[AccessManager] getConfigurations request: ");
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
			
			StaticJsonBuffer<350> jsonBufferResponse;
			JsonObject& jsonObjectResponse = jsonBufferResponse.parseObject(payload);
			
			String brokerHost = jsonObjectResponse["brokerHost"];
			int brokerPort = jsonObjectResponse["brokerPort"];	
			String brokerUser = jsonObjectResponse["brokerUser"];	
			String brokerPwd = jsonObjectResponse["brokerPassword"];	
			
			this->_brokerHost = brokerHost;
			this->_brokerPort = brokerPort;
			this->_brokerUser = brokerUser;
			this->_brokerPwd = brokerPwd;
			
			String hardwareId = jsonObjectResponse["hardwareId"];	
			String hardwareInApplicationId = jsonObjectResponse["hardwareInApplicationId"];	
			
			this->_hardwareId = hardwareId;	
			this->_hardwareInApplicationId = hardwareInApplicationId == "null" ? "" : hardwareInApplicationId;				
			
			Serial.println("AccessManager initialized with success !");
			Serial.print("Broker Host: ");
			Serial.println(this->_brokerHost);
			Serial.print("Broker Port: ");
			Serial.println(this->_brokerPort);
			Serial.print("Broker User: ");
			Serial.println(this->_brokerUser);
			Serial.print("Broker Pwd: ");
			Serial.println(this->_brokerPwd);
			
			Serial.print("Broker HardwareId: ");
			Serial.println(this->_hardwareId);
			Serial.print("Broker HardwareInApplicationId: ");
			Serial.println(this->_hardwareInApplicationId);
			
			this->_initialized = true;
		}
	} else {
		Serial.print("[HTTP] GET... failed, error: ");
		Serial.println(http.errorToString(httpCode).c_str());		
	}
  
  http.end();
}

