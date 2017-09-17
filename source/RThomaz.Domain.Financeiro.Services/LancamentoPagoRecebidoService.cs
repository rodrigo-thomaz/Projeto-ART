using RThomaz.Domain.Financeiro.Services.Converters;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using RThomaz.Infra.CrossCutting.ExceptionHandling;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public class LancamentoPagoRecebidoService : ServiceBase, ILancamentoPagoRecebidoService
    {
        #region constructors

        public LancamentoPagoRecebidoService()
        {

        }

        #endregion

        #region public voids

        public async Task<LancamentoPagoRecebidoMasterListDTO> GetMasterList(PagedListRequest<LancamentoPagoRecebidoMasterDTO> pagedListRequest, MesAnoDTO periodo, long contaId, TipoConta tipoConta)
        {
            LancamentoPagoRecebidoMasterListDTO result;

            try
            {
                DateTime dataInicial = new DateTime(periodo.Ano, periodo.Mes, 1);
                DateTime dataFinal = new DateTime(periodo.Ano, periodo.Mes, DateTime.DaysInMonth(periodo.Ano, periodo.Mes));

                List<LancamentoPagoRecebidoMasterDTO> dataPaged;
                
                int totalRecords;

                decimal saldoAnterior = 0;

                using (var context = new RThomazDbContext())
                {
                    //obtendo saldo anterior a dataInicial

                    decimal saldoAnteriorADataInicial = (await context.Lancamento
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.Pagamento != null)
                        .Where(x => x.Pagamento.DataPagamento < dataInicial)
                        .Where(x => x.ContaId.Equals(contaId))
                        .Where(x => x.TipoConta == tipoConta)
                        .SumAsync(x => (decimal?)x.Pagamento.ValorPagamento)) ?? 0;

                    decimal saldoInicialValor = 0;

                    var conta = await context.Conta
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.ContaId.Equals(contaId))
                        .Where(x => x.TipoConta == tipoConta)
                        .FirstAsync();

                    switch (conta.TipoConta)
                    {
                        case TipoConta.ContaCorrente:
                            saldoInicialValor = ((ContaCorrente)conta).SaldoInicial.Valor;
                            break;
                        case TipoConta.ContaPoupanca:
                            saldoInicialValor = ((ContaPoupanca)conta).SaldoInicial.Valor;
                            break;
                    }

                    var saldoAtual = saldoInicialValor + saldoAnteriorADataInicial;
                    
                    //Obtendo Lançamentos
                    
                    IQueryable<Lancamento> query = context.Lancamento
                        .Include(x => x.Pagamento)
                        .Include(x => x.Pessoa)
                        .Include(x => x.Transferencia)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.Pagamento != null)
                        .Where(x => x.ContaId.Equals(contaId))
                        .Where(x => x.TipoConta == tipoConta)
                        .Where(x => x.Pagamento.DataPagamento >= dataInicial && x.Pagamento.DataPagamento <= dataFinal);
                    
                    //Busca geral
                    if (pagedListRequest.Search != null)
                    {
                        query = query.Where(x =>
                            x.Historico.Contains(pagedListRequest.Search.Value)
                        );
                    }

                    //Ordenando...
                    query = query
                        .OrderBy(x => x.Pagamento.DataPagamento)
                        .ThenBy(x => x.TipoTransacao);

                    totalRecords = await query.Select(x => x.LancamentoId).CountAsync();

                    dataPaged = ConvertEntityListToMasterList(saldoAtual, query.ToList().AsReadOnly())
                        .Skip(pagedListRequest.Param.Skip)
                        .Take(pagedListRequest.Param.Take)
                        .ToList();

                    //Obtendo o saldo anterior

                    if (saldoAnteriorADataInicial == 0)
                    {
                        saldoAnterior = saldoInicialValor;
                    }
                    else if (saldoAnteriorADataInicial != 0 && dataPaged.Any())
                    {
                        var primeiroLancamento = dataPaged.First();
                        saldoAnterior = primeiroLancamento.Saldo - primeiroLancamento.ValorPagamento;
                    }
                }

                var pagedListResponse = new PagedListResponse<LancamentoPagoRecebidoMasterDTO>(
                    data: dataPaged,
                    totalRecords: totalRecords);                

                result = new LancamentoPagoRecebidoMasterListDTO(pagedListResponse, saldoAnterior);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<List<MesAnoDTO>> GetPeriodos(long contaId, TipoConta tipoConta)
        {
            List<MesAnoDTO> result;

            try
            {
                using (var context = new RThomazDbContext())
                {
                    var dataResult = await context.Lancamento
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.Pagamento != null)
                        .Where(x => x.ContaId.Equals(contaId))
                        .Where(x => x.TipoConta == tipoConta)
                        .Select(x => new
                        {
                            x.Pagamento.DataPagamento.Month,
                            x.Pagamento.DataPagamento.Year
                        })
                        .GroupBy(x => x)
                        .Select(x => x.Key)
                        .ToListAsync();

                    result = dataResult.Select(x => new MesAnoDTO
                    (
                        x.Month, 
                        x.Year
                    ))
                    .OrderBy(x => x.Ano)
                    .ThenBy(x => x.Mes)
                    .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<LancamentoPagoRecebidoDetailViewDTO> GetDetail(long lancamentoId)
        {
            Lancamento entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.Lancamento
                        .Include(x => x.Rateios.Select(y => y.CentroCusto))
                        .Include(x => x.Rateios.Select(y => y.PlanoConta))
                        .Include(x => x.Pagamento.Conciliacoes.Select(y => y.Movimento))
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.LancamentoId.Equals(lancamentoId))
                        .Where(x => x.Pagamento != null)
                        .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                if (entity.PessoaId.HasValue)
                {
                    switch (entity.TipoPessoa.Value)
                    {
                        case TipoPessoa.PessoaFisica:
                            await context.Pessoa
                            .OfType<PessoaFisica>()
                            .FirstAsync(x => x.PessoaId.Equals(entity.PessoaId.Value));
                            break;
                        case TipoPessoa.PessoaJuridica:
                            await context.Pessoa
                            .OfType<PessoaJuridica>()
                            .FirstAsync(x => x.PessoaId.Equals(entity.PessoaId.Value));
                            break;
                        default:
                            break;
                    }
                }

                switch (entity.TipoConta)
                {
                    case TipoConta.ContaEspecie:
                        await context.Conta
                            .OfType<ContaEspecie>()
                            .FirstAsync(x => x.ContaId.Equals(entity.ContaId));
                        break;
                    case TipoConta.ContaCorrente:
                        await context.Conta
                            .OfType<ContaCorrente>()
                            .Include(x => x.Banco)
                            .FirstAsync(x => x.ContaId.Equals(entity.ContaId));
                        break;
                    case TipoConta.ContaPoupanca:
                        await context.Conta
                            .OfType<ContaPoupanca>()
                            .Include(x => x.Banco)
                            .FirstAsync(x => x.ContaId.Equals(entity.ContaId));
                        break;
                    case TipoConta.ContaCartaoCredito:
                        await context.Conta
                            .OfType<ContaCartaoCredito>()
                            .Include(x => x.BandeiraCartao)
                            .Include(x => x.ContaCorrente)
                            .FirstAsync(x => x.ContaId.Equals(entity.ContaId));
                        break;
                    default:
                        break;
                }
            }
            finally
            {
                if (context != null)
                {
                    context.Dispose();
                }
            }

            var result = ConvertEntityToDTO(entity);

            return result;
        }

        public async Task<LancamentoPagoRecebidoDetailViewDTO> Edit(LancamentoPagoRecebidoDetailEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            LancamentoPagoRecebidoDetailViewDTO result;

            try
            {
                var entity = await context.Lancamento
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.Pagamento != null)
                    .Where(x => x.LancamentoId.Equals(dto.LancamentoId))
                    .Where(x => x.TipoTransacao == dto.TipoTransacao)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                entity.Observacao = dto.Observacao;

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();

                result = ConvertEntityToDTO(entity);
            }
            catch (Exception ex)
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Rollback();
                }
                throw ex;
            }
            finally
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Dispose();
                }
                if (context != null)
                {
                    context.Dispose();
                }
            }

            return result;
        }

        public async Task<bool> Remove(long lancamentoId, TipoTransacao tipoTransacao)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();
            
            bool result = false;

            try
            {
                var entity = await context.Pagamento
                    .Include(x => x.Conciliacoes)
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.LancamentoId.Equals(lancamentoId))
                    .Where(x => x.TipoTransacao == tipoTransacao)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                context.Conciliacao.RemoveRange(entity.Conciliacoes);
                context.Pagamento.Remove(entity);

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();

                result = true;
            }
            catch (DbUpdateException)
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Rollback();
                }
                throw ex;
            }
            finally
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Dispose();
                }
                if (context != null)
                {
                    context.Dispose();
                }
            }

            return result;
        }

        #endregion

        #region private voids

        private List<LancamentoPagoRecebidoMasterDTO> ConvertEntityListToMasterList(decimal saldoAtualDaConta, ReadOnlyCollection<Lancamento> data)
        {
            var result = new List<LancamentoPagoRecebidoMasterDTO>();

            foreach (var item in data)
            {
                saldoAtualDaConta += item.Pagamento.ValorPagamento;

                var nome = string.Empty;

                if (item.Pessoa != null && item.Pessoa is PessoaFisica)
                {
                    var pessoaFisica = ((PessoaFisica)item.Pessoa);
                    nome = pessoaFisica.NomeCompleto;
                }
                else if (item.Pessoa != null && item.Pessoa is PessoaJuridica)
                {
                    var pessoaJuridica = ((PessoaJuridica)item.Pessoa);
                    nome = pessoaJuridica.NomeFantasia;
                }

                result.Add(new LancamentoPagoRecebidoMasterDTO
                (
                    lancamentoId: item.LancamentoId,
                    dataPagamento: item.Pagamento.DataPagamento,
                    valorPagamento: item.Pagamento.ValorPagamento,
                    historico: item.Historico,
                    tipoTransacao: item.TipoTransacao,
                    pessoaNome: nome,
                    transferenciaId: item.TransferenciaId,
                    programacaoId: item.ProgramacaoId,
                    saldo: saldoAtualDaConta
                ));
            }

            return result;
        }

        private LancamentoPagoRecebidoDetailViewDTO ConvertEntityToDTO(Lancamento entity)
        {
            var conta = ContaConverter.ConvertEntityToDTO(entity.Conta);
            var pessoa = entity.PessoaId.HasValue ? PessoaConverter.ConvertEntityToDTO(entity.Pessoa) : null;
            var rateios = RateioConverter.ConvertEntityToDTO(entity.Rateios);
            var conciliacoes = ConciliacaoConverter.ConvertEntityToDTO(entity.Pagamento.Conciliacoes);
                        
            var result = new LancamentoPagoRecebidoDetailViewDTO
            (
                lancamentoId: entity.LancamentoId,
                tipoTransacao: entity.TipoTransacao,
                conta: conta,
                pessoa: pessoa,
                dataVencimento: entity.DataVencimento,
                historico: entity.Historico,
                numero: entity.Numero,
                observacao: entity.Observacao,                
                valorVencimento: entity.ValorVencimento,
                rateios: rateios,
                conciliacoes: conciliacoes,
                dataPagamento: entity.Pagamento.DataPagamento,
                valorPagamento: entity.Pagamento.ValorPagamento
            );

            return result;
        }

        #endregion       
    }
}
