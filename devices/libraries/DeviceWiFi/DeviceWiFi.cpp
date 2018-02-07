/**************************************************************
DeviceWiFi is a library for the ESP8266/Arduino platform
(https://github.com/esp8266/Arduino) to enable easy
configuration and reconfiguration of WiFi credentials using a Captive Portal
inspired by:
http://www.esp8266.com/viewtopic.php?f=29&t=2520
https://github.com/chriscook8/esp-arduino-apboot
https://github.com/esp8266/Arduino/tree/master/libraries/DNSServer/examples/CaptivePortalAdvanced
Built by AlexT https://github.com/tzapu
Licensed under MIT license
**************************************************************/

#include "DeviceWiFi.h"
#include "ESPDevice.h"

namespace ART
{
	DeviceWiFiParameter::DeviceWiFiParameter(const char *custom) {
		_id = NULL;
		_placeholder = NULL;
		_length = 0;
		_value = NULL;

		_customHTML = custom;
	}

	DeviceWiFiParameter::DeviceWiFiParameter(const char *id, const char *placeholder, const char *defaultValue, int length) {
		init(id, placeholder, defaultValue, length, "");
	}

	DeviceWiFiParameter::DeviceWiFiParameter(const char *id, const char *placeholder, const char *defaultValue, int length, const char *custom) {
		init(id, placeholder, defaultValue, length, custom);
	}

	void DeviceWiFiParameter::init(const char *id, const char *placeholder, const char *defaultValue, int length, const char *custom) {
		_id = id;
		_placeholder = placeholder;
		_length = length;
		_value = new char[length + 1];
		for (int i = 0; i < length; i++) {
			_value[i] = 0;
		}
		if (defaultValue != NULL) {
			strncpy(_value, defaultValue, length);
		}

		_customHTML = custom;
	}

	const char* DeviceWiFiParameter::getValue() {
		return _value;
	}
	const char* DeviceWiFiParameter::getID() {
		return _id;
	}
	const char* DeviceWiFiParameter::getPlaceholder() {
		return _placeholder;
	}
	int DeviceWiFiParameter::getValueLength() {
		return _length;
	}
	const char* DeviceWiFiParameter::getCustomHTML() {
		return _customHTML;
	}




	DeviceWiFi::DeviceWiFi(ESPDevice* espDevice)
	{
		_espDevice = espDevice;

		_stationMacAddress = strdup(WiFi.macAddress().c_str());
		_softAPMacAddress = strdup(WiFi.softAPmacAddress().c_str());

		_pin = 14;
		setAPStaticIPConfig(IPAddress(10, 0, 0, 1), IPAddress(10, 0, 0, 1), IPAddress(255, 255, 255, 0));
	}

	DeviceWiFi::~DeviceWiFi()
	{
		_espDevice->getDeviceDebug()->printlnLevel(DeviceDebug::DEBUG, "DeviceWiFi", "destructor");

		delete (_espDevice);
		delete (_stationMacAddress);
		delete (_softAPMacAddress);
		delete (_hostName);
	}

	void DeviceWiFi::create(DeviceWiFi *(&deviceWiFi), ESPDevice * espDevice)
	{
		deviceWiFi = new DeviceWiFi(espDevice);
	}

