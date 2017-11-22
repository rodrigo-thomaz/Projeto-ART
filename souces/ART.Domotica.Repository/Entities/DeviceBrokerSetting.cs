using ART.Infra.CrossCutting.Repository;
using System;

namespace ART.Domotica.Repository.Entities
{
    public class DeviceBrokerSetting : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string ClientId { get; set; }

        public string Topic { get; set; }

        public DeviceBase DeviceBase { get; set; }        
    }
}
