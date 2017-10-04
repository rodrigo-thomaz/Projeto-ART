namespace ART.MQ.DistributedServices.App_Start
{
    using ART.MQ.DistributedServices.AutoMapper;

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