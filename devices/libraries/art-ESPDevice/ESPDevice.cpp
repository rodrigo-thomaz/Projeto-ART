#include "ESPDevice.h"

ESPDevice::ESPDevice(WiFiManager& wifiManager, char* webApiHost, uint16_t webApiPort, char* webApiUri)
{
	_wifiManager = &wifiManager;
	
	_chipId							= ESP.getChipId();	
	_flashChipId					= ESP.getFlashChipId();	
	_chipSize						= ESP.getFlashChipSize();	
		
	_stationMacAddress 				= strdup(WiFi.macAddress().c_str());	
	_softAPMacAddress				= strdup(WiFi.softAPmacAddress().c_str());		

	_webApiHost 					= new char(sizeof(strlen(webApiHost)));
	_webApiHost 					= webApiHost;
	
	_webApiPort 					= webApiPort;
	
	_webApiUri 						= new char(sizeof(strlen(webApiUri)));
	_webApiUri 						= webApiUri;	
	
	DeviceDebug::createDeviceDebug(_deviceDebug, this);		
}

ESPDevice::~ESPDevice()
{
	delete (_deviceId);
	delete (_stationMacAddress);
	delete (_label);
	
	delete (_deviceInApplication);
	delete (_deviceDebug);
	delete (_deviceMQ);
	delete (_deviceNTP);
	delete (_deviceSensors);
}

void ESPDevice::begin()
{		
	_deviceDebug->begin();
	
	autoLoad();
}

void ESPDevice::loop()
{	
	autoLoad();
	
	_deviceDebug->loop();
	
    // Give a time for ESP8266
    yield();
}

char* ESPDevice::getDeviceId()
{	
	return _deviceId;
}

short ESPDevice::getDeviceDatasheetId()
{	
	return _deviceDatasheetId;
}

int	ESPDevice::getChipId()
{	
	return _chipId;
}

int ESPDevice::getFlashChipId()
{	
	return _flashChipId;
}

long ESPDevice::getChipSize()
{	
	return _chipSize;
}

char* ESPDevice::getStationMacAddress()
{	
	return _stationMacAddress;
}

char* ESPDevice::getSoftAPMacAddress()
{	
	return _softAPMacAddress;
}

char* ESPDevice::getLabel()
{	
	return _label;
}

void ESPDevice::setLabel(char* value)
{	
	_label = new char(sizeof(strlen(value)));
	_label = value;
}

char* ESPDevice::getWebApiHost()
{
	return _webApiHost;
}

uint16_t ESPDevice::getWebApiPort()
{
	return _webApiPort;
}

char* ESPDevice::getWebApiUri()
{
	return _webApiUri;
}		
		
DeviceInApplication* ESPDevice::getDeviceInApplication()
{	
	return _deviceInApplication;
}

DeviceDebug* ESPDevice::getDeviceDebug()
{	
	return _deviceDebug;
}

DeviceMQ* ESPDevice::getDeviceMQ()
{	
	return _deviceMQ;
}

DeviceNTP* ESPDevice::getDeviceNTP()
{	
	return _deviceNTP;
}
		
DeviceSensors* ESPDevice::getDeviceSensors()
{	
	return _deviceSensors;
}

bool ESPDevice::loaded()
{	
	return _loaded;
}

