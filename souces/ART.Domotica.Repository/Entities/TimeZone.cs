using ART.Infra.CrossCutting.Repository;

namespace ART.Domotica.Repository.Entities
{
    public class TimeZone : IEntity<short>
    {
        public short Id { get; set; }
        public string Abreviation { get; set; }
        public decimal TimeStart { get; set; }
        public int GMTOffset { get; set; }
        public string DST { get; set; }
        public short ZoneId { get; set; }
        public Zone Zone { get; set; }
    }
}
