namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class DeviceWiFiConfiguration : EntityTypeConfiguration<DeviceWiFi>
    {
        #region Constructors

        public DeviceWiFiConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.Id,
                x.DeviceDatasheetId,
            });

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //DeviceDatasheetId
            Property(x => x.DeviceDatasheetId)
                .HasColumnOrder(1)
                .IsRequired();

            //DeviceBase
            HasRequired(x => x.DeviceBase)
               .WithRequiredDependent(x => x.DeviceWiFi);

            //StationMacAddress
            Property(x => x.StationMacAddress)
                .HasColumnOrder(2)
                .HasMaxLength(17)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new[]{
                        new IndexAttribute { IsUnique = true }, // MacAddress é único mas mutável via código
                    }));

            //SoftAPMacAddress
            Property(x => x.SoftAPMacAddress)
                .HasColumnOrder(3)
                .HasMaxLength(17)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new[]{
                        new IndexAttribute { IsUnique = true }, // MacAddress é único mas mutável via código
                    }));

            //HostName
            Property(x => x.HostName)
                .HasColumnOrder(4)
                .HasMaxLength(255)
                .IsRequired();
        }

        #endregion Constructors
    }
}