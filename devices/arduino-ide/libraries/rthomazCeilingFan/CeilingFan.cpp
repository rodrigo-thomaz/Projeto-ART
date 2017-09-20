/*
  CeilingFan.cpp - Library for Led Light code.
  Created by Rodrigo Thomaz, June 4, 2017.
  Released into the public domain.
*/

#include "Arduino.h"
#include "CeilingFan.h"

#include "EEPROM.h"

const String _fanDirectionNone = "none";
const String _fanDirectionForward = "forward";
const String _fanDirectionReverse = "reverse";

CeilingFan::CeilingFan(int digitalOutPinFanDirectionForward, int digitalOutPinFanDirectionReverse, int analogOutPinFanSpeed, int addressLastSpeedFanDirectionForward, int addressLastSpeedFanDirectionReverse)
{
  // prepare GPIO FanDirectionForward
  pinMode(digitalOutPinFanDirectionForward, OUTPUT);
  digitalWrite(digitalOutPinFanDirectionForward, LOW);

  // prepare GPIO FanDirectionReverse
  pinMode(digitalOutPinFanDirectionReverse, OUTPUT);
  digitalWrite(digitalOutPinFanDirectionReverse, LOW);
  
  _digitalOutPinFanDirectionForward = digitalOutPinFanDirectionForward;
  _digitalOutPinFanDirectionReverse = digitalOutPinFanDirectionReverse;
  _analogOutPinFanSpeed = analogOutPinFanSpeed;
  
  _addressLastSpeedFanDirectionForward = addressLastSpeedFanDirectionForward;
  _addressLastSpeedFanDirectionReverse = addressLastSpeedFanDirectionReverse;
}

String CeilingFan::getFanDirection()
{
  int forward = digitalRead(_digitalOutPinFanDirectionForward);
  int reverse = digitalRead(_digitalOutPinFanDirectionReverse);
  if(forward == LOW && reverse == LOW){
    return _fanDirectionNone;
  }
  else if(forward == HIGH && reverse == LOW){
    return _fanDirectionForward;
  }
  else if(forward == LOW && reverse == HIGH){
    return _fanDirectionReverse;
  }
  else{
    //erro 
  }  
}

byte CeilingFan::setFanDirection(String value)
{
  setFanDirectionIO(value);
  byte fanSpeed = getLastFanSpeed(value);
  analogWrite(_analogOutPinFanSpeed, fanSpeed);
  return fanSpeed;
}

byte CeilingFan::getFanSpeed()
{
  String fanDirection = getFanDirection();  
  return getLastFanSpeed(fanDirection);
}

void CeilingFan::setFanSpeed(String fanDirection, byte value)
{
  setFanDirectionIO(fanDirection);
  analogWrite(_analogOutPinFanSpeed, value);
  setLastFanSpeed(fanDirection, value);      
}

byte CeilingFan::getLastFanSpeed(String fanDirection){
  if (fanDirection == _fanDirectionForward)
    return EEPROM.read(_addressLastSpeedFanDirectionForward);
  else if (fanDirection == _fanDirectionReverse)
    return EEPROM.read(_addressLastSpeedFanDirectionReverse);
  else if (fanDirection == _fanDirectionNone)
    return 0;
}

void CeilingFan::setLastFanSpeed(String fanDirection, byte value){
  if (fanDirection == _fanDirectionForward)
    EEPROM.write(_addressLastSpeedFanDirectionForward, value);
  else if (fanDirection == _fanDirectionReverse)
    EEPROM.write(_addressLastSpeedFanDirectionReverse, value);  
  EEPROM.commit();
}

void CeilingFan::setFanDirectionIO(String value)
{
  if (value == _fanDirectionForward){
    digitalWrite(_digitalOutPinFanDirectionForward, HIGH);
    digitalWrite(_digitalOutPinFanDirectionReverse, LOW);        
  }
  else if(value == _fanDirectionReverse){
    digitalWrite(_digitalOutPinFanDirectionForward, LOW);
    digitalWrite(_digitalOutPinFanDirectionReverse, HIGH);
  }  
  else if(value == _fanDirectionNone){
    digitalWrite(_digitalOutPinFanDirectionForward, LOW);
    digitalWrite(_digitalOutPinFanDirectionReverse, LOW);
  }  
  else{
    //error 
  }
}
