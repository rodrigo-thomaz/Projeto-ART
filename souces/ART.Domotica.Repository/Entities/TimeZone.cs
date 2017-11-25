using ART.Infra.CrossCutting.Repository;

namespace ART.Domotica.Repository.Entities
{
    public class TimeZone : IEntity<byte>
    {
        public byte Id { get; set; }
        public string DisplayName { get; set; }
        public bool SupportsDaylightSavingTime { get; set; }
        public int UtcTimeOffsetInSecond { get; set; }
    }
}
