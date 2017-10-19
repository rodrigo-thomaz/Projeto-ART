#include "NTPManager.h"
#include "Arduino.h"
#include "DebugManager.h"
#include "NTPClient.h"
#include "WiFiUdp.h"

WiFiUDP ntpUDP;

int ntpUpdateInterval = 60000;
int16_t utc = 0; //UTC

NTPClient timeClient(ntpUDP, "a.st1.ntp.br", utc * 3600, ntpUpdateInterval);

NTPManager::NTPManager(DebugManager& debugManager)
{
	this->_debugManager = &debugManager;
}

NTPManager::~NTPManager()
{
}

void NTPManager::begin()
{
	timeClient.begin();
	timeClient.update();
}

long NTPManager::getEpochTime()
{
	return timeClient.getEpochTime();
}

String NTPManager::getFormattedTime()
{
	int hours = timeClient.getHours();
	int minutes = timeClient.getMinutes();
	
	static char str[5];
	
	sprintf(str, "%02d:%02d", hours, minutes);
	
	return String(str);
}