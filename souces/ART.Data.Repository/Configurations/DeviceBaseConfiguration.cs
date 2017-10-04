namespace ART.Data.Repository.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using ART.Data.Repository.Entities;

    public class DeviceBaseConfiguration : EntityTypeConfiguration<DeviceBase>
    {
        #region Constructors

        public DeviceBaseConfiguration()
        {
            ToTable("DeviceBase");

            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .IsRequired();
        }

        #endregion Constructors
    }
}