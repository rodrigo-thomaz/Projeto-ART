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
                x.DeviceTypeId,
                x.DeviceDatasheetId,
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

            //DeviceTypeId
            Property(x => x.DeviceTypeId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute ("IX_Unique_DeviceKey", 0) { IsUnique = true },
                    }));

            //DeviceDatasheetId
            Property(x => x.DeviceDatasheetId)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute ("IX_Unique_DeviceKey", 1) { IsUnique = true },
                    }));

            //DeviceId
            Property(x => x.DeviceId)
                .HasColumnOrder(3)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute ("IX_Unique_DeviceKey", 2) { IsUnique = true },
                    }));

            //DeviceBase
            HasRequired(x => x.DeviceBase)
                .WithMany(x => x.DevicesInApplication)
                .HasForeignKey(x => new
                {
                    x.DeviceTypeId,
                    x.DeviceDatasheetId,
                    x.DeviceId,
                })
                .WillCascadeOnDelete(false);

            //CreateDate
            Property(x => x.CreateDate)
                .HasColumnOrder(4)
                .IsRequired();

            //CreateByApplicationUserId
            Property(x => x.CreateByApplicationUserId)
                .HasColumnOrder(5);

            //CreateByApplicationUser
            HasRequired(x => x.CreateByApplicationUser)
                .WithMany()
                .HasForeignKey(x => x.CreateByApplicationUserId)
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}