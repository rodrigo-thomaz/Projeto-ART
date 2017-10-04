namespace ART.Seguranca.DistributedServices.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ExternalLoginViewModel
    {
        #region Properties

        public string Name
        {
            get; set;
        }

        public string State
        {
            get; set;
        }

        public string Url
        {
            get; set;
        }

        #endregion Properties
    }

    public class ParsedExternalAccessToken
    {
        #region Properties

        public string app_id
        {
            get; set;
        }

        public string user_id
        {
            get; set;
        }

        #endregion Properties
    }

    public class RegisterExternalBindingModel
    {
        #region Properties

        [Required]
        public string ExternalAccessToken
        {
            get; set;
        }

        [Required]
        public string Provider
        {
            get; set;
        }

        [Required]
        public string UserName
        {
            get; set;
        }

        #endregion Properties
    }
}