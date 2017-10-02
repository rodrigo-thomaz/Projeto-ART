using ART.MQ.Common.Contracts;
using ART.MQ.DistributedServices.Models;
using AutoMapper;

namespace ART.MQ.DistributedServices.AutoMapper
{
    public class DSFamilyTempSensorProfile : Profile
    {
        public DSFamilyTempSensorProfile()
        {
            CreateMap<DSFamilyTempSensorSetResolutionModel, DSFamilyTempSensorSetResolutionContract>();
            CreateMap<DSFamilyTempSensorSetHighAlarmModel, DSFamilyTempSensorSetHighAlarmContract>();
            CreateMap<DSFamilyTempSensorSetLowAlarmModel, DSFamilyTempSensorSetLowAlarmContract>();
        }
    }
}