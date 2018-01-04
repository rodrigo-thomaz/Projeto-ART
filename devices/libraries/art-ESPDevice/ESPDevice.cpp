#include "ESPDevice.h"

ESPDevice::ESPDevice(WiFiManager& wifiManager, char* webApiHost, uint16_t webApiPort, char* webApiUri)
{
	_wifiManager = &wifiManager;
	
	_chipId							= ESP.getChipId();	
	_flashChipId					= ESP.getFlashChipId();	
	_chipSize						= ESP.getFlashChipSize();	
		
	_stationMacAddress 				= strdup(WiFi.macAddress().c_str());	
	_softAPMacAddress				= strdup(WiFi.softAPmacAddress().c_str());		

	_webApiHost = new char(sizeof(strlen(webApiHost)));
	_webApiHost = webApiHost;
	
	_webApiPort = webApiPort;
	
	_webApiUri = new char(sizeof(strlen(webApiUri)));
	_webApiUri = webApiUri;	
}

ESPDevice::~ESPDevice()
{
	delete (_deviceId);
	delete (_stationMacAddress);
	delete (_label);
	
	delete (_deviceMQ);
	delete (_deviceNTP);
	delete (_deviceSensors);
}

void ESPDevice::begin()
{		
	autoLoad();
}

void ESPDevice::loop()
{	
	autoLoad();
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
			
			Serial.println("ESPDevice initialized with success !");
			
			Serial.print("ESPDevice DeviceId: ");
			Serial.println(getDeviceId());
			Serial.print("ESPDevice DeviceDatasheetId: ");
			Serial.println(getDeviceDatasheetId());
			
			Serial.print("DeviceMQ Host: ");
			Serial.println(getDeviceMQ()->getHost());
			Serial.print("DeviceMQ Port: ");
			Serial.println(getDeviceMQ()->getPort());
			Serial.print("DeviceMQ User: ");
			Serial.println(getDeviceMQ()->getUser());
			Serial.print("DeviceMQ Password: ");
			Serial.println(getDeviceMQ()->getPassword());
			Serial.print("DeviceMQ ClientId: ");
			Serial.println(getDeviceMQ()->getClientId());			
			Serial.print("DeviceMQ Device Topic: ");
			Serial.println(getDeviceMQ()->getDeviceTopic());
			
			Serial.print("DeviceNTP Host: ");
			Serial.println(getDeviceNTP()->getHost());
			Serial.print("DeviceNTP Port: ");
			Serial.println(getDeviceNTP()->getPort());			
			Serial.print("DeviceNTP Utc Time Offset in second: ");
			Serial.println(getDeviceNTP()->getUtcTimeOffsetInSecond());
			Serial.print("DeviceNTP Update Interval: ");
			Serial.println(getDeviceNTP()->getUpdateIntervalInMilliSecond());
			
			Serial.print("DeviceInApplication ApplicationId: ");
			Serial.println(getDeviceInApplication()->getApplicationId());
			Serial.print("DeviceInApplication Application Topic: ");
			Serial.println(getDeviceInApplication()->getApplicationTopic());
			
			Serial.print("DeviceSensors PublishIntervalInSeconds: ");
			Serial.println(getDeviceSensors()->getPublishIntervalInSeconds());			
			
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
	DeviceMQ::createDeviceMQ(_deviceMQ, this, jsonObject["deviceMQ"]);		
	DeviceNTP::createDeviceNTP(_deviceNTP, this, jsonObject["deviceNTP"]);		
	DeviceSensors::createDeviceSensors(_deviceSensors, this, jsonObject["deviceSensors"]);	

	_loaded = true;	
}