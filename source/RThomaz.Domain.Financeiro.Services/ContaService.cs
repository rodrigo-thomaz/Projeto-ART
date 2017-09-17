using RThomaz.Domain.Financeiro.Services.Converters;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using RThomaz.Infra.CrossCutting.ExceptionHandling;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public class ContaService : ServiceBase, IContaService
    {
        #region constructors

        public ContaService()
        {

        }

        #endregion

        #region public voids

        public async Task<PagedListResponse<ContaMasterDTO>> GetMasterList(PagedListRequest<ContaMasterDTO> pagedListRequest, TipoConta? tipoConta, bool? ativo)
        {
            var pagedList = await GetPagedList(pagedListRequest, tipoConta, ativo);
            var masterList = ConvertEntityListToMasterList(pagedList.Data);
            var result = new PagedListResponse<ContaMasterDTO>
            (
                data: masterList,
                totalRecords: pagedList.TotalRecords
            );
            return result;
        }

        public async Task<List<ContaSummaryViewDTO>> GetSummaryViewList()
        {
            List<ContaSummaryViewDTO> result = new List<ContaSummaryViewDTO>();

            var context = new RThomazDbContext();

            try
            {
                var totalGroupByConta = context.Movimento
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .GroupBy(row => new { row.ContaId })
                        .Select(g => new
                        {
                            ContaId = g.Key.ContaId,
                            Total = g.Sum(x => x.ValorMovimento)
                        })
                        .ToDictionary(x => x.ContaId, x => x.Total);



                IQueryable<Conta> query = context.Conta
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.Ativo);                

                Expression<Func<Conta, string>> orderByPredicate = x =>
                    x is ContaEspecie ? (x as ContaEspecie).Nome :
                    x is ContaCorrente ? (x as ContaCorrente).Banco.Nome :
                    x is ContaPoupanca ? (x as ContaPoupanca).Banco.Nome :
                    (x as ContaCartaoCredito).Nome;

                query = query.OrderBy(orderByPredicate);

                var data = await query.ToListAsync();

                //Load Bancos

                var bancoIds = data
                    .Where(x => x.TipoConta == TipoConta.ContaCorrente || x.TipoConta == TipoConta.ContaPoupanca)
                    .Select(x => x.TipoConta == TipoConta.ContaCorrente ? (x as ContaCorrente).BancoId : (x as ContaPoupanca).BancoId)
                    .Distinct()
                    .ToList();

                if (bancoIds.Any())
                {
                    await context.Banco
                        .Where(x => bancoIds.Contains(x.BancoId))
                        .LoadAsync();
                }

                //Load BandeiraCartao

                var bandeiraCartaoIds = data
                    .Where(x => x.TipoConta == TipoConta.ContaCartaoCredito)
                    .Select(x => (x as ContaCartaoCredito).BandeiraCartaoId)
                    .Distinct()
                    .ToList();

                if (bandeiraCartaoIds.Any())
                {
                    await context.BandeiraCartao
                        .Where(x => bandeiraCartaoIds.Contains(x.BandeiraCartaoId))
                        .LoadAsync();
                }                

                foreach (var item in data)
                {
                    var totalConta = totalGroupByConta
                            .Where(x => x.Key == item.ContaId)
                            .Select(x => x.Value)
                            .FirstOrDefault();

                    decimal saldoInicialValor = 0;

                    switch (item.TipoConta)
                    {
                        case TipoConta.ContaEspecie:
                            break;
                        case TipoConta.ContaCorrente:
                            saldoInicialValor = ((ContaCorrente)item).SaldoInicial.Valor;
                            break;
                        case TipoConta.ContaPoupanca:
                            saldoInicialValor = ((ContaPoupanca)item).SaldoInicial.Valor;
                            break;
                        case TipoConta.ContaCartaoCredito:
                            break;
                        default:
                            throw new NotImplementedException();
                    }

                    result.Add(new ContaSummaryViewDTO
                        (
                              contaId: item.ContaId
                            , tipoConta: item.TipoConta
                            , conta: ContaConverter.ConvertEntityToDTO(item)
                            , saldoAtual: saldoInicialValor + totalConta
                        ));
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

        public async Task<List<ContaSelectViewDTO>> GetSelectViewList(TipoConta? tipoConta)
        {
            var result = new List<ContaSelectViewDTO>();

            var context = new RThomazDbContext();

            try
            {
                IQueryable<Conta> query = context.Conta
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.Ativo);

                if(tipoConta.HasValue)
                {
                    query = query
                        .Where(x => x.TipoConta == tipoConta.Value);
                }

                Expression<Func<Conta, string>> orderByPredicate = x => 
                    x is ContaEspecie ? (x as ContaEspecie).Nome :
                    x is ContaCorrente ? (x as ContaCorrente).Banco.Nome :
                    x is ContaPoupanca ? (x as ContaPoupanca).Banco.Nome :
                    (x as ContaCartaoCredito).Nome;

                query = query.OrderBy(orderByPredicate);

                var data = await query.ToListAsync();

                await LoadContas(context, data);

                foreach (var item in data)
                {
                    result.Add(ContaConverter.ConvertEntityToDTO(item));
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

        public async Task<List<CountDTO<ContaSelectViewDTO>>> GetWithProgramacaoSelectViewList()
        {
            var result = new List<CountDTO<ContaSelectViewDTO>>();

            var context = new RThomazDbContext();

            try
            {
                IQueryable<Conta> query = context.Conta
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x =>
                           x.Programacoes.Any()
                        || x.TransferenciasProgramadasDeOrigem.Any()
                        || x.TransferenciasProgramadasDeDestino.Any()
                    );

                Expression<Func<Conta, string>> orderByPredicate = x =>
                    x is ContaEspecie ? (x as ContaEspecie).Nome :
                    x is ContaCorrente ? (x as ContaCorrente).Banco.Nome :
                    x is ContaPoupanca ? (x as ContaPoupanca).Banco.Nome :
                    (x as ContaCartaoCredito).Nome;

                query = query.OrderBy(orderByPredicate);

                var data = await query
                    .Select(x => new
                    {
                        conta = x,
                        programacoesCount = x.Programacoes.Count + x.TransferenciasProgramadasDeOrigem.Count + x.TransferenciasProgramadasDeDestino.Count
                    })
                    .ToListAsync();

                //Bancos
                var bancoIds = data
                       .Where(x => x.conta.TipoConta == TipoConta.ContaCorrente || x.conta.TipoConta == TipoConta.ContaPoupanca)
                       .Select(x => x.conta.TipoConta == TipoConta.ContaCorrente ? (x.conta as ContaCorrente).BancoId : (x.conta as ContaPoupanca).BancoId)
                       .Distinct()
                       .ToList();

                if (bancoIds.Any())
                {
                    await context.Banco
                        .Where(x => bancoIds.Contains(x.BancoId))
                        .LoadAsync();
                }

                //Bandeira cartão
                var bandeiraCartaoIds = data
                    .Select(x => x.conta)
                    .OfType<ContaCartaoCredito>()
                    .Select(x => x.BandeiraCartaoId)
                    .Distinct()
                    .ToList();

                if (bandeiraCartaoIds.Any())
                {
                    await context.BandeiraCartao
                        .Where(x => bandeiraCartaoIds.Contains(x.BandeiraCartaoId))
                        .LoadAsync();
                }

                //Conta corrente
                var contaCorrenteIds = data
                    .Select(x => x.conta)
                    .OfType<ContaCartaoCredito>()
                    .Select(x => x.ContaCorrente_ContaCorrenteId)
                    .Distinct()
                    .ToList();

                if (contaCorrenteIds.Any())
                {
                    await context.Conta
                        .OfType<ContaCorrente>()
                        .Where(x => contaCorrenteIds.Contains(x.ContaId))
                        .LoadAsync();
                }

                foreach (var item in data)
                {
                    result.Add(ContaConverter.ConvertEntityToDTO(item.conta, item.programacoesCount));
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

        public async Task<ContaEspecieDetailViewDTO> GetContaEspecieDetail(long id)
        {
            ContaEspecie entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.Conta
                        .OfType<ContaEspecie>()
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.ContaId.Equals(id))
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

            var result = ConvertEntityToDetail(entity);

            return result;
        }

        public async Task<ContaCorrenteDetailViewDTO> GetContaCorrenteDetail(long id)
        {
            ContaCorrente entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.Conta
                        .OfType<ContaCorrente>()
                        .Include(x => x.Banco)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.ContaId.Equals(id))
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

            var result = ConvertEntityToDetail(entity);

            return result;
        }

        public async Task<ContaPoupancaDetailViewDTO> GetContaPoupancaDetail(long id)
        {
            ContaPoupanca entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.Conta
                        .OfType<ContaPoupanca>()
                        .Include(x => x.Banco)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.ContaId.Equals(id))
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

            var result = ConvertEntityToDetail(entity);

            return result;
        }

        public async Task<ContaCartaoCreditoDetailViewDTO> GetContaCartaoCreditoDetail(long id)
        {
            ContaCartaoCredito entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.Conta
                        .OfType<ContaCartaoCredito>()
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.ContaId.Equals(id))
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

            var result = ConvertEntityToDetail(entity);

            return result;
        }

        public async Task<ContaEspecieDetailViewDTO> InsertContaEspecie(ContaEspecieDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            ContaEspecieDetailViewDTO result;

            try
            {
                var entity = new ContaEspecie
                {
                    AplicacaoId = AplicacaoId,
                    Nome = dto.Nome,
                    Descricao = dto.Descricao,
                    Ativo = dto.Ativo,
                    SaldoInicial = new SaldoInicial
                    {
                        Data = dto.SaldoInicial.Data,
                        Valor = dto.SaldoInicial.Valor,
                    },
                };

                context.Conta.Add(entity);

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();

                result = ConvertEntityToDetail(entity);
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

        public async Task<ContaCorrenteDetailViewDTO> InsertContaCorrente(ContaCorrenteDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            ContaCorrenteDetailViewDTO result;

            try
            {
                var banco = await context.Banco
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.BancoId.Equals(dto.BancoId))
                    .FirstOrDefaultAsync();

                if (banco == null)
                {
                    throw new RecordNotFoundException("Banco Not Found");
                }

                var entity = new ContaCorrente
                {
                    AplicacaoId = AplicacaoId,
                    BancoId = dto.BancoId,
                    DadoBancario = new DadoBancario
                    {
                        NumeroAgencia = dto.DadoBancario.NumeroAgencia,
                        NumeroConta = dto.DadoBancario.NumeroConta,
                    },
                    Descricao = dto.Descricao,
                    Ativo = dto.Ativo,
                    SaldoInicial = new SaldoInicial
                    {
                        Data = dto.SaldoInicial.Data,
                        Valor = dto.SaldoInicial.Valor,
                    },
                };

                context.Conta.Add(entity);

                await context.SaveChangesAsync();                

                dbContextTransaction.Commit();

                result = ConvertEntityToDetail(entity);
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

        public async Task<ContaPoupancaDetailViewDTO> InsertContaPoupanca(ContaPoupancaDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            ContaPoupancaDetailViewDTO result;

            try
            {
                var banco = await context.Banco
                   .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                   .Where(x => x.BancoId.Equals(dto.BancoId))
                   .FirstOrDefaultAsync();

                if (banco == null)
                {
                    throw new RecordNotFoundException("Banco Not Found");
                }

                var entity = new ContaPoupanca
                {
                    AplicacaoId = AplicacaoId,
                    BancoId = dto.BancoId,
                    DadoBancario = new DadoBancario
                    {
                        NumeroAgencia = dto.DadoBancario.NumeroAgencia,
                        NumeroConta = dto.DadoBancario.NumeroConta,
                    },
                    Descricao = dto.Descricao,
                    Ativo = dto.Ativo,
                    SaldoInicial = new SaldoInicial
                    {
                        Data = dto.SaldoInicial.Data,
                        Valor = dto.SaldoInicial.Valor,
                    },
                };

                context.Conta.Add(entity);

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();                

                result = ConvertEntityToDetail(entity);
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

        public async Task<ContaCartaoCreditoDetailViewDTO> InsertContaCartaoCredito(ContaCartaoCreditoDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            ContaCartaoCreditoDetailViewDTO result;

            try
            {
                ContaCorrente contaCorrente = null;

                if (dto.ContaCorrenteId.HasValue)
                {
                    contaCorrente = await context.Conta
                        .OfType<ContaCorrente>()
                       .Where(x => x.AplicacaoId == AplicacaoId)
                       .Where(x => x.ContaId == dto.ContaCorrenteId.Value)
                       .FirstOrDefaultAsync();

                    if (contaCorrente == null)
                    {
                        throw new RecordNotFoundException("Conta corrente Not Found");
                    }
                }

                var entity = new ContaCartaoCredito
                {
                    AplicacaoId = AplicacaoId,
                    BandeiraCartaoId = dto.BandeiraCartaoId,
                    ContaCorrente_ContaCorrenteId = dto.ContaCorrenteId,
                    ContaCorrente = contaCorrente,
                    Nome = dto.Nome,
                    Descricao = dto.Descricao,
                    Ativo = dto.Ativo,
                };

                context.Conta.Add(entity);

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();

                result = ConvertEntityToDetail(entity);
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

        public async Task<ContaEspecieDetailViewDTO> EditContaEspecie(ContaEspecieDetailEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            ContaEspecieDetailViewDTO result;

            try
            {
                var entity = await context.Conta
                        .OfType<ContaEspecie>()
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.ContaId.Equals(dto.ContaId))
                        .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                entity.Nome = dto.Nome;
                entity.Descricao = dto.Descricao;
                entity.Ativo = dto.Ativo;
                entity.SaldoInicial.Data = dto.SaldoInicial.Data;
                entity.SaldoInicial.Valor = dto.SaldoInicial.Valor;

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();

                result = ConvertEntityToDetail(entity);
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

        public async Task<ContaCorrenteDetailViewDTO> EditContaCorrente(ContaCorrenteDetailEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            ContaCorrenteDetailViewDTO result;

            try
            {
                var entity = await context.Conta
                        .OfType<ContaCorrente>()
                        .Include(x => x.Banco)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.ContaId.Equals(dto.ContaId))
                        .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                entity.DadoBancario.NumeroAgencia = dto.DadoBancario.NumeroAgencia;
                entity.DadoBancario.NumeroConta = dto.DadoBancario.NumeroConta;
                entity.Descricao = dto.Descricao;
                entity.Ativo = dto.Ativo;
                entity.SaldoInicial.Data = dto.SaldoInicial.Data;
                entity.SaldoInicial.Valor = dto.SaldoInicial.Valor;

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();

                result = ConvertEntityToDetail(entity);
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

        public async Task<ContaPoupancaDetailViewDTO> EditContaPoupanca(ContaPoupancaDetailEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            ContaPoupancaDetailViewDTO result;

            try
            {
                var entity = await context.Conta
                        .OfType<ContaPoupanca>()
                        .Include(x => x.Banco)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.ContaId.Equals(dto.ContaId))
                        .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                entity.DadoBancario.NumeroAgencia = dto.DadoBancario.NumeroAgencia;
                entity.DadoBancario.NumeroConta = dto.DadoBancario.NumeroConta;
                entity.Descricao = dto.Descricao;
                entity.Ativo = dto.Ativo;
                entity.SaldoInicial.Data = dto.SaldoInicial.Data;
                entity.SaldoInicial.Valor = dto.SaldoInicial.Valor;

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();

                result = ConvertEntityToDetail(entity);
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

        public async Task<ContaCartaoCreditoDetailViewDTO> EditContaCartaoCredito(ContaCartaoCreditoDetailEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            ContaCartaoCreditoDetailViewDTO result;

            try
            {
                var entity = await context.Conta
                        .OfType<ContaCartaoCredito>()
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.ContaId.Equals(dto.ContaId))
                        .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                if (dto.ContaCorrenteId.HasValue)
                {
                    var contaCorrente = await context.Conta
                        .OfType<ContaCorrente>()
                       .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                       .Where(x => x.ContaId.Equals(dto.ContaCorrenteId.Value))
                       .FirstOrDefaultAsync();

                    if (contaCorrente == null)
                    {
                        throw new RecordNotFoundException("Conta corrente Not Found");
                    }
                }

                entity.ContaCorrente_ContaCorrenteId = dto.ContaCorrenteId;
                entity.Nome = dto.Nome;
                entity.Descricao = dto.Descricao;
                entity.Ativo = dto.Ativo;

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();

                result = ConvertEntityToDetail(entity);
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

        public async Task<bool> Remove(long contaId, TipoConta tipoConta)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();
            
            bool result = false;

            try
            {
                var entity = await context.Conta
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.ContaId.Equals(contaId))
                    .Where(x => x.TipoConta == tipoConta)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                context.Conta.Remove(entity);

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

        private async Task<PagedListResponse<Conta>> GetPagedList(PagedListRequest<ContaMasterDTO> pagedListRequest, TipoConta? tipoConta, bool? ativo)
        {
            PagedListResponse<Conta> result;

            try
            {
                List<Conta> dataPaged;

                int totalRecords;

                using (var context = new RThomazDbContext())
                {
                    IQueryable<Conta> query = context.Conta
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .AsQueryable();

                    if (tipoConta.HasValue)
                    {
                        query = query
                            .Where(x => x.TipoConta == tipoConta);
                    }

                    if (ativo.HasValue)
                    {
                        query = query
                            .Where(x => x.Ativo == ativo);
                    }

                    //Busca geral
                    if (pagedListRequest.Search != null)
                    {
                        query = query.Where(x => x is ContaCartaoCredito ?
                            (x as ContaCartaoCredito).BandeiraCartao.Nome.Contains(pagedListRequest.Search.Value) || (x as ContaCartaoCredito).Nome.Contains(pagedListRequest.Search.Value) :
                            x is ContaCorrente ?
                            (x as ContaCorrente).Banco.NomeAbreviado.Contains(pagedListRequest.Search.Value) || (x as ContaCorrente).Banco.Numero.Contains(pagedListRequest.Search.Value) :
                            x is ContaEspecie ?
                            (x as ContaEspecie).Nome.Contains(pagedListRequest.Search.Value) :
                            x is ContaPoupanca ?
                            (x as ContaPoupanca).Banco.NomeAbreviado.Contains(pagedListRequest.Search.Value) || (x as ContaPoupanca).Banco.Numero.Contains(pagedListRequest.Search.Value) :
                            false
                        );
                    }                    

                    //Ordenando por campo
                    if (pagedListRequest.OrderColumns != null)
                    {
                        bool isFirstOrderable = true;

                        ExpressionHelper.ApplyOrder<Conta, string, ContaMasterDTO, string>(
                            ref query, x => x.Descricao, pagedListRequest, x => x.Informacao, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<Conta, TipoConta, ContaMasterDTO, byte>(
                            ref query, x => x.TipoConta, pagedListRequest, x => x.TipoConta, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<Conta, bool, ContaMasterDTO, bool>(
                            ref query, x => x.Ativo, pagedListRequest, x => x.Ativo, ref isFirstOrderable);
                    }

                    totalRecords = await query.Select(x => x.ContaId).CountAsync();

                    dataPaged = await query
                        .Skip(pagedListRequest.Param.Skip)
                        .Take(pagedListRequest.Param.Take)
                        .ToListAsync();

                    //Início - Carregando Bancos

                    var bancosIdContaCorrente = dataPaged.OfType<ContaCorrente>().Select(x => x.BancoId).Distinct();
                    var bancosIdContaPoupanca = dataPaged.OfType<ContaPoupanca>().Select(x => x.BancoId).Distinct();
                    var bancosId = bancosIdContaCorrente.Union(bancosIdContaPoupanca).Distinct();

                    if (bancosId.Count() > 0)
                    {
                        await context.Banco
                            .Where(x => bancosId.Contains(x.BancoId))
                            .LoadAsync();
                    }

                    //Fim - Carregando Bancos

                    //Início - Carregando BandeiraCartao

                    var bandeirasCartaoIdContaCartaoCredito = dataPaged.OfType<ContaCartaoCredito>().Select(x => x.BandeiraCartaoId).Distinct();

                    if (bandeirasCartaoIdContaCartaoCredito.Count() > 0)
                    {
                        await context.BandeiraCartao
                            .Where(x => bandeirasCartaoIdContaCartaoCredito.Contains(x.BandeiraCartaoId))
                            .LoadAsync();
                    }

                    //Fim - Carregando BandeiraCartao                    
                }

                result = new PagedListResponse<Conta>(
                    data: dataPaged,
                    totalRecords: totalRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private List<ContaMasterDTO> ConvertEntityListToMasterList(List<Conta> data)
        {
            var result = new List<ContaMasterDTO>();

            foreach (var item in data)
            {
                var informacao = string.Empty;
                string logoStorageObject = null;

                switch (item.TipoConta)
                {
                    case TipoConta.ContaEspecie:
                        informacao = ((ContaEspecie)item).Nome;
                        break;
                    case TipoConta.ContaCorrente:
                        var contaCorrente = (ContaCorrente)item;
                        informacao = contaCorrente.NomeExibicao;
                        logoStorageObject = contaCorrente.Banco.LogoStorageObject;
                        break;
                    case TipoConta.ContaPoupanca:
                        var contaPoupanca = (ContaPoupanca)item;
                        informacao = contaPoupanca.NomeExibicao;
                        logoStorageObject = contaPoupanca.Banco.LogoStorageObject;
                        break;
                    case TipoConta.ContaCartaoCredito:
                        var contaCartaoCredito = (ContaCartaoCredito)item;
                        informacao = contaCartaoCredito.NomeExibicao;
                        logoStorageObject = contaCartaoCredito.BandeiraCartao.LogoStorageObject;
                        break;
                    default:
                        break;
                }

                result.Add(new ContaMasterDTO
                   (
                       contaId: item.ContaId,
                       logoStorageObject: logoStorageObject,
                       informacao: informacao,
                       tipoConta: (byte)item.TipoConta,
                       ativo: item.Ativo
                   ));   
            }

            return result;
        }

        private ContaEspecieDetailViewDTO ConvertEntityToDetail(ContaEspecie entity)
        {
            var result = new ContaEspecieDetailViewDTO
            (
                contaId: entity.ContaId,
                nome: entity.Nome,
                descricao: entity.Descricao,
                ativo: entity.Ativo,
                saldoInicial: new SaldoInicialDTO
                (
                    data: entity.SaldoInicial.Data,
                    valor: entity.SaldoInicial.Valor
                )
            );

            return result;
        }

        private ContaCorrenteDetailViewDTO ConvertEntityToDetail(ContaCorrente entity)
        {
            var result = new ContaCorrenteDetailViewDTO
            (
                contaId: entity.ContaId,
                bancoSelectView: new BancoSelectViewDTO
                ( 
                    bancoId: entity.Banco.BancoId,
                    nome: entity.Banco.Nome,
                    numero: entity.Banco.Numero,
                    logoStorageObject: entity.Banco.LogoStorageObject
                ),
                dadoBancario: new DadoBancarioDTO
                (
                    numeroAgencia: entity.DadoBancario.NumeroAgencia,
                    numeroConta: entity.DadoBancario.NumeroConta
                ),                
                descricao: entity.Descricao,
                ativo: entity.Ativo,
                saldoInicial: new SaldoInicialDTO
                (
                    data: entity.SaldoInicial.Data,
                    valor: entity.SaldoInicial.Valor
                )
            );

            return result;
        }

        private ContaPoupancaDetailViewDTO ConvertEntityToDetail(ContaPoupanca entity)
        {
            var result = new ContaPoupancaDetailViewDTO
            (
                contaId: entity.ContaId,
                bancoSelectView: new BancoSelectViewDTO
                (
                    bancoId: entity.Banco.BancoId,
                    nome: entity.Banco.Nome,
                    numero: entity.Banco.Numero,
                    logoStorageObject: entity.Banco.LogoStorageObject
                ),
                dadoBancario: new DadoBancarioDTO
                (
                    numeroAgencia: entity.DadoBancario.NumeroAgencia,
                    numeroConta: entity.DadoBancario.NumeroConta
                ),                
                descricao: entity.Descricao,
                ativo: entity.Ativo,
                saldoInicial: new SaldoInicialDTO
                (
                    data: entity.SaldoInicial.Data,
                    valor: entity.SaldoInicial.Valor
                )
            );

            return result;
        }

        private ContaCartaoCreditoDetailViewDTO ConvertEntityToDetail(ContaCartaoCredito entity)
        {
            var result = new ContaCartaoCreditoDetailViewDTO
            (
                contaId: entity.ContaId,
                bandeiraCartaoId: entity.BandeiraCartaoId,
                contaCorrenteId: entity.ContaCorrente_ContaCorrenteId,
                nome: entity.Nome,
                descricao: entity.Descricao,
                ativo: entity.Ativo
            );

            return result;
        }

        #endregion

        #region public static voids

        public static async Task LoadContas(RThomazDbContext context, IEnumerable<Conta> contas)
        {
            //BandeiraCartao
            var bandeiraCartaoIds = contas
                .OfType<ContaCartaoCredito>()
                .Select(x => x.BandeiraCartaoId)
                .Distinct()
                .ToList();

            if (bandeiraCartaoIds.Any())
            {
                await context.BandeiraCartao
                    .Where(x => bandeiraCartaoIds.Contains(x.BandeiraCartaoId))
                    .LoadAsync();
            }

            //CartaoCredito_ContaCorrente
            var contaCorrenteIds = contas
                .OfType<ContaCartaoCredito>()
                .Select(x => x.ContaCorrente_ContaCorrenteId)
                .Distinct()
                .ToList();

            if (contaCorrenteIds.Any())
            {
                await context.Conta
                    .OfType<ContaCorrente>()
                    .Include(x => x.Banco)
                    .Where(x => contaCorrenteIds.Contains(x.ContaId))
                    .LoadAsync();
            }

            //Banco
            var bancoIds = contas
                        .Where(x => x.TipoConta == TipoConta.ContaCorrente || x.TipoConta == TipoConta.ContaPoupanca)
                        .Select(x => x.TipoConta == TipoConta.ContaCorrente ? (x as ContaCorrente).BancoId : (x as ContaPoupanca).BancoId)
                        .Distinct()
                        .ToList();            

            if (bancoIds.Any())
            {
                await context.Banco
                    .Where(x => bancoIds.Contains(x.BancoId))
                    .LoadAsync();
            }
        }

        public static string GetContaNome(IEnumerable<Banco> bancos, Conta conta)
        {
            var contaNome = string.Empty;

            switch (conta.TipoConta)
            {
                case TipoConta.ContaEspecie:
                    contaNome = ((ContaEspecie)conta).Nome;
                    break;
                case TipoConta.ContaCorrente:
                    var bancoContaCorrente = bancos.First(x => x.BancoId.Equals(((ContaCorrente)conta).BancoId));
                    contaNome = string.Format("{0} - C/C {1} Ag {2}", bancoContaCorrente.NomeAbreviado, ((ContaCorrente)conta).DadoBancario.NumeroAgencia, ((ContaCorrente)conta).DadoBancario.NumeroConta);
                    break;
                case TipoConta.ContaPoupanca:
                    var bancoContaPoupanca = bancos.First(x => x.BancoId.Equals(((ContaPoupanca)conta).BancoId));
                    contaNome = string.Format("{0} - C/C {1} Ag {2}", bancoContaPoupanca.NomeAbreviado, ((ContaPoupanca)conta).DadoBancario.NumeroAgencia, ((ContaPoupanca)conta).DadoBancario.NumeroConta);
                    break;
                case TipoConta.ContaCartaoCredito:
                    contaNome = ((ContaCartaoCredito)conta).Nome;
                    break;
                default:
                    break;
            }

            return contaNome;
        }

        #endregion
    }
}
