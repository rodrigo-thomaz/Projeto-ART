namespace ART.Domotica.Worker.AutoMapper.Globalization
{
    using ART.Domotica.Model.Globalization;
    using ART.Domotica.Repository.Entities.Globalization;

    using global::AutoMapper;

    public class TimeZoneProfile : Profile
    {
        #region Constructors

        public TimeZoneProfile()
        {
            CreateMap<TimeZone, TimeZoneGetModel>()
                .ForMember(vm => vm.TimeZoneId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DisplayName, m => m.MapFrom(x => x.DisplayName))
                .ForMember(vm => vm.SupportsDaylightSavingTime, m => m.MapFrom(x => x.SupportsDaylightSavingTime))
                .ForMember(vm => vm.UtcTimeOffsetInSecond, m => m.MapFrom(x => x.UtcTimeOffsetInSecond));
        }

        #endregion Constructors
    }
}