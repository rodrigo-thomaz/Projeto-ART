namespace ART.Domotica.Repository.Configurations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class HardwareInApplicationConfiguration : EntityTypeConfiguration<HardwareInApplication>
    {
        #region Constructors

        public HardwareInApplicationConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.ApplicationId,
                x.DeviceId,
            });

            //ApplicationId
            Property(x => x.ApplicationId)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //Application
            HasRequired(x => x.Application)
                .WithMany(x => x.DevicesInApplication)
                .HasForeignKey(x => x.ApplicationId)
                .WillCascadeOnDelete(false);

            //HardwareId
            Property(x => x.DeviceId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute ("IX_Unique_HardwareId") { IsUnique = true },
                    }));

            //HardwareBase
            HasRequired(x => x.DeviceBase)
                .WithMany(x => x.DevicesInApplication)
                .HasForeignKey(x => x.DeviceId)
                .WillCascadeOnDelete(false);

            //CreateDate
            Property(x => x.CreateDate)
                .HasColumnOrder(2)
                .IsRequired();

            //CreateByApplicationUserId
            Property(x => x.CreateByApplicationUserId)
                .HasColumnOrder(3);

            //CreateByApplicationUser
            HasRequired(x => x.CreateByApplicationUser)
                .WithMany()
                .HasForeignKey(x => x.CreateByApplicationUserId)
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}