using System.ComponentModel.DataAnnotations;

namespace RThomaz.Infra.CrossCutting.Identity.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}