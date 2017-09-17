using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface IConciliacaoAppService : IAppServiceBase
    {
        Task<List<ConciliacaoLancamentoMasterViewModel>> GetLancamentosConciliados(long movimentoId, TipoTransacao tipoTransacao);
        Task<List<ConciliacaoMovimentoMasterViewModel>> GetMovimentosConciliados(long lancamentoId, TipoTransacao tipoTransacao);
    }
}