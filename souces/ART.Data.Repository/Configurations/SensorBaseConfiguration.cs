namespace ART.Data.Repository.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using ART.Data.Repository.Entities;

    public class SensorBaseConfiguration : EntityTypeConfiguration<SensorBase>
    {
        #region Constructors

        public SensorBaseConfiguration()
        {
            ToTable("SensorBase");

            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .IsRequired();
        }

        #endregion Constructors
    }
}