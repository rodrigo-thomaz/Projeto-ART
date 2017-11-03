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

            //HardwareInApplicationId
            Property(x => x.HardwareInApplicationId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute("IX_Unique_HardwareInApplicationId_ProjectId", 0) { IsUnique = true },
                    }));

            //ProjectId
            Property(x => x.ProjectId)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute("IX_Unique_HardwareInApplicationId_ProjectId", 1) { IsUnique = true },
                    }));

            //HardwareInApplication
            HasRequired(x => x.HardwareInApplication)
                .WithMany(x => x.HardwaresInProject)
                .HasForeignKey(x => x.HardwareInApplicationId)
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