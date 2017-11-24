using ART.Infra.CrossCutting.Repository;
using System.Collections.Generic;

namespace ART.Domotica.Repository.Entities
{
    public class Country : IEntity<short>
    {
        public short Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<Zone> Zones
        {
            get; set;
        }
    }
}
