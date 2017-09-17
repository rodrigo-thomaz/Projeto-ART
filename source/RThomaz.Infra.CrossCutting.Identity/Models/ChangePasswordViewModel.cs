using System.ComponentModel.DataAnnotations;

namespace RThomaz.Infra.CrossCutting.Identity.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string SenhaAntiga { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; }
    }
}