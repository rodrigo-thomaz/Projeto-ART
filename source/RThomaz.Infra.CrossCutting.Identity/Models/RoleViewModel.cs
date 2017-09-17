using System.ComponentModel.DataAnnotations;

namespace RThomaz.Infra.CrossCutting.Identity.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Nome da Role")]
        public string Name { get; set; }
    }
}