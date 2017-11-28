namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorRangeProfile : Profile
    {
        #region Constructors

        public SensorRangeProfile()
        {
            CreateMap<SensorRange, SensorRangeDetailModel>()
                .ForMember(vm => vm.SensorRangeId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Max, m => m.MapFrom(x => x.Max))
                .ForMember(vm => vm.Min, m => m.MapFrom(x => x.Min));
        }

        #endregion Constructors
    }
}