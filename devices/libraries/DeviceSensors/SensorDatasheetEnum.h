#ifndef SensorDatasheetEnum_h
#define SensorDatasheetEnum_h

namespace ART
{
	enum SensorDatasheetEnum
	{
		// ProximityDistance = 5000 - 5999
		Ultrasonic_HCSR04 = 5001,

		// SensorType Temperature = 6000 - 6999
		Temperature_DS18B20 = 6001,
	};

	class SensorDatasheetEnumConverter
	{
	public:

		static char* convertToString(SensorDatasheetEnum obj)
		{
			switch (obj)
			{
			case ART::Ultrasonic_HCSR04:
				return "Ultrasonic_HCSR04";
			case ART::Temperature_DS18B20:
				return "Temperature_DS18B20";
			default:
				break;
			}
		}

	};
}

#endif