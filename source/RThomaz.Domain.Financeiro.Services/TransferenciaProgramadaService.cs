using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using System.Linq;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.Converters;
using RThomaz.Infra.CrossCutting.ExceptionHandling;

namespace RThomaz.Domain.Financeiro.Services
{
    public class TransferenciaProgramadaService : ProgramacaoBaseService, ITransferenciaProgramadaService
    {
        #region constructors

        public TransferenciaProgramadaService()
        {

        }

        #endregion

        #region public voids

        public async Task<TransferenciaProgramadaDetailViewDTO> GetDetail(long id)
        {
            TransferenciaProgramada entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.TransferenciaProgramada
                    .Include(x => x.ContaDestino)
                    .Include(x => x.ContaOrigem)
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.TransferenciaProgramadaId == id)
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

        public async Task<TransferenciaProgramadaDetailViewDTO> Insert(TransferenciaProgramadaDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            TransferenciaProgramadaDetailViewDTO result;

            try
            {
                //Inserindo a TransferenciaProgramada

                var transferenciaProgramadaEntity = new TransferenciaProgramada
                {
                    AplicacaoId = AplicacaoId,
                    Programador = new Programador
                    {
                        Dia = dto.Dia,
                        HasDomingo = dto.HasDomingo,
                        HasSegunda = dto.HasSegunda,
                        HasTerca = dto.HasTerca,
                        HasQuarta = dto.HasQuarta,
                        HasQuinta = dto.HasQuinta,
                        HasSexta = dto.HasSexta,
                        HasSabado = dto.HasSabado,
                        DataInicial = dto.DataInicial,
                        DataFinal = dto.DataFinal,
                        Frequencia = dto.Frequencia,
                        Historico = dto.Historico,
                        ValorVencimento = dto.ValorVencimento,
                        Observacao = dto.Observacao,
                    },                    
                    ContaOrigem_ContaId = dto.ContaOrigemId,
                    ContaOrigem_TipoConta = dto.TipoContaOrigem,
                    ContaDestino_ContaId = dto.ContaDestinoId,                    
                    ContaDestino_TipoConta = dto.TipoContaDestino,                    
                };

                context.TransferenciaProgramada.Add(transferenciaProgramadaEntity);

                //Inserindo as Transferencias

                var listOfDate = GetListOfDate(dto);

                foreach (var data in listOfDate)
                {
                    var lancamentoContaPagarEntity = new Lancamento
                    {
                        AplicacaoId = AplicacaoId,                        
                        TipoTransacao = TipoTransacao.Debito,
                        ContaId = dto.ContaOrigemId,
                        TipoConta = dto.TipoContaOrigem,
                        DataVencimento = data,
                        ValorVencimento = dto.ValorVencimento,
                        Historico = dto.Historico,
                        Observacao = dto.Observacao,
                        TransferenciaProgramada = transferenciaProgramadaEntity
                    };

                    var lancamentoContaReceberEntity = new Lancamento
                    {
                        AplicacaoId = AplicacaoId,
                        TipoTransacao = TipoTransacao.Credito,
                        ContaId = dto.ContaDestinoId,
                        TipoConta = dto.TipoContaDestino,
                        DataVencimento = data,
                        ValorVencimento = dto.ValorVencimento,
                        Historico = dto.Historico,
                        Observacao = dto.Observacao,
                        TransferenciaProgramada = transferenciaProgramadaEntity
                    };

                    var transferenciaEntity = new Transferencia
                    {
                        AplicacaoId = AplicacaoId,
                        TransferenciaProgramadaId = transferenciaProgramadaEntity.TransferenciaProgramadaId,
                        DataVencimento = data,
                        Historico = dto.Historico,
                        Observacao = dto.Observacao,
                        ValorVencimento = dto.ValorVencimento,
                        Lancamentos = new HashSet<Lancamento> 
                            {
                                lancamentoContaPagarEntity,
                                lancamentoContaReceberEntity,
                            },
                    };

                    if (transferenciaProgramadaEntity.Transferencias == null)
                        transferenciaProgramadaEntity.Transferencias = new HashSet<Transferencia>();

                    transferenciaProgramadaEntity.Transferencias.Add(transferenciaEntity);
                }

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();

                result = ConvertEntityToDetail(transferenciaProgramadaEntity);
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

        public async Task<TransferenciaProgramadaDetailViewDTO> Edit(TransferenciaProgramadaDetailEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            TransferenciaProgramadaDetailViewDTO result;

            try
            {
                //Editando TransferenciaProgramada

                var entity = await context.TransferenciaProgramada
                    .Include(x => x.ContaDestino)
                    .Include(x => x.ContaOrigem)
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.TransferenciaProgramadaId.Equals(dto.ProgramacaoId))
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                entity.Programador.Historico = dto.Historico;
                entity.Programador.ValorVencimento = dto.ValorVencimento;
                entity.Programador.Observacao = dto.Observacao;
                entity.ContaOrigem_ContaId = dto.ContaOrigemId;
                entity.ContaOrigem_TipoConta = dto.TipoContaOrigem;
                entity.ContaDestino_ContaId = dto.ContaDestinoId;
                entity.ContaDestino_TipoConta = dto.TipoContaDestino;
                entity.Programador.DataInicial = dto.DataInicial;
                entity.Programador.DataFinal = dto.DataFinal;
                entity.Programador.Frequencia = dto.Frequencia;
                entity.Programador.Dia = dto.Dia;
                entity.Programador.HasDomingo = dto.HasDomingo;
                entity.Programador.HasSegunda = dto.HasSegunda;
                entity.Programador.HasTerca = dto.HasTerca;
                entity.Programador.HasQuarta = dto.HasQuarta;
                entity.Programador.HasQuinta = dto.HasQuinta;
                entity.Programador.HasSexta = dto.HasSexta;
                entity.Programador.HasSabado = dto.HasSabado;

                ////
                //Transferencias
                ////

                var transferenciasExistentes = await context.Transferencia
                    .Include(x => x.Lancamentos)
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.TransferenciaProgramadaId == dto.ProgramacaoId)
                    .ToListAsync();

                var listOfDate = GetListOfDate(dto);

                // Removendo
                transferenciasExistentes.ForEach(x =>
                {
                    if (!listOfDate.Contains(x.DataVencimento))
                    {
                        context.Lancamento.RemoveRange(x.Lancamentos);
                        context.Transferencia.Remove(x);
                    }
                });

                foreach (var date in listOfDate)
                {
                    var transferenciaExistente = transferenciasExistentes.FirstOrDefault(x => x.DataVencimento == date);

                    if (transferenciaExistente != null)
                    {
                        //Altera
                        transferenciaExistente.DataVencimento = date;
                        transferenciaExistente.ValorVencimento = dto.ValorVencimento;
                        transferenciaExistente.Historico = dto.Historico;
                        transferenciaExistente.Observacao = transferenciaExistente.Observacao;

                        var lancamentoContaPagarEntity = transferenciaExistente.Lancamentos.First(x => x.TipoTransacao == TipoTransacao.Debito);

                        lancamentoContaPagarEntity.ContaId = dto.ContaOrigemId;
                        lancamentoContaPagarEntity.TipoConta = dto.TipoContaOrigem;
                        lancamentoContaPagarEntity.DataVencimento = date;
                        lancamentoContaPagarEntity.ValorVencimento = dto.ValorVencimento;
                        lancamentoContaPagarEntity.Historico = dto.Historico;
                        lancamentoContaPagarEntity.Observacao = dto.Observacao;

                        var lancamentoContaReceberEntity = transferenciaExistente.Lancamentos.First(x => x.TipoTransacao == TipoTransacao.Credito);

                        lancamentoContaReceberEntity.ContaId = dto.ContaDestinoId;
                        lancamentoContaReceberEntity.TipoConta = dto.TipoContaDestino;
                        lancamentoContaReceberEntity.DataVencimento = date;
                        lancamentoContaReceberEntity.ValorVencimento = dto.ValorVencimento;
                        lancamentoContaReceberEntity.Historico = dto.Historico;
                        lancamentoContaReceberEntity.Observacao = dto.Observacao;
                    }
                    else if (transferenciaExistente == null)
                    {
                        var transferenciaEntity = new Transferencia
                        {
                            DataVencimento = date,
                            ValorVencimento = dto.ValorVencimento,
                            Historico = dto.Historico,
                            Observacao = dto.Observacao,
                            AplicacaoId = AplicacaoId,
                        };

                        //Inserindo os Lancamentos

                        var lancamentoContaPagarEntity = new Lancamento
                        {
                            TransferenciaProgramada = entity,
                            TipoTransacao = TipoTransacao.Debito,
                            ContaId = dto.ContaOrigemId,
                            TipoConta = dto.TipoContaOrigem,
                            DataVencimento = date,
                            ValorVencimento = dto.ValorVencimento,
                            Historico = dto.Historico,
                            Observacao = dto.Observacao,
                            AplicacaoId = AplicacaoId,
                        };

                        var lancamentoContaReceberEntity = new Lancamento
                        {
                            TransferenciaProgramada = entity,
                            TipoTransacao = TipoTransacao.Credito,
                            ContaId = dto.ContaDestinoId,
                            TipoConta = dto.TipoContaDestino,
                            DataVencimento = date,
                            ValorVencimento = dto.ValorVencimento,
                            Historico = dto.Historico,
                            Observacao = dto.Observacao,
                            AplicacaoId = AplicacaoId,
                        };

                        if (transferenciaEntity.Lancamentos == null)
                        {
                            transferenciaEntity.Lancamentos = new HashSet<Lancamento>();
                        }
                        if (entity.Transferencias == null)
                        {
                            entity.Transferencias = new HashSet<Transferencia>();
                        }

                        transferenciaEntity.Lancamentos.Add(lancamentoContaPagarEntity);
                        transferenciaEntity.Lancamentos.Add(lancamentoContaReceberEntity);

                        entity.Transferencias.Add(transferenciaEntity);
                    }
                }

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

        public async Task<bool> Remove(long programacaoId)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();
            
            bool result = false;

            try
            {
                var entity = await context.TransferenciaProgramada
                    .Include(x => x.Transferencias.Select(y => y.Lancamentos))
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.TransferenciaProgramadaId == programacaoId)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }
                
                foreach (var transferencia in entity.Transferencias)
                {
                    context.Lancamento.RemoveRange(transferencia.Lancamentos);
                }

                context.Transferencia.RemoveRange(entity.Transferencias);                                
                context.TransferenciaProgramada.Remove(entity);
                
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

        private TransferenciaProgramadaDetailViewDTO ConvertEntityToDetail(TransferenciaProgramada entity)
        {
            var contaOrigemDTO = ContaConverter.ConvertEntityToDTO(entity.ContaOrigem);
            var contaDestinoDTO = ContaConverter.ConvertEntityToDTO(entity.ContaDestino);

            var result = new TransferenciaProgramadaDetailViewDTO
            (
                programacaoId: entity.TransferenciaProgramadaId,
                contaOrigem: contaOrigemDTO,
                contaDestino: contaDestinoDTO,
                dataInicial: entity.Programador.DataInicial,
                dataFinal: entity.Programador.DataFinal,
                frequencia: entity.Programador.Frequencia,
                dia: entity.Programador.Dia,
                hasDomingo: entity.Programador.HasDomingo,
                hasSegunda: entity.Programador.HasSegunda,
                hasTerca: entity.Programador.HasTerca,
                hasQuarta: entity.Programador.HasQuarta,
                hasQuinta: entity.Programador.HasQuinta,
                hasSexta: entity.Programador.HasSexta,
                hasSabado: entity.Programador.HasSabado,
                historico: entity.Programador.Historico,
                valorVencimento: entity.Programador.ValorVencimento,
                observacao: entity.Programador.Observacao
            );

            return result;
        } 

        #endregion
    }
}
