namespace ART.Domotica.WebApi.App_Start
{
    using ART.Domotica.WebApi.AutoMapper;

    using global::AutoMapper;

    public class AutoMapperConfig
    {
        #region Methods

        public static void RegisterMappings()
        {
            Mapper.Initialize(x => x.AddProfile(new DSFamilyTempSensorProfile()));
        }

        #endregion Methods
    }
}