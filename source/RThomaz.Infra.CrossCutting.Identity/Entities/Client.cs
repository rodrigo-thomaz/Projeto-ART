using RThomaz.Infra.CrossCutting.Identity.Enums;
using System.ComponentModel.DataAnnotations;

namespace RThomaz.Infra.CrossCutting.Identity.Entities
{
    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public ApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public string AllowedOrigin { get; set; }
    }
}