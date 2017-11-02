#include "AccessManager.h"

AccessManager::AccessManager(DebugManager& debugManager, String host, uint16_t port, String uri)
{ 
	this->_debugManager = &debugManager;
	
	this->_host = host;
	this->_port = port;
	this->_uri = uri;
	
	this->_chipId = ESP.getChipId();
	this->_flashChipId = ESP.getFlashChipId();
	this->_macAddress = WiFi.macAddress();
}

void AccessManager::begin()
{	
	this->getConfigurations();
}

const char *AccessManager::getBrokerHost()
{	
	return this->_brokerHost;
}

int AccessManager::getBrokerPort()
{	
	return this->_brokerPort;
}

const char *AccessManager::getBrokerUser()
{	
	return this->_brokerUser;
}

const char *AccessManager::getBrokerPwd()
{	
	return this->_brokerPwd;
}

void AccessManager::getConfigurations()
{	
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
			
			StaticJsonBuffer<200> jsonBufferResponse;
			JsonObject& jsonObjectResponse = jsonBufferResponse.parseObject(payload);
			
			this->_brokerHost = jsonObjectResponse["brokerHost"];
			this->_brokerPort = jsonObjectResponse["brokerPort"];	
			this->_brokerUser = jsonObjectResponse["brokerUser"];	
			this->_brokerPwd = jsonObjectResponse["brokerPassword"];				
			
			Serial.print("Broker Host: ");
			Serial.println(this->_brokerHost);
			Serial.print("Broker Port: ");
			Serial.println(this->_brokerPort);
			Serial.print("Broker User: ");
			Serial.println(this->_brokerUser);
			Serial.print("Broker Pwd: ");
			Serial.println(this->_brokerPwd);
		}
	} else {
		Serial.print("[HTTP] GET... failed, error: ");
		Serial.println(http.errorToString(httpCode).c_str());
	}
  
  http.end();
}

