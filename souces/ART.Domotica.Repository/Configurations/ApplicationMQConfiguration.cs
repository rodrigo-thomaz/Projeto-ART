namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class ApplicationMQConfiguration : EntityTypeConfiguration<ApplicationMQ>
    {
        #region Constructors

        public ApplicationMQConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //Application
            HasRequired(x => x.Application)
               .WithRequiredDependent(x => x.ApplicationMQ);

            //User
            Property(x => x.User)
                .HasColumnOrder(1)
                .HasMaxLength(12)
                .IsRequired();
                //.HasColumnAnnotation(IndexAnnotation.AnnotationName,
                //    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //Password
            Property(x => x.Password)
                .HasColumnOrder(2)
                .HasMaxLength(12)
                .IsRequired();
                //.HasColumnAnnotation(IndexAnnotation.AnnotationName,
                //    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //Topic
            Property(x => x.Topic)
                .HasColumnOrder(3)
                .HasMaxLength(32)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));
        }

        #endregion Constructors
    }
}