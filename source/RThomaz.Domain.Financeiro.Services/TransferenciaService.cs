using RThomaz.Domain.Financeiro.Services.Converters;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Data.Entity;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using RThomaz.Infra.CrossCutting.ExceptionHandling;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public class TransferenciaService : ServiceBase, ITransferenciaService
    {
        #region constructors

        public TransferenciaService()
        {

        }

        #endregion

        #region public voids

        public async Task<TransferenciaDetailViewDTO> GetDetail(long transferenciaId)
        {
            Transferencia entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.Transferencia
                    .Include(x => x.Lancamentos.Select(y => y.Conta))
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.TransferenciaId.Equals(transferenciaId))
                    .FirstOrDefaultAsync();

                var contas = entity.Lancamentos.Select(x => x.Conta);

                await ContaService.LoadContas(context, contas);               
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

        public async Task<TransferenciaDetailViewDTO> Insert(TransferenciaDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            TransferenciaDetailViewDTO result;

            try
            {
                var contaOrigem = await context.Conta
                        .Where(x => x.ContaId == dto.ContaOrigemId)
                        .Where(x => x.TipoConta == dto.TipoContaOrigem)
                        .FirstOrDefaultAsync();

                if (contaOrigem == null)
                {
                    throw new RecordNotFoundException("Conta Origem Not Found");
                }

                var contaDestino = await context.Conta
                        .Where(x => x.ContaId == dto.ContaDestinoId)
                        .Where(x => x.TipoConta == dto.TipoContaDestino)
                        .FirstOrDefaultAsync();

                if (contaDestino == null)
                {
                    throw new RecordNotFoundException("Conta Destino Not Found");
                }

                //Inserindo a Transferencia

                var transferenciaEntity = new Transferencia
                {
                    AplicacaoId = AplicacaoId,
                    DataVencimento = dto.DataVencimento,
                    ValorVencimento = dto.ValorVencimento,
                    Historico = dto.Historico,
                    Numero = dto.Numero,
                    Observacao = dto.Observacao,
                };

                context.Transferencia.Add(transferenciaEntity);

                await context.SaveChangesAsync();

                //Inserindo os Lancamentos

                var lancamentoContaPagarEntity = new Lancamento
                {
                    AplicacaoId = AplicacaoId,
                    TransferenciaId = transferenciaEntity.TransferenciaId,
                    TipoTransacao = TipoTransacao.Debito,
                    ContaId = dto.ContaOrigemId,
                    TipoConta = dto.TipoContaOrigem,
                    DataVencimento = dto.DataVencimento,
                    ValorVencimento = dto.ValorVencimento,
                    Historico = dto.Historico,
                    Numero = dto.Numero,
                    Observacao = dto.Observacao,
                };

                var lancamentoContaReceberEntity = new Lancamento
                {
                    AplicacaoId = AplicacaoId,
                    TransferenciaId = transferenciaEntity.TransferenciaId,
                    TipoTransacao = TipoTransacao.Credito,
                    ContaId = dto.ContaDestinoId,
                    TipoConta = dto.TipoContaDestino,
                    DataVencimento = dto.DataVencimento,
                    ValorVencimento = dto.ValorVencimento,
                    Historico = dto.Historico,
                    Numero = dto.Numero,
                    Observacao = dto.Observacao,
                };

                context.Lancamento.Add(lancamentoContaPagarEntity);
                context.Lancamento.Add(lancamentoContaReceberEntity);

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();

                var contas = transferenciaEntity.Lancamentos.Select(x => x.Conta);

                await ContaService.LoadContas(context, contas);

                result = ConvertEntityToDetail(transferenciaEntity);
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

        public async Task<TransferenciaDetailViewDTO> Edit(TransferenciaDetailEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            TransferenciaDetailViewDTO result;

            try
            {
                //Transferencia

                var transferenciaEntity = await context.Transferencia
                    .Include(MovimentoImportacaoOFXDTO => MovimentoImportacaoOFXDTO.Lancamentos)
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.TransferenciaId == dto.TransferenciaId)
                    .FirstOrDefaultAsync();

                if (transferenciaEntity == null)
                {
                    throw new RecordNotFoundException();
                }

                var contaOrigem = await context.Conta
                        .Where(x => x.ContaId == dto.ContaOrigemId)
                        .Where(x => x.TipoConta == dto.TipoContaOrigem)
                        .FirstOrDefaultAsync();

                if (contaOrigem == null)
                {
                    throw new RecordNotFoundException("Conta Origem Not Found");
                }

                var contaDestino = await context.Conta
                        .Where(x => x.ContaId == dto.ContaDestinoId)
                        .Where(x => x.TipoConta == dto.TipoContaDestino)
                        .FirstOrDefaultAsync();

                if (contaDestino == null)
                {
                    throw new RecordNotFoundException("Conta Destino Not Found");
                }

                transferenciaEntity.DataVencimento = dto.DataVencimento;
                transferenciaEntity.ValorVencimento = dto.ValorVencimento;
                transferenciaEntity.Historico = dto.Historico;
                transferenciaEntity.Numero = dto.Numero;
                transferenciaEntity.Observacao = dto.Observacao;

                //Lancamentos

                var lancamentoContaPagarEntity = transferenciaEntity.Lancamentos
                    .Where(x => x.TipoTransacao == TipoTransacao.Debito)
                    .First();                

                lancamentoContaPagarEntity.ContaId = dto.ContaOrigemId;
                lancamentoContaPagarEntity.TipoConta = dto.TipoContaOrigem;
                lancamentoContaPagarEntity.DataVencimento = dto.DataVencimento;
                lancamentoContaPagarEntity.ValorVencimento = dto.ValorVencimento;
                lancamentoContaPagarEntity.Historico = dto.Historico;
                lancamentoContaPagarEntity.Numero = dto.Numero;
                lancamentoContaPagarEntity.Observacao = dto.Observacao;

                var lancamentoContaReceberEntity = transferenciaEntity.Lancamentos
                    .Where(x => x.TipoTransacao == TipoTransacao.Credito)
                    .First();

                lancamentoContaReceberEntity.ContaId = dto.ContaDestinoId;
                lancamentoContaReceberEntity.TipoConta = dto.TipoContaDestino;
                lancamentoContaReceberEntity.DataVencimento = dto.DataVencimento;
                lancamentoContaReceberEntity.ValorVencimento = dto.ValorVencimento;
                lancamentoContaReceberEntity.Historico = dto.Historico;
                lancamentoContaReceberEntity.Numero = dto.Numero;
                lancamentoContaReceberEntity.Observacao = dto.Observacao;
                
                //Salvando

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();

                var contas = transferenciaEntity.Lancamentos.Select(x => x.Conta);

                await ContaService.LoadContas(context, contas);

                result = ConvertEntityToDetail(transferenciaEntity);
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

        public async Task<bool> Remove(long transferenciaId)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();
            
            bool result = false;

            try
            {
                var entity = await context.Transferencia
                    .Include(x => x.Lancamentos)
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.TransferenciaId.Equals(transferenciaId))
                    .Where(x => !x.Lancamentos.Any(y => y.Pagamento != null))
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                context.Lancamento.RemoveRange(entity.Lancamentos);
                context.Transferencia.Remove(entity);

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

        private TransferenciaDetailViewDTO ConvertEntityToDetail(Transferencia entity)
        {
            var lancamentoContaPagarEntity = entity.Lancamentos.First(x => x.TipoTransacao == TipoTransacao.Debito);
            var lancamentoContaReceberEntity = entity.Lancamentos.First(x => x.TipoTransacao == TipoTransacao.Credito);

            var result = new TransferenciaDetailViewDTO
                (
                    contaOrigem: ContaConverter.ConvertEntityToDTO(lancamentoContaPagarEntity.Conta),
                    contaDestino: ContaConverter.ConvertEntityToDTO(lancamentoContaReceberEntity.Conta),
                    transferenciaId: entity.TransferenciaId,
                    dataVencimento: entity.DataVencimento,
                    historico: entity.Historico,
                    numero: entity.Numero,
                    observacao: entity.Observacao,
                    valorVencimento: entity.ValorVencimento
               );

            return result;
        }
        #endregion
    }
}
