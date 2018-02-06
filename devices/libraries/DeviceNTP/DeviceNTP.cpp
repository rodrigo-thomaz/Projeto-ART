#include "DeviceNTP.h"
#include "ESPDevice.h"

namespace ART
{
	DeviceNTP::DeviceNTP(ESPDevice* espDevice)
	{
		_espDevice = espDevice;
		_udp = new WiFiUDP();		
	}

	DeviceNTP::~DeviceNTP()
	{
		delete (_espDevice);
		delete (_host);
	}

	void DeviceNTP::create(DeviceNTP *(&deviceNTP), ESPDevice * espDevice)
	{
		deviceNTP = new DeviceNTP(espDevice);
	}

	void DeviceNTP::load(JsonObject& jsonObject)
	{
		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		deviceDebug->print("DeviceNTP", "load", "begin\n");

		char* host = strdup(jsonObject["host"]);
		_host = new char(sizeof(strlen(host)));
		_host = host;

		_port = jsonObject["port"];

		_utcTimeOffsetInSecond = jsonObject["utcTimeOffsetInSecond"];
		_updateIntervalInMilliSecond = jsonObject["updateIntervalInMilliSecond"];

		_udp->begin(_port);
		_udpSetup = true;

		_loaded = true;

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) {

			deviceDebug->printf("DeviceNTP", "load", "host: %s\n", _host);
			deviceDebug->printf("DeviceNTP", "load", "port: %d\n", (char*)_port);
			deviceDebug->printf("DeviceNTP", "load", "utcTimeOffsetInSecond: %d\n", (char*)_utcTimeOffsetInSecond);
			deviceDebug->printf("DeviceNTP", "load", "updateIntervalInMilliSecond: %d\n", (char*)_updateIntervalInMilliSecond);

			deviceDebug->print("DeviceNTP", "load", "end\n");
		}
	}

	void DeviceNTP::begin() {
		_espDevice->getDeviceMQ()->addSubscriptionCallback([=](char* topicKey, char* json) { return onDeviceMQSubscription(topicKey, json); });
		_espDevice->getDeviceMQ()->addSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQSubscribeDeviceInApplication(); });
		_espDevice->getDeviceMQ()->addUnSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQUnSubscribeDeviceInApplication(); });
	}

	char* DeviceNTP::getHost() const
	{
		return (_host);
	}

	int DeviceNTP::getPort()
	{
		return _port;
	}

	int DeviceNTP::getUtcTimeOffsetInSecond()
	{
		return _utcTimeOffsetInSecond;
	}

	void DeviceNTP::setUtcTimeOffsetInSecond(char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);

		if (!root.success()) {
			Serial.print("[ConfigurationManager::setUtcTimeOffsetInSecond] parse failed: ");
			Serial.println(json);
			return;
		}

		_utcTimeOffsetInSecond = root["utcTimeOffsetInSecond"];

		Serial.println("[ConfigurationManager::setUtcTimeOffsetInSecond] ");
		Serial.print("utcTimeOffsetInSecond: ");
		Serial.println(_utcTimeOffsetInSecond);
	}

	int DeviceNTP::getUpdateIntervalInMilliSecond()
	{
		return _updateIntervalInMilliSecond;
	}

	void DeviceNTP::setUpdateIntervalInMilliSecond(char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);

		if (!root.success()) {
			Serial.print("[ConfigurationManager::setUpdateIntervalInMilliSecond] parse failed: ");
			Serial.println(json);
			return;
		}

		_updateIntervalInMilliSecond = root["updateIntervalInMilliSecond"];

		Serial.println("[ConfigurationManager::setUpdateIntervalInMilliSecond] ");
		Serial.print("updateIntervalInMilliSecond: ");
		Serial.println(_updateIntervalInMilliSecond);
	}

	bool DeviceNTP::forceUpdate() {
#ifdef DEBUG_NTPManager
		Serial.println("Update from NTP Server");
#endif

		this->sendNTPPacket();

		// Wait till data is there or timeout...
		byte timeout = 0;
		int cb = 0;
		do {
			delay(10);
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

	bool DeviceNTP::update() {

		if (!_loaded) {
			return false;
		}

		if ((millis() - this->_lastUpdate >= _updateIntervalInMilliSecond)     		// Update after _updateInterval
			|| this->_lastUpdate == 0) {                                // Update if there was no update yet.
			//if (!this->_udpSetup) this->begin();                        // setup the UDP client if needed
																		//notify
			bool result = this->forceUpdate();
			if (_updateCallback != NULL) {
				Serial.print("[NTP Manager] UpdateCallback => update: ");
				Serial.print(result);
				Serial.println(", forceUpdate: true");
				_updateCallback(result, true);
			}
			return result;
		}
		if (_updateCallback != NULL) {
			Serial.println("[NTP Manager] UpdateCallback => update: true, forceUpdate: false");
			_updateCallback(true, false);
		}
		return true;
	}

	unsigned long DeviceNTP::getEpochTime() {
		return _utcTimeOffsetInSecond + // User offset	
			this->_currentEpoc + // Epoc returned by the NTP server
			((millis() - this->_lastUpdate) / 1000); // Time since last update
	}

	unsigned long DeviceNTP::getEpochTimeUTC() {
		return this->_currentEpoc + // Epoc returned by the NTP server
			((millis() - this->_lastUpdate) / 1000); // Time since last update
	}

	int DeviceNTP::getDay() {
		return (((this->getEpochTime() / 86400L) + 4) % 7); //0 is Sunday
	}
	int DeviceNTP::getHours() {
		return ((this->getEpochTime() % 86400L) / 3600);
	}
	int DeviceNTP::getMinutes() {
		return ((this->getEpochTime() % 3600) / 60);
	}
	int DeviceNTP::getSeconds() {
		return (this->getEpochTime() % 60);
	}

	String DeviceNTP::getFormattedTimeOld() {
		unsigned long rawTime = this->getEpochTime();
		unsigned long hours = (rawTime % 86400L) / 3600;
		String hoursStr = hours < 10 ? "0" + String(hours) : String(hours);

		unsigned long minutes = (rawTime % 3600) / 60;
		String minuteStr = minutes < 10 ? "0" + String(minutes) : String(minutes);

		unsigned long seconds = rawTime % 60;
		String secondStr = seconds < 10 ? "0" + String(seconds) : String(seconds);

		return hoursStr + ":" + minuteStr + ":" + secondStr;
	}

	String DeviceNTP::getFormattedTime() {

		int hours = this->getHours();
		int minutes = this->getMinutes();

		static char str[5];

		sprintf(str, "%02d:%02d", hours, minutes);

		return String(str);

	}

	void DeviceNTP::end() {
		this->_udp->stop();

		this->_udpSetup = false;
	}

	void DeviceNTP::sendNTPPacket() {
		// set all bytes in the buffer to 0
		memset(this->_packetBuffer, 0, NTP_PACKET_SIZE);
		// Initialize values needed to form NTP request
		// (see URL above for details on the packets)
		this->_packetBuffer[0] = 0b11100011;   // LI, Version, Mode
		this->_packetBuffer[1] = 0;     // Stratum, or type of clock
		this->_packetBuffer[2] = 6;     // Polling Interval
		this->_packetBuffer[3] = 0xEC;  // Peer Clock Precision
										// 8 bytes of zero for Root Delay & Root Dispersion
		this->_packetBuffer[12] = 49;
		this->_packetBuffer[13] = 0x4E;
		this->_packetBuffer[14] = 49;
		this->_packetBuffer[15] = 52;

		// all NTP fields have been given values, now
		// you can send a packet requesting a timestamp:

		this->_udp->beginPacket(_host, 123); // Erro quando usa deviceNTP->getPort()
		this->_udp->write(this->_packetBuffer, NTP_PACKET_SIZE);
		this->_udp->endPacket();
	}

	DeviceNTP& DeviceNTP::setUpdateCallback(DEVICE_NTP_SET_UPDATE_CALLBACK_SIGNATURE callback) {
		this->_updateCallback = callback;
		return *this;
	}

	void DeviceNTP::onDeviceMQSubscribeDeviceInApplication()
	{
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_NTP_SET_UTC_TIME_OFF_SET_IN_SECOND_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_NTP_SET_UPDATE_INTERVAL_IN_MILLI_SECOND_TOPIC_SUB);
	}

	void DeviceNTP::onDeviceMQUnSubscribeDeviceInApplication()
	{
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_NTP_SET_UTC_TIME_OFF_SET_IN_SECOND_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_NTP_SET_UPDATE_INTERVAL_IN_MILLI_SECOND_TOPIC_SUB);
	}

	bool DeviceNTP::onDeviceMQSubscription(char* topicKey, char* json)
	{
		if (strcmp(topicKey, DEVICE_NTP_SET_UTC_TIME_OFF_SET_IN_SECOND_TOPIC_SUB) == 0) {
			setUtcTimeOffsetInSecond(json);
			return true;
		}
		else if (strcmp(topicKey, DEVICE_NTP_SET_UPDATE_INTERVAL_IN_MILLI_SECOND_TOPIC_SUB) == 0) {
			setUpdateIntervalInMilliSecond(json);
			return true;
		}
		else {
			return false;
		}
	}
}