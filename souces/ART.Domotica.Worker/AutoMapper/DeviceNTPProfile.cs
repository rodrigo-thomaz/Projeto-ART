namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DeviceNTPProfile : Profile
    {
        #region Constructors

        public DeviceNTPProfile()
        {
            CreateMap<DeviceNTP, DeviceNTPDetailModel>();
        }

        #endregion Constructors
    }
}