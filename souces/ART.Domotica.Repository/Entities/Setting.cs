using ART.Infra.CrossCutting.Repository;

namespace ART.Domotica.Repository.Entities
{
    public class Setting : IEntity<short>
    {
        public short Id { get; set; }
        public string Value { get; set; }
    }
}
