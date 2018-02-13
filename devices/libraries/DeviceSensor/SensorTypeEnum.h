#ifndef SensorTypeEnum_h
#define SensorTypeEnum_h

namespace ART
{
	enum SensorTypeEnum
	{
		Generic = 1,
		Luminosity = 2,
		Motion = 3,
		Pressure = 4,
		ProximityDistance = 5,
		Temperature = 6,
	};

	class SensorTypeEnumConverter
	{
	public:

		static char* convertToString(SensorTypeEnum obj)
		{
			switch (obj)
			{
			case ART::Generic:
				return "Generic";
			case ART::Luminosity:
				return "Luminosity";
			case ART::Motion:
				return "Motion";
			case ART::Pressure:
				return "Pressure";
			case ART::ProximityDistance:
				return "ProximityDistance";
			case ART::Temperature:
				return "Temperature";
			default:
				break;
			}
		}

	};
}

#endif