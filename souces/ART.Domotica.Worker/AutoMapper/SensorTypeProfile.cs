namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorTypeProfile : Profile
    {
        #region Constructors

        public SensorTypeProfile()
        {
            CreateMap<SensorType, SensorTypeGetModel>()
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Name, m => m.MapFrom(x => x.Name));
        }

        #endregion Constructors
    }
}