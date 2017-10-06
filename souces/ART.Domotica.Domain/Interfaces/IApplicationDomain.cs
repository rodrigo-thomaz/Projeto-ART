using ART.Domotica.Repository.Entities;
using ART.Infra.CrossCutting.MQ.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ART.Domotica.Domain.Interfaces
{
    public interface IApplicationDomain
    {
        Task<List<Application>> GetAll(AuthenticatedMessageContract message);
    }
}