using AutoMapper;
using System;
using System.Linq;

namespace RThomaz.DistributedServices.Financeiro
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(x => !x.IsDynamic);

            Mapper.Initialize(cfg => cfg.AddProfiles(assemblies));            
        }
    }
}
