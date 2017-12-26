#include "UpdateManager.h"

UpdateManager::UpdateManager(DebugManager& debugManager, WiFiManager& wifiManager, String host, uint16_t port, String uri)
{
	this->_debugManager = &debugManager;
	this->_wifiManager = &wifiManager;
	
	this->_host = host;
	this->_port = port;
	this->_uri = uri;
}

UpdateManager::~UpdateManager()
{
}

void UpdateManager::loop()
{
	if(!this->_wifiManager->isConnected()){
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
	 t_httpUpdate_return ret = ESPhttpUpdate.update(this->_host, this->_port, this->_uri + "api/espDevice/checkForUpdates");

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