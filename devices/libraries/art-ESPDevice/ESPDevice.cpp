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
	
	_debug							= new RemoteDebug();
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
	_debug->begin("remotedebug-sample"); // Initiaze the telnet server
	_debug->setResetCmdEnabled(true); // Enable the reset command	
	_debug->setSerialEnabled(true); // Setup after Debug.begin - All messages too send to serial too, and can be see in serial monitor
	
	autoLoad();
}

void ESPDevice::loop()
{	
	autoLoad();
	
	// Remote debug over telnet
    _debug->handle();
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

RemoteDebug* ESPDevice::getDebug()
{	
	return _debug;
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
			
			if (_debug->isActive(_debug->DEBUG)) { 
			
				 _debug->printf("ESPDevice initialized with success !\n"); 
				
				_debug->printf("ESPDevice DeviceId: %s\n", _deviceId);
				_debug->printf("ESPDevice DeviceDatasheetId: %d\n", _deviceDatasheetId);
				
				_debug->printf("DeviceDebug Active: %s\n", getDeviceDebug()->getActive() ? "true" : "false");
				
				_debug->printf("DeviceMQ Host: %s\n", getDeviceMQ()->getHost());
				_debug->printf("DeviceMQ Port: %d\n", getDeviceMQ()->getPort());
				_debug->printf("DeviceMQ User: %s\n", getDeviceMQ()->getUser());
				_debug->printf("DeviceMQ Password: %s\n", getDeviceMQ()->getPassword());
				_debug->printf("DeviceMQ ClientId: %s\n", getDeviceMQ()->getClientId());
				_debug->printf("DeviceMQ Device Topic: %s\n", getDeviceMQ()->getDeviceTopic());			
				
				_debug->printf("DeviceNTP Host: %s\n", getDeviceNTP()->getHost());
				_debug->printf("DeviceNTP Port: %d\n", getDeviceNTP()->getPort());
				_debug->printf("DeviceNTP UtcTimeOffsetInSecond: %d\n", getDeviceNTP()->getUtcTimeOffsetInSecond());
				_debug->printf("DeviceNTP UpdateIntervalInMilliSecond: %d\n", getDeviceNTP()->getUpdateIntervalInMilliSecond());
				
				_debug->printf("DeviceInApplication ApplicationId: %s\n", getDeviceInApplication()->getApplicationId());
				_debug->printf("DeviceInApplication ApplicationTopic: %s\n", getDeviceInApplication()->getApplicationTopic());
				
				_debug->printf("DeviceSensors PublishIntervalInSeconds: %d\n", getDeviceSensors()->getPublishIntervalInSeconds());		
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
	
	DeviceInApplication::createDeviceInApplication(_deviceInApplication, this, jsonObject["deviceInApplication"]);		
	DeviceDebug::createDeviceDebug(_deviceDebug, this, jsonObject["deviceDebug"]);		
	DeviceMQ::createDeviceMQ(_deviceMQ, this, jsonObject["deviceMQ"]);		
	DeviceNTP::createDeviceNTP(_deviceNTP, this, jsonObject["deviceNTP"]);		
	DeviceSensors::createDeviceSensors(_deviceSensors, this, jsonObject["deviceSensors"]);	
	
	_loaded = true;	
}