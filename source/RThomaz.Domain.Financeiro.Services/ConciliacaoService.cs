using RThomaz.Domain.Financeiro.Services.Converters;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Infra.CrossCutting.ExceptionHandling;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public class ConciliacaoService : ServiceBase, IConciliacaoService
    {
        #region constructors

        public ConciliacaoService()
        {

        }

        #endregion

        #region public voids

        public async Task<List<ConciliacaoLancamentoMasterViewDTO>> GetLancamentosConciliados(long movimentoId, TipoTransacao tipoTransacao)
        {
            List<ConciliacaoLancamentoMasterViewDTO> result;

            var context = new RThomazDbContext();

            try
            {
                var entity = await context.Movimento
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.MovimentoId.Equals(movimentoId))
                        .Where(x => x.TipoTransacao == tipoTransacao)
                        .Include(x => x.Conciliacoes.Select(y => y.Pagamento.Lancamento.Pessoa))
                        .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                if(entity.Conciliacoes.Any())
                {
                    result = ConciliacaoConverter.ConvertEntityToLancamentoDTO(entity.Conciliacoes);
                }
                else
                {
                    result = new List<ConciliacaoLancamentoMasterViewDTO>();
                }
            }
            finally
            {
                if (context != null)
                {
                    context.Dispose();
                }
            }

            return result;
        }

        public async Task<List<ConciliacaoMovimentoMasterViewDTO>> GetMovimentosConciliados(long lancamentoId, TipoTransacao tipoTransacao)
        {
            List<ConciliacaoMovimentoMasterViewDTO> result = null;

            var context = new RThomazDbContext();

            try
            {
                var entity = await context.Lancamento
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.LancamentoId.Equals(lancamentoId))
                        .Where(x => x.TipoTransacao == tipoTransacao)
                        .Include(x => x.Pagamento.Conciliacoes.Select(y => y.Movimento.Conta))
                        .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                if (entity.Pagamento != null && entity.Pagamento.Conciliacoes.Any())
                {
                    var movimentos = entity.Pagamento.Conciliacoes.Select(x => x.Movimento);                   

                    var bancosIds = new List<long>();                    

                    bancosIds.AddRange(movimentos
                            .Where(x => x.TipoConta == TipoConta.ContaCorrente)
                            .Select(x => ((ContaCorrente)x.Conta).BancoId)
                    );

                    bancosIds.AddRange(movimentos
                            .Where(x => x.TipoConta == TipoConta.ContaPoupanca)
                            .Select(x => ((ContaPoupanca)x.Conta).BancoId)
                    );

                    if (bancosIds.Any())
                    {
                        await context.Banco
                            .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                            .Where(x => bancosIds.Contains(x.BancoId))
                            .LoadAsync();
                    }

                    result = ConciliacaoConverter.ConvertEntityToMovimentoDTO(entity.Pagamento.Conciliacoes);
                }
                else
                {
                    result = new List<ConciliacaoMovimentoMasterViewDTO>();
                }                
            }
            finally
            {
                if (context != null)
                {
                    context.Dispose();
                }
            }

            return result;
        }
        
        #endregion
    }
}
