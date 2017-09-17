using System.ComponentModel.DataAnnotations;

namespace RThomaz.Infra.CrossCutting.Identity.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}