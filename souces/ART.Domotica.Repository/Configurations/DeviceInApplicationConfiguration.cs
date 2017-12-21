namespace ART.Domotica.Repository.Configurations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class DeviceInApplicationConfiguration : EntityTypeConfiguration<DeviceInApplication>
    {
        #region Constructors

        public DeviceInApplicationConfiguration()
        {
            //Primary Keys
            HasKey(x => new
            {
                x.ApplicationId,
                x.DeviceId,
                x.DeviceDatasheetId,
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

            //DeviceId
            Property(x => x.DeviceId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute ("IX_Unique_DeviceId", 0) { IsUnique = true },
                    }));

            //DeviceDatasheetId
            Property(x => x.DeviceDatasheetId)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute ("IX_Unique_DeviceId", 1) { IsUnique = true },
                    }));

            //DeviceBase
            HasRequired(x => x.DeviceBase)
                .WithMany(x => x.DevicesInApplication)
                .HasForeignKey(x => new
                {
                    x.DeviceId,
                    x.DeviceDatasheetId,
                })
                .WillCascadeOnDelete(false);

            //CreateDate
            Property(x => x.CreateDate)
                .HasColumnOrder(3)
                .IsRequired();

            //CreateByApplicationUserId
            Property(x => x.CreateByApplicationUserId)
                .HasColumnOrder(4);

            //CreateByApplicationUser
            HasRequired(x => x.CreateByApplicationUser)
                .WithMany()
                .HasForeignKey(x => x.CreateByApplicationUserId)
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}