#include <TemperatureSensorManager.h>

TemperatureSensorManager temperatureSensorManager;

void setup(void)
{ 
  // start serial port
  Serial.begin(9600);
  Serial.println("Dallas Temperature IC Control Library Demo");

  temperatureSensorManager.begin();  
}

// function to print a device address
void printAddress(byte deviceAddress[8])
{
  for (uint8_t i = 0; i < 8; i++)
  {
    // zero pad the address if necessary
    if (deviceAddress[i] < 16) Serial.print("0");
    Serial.print(deviceAddress[i], HEX);
  }
}

void printData(TemperatureSensor temperatureSensor)
{
  Serial.print("Address: "); 
  printAddress(temperatureSensor.deviceAddress); 
  Serial.print(" ValidFamily: "); 
  Serial.print(temperatureSensor.validFamily); 
  Serial.print(" Family: "); 
  Serial.print(temperatureSensor.family); 
  Serial.print(" Connected: "); 
  Serial.print(temperatureSensor.isConnected); 
  Serial.print(" Resolution: "); 
  Serial.print(temperatureSensor.resolution); 
  Serial.print(" Temp C: ");
  Serial.print(temperatureSensor.tempCelsius);
  Serial.print(" Temp F: ");
  Serial.print(temperatureSensor.tempFahrenheit);
  Serial.print(" HasAlarm: ");
  Serial.print(temperatureSensor.hasAlarm);
  Serial.print(" LowAlarmTemp: ");
  Serial.print(temperatureSensor.lowAlarmTemp);
  Serial.print(" HighAlarmTemp: ");
  Serial.print(temperatureSensor.highAlarmTemp);
  Serial.println();
}

void loop(void)
{ 
  TemperatureSensor *arr = temperatureSensorManager.getSensors();  
  for(int i = 0; i < sizeof(arr)/sizeof(int); ++i){
    printData(arr[i]);
  }  
}

