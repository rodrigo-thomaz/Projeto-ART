namespace ART.Seguranca.DistributedServices
{
    using ART.Seguranca.Domain.AutoMapper;

    using AutoMapper;

    public class AutoMapperConfig
    {
        #region Methods

        public static void RegisterMappings()
        {
            Mapper.Initialize(x => x.AddProfile(new ApplicationUserProfile()));
        }

        #endregion Methods
    }
}