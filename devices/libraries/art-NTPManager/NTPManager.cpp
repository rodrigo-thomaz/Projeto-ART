/**
 * The MIT License (MIT)
 * Copyright (c) 2015 by Fabrice Weinberg
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

#include "NTPManager.h"
#include "WiFiUdp.h"

NTPManager::NTPManager(ESPDevice& espDevice) {
  
  this->_espDevice = &espDevice;  
  
  this->_udp            = new WiFiUDP();  
}

NTPManager::NTPManager(UDP& udp) {
  this->_udp            = &udp;
}

bool NTPManager::begin() {
	
	if(this->_initialized){
      return true;;
    }
	
	if(!this->_espDevice->loaded()){
	  Serial.println("[ESPDevice] Not loaded !");
      return false;
    }
	
	DeviceNTP* deviceNTP = this->_espDevice->getDeviceNTP();
  
	int port = deviceNTP->getPort();
	
	this->_udp->begin(port);
	this->_udpSetup = true;
	
	this->_initialized = true;
	
	Serial.println("[NTP Manager] Initialized with success !");
	
	return true;
}

bool NTPManager::forceUpdate() {
  #ifdef DEBUG_NTPManager
    Serial.println("Update from NTP Server");
  #endif

  this->sendNTPPacket();

  // Wait till data is there or timeout...
  byte timeout = 0;
  int cb = 0;
  do {
    delay ( 10 );
    cb = this->_udp->parsePacket();
    if (timeout > 100) return false; // timeout after 1000 ms
    timeout++;
  } while (cb == 0);

  this->_lastUpdate = millis() - (10 * (timeout + 1)); // Account for delay in reading the time

  this->_udp->read(this->_packetBuffer, NTP_PACKET_SIZE);

  unsigned long highWord = word(this->_packetBuffer[40], this->_packetBuffer[41]);
  unsigned long lowWord = word(this->_packetBuffer[42], this->_packetBuffer[43]);
  // combine the four bytes (two words) into a long integer
  // this is NTP time (seconds since Jan 1 1900):
  unsigned long secsSince1900 = highWord << 16 | lowWord;

  this->_currentEpoc = secsSince1900 - SEVENZYYEARS;

  return true;
}

bool NTPManager::update() {
	
	if(!this->begin()){
      return false;
    }
	
	DeviceNTP* deviceNTP = this->_espDevice->getDeviceNTP();
  
	int updateInterval = deviceNTP->getUpdateIntervalInMilliSecond();
  
	if ((millis() - this->_lastUpdate >= updateInterval)     		// Update after _updateInterval
		|| this->_lastUpdate == 0) {                                // Update if there was no update yet.
		if (!this->_udpSetup) this->begin();                        // setup the UDP client if needed
		//notify
		bool result = this->forceUpdate();
		if ( _updateCallback != NULL) {	 			
			Serial.print("[NTP Manager] UpdateCallback => update: ");
			Serial.print(result);
			Serial.println(", forceUpdate: true");			
			_updateCallback(result, true);
		}
		return result;
	}
	if ( _updateCallback != NULL) {	 	
		Serial.println("[NTP Manager] UpdateCallback => update: true, forceUpdate: false");	
	_updateCallback(true, false);
	}
	return true;
}

unsigned long NTPManager::getEpochTime() {	

	DeviceNTP* deviceNTP = this->_espDevice->getDeviceNTP();
  
	int timeOffset = deviceNTP->getUtcTimeOffsetInSecond();
	
	return timeOffset + // User offset	
		this->_currentEpoc + // Epoc returned by the NTP server
        ((millis() - this->_lastUpdate) / 1000); // Time since last update
}

unsigned long NTPManager::getEpochTimeUTC() {	
	return this->_currentEpoc + // Epoc returned by the NTP server
         ((millis() - this->_lastUpdate) / 1000); // Time since last update
}

int NTPManager::getDay() {
  return (((this->getEpochTime()  / 86400L) + 4 ) % 7); //0 is Sunday
}
int NTPManager::getHours() {
  return ((this->getEpochTime()  % 86400L) / 3600);
}
int NTPManager::getMinutes() {
  return ((this->getEpochTime() % 3600) / 60);
}
int NTPManager::getSeconds() {
  return (this->getEpochTime() % 60);
}

String NTPManager::getFormattedTimeOld() {
  unsigned long rawTime = this->getEpochTime();
  unsigned long hours = (rawTime % 86400L) / 3600;
  String hoursStr = hours < 10 ? "0" + String(hours) : String(hours);

  unsigned long minutes = (rawTime % 3600) / 60;
  String minuteStr = minutes < 10 ? "0" + String(minutes) : String(minutes);

  unsigned long seconds = rawTime % 60;
  String secondStr = seconds < 10 ? "0" + String(seconds) : String(seconds);

  return hoursStr + ":" + minuteStr + ":" + secondStr;
}

String NTPManager::getFormattedTime() {
	
  int hours = this->getHours();
  int minutes = this->getMinutes();
	
  static char str[5];
	
  sprintf(str, "%02d:%02d", hours, minutes);
	
  return String(str);
  
}

void NTPManager::end() {
  this->_udp->stop();

  this->_udpSetup = false;
}

void NTPManager::sendNTPPacket() {
  // set all bytes in the buffer to 0
  memset(this->_packetBuffer, 0, NTP_PACKET_SIZE);
  // Initialize values needed to form NTP request
  // (see URL above for details on the packets)
  this->_packetBuffer[0] = 0b11100011;   // LI, Version, Mode
  this->_packetBuffer[1] = 0;     // Stratum, or type of clock
  this->_packetBuffer[2] = 6;     // Polling Interval
  this->_packetBuffer[3] = 0xEC;  // Peer Clock Precision
  // 8 bytes of zero for Root Delay & Root Dispersion
  this->_packetBuffer[12]  = 49;
  this->_packetBuffer[13]  = 0x4E;
  this->_packetBuffer[14]  = 49;
  this->_packetBuffer[15]  = 52;

  // all NTP fields have been given values, now
  // you can send a packet requesting a timestamp:
  
  DeviceNTP* deviceNTP = this->_espDevice->getDeviceNTP();
  
  this->_udp->beginPacket(deviceNTP->getHost(), 123); // Erro quando usa deviceNTP->getPort()
  this->_udp->write(this->_packetBuffer, NTP_PACKET_SIZE);
  this->_udp->endPacket();
}

NTPManager& NTPManager::setUpdateCallback(NTP_MANAGER_SET_UPDATE_CALLBACK_SIGNATURE callback) {
	this->_updateCallback = callback;
	return *this;
}