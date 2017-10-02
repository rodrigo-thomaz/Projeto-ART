using ART.MQ.DistributedServices.AutoMapper;
using AutoMapper;

namespace ART.MQ.DistributedServices.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x => x.AddProfile(new DSFamilyTempSensorProfile()));
        }
    }
}