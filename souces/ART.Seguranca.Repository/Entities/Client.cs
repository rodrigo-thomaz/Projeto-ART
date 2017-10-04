namespace ART.Seguranca.Repository.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Client
    {
        #region Properties

        public bool Active
        {
            get; set;
        }

        [MaxLength(100)]
        public string AllowedOrigin
        {
            get; set;
        }

        public ApplicationTypes ApplicationType
        {
            get; set;
        }

        [Key]
        public string Id
        {
            get; set;
        }

        [Required]
        [MaxLength(100)]
        public string Name
        {
            get; set;
        }

        public int RefreshTokenLifeTime
        {
            get; set;
        }

        [Required]
        public string Secret
        {
            get; set;
        }

        #endregion Properties
    }
}