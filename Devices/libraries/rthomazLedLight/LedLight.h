/*
  LedLight.h - Library for Led Light code.
  Created by Rodrigo Thomaz, June 4, 2017.
  Released into the public domain.
*/
#ifndef LedLight_h
#define LedLight_h

#include "Arduino.h"

class LedLight
{
  public:
    LedLight(int pin);
    bool getPower();
    void setPower(bool value);    
  private:
    int _pin;                     // Digital output pin that the LED is attached to
};

#endif
