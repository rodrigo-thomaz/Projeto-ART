#ifndef DeviceTypeEnum_h
#define DeviceTypeEnum_h

namespace ART
{
	enum DeviceTypeEnum
	{
		ESPType = 1,
		RaspberryType = 2,
	};

	class DeviceTypeEnumConverter
	{
	public:

		static char* convertToString(DeviceTypeEnum deviceType) 
		{
			switch (deviceType)
			{
			case ART::ESPType:
				return "ESP";
			case ART::RaspberryType:
				return "Raspberry";
			default:
				break;
			}
		}

	private:

		
	};
}

#endif