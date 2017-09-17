using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Enums;
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
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using System.Linq.Expressions;
using RThomaz.Infra.CrossCutting.ExceptionHandling;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public class MovimentoService : ServiceBase, IMovimentoService
    {
        #region constructors

        public MovimentoService()
        {

        }

        #endregion

        #region public voids

        public async Task<MovimentoMasterListDTO> GetMasterList(PagedListRequest<MovimentoMasterDTO> pagedListRequest, MesAnoDTO periodo, long contaId, TipoConta tipoConta, TipoTransacao? tipoTransacao, ConciliacaoStatus? conciliacaoStatus, ConciliacaoOrigem? conciliacaoOrigem)
        {
            MovimentoMasterListDTO result;

            try
            {
                List<MovimentoMasterDTO> dataPaged;

                DateTime dataInicial = new DateTime(periodo.Ano, periodo.Mes, 1);
                DateTime dataFinal = new DateTime(periodo.Ano, periodo.Mes, DateTime.DaysInMonth(periodo.Ano, periodo.Mes));

                int totalRecords;

                using (var context = new RThomazDbContext())
                {
                    decimal? saldoAtual = null;

                    if (
                           !tipoTransacao.HasValue 
                        && !conciliacaoStatus.HasValue
                        && !conciliacaoOrigem.HasValue 
                        && pagedListRequest.Search == null)
                    {
                        //obtendo saldo anterior a dataInicial

                        var totalMovimento = await context.Movimento
                                .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                                .Where(x => x.DataMovimento < dataInicial)
                                .Where(x => x.ContaId.Equals(contaId))
                                .Where(x => x.TipoConta == tipoConta)
                                .SumAsync(x => (decimal?)x.ValorMovimento) ?? 0;

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

                        saldoAtual = saldoInicialValor + totalMovimento;
                    }

                    //Obtendo Movimentos

                    IQueryable<Movimento> query = context.Movimento
                        .Include(x => x.Conciliacoes)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.ContaId.Equals(contaId))
                        .Where(x => x.TipoConta == tipoConta)
                        .Where(x => x.DataMovimento >= dataInicial && x.DataMovimento <= dataFinal);

                    //Busca geral
                    if (pagedListRequest.Search != null)
                    {
                        query = query.Where(x =>
                            x.Historico.Contains(pagedListRequest.Search.Value)
                        );
                    }

                    if (tipoTransacao.HasValue)
                    {
                        query = query
                            .Where(x => x.TipoTransacao == tipoTransacao.Value);
                    }

                    if (conciliacaoStatus.HasValue)
                    {
                        switch (conciliacaoStatus.Value)
                        {
                            case ConciliacaoStatus.NaoConciliado:
                                query = query
                                    .Where(x => !x.Conciliacoes.Any());
                                break;
                            case ConciliacaoStatus.Parcialmente:
                                query = query
                                    .Where(x => x.Conciliacoes.Any())
                                    .Where(x => !x.EstaConciliado);
                                break;
                            case ConciliacaoStatus.Conciliado:
                                query = query
                                    .Where(x => x.EstaConciliado);
                                break;
                            default:
                                break;
                        }
                    }

                    if(conciliacaoOrigem.HasValue)
                    {
                        switch (conciliacaoOrigem.Value)
                        {
                            case ConciliacaoOrigem.Importado:
                                query = query
                                    .Where(x => x.MovimentoImportacaoId.HasValue);
                                break;
                            case ConciliacaoOrigem.Manual:
                                query = query
                                    .Where(x => !x.MovimentoImportacaoId.HasValue);
                                break;
                            default:
                                break;
                        }
                    }

                    //Ordenando...
                    query = query
                        .OrderBy(x => x.DataMovimento)
                        .ThenBy(x => x.TipoTransacao);

                    totalRecords = await query.Select(x => x.MovimentoId).CountAsync();

                    dataPaged = ConvertEntityListToMasterList(saldoAtual, query.ToList().AsReadOnly())
                        .Skip(pagedListRequest.Param.Skip)
                        .Take(pagedListRequest.Param.Take)
                        .ToList();
                }
                
                var pagedListResponse = new PagedListResponse<MovimentoMasterDTO>(
                    data: dataPaged,
                    totalRecords: totalRecords);

                //Obtendo o saldo anterior

                decimal? saldoAnterior = null;

                if (
                        pagedListResponse.Data.Any()
                    && !tipoTransacao.HasValue
                    && !conciliacaoStatus.HasValue
                    && !conciliacaoOrigem.HasValue
                    && pagedListRequest.Search == null)
                {
                    var primeiroLancamento = pagedListResponse.Data.First();
                    saldoAnterior = primeiroLancamento.Saldo.Value - primeiroLancamento.ValorMovimento;
                }

                result = new MovimentoMasterListDTO(pagedListResponse, saldoAnterior);
            }
            catch (Exception ex)
            {
                throw ex;
            }            

            return result;
        }

        public async Task<SelectListResponseDTO<MovimentoSelectViewDTO>> GetSelectViewList(SelectListRequestDTO selectListDTORequest, TipoTransacao tipoTransacao)
        {
            SelectListResponseDTO<MovimentoSelectViewDTO> result = null;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<Movimento> query = context.Movimento
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Include(x => x.Conciliacoes)
                        .Where(x => x.TipoTransacao == tipoTransacao)
                        .Where(x => x.EstaConciliado == false);

                //Busca geral
                if (!string.IsNullOrEmpty(selectListDTORequest.Search))
                {
                    query = query.Where(x =>
                        x.Historico.Contains(selectListDTORequest.Search)
                    );
                }

                Expression<Func<Movimento, DateTime>> orderByPredicate = x => x.DataMovimento;

                query = query.OrderBy(orderByPredicate);
                
                var totalRecords = await query.Select(x => x.MovimentoId).CountAsync();

                var dataPaged = await query
                    .Skip(selectListDTORequest.Skip)
                    .Take(selectListDTORequest.PageSize)
                    .ToListAsync();

                var dtos = ConvertEntityListToSelectViewList(dataPaged);                

                result = new SelectListResponseDTO<MovimentoSelectViewDTO>(
                     data: dtos,
                     totalRecords: totalRecords);
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

        public async Task<List<MesAnoDTO>> GetPeriodos(long contaId, TipoConta tipoConta)
        {
            List<MesAnoDTO> result;

            try
            {
                using (var context = new RThomazDbContext())
                {
                    var dataResult = await context.Movimento
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.ContaId.Equals(contaId))
                        .Where(x => x.TipoConta == tipoConta)
                        .Select(x => new
                        {
                            x.DataMovimento.Month,
                            x.DataMovimento.Year
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

        public async Task<MovimentoDetailViewDTO> GetDetail(long id)
        {
            Movimento entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.Movimento
                        .Include(x => x.MovimentoImportacao)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.MovimentoId.Equals(id))
                        .FirstOrDefaultAsync();
            }
            finally
            {
                if (context != null)
                {
                    context.Dispose();
                }
            }

            if (entity == null)
            {
                throw new RecordNotFoundException();
            }

            var result = ConvertEntityToDTO(entity);

            return result;
        }

        public async Task<MovimentoDetailViewDTO> Insert(MovimentoDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            long newMovimentoId;

            try
            {
                var entityMovimento = new Movimento
                {
                    AplicacaoId = AplicacaoId,
                    TipoTransacao = dto.TipoTransacao,
                    ContaId = dto.ContaId,
                    TipoConta = dto.TipoConta,
                    DataMovimento = dto.DataMovimento,
                    ValorMovimento = dto.ValorMovimento,
                    Historico = dto.Historico,
                    Observacao = dto.Observacao,
                };

                context.Movimento.Add(entityMovimento);

                await context.SaveChangesAsync();

                newMovimentoId = entityMovimento.MovimentoId;                

                dbContextTransaction.Commit();
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                throw ex;
            }
            finally
            {
                dbContextTransaction.Dispose();
                context.Dispose();
            }

            var result = await GetDetail(newMovimentoId);

            return result;
        }

        public async Task<MovimentoDetailViewDTO> EditManual(MovimentoManualEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            try
            {
                var entity = await context.Movimento
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.MovimentoId.Equals(dto.MovimentoId))
                    .Where(x => x.TipoTransacao == dto.TipoTransacao)
                    .Where(x => !x.MovimentoImportacaoId.HasValue)
                    .Where(x => !x.EstaConciliado)
                    .FirstAsync();

                entity.ContaId = dto.ContaId;
                entity.TipoConta = dto.TipoConta;
                entity.DataMovimento = dto.DataMovimento;
                entity.ValorMovimento = dto.ValorMovimento;
                entity.Historico = dto.Historico;
                entity.Observacao = dto.Observacao;

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                throw ex;
            }
            finally
            {
                dbContextTransaction.Dispose();
                context.Dispose();
            }

            var result = await GetDetail(dto.MovimentoId);

            return result;
        }

        public async Task<MovimentoDetailViewDTO> EditImportado(MovimentoImportadoEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            try
            {
                var entity = await context.Movimento
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.MovimentoId.Equals(dto.MovimentoId))
                    .Where(x => x.TipoTransacao == dto.TipoTransacao)
                    .Where(x => x.MovimentoImportacaoId == dto.MovimentoImportacaoId)
                    .Where(x => !x.EstaConciliado)
                    .FirstAsync();

                entity.Observacao = dto.Observacao;

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                throw ex;
            }
            finally
            {
                dbContextTransaction.Dispose();
                context.Dispose();
            }

            var result = await GetDetail(dto.MovimentoId);

            return result;
        }

        public async Task<MovimentoDetailViewDTO> EditConciliado(MovimentoConciliadoEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            try
            {
                var entity = await context.Movimento
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.MovimentoId.Equals(dto.MovimentoId))
                    .Where(x => x.TipoTransacao == dto.TipoTransacao)
                    .Where(x => x.EstaConciliado)
                    .FirstAsync();

                entity.Observacao = dto.Observacao;

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                throw ex;
            }
            finally
            {
                dbContextTransaction.Dispose();
                context.Dispose();
            }

            var result = await GetDetail(dto.MovimentoId);

            return result;
        }

        public async Task<bool> Remove(long movimentoId)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            bool result = false;

            try
            {
                var entity = await context.Movimento
                    .Where(x => x.AplicacaoId == AplicacaoId)
                    .Where(x => movimentoId == x.MovimentoId)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                context.Movimento.Remove(entity);

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

        private PagedListResponse<Movimento> GetPagedList(PagedListRequest<MovimentoMasterDTO> pagedListRequest, TipoTransacao tipoTransacao)
        {
            PagedListResponse<Movimento> result;

            try
            {
                List<Movimento> dataPaged;

                int totalRecords;

                using (var context = new RThomazDbContext())
                {
                    IQueryable<Movimento> query = context.Movimento
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Include(x => x.Conciliacoes)
                        .Where(x => x.TipoTransacao == tipoTransacao)
                        .Where(x => x.EstaConciliado == false);

                    //Busca geral
                    if (pagedListRequest.Search != null)
                    {
                        query = query.Where(x =>
                            x.Historico.Contains(pagedListRequest.Search.Value)
                        );
                    }

                    //Ordenando por campo
                    if (pagedListRequest.OrderColumns != null)
                    {
                        bool isFirstOrderable = true;

                        ExpressionHelper.ApplyOrder<Movimento, DateTime, MovimentoMasterDTO, DateTime>(
                            ref query, x => x.DataMovimento, pagedListRequest, x => x.DataMovimento, ref isFirstOrderable);                        
                    }

                    totalRecords = query.Select(x => x.MovimentoId).Count();

                    dataPaged = query
                        .Skip(pagedListRequest.Param.Skip)
                        .Take(pagedListRequest.Param.Take)
                        .ToList();
                }

                result = new PagedListResponse<Movimento>(
                    data: dataPaged,
                    totalRecords: totalRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private List<MovimentoSelectViewDTO> ConvertEntityListToSelectViewList(List<Movimento> data)
        {
            var result = new List<MovimentoSelectViewDTO>();
            foreach (var item in data)
            {
                result.Add(ConvertEntityToSelectViewDTO(item));
            }
            return result;
        }

        public static MovimentoSelectViewDTO ConvertEntityToSelectViewDTO(Movimento entity)
        {
            var value = entity.MovimentoId.ToString();
            
            var totalValorConciliado = entity.Conciliacoes.Sum(x => (decimal?)x.ValorConciliado) ?? 0;
            var valorDisponivel = Math.Abs((entity.ValorMovimento - totalValorConciliado));

            return new MovimentoSelectViewDTO
            (
                movimentoId: entity.MovimentoId,
                historico: entity.Historico,                
                dataMovimento: entity.DataMovimento,
                valorMovimento: entity.ValorMovimento,
                valorDisponivel: valorDisponivel
            );
        }

        private List<MovimentoMasterDTO> ConvertEntityListToMasterList(decimal? saldoAtualDaConta, ReadOnlyCollection<Movimento> data)
        {
            var result = new List<MovimentoMasterDTO>();

            foreach (var item in data)
            {
                decimal totalConciliado = 0;

                if(item.Conciliacoes.Any())
                {
                    totalConciliado = item.Conciliacoes.Sum(x => x.ValorConciliado);
                }

                saldoAtualDaConta += item.ValorMovimento;         

                result.Add(new MovimentoMasterDTO
                (
                    movimentoId: item.MovimentoId,
                    tipoTransacao: item.TipoTransacao,
                    tipoConta: item.TipoConta,
                    dataMovimento: item.DataMovimento,
                    valorMovimento: item.ValorMovimento,
                    historico: item.Historico,                    
                    saldo: saldoAtualDaConta.HasValue ? saldoAtualDaConta : null,
                    totalConciliado: totalConciliado,
                    estaConciliado: item.EstaConciliado,
                    movimentoImportacaoId: item.MovimentoImportacaoId
                ));
            }

            return result;
        }

        private MovimentoDetailViewDTO ConvertEntityToDTO(Movimento entity)
        {
            var result = new MovimentoDetailViewDTO
            (
                movimentoId: entity.MovimentoId,
                tipoTransacao: entity.TipoTransacao,
                contaId: entity.ContaId,
                tipoConta: entity.TipoConta,
                dataMovimento: entity.DataMovimento,
                historico: entity.Historico,
                observacao: entity.Observacao,
                valorMovimento: entity.ValorMovimento,
                movimentoImportacao: entity.MovimentoImportacaoId.HasValue 
                    ? new MovimentoImportacaoDetailViewDTO 
                        (
                            entity.MovimentoImportacao.MovimentoImportacaoId, 
                            entity.MovimentoImportacao.ImportadoEm
                        ) 
                    : null,
                estaConciliado: entity.EstaConciliado
            );

            return result;
        }

        #endregion
    }
}
