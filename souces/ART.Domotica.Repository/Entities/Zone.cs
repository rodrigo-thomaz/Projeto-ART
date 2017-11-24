using ART.Infra.CrossCutting.Repository;
using System.Collections.Generic;

namespace ART.Domotica.Repository.Entities
{
    public class Zone : IEntity<short>
    {
        public short Id { get; set; }

        public short CountryId { get; set; }

        public Country Country { get; set; }

        public string Name { get; set; }

        public ICollection<TimeZone> TimeZones
        {
            get; set;
        }
    }
}
