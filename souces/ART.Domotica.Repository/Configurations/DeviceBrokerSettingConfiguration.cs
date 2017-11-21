using ART.Domotica.Repository.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace ART.Domotica.Repository.Configurations
{
    public class DeviceBrokerSettingConfiguration : EntityTypeConfiguration<DeviceBrokerSetting>
    {
        #region Constructors

        public DeviceBrokerSettingConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //DeviceBase
            HasRequired(x => x.DeviceBase)               
               .WithRequiredDependent(x => x.BrokerSetting);

            //User
            Property(x => x.User)
                .HasColumnOrder(1)
                .HasMaxLength(12)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //Password
            Property(x => x.Password)
                .HasColumnOrder(2)
                .HasMaxLength(12)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //ClientId
            Property(x => x.ClientId)
                .HasColumnOrder(3)
                .HasMaxLength(4)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));
        }

        #endregion Constructors
    }
}
