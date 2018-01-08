#include "UpdateManager.h"

UpdateManager::UpdateManager(ESPDevice& espDevice)
{
	this->_espDevice = &espDevice;
}

UpdateManager::~UpdateManager()
{
}

void UpdateManager::loop()
{
	if(!this->_espDevice->getDeviceWiFi()->isConnected()){
		return;
	}	
	
	 uint64_t now = millis();   
	 
	 if(now - this->_checkForUpdatesTimestamp > CHECKFORUPDATES_INTERVAL) {
      this->_checkForUpdatesTimestamp = now;
      this->update();
    }   
}

void UpdateManager::update()
{	
	std::string uri;
	uri.append(_espDevice->getWebApiUri());
	uri.append("api/espDevice/checkForUpdates");
	
	t_httpUpdate_return ret = ESPhttpUpdate.update(_espDevice->getWebApiHost(), _espDevice->getWebApiPort(), uri.c_str());

	switch(ret) {
		case HTTP_UPDATE_FAILED:
			Serial.printf("HTTP_UPDATE_FAILD Error (%d): %s", ESPhttpUpdate.getLastError(), ESPhttpUpdate.getLastErrorString().c_str());
			break;

		case HTTP_UPDATE_NO_UPDATES:
			Serial.println("HTTP_UPDATE_NO_UPDATES");
			break;

		case HTTP_UPDATE_OK:
			Serial.println("HTTP_UPDATE_OK");
			break;
	}

	Serial.println();
}