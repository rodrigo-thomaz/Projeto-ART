namespace ART.Security.Repository.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RefreshToken
    {
        #region Properties

        [Required]
        [MaxLength(50)]
        public string ClientId
        {
            get; set;
        }

        public DateTime ExpiresUtc
        {
            get; set;
        }

        [Key]
        public string Id
        {
            get; set;
        }

        public DateTime IssuedUtc
        {
            get; set;
        }

        [Required]
        public string ProtectedTicket
        {
            get; set;
        }

        [Required]
        [MaxLength(50)]
        public string Subject
        {
            get; set;
        }

        #endregion Properties
    }
}