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
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            //ApplicationId
            Property(x => x.ApplicationId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute("IX_Unique_ApplicationId_DeviceBaseId", 0) { IsUnique = true },
                    }));

            //Application
            HasRequired(x => x.Application)
                .WithMany(x => x.DevicesInApplication)
                .HasForeignKey(x => x.ApplicationId)
                .WillCascadeOnDelete(false);

            //DeviceBaseId
            Property(x => x.DeviceBaseId)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute ("IX_Unique_DeviceBaseId") { IsUnique = true },
                        new IndexAttribute("IX_Unique_ApplicationId_DeviceBaseId", 1) { IsUnique = true },
                    }));

            //DeviceBase
            HasRequired(x => x.DeviceBase)
                .WithMany(x => x.DevicesInApplication)
                .HasForeignKey(x => x.DeviceBaseId)
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