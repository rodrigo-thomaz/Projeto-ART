namespace ART.Domotica.Repository.Configurations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class HardwaresInProjectConfiguration : EntityTypeConfiguration<HardwaresInProject>
    {
        #region Constructors

        public HardwaresInProjectConfiguration()
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
                        new IndexAttribute("IX_Unique_DeviceInApplication_ProjectId", 0) { IsUnique = true },
                    }));

            //DeviceId
            Property(x => x.DeviceId)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute("IX_Unique_DeviceInApplication_ProjectId", 1) { IsUnique = true },
                    }));

            //ProjectId
            Property(x => x.ProjectId)
                .HasColumnOrder(3)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute("IX_Unique_DeviceInApplication_ProjectId", 2) { IsUnique = true },
                    }));

            //DeviceInApplication
            HasRequired(x => x.DeviceInApplication)
                .WithMany(x => x.HardwaresInProject)
                .HasForeignKey(x => new
                {
                    x.ApplicationId,
                    x.DeviceId,
                })
                .WillCascadeOnDelete(false);

            //Project
            HasRequired(x => x.Project)
                .WithMany(x => x.HardwaresInProject)
                .HasForeignKey(x => x.ProjectId)
                .WillCascadeOnDelete(false);
        }

        #endregion Constructors
    }
}