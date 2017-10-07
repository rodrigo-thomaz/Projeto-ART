using ART.Infra.CrossCutting.Repository;
using System;
using System.Collections.Generic;

namespace ART.Domotica.Repository.Entities
{
    public class Project : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ApplicationId { get; set; }
        public Application Application { get; set; }
        public ICollection<HardwaresInProject> HardwaresInProject
        {
            get; set;
        }
    }
}
