using ART.Data.Repository.Entities;
using ART.MQ.Worker.Models;
using AutoMapper;

namespace ART.MQ.Worker.AutoMapper
{
    public class TemperatureScaleProfile : Profile
    {
        public TemperatureScaleProfile()
        {
            CreateMap<TemperatureScale, TemperatureScaleModel>();
        }
    }
}