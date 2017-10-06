namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class SensorsInDeviceConfiguration : EntityTypeConfiguration<SensorsInDevice>
    {
        #region Constructors

        public SensorsInDeviceConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.SensorBaseId,
                x.DeviceBaseId,
            });

            //SensorBaseId
            Property(x => x.SensorBaseId)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //DeviceBaseId
            Property(x => x.DeviceBaseId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //SensorBase
            HasRequired(x => x.SensorBase)
                .WithMany(x => x.SensorsInDevice)
                .HasForeignKey(x => x.SensorBaseId)
                .WillCascadeOnDelete(false);

            //DeviceBase
            HasRequired(x => x.DeviceBase)
                .WithMany(x => x.SensorsInDevice)
                .HasForeignKey(x => x.DeviceBaseId)
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}