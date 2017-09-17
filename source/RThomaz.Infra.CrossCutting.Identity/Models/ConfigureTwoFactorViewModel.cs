using System.Collections.Generic;

namespace RThomaz.Infra.CrossCutting.Identity.Models
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<string> Providers { get; set; }
    }
}