#ifndef DisplayDeviceBinary_h
#define DisplayDeviceBinary_h

#include "Arduino.h"
#include "DisplayDevice.h"

class DisplayDeviceBinary
{
public:
	DisplayDeviceBinary(DisplayDevice& displayDevice);
	~DisplayDeviceBinary();


private:

	DisplayDevice * _displayDevice;

};

#endif