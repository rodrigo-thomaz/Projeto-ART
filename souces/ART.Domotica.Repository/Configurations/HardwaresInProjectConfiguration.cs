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

            //HardwaresInApplicationId
            Property(x => x.HardwaresInApplicationId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute("IX_Unique_HardwaresInApplicationId_ProjectId", 0) { IsUnique = true },
                    }));

            //ProjectId
            Property(x => x.ProjectId)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new List<IndexAttribute>
                    {
                        new IndexAttribute("IX_Unique_HardwaresInApplicationId_ProjectId", 1) { IsUnique = true },
                    }));

            //HardwaresInApplication
            HasRequired(x => x.HardwaresInApplication)
                .WithMany(x => x.HardwaresInProject)
                .HasForeignKey(x => x.HardwaresInApplicationId)
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