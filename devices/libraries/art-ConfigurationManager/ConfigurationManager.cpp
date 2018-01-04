#include "ConfigurationManager.h"

// ConfigurationManager

ConfigurationManager::ConfigurationManager(WiFiManager& wifiManager, ESPDevice& espDevice)
{ 
	this->_wifiManager = &wifiManager;
	this->_espDevice = &espDevice;
}

void ConfigurationManager::begin()
{		
	this->autoInitialize();
}

bool ConfigurationManager::initialized()
{	
	return _initialized;
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

	String apiUri = String(_espDevice->getWebApiUri()) + "api/espDevice/getConfigurations";
	http.begin(_espDevice->getWebApiHost(), _espDevice->getWebApiPort(), apiUri); 

	StaticJsonBuffer<200> jsonBufferRequest;
	JsonObject& jsonObjectRequest = jsonBufferRequest.createObject();
	
	jsonObjectRequest["chipId"] = _espDevice->getChipId();
	jsonObjectRequest["flashChipId"] = _espDevice->getFlashChipId();
	jsonObjectRequest["stationMacAddress"] = _espDevice->getStationMacAddress();
	jsonObjectRequest["softAPMacAddress"] = _espDevice->getSoftAPMacAddress();

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
			
			_espDevice->load(payload);
			
			Serial.println("ConfigurationManager initialized with success !");
			
			Serial.print("ESPDevice DeviceId: ");
			Serial.println(_espDevice->getDeviceId());
			Serial.print("ESPDevice DeviceDatasheetId: ");
			Serial.println(_espDevice->getDeviceDatasheetId());
			
			Serial.print("DeviceMQ Host: ");
			Serial.println(_espDevice->getDeviceMQ()->getHost());
			Serial.print("DeviceMQ Port: ");
			Serial.println(_espDevice->getDeviceMQ()->getPort());
			Serial.print("DeviceMQ User: ");
			Serial.println(_espDevice->getDeviceMQ()->getUser());
			Serial.print("DeviceMQ Password: ");
			Serial.println(_espDevice->getDeviceMQ()->getPassword());
			Serial.print("DeviceMQ ClientId: ");
			Serial.println(_espDevice->getDeviceMQ()->getClientId());			
			Serial.print("DeviceMQ Device Topic: ");
			Serial.println(_espDevice->getDeviceMQ()->getDeviceTopic());
			
			Serial.print("DeviceNTP Host: ");
			Serial.println(_espDevice->getDeviceNTP()->getHost());
			Serial.print("DeviceNTP Port: ");
			Serial.println(_espDevice->getDeviceNTP()->getPort());			
			Serial.print("DeviceNTP Utc Time Offset in second: ");
			Serial.println(_espDevice->getDeviceNTP()->getUtcTimeOffsetInSecond());
			Serial.print("DeviceNTP Update Interval: ");
			Serial.println(_espDevice->getDeviceNTP()->getUpdateIntervalInMilliSecond());
			
			Serial.print("DeviceInApplication ApplicationId: ");
			Serial.println(_espDevice->getDeviceInApplication()->getApplicationId());
			Serial.print("DeviceInApplication Application Topic: ");
			Serial.println(_espDevice->getDeviceInApplication()->getApplicationTopic());
			
			Serial.print("DeviceSensors PublishIntervalInSeconds: ");
			Serial.println(_espDevice->getDeviceSensors()->getPublishIntervalInSeconds());
			
			_initialized = true;
		}
	} else {
		Serial.print("[HTTP] GET... failed, error: ");
		Serial.println(http.errorToString(httpCode).c_str());		
	}
  
  http.end();
}