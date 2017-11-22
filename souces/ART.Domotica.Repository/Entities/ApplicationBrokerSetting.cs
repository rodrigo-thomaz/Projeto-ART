using ART.Infra.CrossCutting.Repository;
using System;

namespace ART.Domotica.Repository.Entities
{
    public class ApplicationBrokerSetting : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Topic { get; set; }

        public Application Application { get; set; }        
    }
}
