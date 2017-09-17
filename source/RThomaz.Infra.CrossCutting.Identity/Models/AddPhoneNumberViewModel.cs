using System.ComponentModel.DataAnnotations;

namespace RThomaz.Infra.CrossCutting.Identity.Models
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}