void ESPDevice::autoLoad()
{	
	if(!_wifiManager->isConnected() || _loaded){
		return;
	}
	
	HTTPClient http; 

	String apiUri = String(_webApiUri) + "api/espDevice/getConfigurations";
	http.begin(_webApiHost, _webApiPort, apiUri); 

	StaticJsonBuffer<200> jsonBufferRequest;
	JsonObject& jsonObjectRequest = jsonBufferRequest.createObject();
	
	jsonObjectRequest["chipId"] = _chipId;
	jsonObjectRequest["flashChipId"] = _flashChipId;
	jsonObjectRequest["stationMacAddress"] = _stationMacAddress;
	jsonObjectRequest["softAPMacAddress"] = _softAPMacAddress;

	int lenRequest = jsonObjectRequest.measureLength();
	char dataRequest[lenRequest + 1];
	jsonObjectRequest.printTo(dataRequest, sizeof(dataRequest));

	Serial.print("[ESPDevice] getConfigurations request: ");
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
			
			load(payload);			
			
			if (_deviceDebug->getDebug()->isActive(_deviceDebug->getDebug()->DEBUG)) { 
			
				 _deviceDebug->getDebug()->printf("ESPDevice initialized with success !\n"); 
				
				_deviceDebug->getDebug()->printf("ESPDevice DeviceId: %s\n", _deviceId);
				_deviceDebug->getDebug()->printf("ESPDevice DeviceDatasheetId: %d\n", _deviceDatasheetId);
				
				_deviceDebug->getDebug()->printf("DeviceDebug RemoteEnabled: %s\n", getDeviceDebug()->getRemoteEnabled() ? "true" : "false");
				_deviceDebug->getDebug()->printf("DeviceDebug ResetCmdEnabled: %s\n", getDeviceDebug()->getResetCmdEnabled() ? "true" : "false");
				_deviceDebug->getDebug()->printf("DeviceDebug SerialEnabled: %s\n", getDeviceDebug()->getSerialEnabled() ? "true" : "false");
				_deviceDebug->getDebug()->printf("DeviceDebug ShowColors: %s\n", getDeviceDebug()->getShowColors() ? "true" : "false");
				_deviceDebug->getDebug()->printf("DeviceDebug ShowDebugLevel: %s\n", getDeviceDebug()->getShowDebugLevel() ? "true" : "false");
				_deviceDebug->getDebug()->printf("DeviceDebug ShowProfiler: %s\n", getDeviceDebug()->getShowProfiler() ? "true" : "false");
				_deviceDebug->getDebug()->printf("DeviceDebug ShowTime: %s\n", getDeviceDebug()->getShowTime() ? "true" : "false");
				
				_deviceDebug->getDebug()->printf("DeviceMQ Host: %s\n", getDeviceMQ()->getHost());
				_deviceDebug->getDebug()->printf("DeviceMQ Port: %d\n", getDeviceMQ()->getPort());
				_deviceDebug->getDebug()->printf("DeviceMQ User: %s\n", getDeviceMQ()->getUser());
				_deviceDebug->getDebug()->printf("DeviceMQ Password: %s\n", getDeviceMQ()->getPassword());
				_deviceDebug->getDebug()->printf("DeviceMQ ClientId: %s\n", getDeviceMQ()->getClientId());
				_deviceDebug->getDebug()->printf("DeviceMQ Device Topic: %s\n", getDeviceMQ()->getDeviceTopic());			
				
				_deviceDebug->getDebug()->printf("DeviceNTP Host: %s\n", getDeviceNTP()->getHost());
				_deviceDebug->getDebug()->printf("DeviceNTP Port: %d\n", getDeviceNTP()->getPort());
				_deviceDebug->getDebug()->printf("DeviceNTP UtcTimeOffsetInSecond: %d\n", getDeviceNTP()->getUtcTimeOffsetInSecond());
				_deviceDebug->getDebug()->printf("DeviceNTP UpdateIntervalInMilliSecond: %d\n", getDeviceNTP()->getUpdateIntervalInMilliSecond());
				
				_deviceDebug->getDebug()->printf("DeviceInApplication ApplicationId: %s\n", getDeviceInApplication()->getApplicationId());
				_deviceDebug->getDebug()->printf("DeviceInApplication ApplicationTopic: %s\n", getDeviceInApplication()->getApplicationTopic());
				
				_deviceDebug->getDebug()->printf("DeviceSensors PublishIntervalInSeconds: %d\n", getDeviceSensors()->getPublishIntervalInSeconds());		
			}		
		}
	} else {
		Serial.print("[HTTP] GET... failed, error: ");
		Serial.println(http.errorToString(httpCode).c_str());		
	}

  http.end();

}

void ESPDevice::load(String json)
{	
	DynamicJsonBuffer 				  jsonBuffer;
	JsonObject& jsonObject 			= jsonBuffer.parseObject(json);		
			
	_deviceId 						= strdup(jsonObject["deviceId"]);	
	_deviceDatasheetId 				= jsonObject["deviceDatasheetId"];	
	
	_label 							= strdup(jsonObject["label"]);
	
	_deviceDebug->load(jsonObject["deviceDebug"]);		
	
	DeviceInApplication::createDeviceInApplication(_deviceInApplication, this, jsonObject["deviceInApplication"]);			
	DeviceMQ::createDeviceMQ(_deviceMQ, this, jsonObject["deviceMQ"]);		
	DeviceNTP::createDeviceNTP(_deviceNTP, this, jsonObject["deviceNTP"]);		
	DeviceSensors::createDeviceSensors(_deviceSensors, this, jsonObject["deviceSensors"]);	
	
	_loaded = true;	
}