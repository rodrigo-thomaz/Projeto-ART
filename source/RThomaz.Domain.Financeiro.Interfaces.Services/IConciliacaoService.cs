using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface IConciliacaoService : IServiceBase
    {
        Task<List<ConciliacaoLancamentoMasterViewDTO>> GetLancamentosConciliados(long movimentoId, TipoTransacao tipoTransacao);
        Task<List<ConciliacaoMovimentoMasterViewDTO>> GetMovimentosConciliados(long lancamentoId, TipoTransacao tipoTransacao);
    }
}