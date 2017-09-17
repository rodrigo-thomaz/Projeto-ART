using RThomaz.Domain.Financeiro.Interfaces.Repositories;
using RThomaz.Infra.Data.Core;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Infra.Data.Persistence.Contexts;

namespace RThomaz.Infra.Data.Financeiro.Repositories
{
    public class CentroCustoRepository : RepositoryBase<CentroCusto, RThomazDbContext>, ICentroCustoRepository
    {
      
    }
}