	void DeviceWiFi::begin()
	{
		//_espDevice->getDeviceMQ()->addSubscriptionCallback([=](char* topicKey, char* json) { return onDeviceMQSubscription(topicKey, json); });
		_espDevice->getDeviceMQ()->addSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQSubscribeDeviceInApplication(); });
		_espDevice->getDeviceMQ()->addUnSubscribeDeviceInApplicationCallback([=]() { return onDeviceMQUnSubscribeDeviceInApplication(); });
	}

	void DeviceWiFi::load(JsonObject& jsonObject)
	{
		DeviceDebug* deviceDebug = _espDevice->getDeviceDebug();

		deviceDebug->print("DeviceWiFi", "load", "begin\n");

		char* hostName = strdup(jsonObject["hostName"]);
		_hostName = new char(sizeof(strlen(hostName)));
		_hostName = hostName;

		_publishIntervalInMilliSeconds = jsonObject["publishIntervalInMilliSeconds"];

		if (deviceDebug->isActive(DeviceDebug::DEBUG)) {

			deviceDebug->printf("DeviceWiFi", "load", "hostName: %s\n", _hostName);
			deviceDebug->printf("DeviceWiFi", "load", "stationMacAddress: %s\n", _stationMacAddress);
			deviceDebug->printf("DeviceWiFi", "load", "softAPMacAddress: %s\n", _softAPMacAddress);
			deviceDebug->printf("DeviceWiFi", "load", "publishIntervalInMilliSeconds: %d\n", (char*)_publishIntervalInMilliSeconds);

			deviceDebug->print("DeviceWiFi", "load", "end\n");
		}		
	}

	char* DeviceWiFi::getStationMacAddress() const
	{
		return (_stationMacAddress);
	}

	char* DeviceWiFi::getSoftAPMacAddress() const
	{
		return (_softAPMacAddress);
	}

	char* DeviceWiFi::getHostName() const
	{
		return (_hostName);
	}

	void DeviceWiFi::setHostName(char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceWiFi", "setHostName", "Parse failed: %s\n", json);
			return;
		}
		char* value = strdup(root["value"]);
		_hostName = new char(sizeof(strlen(value)));
		_hostName = value;
	}

	long DeviceWiFi::getPublishIntervalInMilliSeconds()
	{
		return _publishIntervalInMilliSeconds;
	}

	void DeviceWiFi::setPublishIntervalInMilliSeconds(char* json)
	{
		StaticJsonBuffer<200> jsonBuffer;
		JsonObject& root = jsonBuffer.parseObject(json);
		if (!root.success()) {
			printf("DeviceWiFi", "setPublishIntervalInMilliSeconds", "Parse failed: %s\n", json);
			return;
		}
		_publishIntervalInMilliSeconds = root["value"].as<int>();
	}

	bool DeviceWiFi::publish()
	{
		uint64_t now = millis();

		if (now - _publishIntervalTimestamp > _publishIntervalInMilliSeconds) {
			_publishIntervalTimestamp = now;
		}
		else {
			return false;
		}
				
		DynamicJsonBuffer jsonBuffer;
		JsonObject& root = jsonBuffer.createObject();

		root["deviceId"] = _espDevice->getDeviceId();
		root["deviceDatasheetId"] = _espDevice->getDeviceDatasheetId();
		root["wifiQuality"] = _espDevice->getDeviceWiFi()->getQuality();
		root["epochTimeUtc"] = _espDevice->getDeviceNTP()->getEpochTimeUTC();
		root["localIPAddress"] = _espDevice->getDeviceWiFi()->getLocalIPAddress();

		int messageJsonLen = root.measureLength();
		char messageJson[messageJsonLen + 1];

		root.printTo(messageJson, sizeof(messageJson));

		Serial.print("DeviceWiFi enviando para o servidor (Char Len)=> ");
		Serial.println(messageJsonLen);

		_espDevice->getDeviceMQ()->publishInApplication(DEVICE_WIFI_MESSAGE_TOPIC_PUB, messageJson);

		return true;
	}

	void DeviceWiFi::addParameter(DeviceWiFiParameter *p) {
		if (_paramsCount + 1 > DEVICE_WIFI_MAX_PARAMS)
		{
			//Max parameters exceeded!
			DEBUG_WM("DEVICE_WIFI_MAX_PARAMS exceeded, increase number (in DeviceWiFi.h) before adding more parameters!");
			DEBUG_WM("Skipping parameter with ID:");
			DEBUG_WM(p->getID());
			return;
		}
		_params[_paramsCount] = p;
		_paramsCount++;
		DEBUG_WM("Adding parameter");
		DEBUG_WM(p->getID());
	}

	void DeviceWiFi::setupConfigPortal() {
		dnsServer.reset(new DNSServer());
		server.reset(new ESP8266WebServer(80));

		DEBUG_WM(F(""));
		_configPortalStart = millis();

		DEBUG_WM(F("Configuring access point... "));
		DEBUG_WM(_apName);
		if (_apPassword != NULL) {
			if (strlen(_apPassword) < 8 || strlen(_apPassword) > 63) {
				// fail passphrase to short or long!
				DEBUG_WM(F("Invalid AccessPoint password. Ignoring - len < 8 ou > 63"));
				_apPassword = NULL;
			}
			DEBUG_WM(_apPassword);
		}

		//optional soft ip config
		if (_ap_static_ip) {
			DEBUG_WM(F("Custom AP IP/GW/Subnet"));
			WiFi.softAPConfig(_ap_static_ip, _ap_static_gw, _ap_static_sn);
		}

		if (_apPassword != NULL) {
			WiFi.softAP(_apName, _apPassword);//password option
		}
		else {
			WiFi.softAP(_apName);
		}

		delay(500); // Without delay I've seen the IP address blank
		DEBUG_WM(F("AP IP address: "));
		DEBUG_WM(WiFi.softAPIP());

		/* Setup the DNS server redirecting all the domains to the apIP */
		dnsServer->setErrorReplyCode(DNSReplyCode::NoError);
		dnsServer->start(DNS_PORT, "*", WiFi.softAPIP());

		/* Setup web pages: root, wifi config pages, SO captive portal detectors and not found. */
		server->on("/", std::bind(&DeviceWiFi::handleRoot, this));
		server->on("/wifi", std::bind(&DeviceWiFi::handleWifi, this, true));
		server->on("/0wifi", std::bind(&DeviceWiFi::handleWifi, this, false));
		server->on("/wifisave", std::bind(&DeviceWiFi::handleWifiSave, this));
		server->on("/i", std::bind(&DeviceWiFi::handleInfo, this));
		server->on("/r", std::bind(&DeviceWiFi::handleReset, this));
		//server->on("/generate_204", std::bind(&DeviceWiFi::handle204, this));  //Android/Chrome OS captive portal check.
		server->on("/fwlink", std::bind(&DeviceWiFi::handleRoot, this));  //Microsoft captive portal. Maybe not needed. Might be handled by notFound handler.
		server->onNotFound(std::bind(&DeviceWiFi::handleNotFound, this));
		server->begin(); // Web server start
		DEBUG_WM(F("HTTP server started"));

	}

	//boolean DeviceWiFi::autoConnect() {
	//  String ssid = "ESP" + String(ESP.getChipId());
	//  return autoConnect(ssid.c_str(), NULL);
	//}

	void DeviceWiFi::autoConnect() {

		DEBUG_WM(F(""));
		DEBUG_WM(F("AR AutoConnect"));

		char const *apName = "ART";
		char const *apPassword = "12345678";

		// read eeprom for ssid and pass
		_ssid = getSSID();
		_pass = getPassword();

		//se já está conectado a rede WI-FI, nada é feito. 
		//Caso contrário, é efetuado uma tentativa de conexão
		if (firstAutoConnect) {

			firstAutoConnect = false;

			bool resetWifiSettings = digitalRead(_pin);

			if (resetWifiSettings) {

				Serial.println("reset mode");

				resetSettings();

				//problema: quando passa no metodo o password altera
				//char const *rdnPassword = generatePassword(8);
				//startConfigPortal(apName, "12345678");

				startConfigPortal(apName, apPassword);
			}
		}


		if (WiFi.status() == WL_CONNECTED) {
			int rssi = WiFi.RSSI();
			int quality = getRSSIasQuality(rssi);
			int bars = convertQualitytToBarsSignal(quality);
			String debugMessage = "Wifi Connect => quality: " + String(quality) + "  bars: " + String(bars);
			DEBUG_WM(debugMessage.c_str());
			return;
		}

		// attempt to connect; should it fail, fall back to AP	
		connectWifi("", "");
		int connectionResult = WiFi.status();
		if (connectionResult != WL_CONNECTED) {
			String debugMessage = "Failed to connect: connectionResult: " + String(connectionResult);
			DEBUG_WM(debugMessage);
		}
	}

	boolean DeviceWiFi::configPortalHasTimeout() {
		if (_configPortalTimeout == 0 || wifi_softap_get_station_num() > 0) {
			_configPortalStart = millis(); // kludge, bump configportal start time to skew timeouts
			return false;
		}
		return (millis() > _configPortalStart + _configPortalTimeout);
	}

	boolean DeviceWiFi::startConfigPortal() {
		String ssid = "ESP" + String(ESP.getChipId());
		return startConfigPortal(ssid.c_str(), NULL);
	}

	boolean  DeviceWiFi::startConfigPortal(char const *apName, char const *apPassword) {
		//setup AP
		WiFi.mode(WIFI_AP_STA);
		DEBUG_WM("SET AP STA");

		_apName = apName;
		_apPassword = apPassword;

		//notify we entered AP mode
		if (_startConfigPortalCallback != NULL) {
			_startConfigPortalCallback();
		}

		connect = false;
		setupConfigPortal();

		while (1) {

			// check if timeout
			if (configPortalHasTimeout()) break;

			//DNS
			dnsServer->processNextRequest();
			//HTTP
			server->handleClient();


			if (connect) {
				connect = false;
				delay(2000);
				DEBUG_WM(F("Connecting to new AP"));
				if (_connectingConfigPortalCallback != NULL) {
					_connectingConfigPortalCallback();
				}
				// using user-provided  _ssid, _pass in place of system-stored ssid and pass

				int connectionResult = connectWifi(_ssid, _pass);

				if (connectionResult != WL_CONNECTED) {
					DEBUG_WM(F("Failed to connect."));
					if (_failedConfigPortalCallback != NULL) {
						_failedConfigPortalCallback(connectionResult);
					}
					WiFi.mode(WIFI_AP_STA);
				}
				else {
					DEBUG_WM(F("Connected."));
					if (_successConfigPortalCallback != NULL) {
						_successConfigPortalCallback();
					}
					break;
				}
			}
			yield();
		}

		server.reset();
		dnsServer.reset();

		return  WiFi.status() == WL_CONNECTED;
	}


	int DeviceWiFi::connectWifi(String ssid, String pass) {
		DEBUG_WM(F("Connecting as wifi client..."));

		WiFi.mode(WIFI_STA);

		// check if we've got static_ip settings, if we do, use those.
		if (_sta_static_ip) {
			DEBUG_WM(F("Custom STA IP/GW/Subnet"));
			WiFi.config(_sta_static_ip, _sta_static_gw, _sta_static_sn);
			DEBUG_WM(WiFi.localIP());
		}
		//fix for auto connect racing issue
		if (WiFi.status() == WL_CONNECTED) {
			DEBUG_WM("Already connected. Bailing out.");
			return WL_CONNECTED;
		}
		//check if we have ssid and pass and force those, if not, try with last saved values
		if (ssid != "") {
			WiFi.begin(ssid.c_str(), pass.c_str());
		}
		else {
			if (WiFi.SSID()) {
				DEBUG_WM("Using last saved values, should be faster");
				//trying to fix connection in progress hanging
				ETS_UART_INTR_DISABLE();
				wifi_station_disconnect();
				ETS_UART_INTR_ENABLE();

				WiFi.begin();
			}
			else {
				DEBUG_WM("No saved credentials");
			}
		}

		int connRes = waitForConnectResult();
		DEBUG_WM("Connection result: ");
		DEBUG_WM(connRes);
		//not connected, WPS enabled, no pass - first attempt
		if (_tryWPS && connRes != WL_CONNECTED && pass == "") {
			startWPS();
			//should be connected at the end of WPS
			connRes = waitForConnectResult();
		}
		return connRes;
	}

	uint8_t DeviceWiFi::waitForConnectResult() {
		if (_connectTimeout == 0) {
			return WiFi.waitForConnectResult();
		}
		else {
			DEBUG_WM(F("Waiting for connection result with time out"));
			unsigned long start = millis();
			boolean keepConnecting = true;
			uint8_t status;
			while (keepConnecting) {
				status = WiFi.status();
				if (millis() > start + _connectTimeout) {
					keepConnecting = false;
					DEBUG_WM(F("Connection timed out"));
				}
				if (status == WL_CONNECTED || status == WL_CONNECT_FAILED) {
					keepConnecting = false;
				}
				delay(100);
			}
			return status;
		}
	}

	void DeviceWiFi::startWPS() {
		DEBUG_WM("START WPS");
		WiFi.beginWPSConfig();
		DEBUG_WM("END WPS");
	}

	String DeviceWiFi::getSSID() {
		if (_ssid == "") {
			DEBUG_WM(F("Reading SSID"));
			_ssid = WiFi.SSID();
			DEBUG_WM(F("SSID: "));
			DEBUG_WM(_ssid);
		}
		return _ssid;
	}

	String DeviceWiFi::getPassword() {
		if (_pass == "") {
			DEBUG_WM(F("Reading Password"));
			_pass = WiFi.psk();
			DEBUG_WM("Password: " + _pass);
			//DEBUG_WM(_pass);
		}
		return _pass;
	}


	const char *DeviceWiFi::generatePassword(const int len) {
		char arr[] = { 'a', 'b','c', 'd', 'e','f', 'g', 'h', 'i', 'j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','0','1','2','3','4','5','6','7','8','9' };
		const byte arrLength = sizeof(arr) / sizeof(arr[0]);
		char result[len + 1];  //allow 1 extra for the NULL	
		for (int n = 0; n < len; n++)
		{
			result[n] = arr[random(0, arrLength)];
			result[n + 1] = '\0';
		}
		return result;
	}

	String DeviceWiFi::getConfigPortalSSID() {
		return _apName;
	}

	String DeviceWiFi::getConfigPortalPwd() {
		return _apPassword;
	}

	void DeviceWiFi::resetSettings() {
		DEBUG_WM(F("settings invalidated"));
		DEBUG_WM(F("THIS MAY CAUSE AP NOT TO START UP PROPERLY. YOU NEED TO COMMENT IT OUT AFTER ERASING THE DATA."));
		WiFi.disconnect(true);
		//delay(200);
	}
	void DeviceWiFi::setTimeout(unsigned long seconds) {
		setConfigPortalTimeout(seconds);
	}

	void DeviceWiFi::setConfigPortalTimeout(unsigned long seconds) {
		_configPortalTimeout = seconds * 1000;
	}

	void DeviceWiFi::setConnectTimeout(unsigned long seconds) {
		_connectTimeout = seconds * 1000;
	}

	void DeviceWiFi::setAPStaticIPConfig(IPAddress ip, IPAddress gw, IPAddress sn) {
		_ap_static_ip = ip;
		_ap_static_gw = gw;
		_ap_static_sn = sn;
	}

	void DeviceWiFi::setSTAStaticIPConfig(IPAddress ip, IPAddress gw, IPAddress sn) {
		_sta_static_ip = ip;
		_sta_static_gw = gw;
		_sta_static_sn = sn;
	}

	void DeviceWiFi::setMinimumSignalQuality(int quality) {
		_minimumQuality = quality;
	}

	/** Handle root or redirect to captive portal */
	void DeviceWiFi::handleRoot() {
		DEBUG_WM(F("Handle root"));
		if (captivePortal()) { // If caprive portal redirect instead of displaying the page.
			return;
		}

		String page = FPSTR(HTTP_HEAD);
		page.replace("{v}", "Options");
		page += FPSTR(HTTP_SCRIPT);
		page += FPSTR(HTTP_STYLE);
		page += _customHeadElement;
		page += FPSTR(HTTP_HEAD_END);
		page += "<h1>";
		page += _apName;
		page += "</h1>";
		page += F("<h3>DeviceWiFi</h3>");
		page += FPSTR(HTTP_PORTAL_OPTIONS);
		page += FPSTR(HTTP_END);

		server->sendHeader("Content-Length", String(page.length()));
		server->send(200, "text/html", page);

	}

	/** Wifi config page handler */
	void DeviceWiFi::handleWifi(boolean scan) {

		String page = FPSTR(HTTP_HEAD);
		page.replace("{v}", "Config ESP");
		page += FPSTR(HTTP_SCRIPT);
		page += FPSTR(HTTP_STYLE);
		page += _customHeadElement;
		page += FPSTR(HTTP_HEAD_END);

		if (scan) {
			int n = WiFi.scanNetworks();
			DEBUG_WM(F("Scan done"));
			if (n == 0) {
				DEBUG_WM(F("No networks found"));
				page += F("No networks found. Refresh to scan again.");
			}
			else {

				//sort networks
				int indices[n];
				for (int i = 0; i < n; i++) {
					indices[i] = i;
				}

				// RSSI SORT

				// old sort
				for (int i = 0; i < n; i++) {
					for (int j = i + 1; j < n; j++) {
						if (WiFi.RSSI(indices[j]) > WiFi.RSSI(indices[i])) {
							std::swap(indices[i], indices[j]);
						}
					}
				}

				/*std::sort(indices, indices + n, [](const int & a, const int & b) -> bool
				{
				return WiFi.RSSI(a) > WiFi.RSSI(b);
				});*/

				// remove duplicates ( must be RSSI sorted )
				if (_removeDuplicateAPs) {
					String cssid;
					for (int i = 0; i < n; i++) {
						if (indices[i] == -1) continue;
						cssid = WiFi.SSID(indices[i]);
						for (int j = i + 1; j < n; j++) {
							if (cssid == WiFi.SSID(indices[j])) {
								DEBUG_WM("DUP AP: " + WiFi.SSID(indices[j]));
								indices[j] = -1; // set dup aps to index -1
							}
						}
					}
				}

				//display networks in page
				for (int i = 0; i < n; i++) {
					if (indices[i] == -1) continue; // skip dups
					DEBUG_WM(WiFi.SSID(indices[i]));
					DEBUG_WM(WiFi.RSSI(indices[i]));
					int quality = getRSSIasQuality(WiFi.RSSI(indices[i]));

					if (_minimumQuality == -1 || _minimumQuality < quality) {
						String item = FPSTR(HTTP_ITEM);
						String rssiQ;
						rssiQ += quality;
						item.replace("{v}", WiFi.SSID(indices[i]));
						item.replace("{r}", rssiQ);
						if (WiFi.encryptionType(indices[i]) != ENC_TYPE_NONE) {
							item.replace("{i}", "l");
						}
						else {
							item.replace("{i}", "");
						}
						//DEBUG_WM(item);
						page += item;
						delay(0);
					}
					else {
						DEBUG_WM(F("Skipping due to quality"));
					}

				}
				page += "<br/>";
			}
		}

		page += FPSTR(HTTP_FORM_START);
		char parLength[5];
		// add the extra parameters to the form
		for (int i = 0; i < _paramsCount; i++) {
			if (_params[i] == NULL) {
				break;
			}

			String pitem = FPSTR(HTTP_FORM_PARAM);
			if (_params[i]->getID() != NULL) {
				pitem.replace("{i}", _params[i]->getID());
				pitem.replace("{n}", _params[i]->getID());
				pitem.replace("{p}", _params[i]->getPlaceholder());
				snprintf(parLength, 5, "%d", _params[i]->getValueLength());
				pitem.replace("{l}", parLength);
				pitem.replace("{v}", _params[i]->getValue());
				pitem.replace("{c}", _params[i]->getCustomHTML());
			}
			else {
				pitem = _params[i]->getCustomHTML();
			}

			page += pitem;
		}
		if (_params[0] != NULL) {
			page += "<br/>";
		}

		if (_sta_static_ip) {

			String item = FPSTR(HTTP_FORM_PARAM);
			item.replace("{i}", "ip");
			item.replace("{n}", "ip");
			item.replace("{p}", "Static IP");
			item.replace("{l}", "15");
			item.replace("{v}", _sta_static_ip.toString());

			page += item;

			item = FPSTR(HTTP_FORM_PARAM);
			item.replace("{i}", "gw");
			item.replace("{n}", "gw");
			item.replace("{p}", "Static Gateway");
			item.replace("{l}", "15");
			item.replace("{v}", _sta_static_gw.toString());

			page += item;

			item = FPSTR(HTTP_FORM_PARAM);
			item.replace("{i}", "sn");
			item.replace("{n}", "sn");
			item.replace("{p}", "Subnet");
			item.replace("{l}", "15");
			item.replace("{v}", _sta_static_sn.toString());

			page += item;

			page += "<br/>";
		}

		page += FPSTR(HTTP_FORM_END);
		page += FPSTR(HTTP_SCAN_LINK);

		page += FPSTR(HTTP_END);

		server->sendHeader("Content-Length", String(page.length()));
		server->send(200, "text/html", page);


		DEBUG_WM(F("Sent config page"));
	}

	/** Handle the WLAN save form and redirect to WLAN config page again */
	void DeviceWiFi::handleWifiSave() {
		DEBUG_WM(F("WiFi save"));

		//SAVE/connect here
		_ssid = server->arg("s").c_str();
		_pass = server->arg("p").c_str();

		//parameters
		for (int i = 0; i < _paramsCount; i++) {
			if (_params[i] == NULL) {
				break;
			}
			//read parameter
			String value = server->arg(_params[i]->getID()).c_str();
			//store it in array
			value.toCharArray(_params[i]->_value, _params[i]->_length);
			DEBUG_WM(F("Parameter"));
			DEBUG_WM(_params[i]->getID());
			DEBUG_WM(value);
		}

		if (server->arg("ip") != "") {
			DEBUG_WM(F("static ip"));
			DEBUG_WM(server->arg("ip"));
			//_sta_static_ip.fromString(server->arg("ip"));
			String ip = server->arg("ip");
			optionalIPFromString(&_sta_static_ip, ip.c_str());
		}
		if (server->arg("gw") != "") {
			DEBUG_WM(F("static gateway"));
			DEBUG_WM(server->arg("gw"));
			String gw = server->arg("gw");
			optionalIPFromString(&_sta_static_gw, gw.c_str());
		}
		if (server->arg("sn") != "") {
			DEBUG_WM(F("static netmask"));
			DEBUG_WM(server->arg("sn"));
			String sn = server->arg("sn");
			optionalIPFromString(&_sta_static_sn, sn.c_str());
		}

		String page = FPSTR(HTTP_HEAD);
		page.replace("{v}", "Credentials Saved");
		page += FPSTR(HTTP_SCRIPT);
		page += FPSTR(HTTP_STYLE);
		page += _customHeadElement;
		page += FPSTR(HTTP_HEAD_END);
		page += FPSTR(HTTP_SAVED);
		page += FPSTR(HTTP_END);

		server->sendHeader("Content-Length", String(page.length()));
		server->send(200, "text/html", page);

		DEBUG_WM(F("Sent wifi save page"));

		connect = true; //signal ready to connect/reset
	}

	/** Handle the info page */
	void DeviceWiFi::handleInfo() {
		DEBUG_WM(F("Info"));

		String page = FPSTR(HTTP_HEAD);
		page.replace("{v}", "Info");
		page += FPSTR(HTTP_SCRIPT);
		page += FPSTR(HTTP_STYLE);
		page += _customHeadElement;
		page += FPSTR(HTTP_HEAD_END);
		page += F("<dl>");
		page += F("<dt>Chip ID</dt><dd>");
		page += ESP.getChipId();
		page += F("</dd>");
		page += F("<dt>Flash Chip ID</dt><dd>");
		page += ESP.getFlashChipId();
		page += F("</dd>");
		page += F("<dt>IDE Flash Size</dt><dd>");
		page += ESP.getFlashChipSize();
		page += F(" bytes</dd>");
		page += F("<dt>Real Flash Size</dt><dd>");
		page += ESP.getFlashChipRealSize();
		page += F(" bytes</dd>");
		page += F("<dt>Soft AP IP</dt><dd>");
		page += WiFi.softAPIP().toString();
		page += F("</dd>");
		page += F("<dt>Soft AP MAC</dt><dd>");
		page += WiFi.softAPmacAddress();
		page += F("</dd>");
		page += F("<dt>Station MAC</dt><dd>");
		page += WiFi.macAddress();
		page += F("</dd>");
		page += F("</dl>");
		page += FPSTR(HTTP_END);

		server->sendHeader("Content-Length", String(page.length()));
		server->send(200, "text/html", page);

		DEBUG_WM(F("Sent info page"));
	}

	/** Handle the reset page */
	void DeviceWiFi::handleReset() {
		DEBUG_WM(F("Reset"));

		String page = FPSTR(HTTP_HEAD);
		page.replace("{v}", "Info");
		page += FPSTR(HTTP_SCRIPT);
		page += FPSTR(HTTP_STYLE);
		page += _customHeadElement;
		page += FPSTR(HTTP_HEAD_END);
		page += F("Module will reset in a few seconds.");
		page += FPSTR(HTTP_END);

		server->sendHeader("Content-Length", String(page.length()));
		server->send(200, "text/html", page);

		DEBUG_WM(F("Sent reset page"));
		delay(5000);
		ESP.reset();
		delay(2000);
	}

	void DeviceWiFi::handleNotFound() {
		if (captivePortal()) { // If captive portal redirect instead of displaying the error page.
			return;
		}
		String message = "File Not Found\n\n";
		message += "URI: ";
		message += server->uri();
		message += "\nMethod: ";
		message += (server->method() == HTTP_GET) ? "GET" : "POST";
		message += "\nArguments: ";
		message += server->args();
		message += "\n";

		for (uint8_t i = 0; i < server->args(); i++) {
			message += " " + server->argName(i) + ": " + server->arg(i) + "\n";
		}
		server->sendHeader("Cache-Control", "no-cache, no-store, must-revalidate");
		server->sendHeader("Pragma", "no-cache");
		server->sendHeader("Expires", "-1");
		server->sendHeader("Content-Length", String(message.length()));
		server->send(404, "text/plain", message);
	}


	/** Redirect to captive portal if we got a request for another domain. Return true in that case so the page handler do not try to handle the request again. */
	boolean DeviceWiFi::captivePortal() {
		if (!isIp(server->hostHeader())) {
			DEBUG_WM(F("Request redirected to captive portal"));
			server->sendHeader("Location", String("http://") + toStringIp(server->client().localIP()), true);
			server->send(302, "text/plain", ""); // Empty content inhibits Content-length header so we have to close the socket ourselves.
			server->client().stop(); // Stop is needed because we sent no content length

									 //notify we entered captive portal
			if (_captivePortalCallback != NULL) {
				_captivePortalCallback(toStringIp(_ap_static_ip));
			}

			return true;
		}
		return false;
	}

	DeviceWiFi& DeviceWiFi::setStartConfigPortalCallback(DEVICE_WIFI_SET_START_CONFIG_PORTAL_CALLBACK_SIGNATURE callback) {
		this->_startConfigPortalCallback = callback;
		return *this;
	}

	DeviceWiFi& DeviceWiFi::setCaptivePortalCallback(DEVICE_WIFI_SET_CAPTIVE_PORTAL_CALLBACK_SIGNATURE callback) {
		this->_captivePortalCallback = callback;
		return *this;
	}

	DeviceWiFi& DeviceWiFi::setSuccessConfigPortalCallback(DEVICE_WIFI_SET_SUCCESS_CONFIG_PORTAL_CALLBACK_SIGNATURE callback) {
		this->_successConfigPortalCallback = callback;
		return *this;
	}

	DeviceWiFi& DeviceWiFi::setFailedConfigPortalCallback(DEVICE_WIFI_SET_FAILED_CONFIG_PORTAL_CALLBACK_SIGNATURE callback) {
		this->_failedConfigPortalCallback = callback;
		return *this;
	}

	DeviceWiFi& DeviceWiFi::setConnectingConfigPortalCallback(DEVICE_WIFI_SET_CONNECTING_CONFIG_PORTAL_CALLBACK_SIGNATURE callback) {
		this->_connectingConfigPortalCallback = callback;
		return *this;
	}

	//sets a custom element to add to head, like a new style tag
	void DeviceWiFi::setCustomHeadElement(const char* element) {
		_customHeadElement = element;
	}

	//if this is true, remove duplicated Access Points - defaut true
	void DeviceWiFi::setRemoveDuplicateAPs(boolean removeDuplicates) {
		_removeDuplicateAPs = removeDuplicates;
	}

	template <typename Generic>
	void DeviceWiFi::DEBUG_WM(Generic text) {
		Serial.print("*WM: ");
		Serial.println(text);
	}

	int DeviceWiFi::getRSSIasQuality(int rssi) {
		int quality = 0;

		if (rssi <= -100) {
			quality = 0;
		}
		else if (rssi >= -50) {
			quality = 100;
		}
		else {
			quality = 2 * (rssi + 100);
		}
		return quality;
	}

	int DeviceWiFi::getQuality() {
		int rssi = WiFi.RSSI();
		return getRSSIasQuality(rssi);
	}

	bool DeviceWiFi::isConnected() {
		return WiFi.status() == WL_CONNECTED;
	}

	int DeviceWiFi::convertQualitytToBarsSignal(int quality) {

		// 4. Good quality:     > 90%
		// 3. Hight quality:    > 60%
		// 2. Medium quality:   > 30%
		// 1. Low quality:      > 9% 
		// 0. Unusable quality: > 0% < 8%

		int bars;

		if (quality >= 90) {
			bars = 4;
		}
		else if (quality >= 60 & quality < 90) {
			bars = 3;
		}
		else if (quality >= 30 & quality < 60) {
			bars = 2;
		}
		else if (quality >= 9 & quality < 30) {
			bars = 1;
		}
		else {
			bars = 0;
		}

		return bars;
	}

	String DeviceWiFi::getLocalIPAddress()
	{
		return this->toStringIp(WiFi.localIP());
	}

	/** Is this an IP? */
	boolean DeviceWiFi::isIp(String str) {
		for (int i = 0; i < str.length(); i++) {
			int c = str.charAt(i);
			if (c != '.' && (c < '0' || c > '9')) {
				return false;
			}
		}
		return true;
	}

	/** IP to String? */
	String DeviceWiFi::toStringIp(IPAddress ip) {
		String res = "";
		for (int i = 0; i < 3; i++) {
			res += String((ip >> (8 * i)) & 0xFF) + ".";
		}
		res += String(((ip >> 8 * 3)) & 0xFF);
		return res;
	}

	void DeviceWiFi::onDeviceMQSubscribeDeviceInApplication()
	{
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_WIFI_SET_HOST_NAME_TOPIC_SUB);
		_espDevice->getDeviceMQ()->subscribeDeviceInApplication(DEVICE_WIFI_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB);
	}

	void DeviceWiFi::onDeviceMQUnSubscribeDeviceInApplication()
	{
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_WIFI_SET_HOST_NAME_TOPIC_SUB);
		_espDevice->getDeviceMQ()->unSubscribeDeviceInApplication(DEVICE_WIFI_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB);
	}

	bool DeviceWiFi::onDeviceMQSubscription(char* topicKey, char* json)
	{
		/*if (strcmp(topicKey, DEVICE_WIFI_SET_HOST_NAME_TOPIC_SUB) == 0) {
			setHostName(json);
		}
		else if (strcmp(topicKey, DEVICE_WIFI_SET_PUBLISH_INTERVAL_IN_MILLI_SECONDS_TOPIC_SUB) == 0) {
			setPublishIntervalInMilliSeconds(json);
		}*/
	}
}