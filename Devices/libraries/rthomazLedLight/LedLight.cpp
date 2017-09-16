/*
  LedLight.cpp - Library for Led Light code.
  Created by Rodrigo Thomaz, June 4, 2017.
  Released into the public domain.
*/

#include "Arduino.h"
#include "LedLight.h"

LedLight::LedLight(int pin)
{
  // prepare GPIO Lamp
  pinMode(pin, OUTPUT);
  digitalWrite(pin, LOW);
  
  _pin = pin;
}

bool LedLight::getPower()
{
  int value = digitalRead(_pin);
  return value == HIGH;
}

void LedLight::setPower(bool value)
{
  if (value){
    digitalWrite(_pin, HIGH);
  }
  else{
    digitalWrite(_pin, LOW);
  }  
}
