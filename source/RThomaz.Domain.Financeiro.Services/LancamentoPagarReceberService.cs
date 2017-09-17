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
    public class LancamentoPagarReceberService : ServiceBase, ILancamentoPagarReceberService
    {
        #region constructors

        public LancamentoPagarReceberService()
        {

        }

        #endregion

        #region public voids

        public async Task<LancamentoPagarReceberMasterListDTO> GetMasterList(PagedListRequest<LancamentoPagarReceberMasterDTO> pagedListRequest, MesAnoDTO periodo, long contaId, TipoConta tipoConta)
        {
            LancamentoPagarReceberMasterListDTO result;

            try
            {
                List<LancamentoPagarReceberMasterDTO> dataPaged;

                DateTime dataInicial = new DateTime(periodo.Ano, periodo.Mes, 1);
                DateTime dataFinal = new DateTime(periodo.Ano, periodo.Mes, DateTime.DaysInMonth(periodo.Ano, periodo.Mes));

                int totalRecords;

                using (var context = new RThomazDbContext())
                {
                    //obtendo saldo da conta
                    decimal saldoMovimento = 0;

                    saldoMovimento = (await context.Movimento
                            .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                            .Where(x => x.ContaId.Equals(contaId))
                            .Where(x => x.TipoConta == tipoConta)
                            .SumAsync(x => (decimal?)x.ValorMovimento)) ?? 0;                   

                    //obtendo saldo anterior a dataInicial
                    decimal saldoParcialLancamentos = 0;

                    saldoParcialLancamentos = (await context.Lancamento
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.Pagamento == null)
                        .Where(x => x.DataVencimento < dataInicial)
                        .Where(x => x.ContaId.Equals(contaId))
                        .Where(x => x.TipoConta == tipoConta)
                        .SumAsync(x => (decimal?)x.ValorVencimento)) ?? 0;

                    //Saldo Inicial
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

                    var saldoAtual = saldoInicialValor + saldoMovimento + saldoParcialLancamentos;
                    
                    //Obtendo Lançamentos
                    
                    IQueryable<Lancamento> query = context.Lancamento
                        .Include(x => x.Pessoa)
                        .Include(x => x.Transferencia)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.Pagamento == null)
                        .Where(x => x.ContaId.Equals(contaId))
                        .Where(x => x.TipoConta == tipoConta)
                        .Where(x => x.DataVencimento >= dataInicial && x.DataVencimento <= dataFinal);
                    
                    //Busca geral
                    if (pagedListRequest.Search != null)
                    {
                        query = query.Where(x =>
                            x.Historico.Contains(pagedListRequest.Search.Value)
                        );
                    }

                    //Ordenando...
                    query = query
                        .OrderBy(x => x.DataVencimento)
                        .ThenBy(x => x.TipoTransacao);

                    totalRecords = await query.Select(x => x.LancamentoId).CountAsync();

                    dataPaged = ConvertEntityListToMasterList(saldoAtual, (await query.ToListAsync()).AsReadOnly())
                        .Skip(pagedListRequest.Param.Skip)
                        .Take(pagedListRequest.Param.Take)
                        .ToList();
                }

                var pagedListResponse = new PagedListResponse<LancamentoPagarReceberMasterDTO>(
                    data: dataPaged,
                    totalRecords: totalRecords);

                //Obtendo o saldo anterior

                decimal saldoAnterior = 0;

                if (pagedListResponse.Data.Any())
                {
                    var primeiroLancamento = pagedListResponse.Data.First();
                    saldoAnterior = primeiroLancamento.Saldo - primeiroLancamento.ValorVencimento;
                }

                result = new LancamentoPagarReceberMasterListDTO(pagedListResponse, saldoAnterior);
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
                        .Where(x => x.Pagamento == null)
                        .Where(x => x.ContaId.Equals(contaId))
                        .Where(x => x.TipoConta == tipoConta)
                        .Select(x => new
                        {
                            x.DataVencimento.Month,
                            x.DataVencimento.Year
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

        public async Task<LancamentoPagarReceberDetailViewDTO> GetDetail(long lancamentoId)
        {
            Lancamento entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.Lancamento
                    .Include(x => x.Rateios.Select(y => y.PlanoConta))
                    .Include(x => x.Rateios.Select(y => y.CentroCusto))
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.LancamentoId.Equals(lancamentoId))
                    .Where(x => x.Pagamento == null)
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

        public async Task<LancamentoPagarReceberDetailViewDTO> Insert(LancamentoPagarReceberDetailInsertDTO dto)
        {           
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            LancamentoPagarReceberDetailViewDTO result;
            
            try
            {
                //Obtendo a Conta

                Conta conta = null;

                switch (dto.TipoConta)
                {
                    case TipoConta.ContaEspecie:
                        conta = await context.Conta
                            .OfType<ContaEspecie>()
                            .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                            .FirstAsync(x => x.ContaId.Equals(dto.ContaId));
                        break;
                    case TipoConta.ContaCorrente:
                        conta = await context.Conta
                            .OfType<ContaCorrente>()
                            .Include(x => x.Banco)
                            .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                            .FirstAsync(x => x.ContaId.Equals(dto.ContaId));
                        break;
                    case TipoConta.ContaPoupanca:
                        conta = await context.Conta
                            .OfType<ContaPoupanca>()
                            .Include(x => x.Banco)
                            .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                            .FirstAsync(x => x.ContaId.Equals(dto.ContaId));
                        break;
                    case TipoConta.ContaCartaoCredito:
                        conta = await context.Conta
                            .OfType<ContaCartaoCredito>()
                            .Include(x => x.BandeiraCartao)
                            .Include(x => x.ContaCorrente)
                            .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                            .FirstAsync(x => x.ContaId.Equals(dto.ContaId));
                        break;
                    default:
                        break;
                }

                if (conta == null)
                {
                    throw new RecordNotFoundException("Conta Not Found");
                }

                //Obtendo a Pessoa

                Pessoa pessoa = null;

                if (dto.PessoaId.HasValue)
                {
                    switch (dto.TipoPessoa.Value)
                    {
                        case TipoPessoa.PessoaFisica:
                            pessoa = await context.Pessoa
                            .OfType<PessoaFisica>()
                            .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                            .FirstAsync(x => x.PessoaId.Equals(dto.PessoaId.Value));
                            break;
                        case TipoPessoa.PessoaJuridica:
                            pessoa = await context.Pessoa
                            .OfType<PessoaJuridica>()
                            .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                            .FirstAsync(x => x.PessoaId.Equals(dto.PessoaId.Value));
                            break;
                        default:
                            break;
                    }
                }

                if (pessoa == null)
                {
                    throw new RecordNotFoundException("Pessoa Not Found");
                }

                //Lançamento

                var entity = new Lancamento
                {
                    AplicacaoId = AplicacaoId,
                    TipoTransacao = dto.TipoTransacao,
                    Pessoa = pessoa,
                    Conta = conta,
                    DataVencimento = dto.DataVencimento,
                    ValorVencimento = dto.ValorVencimento,                    
                    Historico = dto.Historico,
                    Numero = dto.Numero,
                    Observacao = dto.Observacao,
                };                           

                if(dto.Rateios.Any())
                {
                    entity.Rateios = new List<LancamentoRateio>();

                    //CentroCusto
                    var centroCustoIds = dto.Rateios.Select(y => y.CentroCustoId).Distinct();
                    var centrosCusto = await context.CentroCusto
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => centroCustoIds.Contains(x.CentroCustoId))
                        .ToListAsync();

                    //PlanoConta
                    var planoContaIds = dto.Rateios.Select(y => y.PlanoContaId).Distinct();
                    var planosConta = await context.PlanoConta
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => planoContaIds.Contains(x.PlanoContaId))
                        .ToListAsync();

                    foreach (var item in dto.Rateios)
                    {
                        var centroCusto = centrosCusto.FirstOrDefault(x => x.CentroCustoId.Equals(item.CentroCustoId));
                        if (centroCusto == null)
                        {
                            throw new RecordNotFoundException("CentroCusto Not Found");
                        }

                        var planoConta = planosConta.FirstOrDefault(x => x.PlanoContaId.Equals(item.PlanoContaId));
                        if (planoConta == null)
                        {
                            throw new RecordNotFoundException("PlanoConta Not Found");
                        }

                        entity.Rateios.Add(new LancamentoRateio
                        {
                            AplicacaoId = AplicacaoId,
                            LancamentoId = entity.LancamentoId,
                            TipoTransacao = entity.TipoTransacao,
                            CentroCusto = centroCusto,
                            PlanoConta = planoConta,                            
                            Observacao = item.Observacao,
                            Porcentagem = item.Porcentagem
                        });
                    }
                }                                                               

                if (dto.EstaPago)
                {
                    entity.Pagamento = new Pagamento
                    {
                        AplicacaoId = AplicacaoId,
                        DataPagamento = dto.DataPagamento.Value,
                        ValorPagamento = dto.ValorPagamento.Value,
                    };

                    if (dto.Conciliacoes != null)
                    {
                        var movimentacoesId = dto.Conciliacoes
                            .Select(x => x.MovimentoId)
                            .ToList();

                        var movimentacoes = await context.Movimento
                            .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                            .Where(x => movimentacoesId.Contains(x.MovimentoId))
                            .ToListAsync();

                        entity.Pagamento.Conciliacoes = new List<Conciliacao>();

                        foreach (var item in dto.Conciliacoes)
                        {
                            var movimento = movimentacoes
                                .Where(x => x.MovimentoId.Equals(item.MovimentoId))
                                .FirstOrDefault();

                            if (movimento == null)
                            {
                                throw new RecordNotFoundException("Movimento Not Found");
                            }

                            if (movimento.ValorMovimento == item.ValorConciliado)
                            {
                                movimento.EstaConciliado = true;
                            }

                            entity.Pagamento.Conciliacoes.Add(new Conciliacao
                            {
                                AplicacaoId = AplicacaoId,
                                TipoTransacao = entity.TipoTransacao,
                                Movimento = movimento,
                                ValorConciliado = item.ValorConciliado,
                            });
                        }                        
                    }
                }

                context.Lancamento.Add(entity);

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

        public async Task<LancamentoPagarReceberDetailViewDTO> Edit(LancamentoPagarReceberDetailEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            LancamentoPagarReceberDetailViewDTO result;

            try
            {
                var entity = await context.Lancamento
                    .Include(x => x.Rateios)
                    .Include(x => x.Pagamento)
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.LancamentoId.Equals(dto.LancamentoId))
                    .Where(x => x.TipoTransacao == dto.TipoTransacao)
                    .Where(x => x.Pagamento == null)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                entity.PessoaId = dto.PessoaId;
                entity.TipoPessoa = dto.TipoPessoa;
                entity.ContaId = dto.ContaId;
                entity.TipoConta = dto.TipoConta;
                entity.DataVencimento = dto.DataVencimento;
                entity.ValorVencimento = dto.ValorVencimento;
                entity.Historico = dto.Historico;
                entity.Numero = dto.Numero;
                entity.Observacao = dto.Observacao;

                if (dto.EstaPago)                
                {
                    entity.Pagamento = new Pagamento
                    {
                        AplicacaoId = AplicacaoId,
                        LancamentoId = dto.LancamentoId,
                        DataPagamento = dto.DataPagamento.Value,
                        ValorPagamento = dto.ValorPagamento.Value,
                    };
                }

                await context.SaveChangesAsync();

                //Rateios

                var rateiosToDelete = context.LancamentoRateio
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.LancamentoId.Equals(dto.LancamentoId));

                context.LancamentoRateio.RemoveRange(rateiosToDelete);

                await context.SaveChangesAsync();

                foreach (var item in dto.Rateios)
                {
                    context.LancamentoRateio.Add(new LancamentoRateio
                    {
                        AplicacaoId = AplicacaoId,
                        LancamentoId = entity.LancamentoId,
                        TipoTransacao = entity.TipoTransacao,
                        PlanoContaId = item.PlanoContaId,
                        CentroCustoId = item.CentroCustoId,
                        Observacao = item.Observacao,
                        Porcentagem = item.Porcentagem
                    });
                }

                await context.SaveChangesAsync();

                //Conciliações

                if (dto.Conciliacoes != null)
                {
                    var movimentacoesId = dto.Conciliacoes
                        .Select(x => x.MovimentoId)
                        .ToList();

                    var movimentacoes = await context.Movimento
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => movimentacoesId.Contains(x.MovimentoId))
                        .ToListAsync();

                    foreach (var item in dto.Conciliacoes)
                    {
                        var movimento = movimentacoes
                           .Where(x => x.MovimentoId.Equals(item.MovimentoId))
                           .First();

                        if (movimento.ValorMovimento == item.ValorConciliado)
                        {
                            movimento.EstaConciliado = true;
                        }

                        context.Conciliacao.Add(new Conciliacao
                        {
                            LancamentoId = entity.LancamentoId,
                            AplicacaoId = AplicacaoId,
                            TipoTransacao = entity.TipoTransacao,
                            MovimentoId = item.MovimentoId,
                            ValorConciliado = item.ValorConciliado,
                        });
                    }

                    await context.SaveChangesAsync();
                }

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
                var entity = await context.Lancamento
                    .Include(x => x.Rateios)
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.LancamentoId.Equals(lancamentoId))
                    .Where(x => x.TipoTransacao == tipoTransacao)
                    .Where(x => x.Pagamento == null)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                context.LancamentoRateio.RemoveRange(entity.Rateios);
                context.Lancamento.Remove(entity);

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

        private List<LancamentoPagarReceberMasterDTO> ConvertEntityListToMasterList(decimal saldoAtualDaConta, ReadOnlyCollection<Lancamento> data)
        {
            var result = new List<LancamentoPagarReceberMasterDTO>();

            foreach (var item in data)
            {
                saldoAtualDaConta += item.ValorVencimento;

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

                result.Add(new LancamentoPagarReceberMasterDTO
                (
                    lancamentoId: item.LancamentoId,
                    dataVencimento: item.DataVencimento,
                    valorVencimento: item.ValorVencimento,
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

        private LancamentoPagarReceberDetailViewDTO ConvertEntityToDTO(Lancamento entity)
        {
            var conta = ContaConverter.ConvertEntityToDTO(entity.Conta);
            var pessoa = entity.PessoaId.HasValue ? PessoaConverter.ConvertEntityToDTO(entity.Pessoa) : null;
            var rateios = RateioConverter.ConvertEntityToDTO(entity.Rateios);

            var result = new LancamentoPagarReceberDetailViewDTO
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
                rateios: rateios
            );

            return result;
        }

        #endregion       
    }
}
