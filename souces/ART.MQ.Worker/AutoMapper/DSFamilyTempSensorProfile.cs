using ART.Data.Repository.Entities;
using ART.MQ.Worker.Models;
using AutoMapper;

namespace ART.MQ.Worker.AutoMapper
{
    public class DSFamilyTempSensorProfile : Profile
    {
        public DSFamilyTempSensorProfile()
        {
            CreateMap<DSFamilyTempSensorResolution, DSFamilyTempSensorResolutionModel>();
            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorModel>();
        }
    }
}