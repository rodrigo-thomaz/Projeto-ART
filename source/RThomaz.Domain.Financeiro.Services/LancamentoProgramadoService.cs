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
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Infra.CrossCutting.ExceptionHandling;

namespace RThomaz.Domain.Financeiro.Services
{
    public class LancamentoProgramadoService : ProgramacaoBaseService, ILancamentoProgramadoService
    {
        #region constructors

        public LancamentoProgramadoService()
        {

        }

        #endregion

        #region public voids

        public async Task<PagedListResponse<ProgramacaoMasterDTO>> GetMasterList(PagedListRequest<ProgramacaoMasterDTO> pagedListRequest, long? contaId, TipoConta? tipoConta)
        {
            PagedListResponse<ProgramacaoMasterDTO> result;

            try
            {
                List<ProgramacaoMasterDTO> data;
                List<ProgramacaoMasterDTO> dataPaged;

                int totalRecords;

                using (var context = new RThomazDbContext())
                {
                    var query1 = context.TransferenciaProgramada
                        .Include(x => x.ContaOrigem)
                        .Include(x => x.ContaDestino)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .AsQueryable();                    

                    var query2 = context.LancamentoProgramado
                        .Include(x => x.Pessoa)
                        .Include(x => x.Conta)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .AsQueryable(); 

                    //Busca geral
                    if (pagedListRequest.Search != null)
                    {
                        query1 = query1.Where(x =>
                            x.Programador.Historico.Contains(pagedListRequest.Search.Value)
                        );

                        query2 = query2.Where(x =>
                            x.Programador.Historico.Contains(pagedListRequest.Search.Value)
                        );
                    }
                                        
                    if(contaId.HasValue && tipoConta.HasValue)
                    {
                        query1 = query1
                            .Where(x => 
                                   (x.ContaOrigem_ContaId.Equals(contaId.Value) && x.ContaOrigem_TipoConta == tipoConta.Value)
                                || (x.ContaDestino_ContaId.Equals(contaId.Value) && x.ContaDestino_TipoConta == tipoConta.Value)
                            );

                        query2 = query2
                            .Where(x => x.ContaId.Equals(contaId.Value))
                            .Where(x => x.TipoConta == tipoConta.Value);
                    }

                    var bancos = await context.Banco
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .ToListAsync();

                    var lista1 = (await query1.ToListAsync()).Select(x => new ProgramacaoMasterDTO
                        (
                            x.TransferenciaProgramadaId,
                            x.Programador.Frequencia,
                            x.Programador.DataInicial,
                            x.Programador.DataFinal,
                            x.Programador.Historico,
                            x.Programador.Observacao,
                            x.Programador.ValorVencimento,
                            null,
                            string.Empty,
                            ContaService.GetContaNome(bancos, x.ContaOrigem) + " >> " + ContaService.GetContaNome(bancos, x.ContaDestino)
                        ));                    

                    var lista2 = (await query2.ToListAsync()).Select(x => new ProgramacaoMasterDTO
                        (
                            x.ProgramacaoId,
                            x.Programador.Frequencia,
                            x.Programador.DataInicial,
                            x.Programador.DataFinal,
                            x.Programador.Historico,
                            x.Programador.Observacao,
                            x.Programador.ValorVencimento,
                            x.TipoTransacao,
                            x.Pessoa == null ? string.Empty : x.Pessoa is PessoaFisica ? ((PessoaFisica)x.Pessoa).NomeCompleto : ((PessoaJuridica)x.Pessoa).NomeFantasia,
                            ContaService.GetContaNome(bancos, x.Conta)
                        ));

                    data = lista1.ToList().Union(lista2.ToList()).ToList();
                    totalRecords = data.Count();

                    //Ordenando                    
                    IOrderedEnumerable<ProgramacaoMasterDTO> orderedQueryable = null;
                    foreach (var column in pagedListRequest.OrderColumns)
                    {
                        Func<ProgramacaoMasterDTO, string> orderingFunction = (x =>
                            column.ColumnName == "TipoTransacao" ? x.TipoTransacao.ToString() :
                            column.ColumnName == "DataInicial" ? x.DataInicial.ToString() :
                            column.ColumnName == "DataFinal" ? x.DataFinal.ToString() :
                            column.ColumnName == "Frequencia" ? ((byte)x.Frequencia).ToString() :
                            column.ColumnName == "Historico" ? x.Historico :
                            column.ColumnName == "PessoaNome" ? x.PessoaNome :
                            column.ColumnName == "ContaNome" ? x.ContaNome :
                            column.ColumnName == "ValorVencimento" ? x.ValorVencimento.ToString() :
                           "");

                        if (column.OrderDirection == PagedListOrderDirection.Asc)
                        {
                            if (orderedQueryable == null)
                                orderedQueryable = data.OrderBy(orderingFunction);
                            else
                                orderedQueryable = orderedQueryable.ThenBy(orderingFunction);
                        }
                        else
                        {
                            if (orderedQueryable == null)
                                orderedQueryable = data.OrderByDescending(orderingFunction);
                            else
                                orderedQueryable = orderedQueryable.ThenByDescending(orderingFunction);
                        }
                    }

                    if (orderedQueryable != null)
                        data = orderedQueryable.ToList();

                    totalRecords = data.Select(x => x.ProgramacaoId).Count();

                    dataPaged = data
                        .Skip(pagedListRequest.Param.Skip)
                        .Take(pagedListRequest.Param.Take)
                        .ToList();
                }

                result = new PagedListResponse<ProgramacaoMasterDTO>(
                    data: dataPaged,
                    totalRecords: totalRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }        

        public async Task<LancamentoProgramadoDetailViewDTO> GetDetail(long id)
        {
            LancamentoProgramado entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.LancamentoProgramado
                        .Include(x => x.Rateios.Select(y => y.CentroCusto))
                        .Include(x => x.Rateios.Select(y => y.PlanoConta))
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.ProgramacaoId == id)
                        .FirstAsync();

                if (entity.PessoaId.HasValue)
                {
                    if (entity.TipoPessoa == TipoPessoa.PessoaFisica)
                    {
                        await context.Pessoa.OfType<PessoaFisica>().FirstAsync(x => x.PessoaId.Equals(entity.PessoaId.Value));
                    }
                    else
                    {
                        await context.Pessoa.OfType<PessoaJuridica>().FirstAsync(x => x.PessoaId.Equals(entity.PessoaId.Value));
                    }
                }
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

        public async Task<LancamentoProgramadoDetailViewDTO> Insert(LancamentoProgramadoInsertDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            long newLancamentoId;

            try
            {
                //Inserindo a LancamentoProgramado

                var lancamentoProgramadoEntity = new LancamentoProgramado
                {
                    Programador = new Programador
                    {                        
                        DataInicial = dto.DataInicial,
                        DataFinal = dto.DataFinal,
                        Frequencia = dto.Frequencia,
                        Dia = dto.Dia,
                        HasDomingo = dto.HasDomingo,
                        HasSegunda = dto.HasSegunda,
                        HasTerca = dto.HasTerca,
                        HasQuarta = dto.HasQuarta,
                        HasQuinta = dto.HasQuinta,
                        HasSexta = dto.HasSexta,
                        HasSabado = dto.HasSabado,
                        Historico = dto.Historico,
                        ValorVencimento = dto.ValorVencimento,
                        Observacao = dto.Observacao,
                    },    
                    AplicacaoId = AplicacaoId,                    
                    TipoTransacao = dto.TipoTransacao,                    
                    PessoaId = dto.PessoaId,
                    TipoPessoa = dto.TipoPessoa,
                    ContaId = dto.ContaId,
                    TipoConta = dto.TipoConta, 
                };

                context.LancamentoProgramado.Add(lancamentoProgramadoEntity);

                await context.SaveChangesAsync();

                foreach (var item in dto.Rateios)
                {
                    context.ProgramacaoRateio.Add(new ProgramacaoRateio
                    {
                        AplicacaoId = AplicacaoId,
                        ProgramacaoId = lancamentoProgramadoEntity.ProgramacaoId,
                        TipoProgramacao = lancamentoProgramadoEntity.TipoProgramacao,
                        TipoTransacao = lancamentoProgramadoEntity.TipoTransacao,
                        PlanoContaId = item.PlanoContaId,
                        CentroCustoId = item.CentroCustoId,                        
                        Observacao = item.Observacao,
                        Porcentagem = item.Porcentagem
                    });
                }

                await context.SaveChangesAsync();

                //Inserindo os Lancamentos

                var listOfDate = GetListOfDate(dto);

                var programacaoRateios = await context.ProgramacaoRateio
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.ProgramacaoId.Equals(lancamentoProgramadoEntity.ProgramacaoId))
                    .ToListAsync();

                foreach (var date in listOfDate)
                {
                    var lancamento = context.Lancamento.Add(new Lancamento
                    {
                        AplicacaoId = AplicacaoId,
                        ProgramacaoId = lancamentoProgramadoEntity.ProgramacaoId,
                        TipoTransacao = lancamentoProgramadoEntity.TipoTransacao,
                        TipoProgramacao = lancamentoProgramadoEntity.TipoProgramacao,
                        Historico = dto.Historico,
                        DataVencimento = date,
                        ValorVencimento = dto.ValorVencimento,
                        Observacao = dto.Observacao,
                        PessoaId = dto.PessoaId,
                        TipoPessoa = dto.TipoPessoa,
                        ContaId = dto.ContaId,
                        TipoConta = dto.TipoConta,
                    });

                    await context.SaveChangesAsync();

                    foreach (var item in programacaoRateios)
                    {
                        context.LancamentoRateio.Add(new LancamentoRateio
                        {
                            AplicacaoId = AplicacaoId,
                            LancamentoId = lancamento.LancamentoId,
                            TipoTransacao = lancamento.TipoTransacao,
                            PlanoContaId = item.PlanoContaId,
                            CentroCustoId = item.CentroCustoId,
                            Observacao = item.Observacao,
                            Porcentagem = item.Porcentagem
                        });
                    }
                }

                await context.SaveChangesAsync();

                newLancamentoId = lancamentoProgramadoEntity.ProgramacaoId;

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

            var result = await GetDetail(newLancamentoId);

            return result;
        }

        public async Task<LancamentoProgramadoDetailViewDTO> Edit(LancamentoProgramadoEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            try
            {
                //Editando LancamentoProgramado

                var entity = await context.LancamentoProgramado
                    .Include(x => x.Rateios)
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.ProgramacaoId.Equals(dto.ProgramacaoId))
                    .FirstAsync();

                entity.Programador.Historico = dto.Historico;
                entity.Programador.ValorVencimento = dto.ValorVencimento;
                entity.Programador.Observacao = dto.Observacao;
                entity.PessoaId = dto.PessoaId;
                entity.TipoPessoa = dto.TipoPessoa;
                entity.ContaId = dto.ContaId;
                entity.TipoConta = dto.TipoConta;
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

                await context.SaveChangesAsync();

                var rateiosToDelete = await context.ProgramacaoRateio
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.ProgramacaoId.Equals(dto.ProgramacaoId))
                    .ToListAsync();

                context.ProgramacaoRateio.RemoveRange(rateiosToDelete);

                await context.SaveChangesAsync();

                foreach (var item in dto.Rateios)
                {
                    context.ProgramacaoRateio.Add(new ProgramacaoRateio
                    {
                        AplicacaoId = AplicacaoId,                        
                        ProgramacaoId = entity.ProgramacaoId,
                        TipoTransacao = entity.TipoTransacao,
                        TipoProgramacao = entity.TipoProgramacao,
                        PlanoContaId = item.PlanoContaId,
                        CentroCustoId = item.CentroCustoId,
                        Observacao = item.Observacao,
                        Porcentagem = item.Porcentagem,
                    });
                }

                await context.SaveChangesAsync();

                //Lancamentos.....                        

                var lancamentosExistentes = await context.Lancamento
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.Pagamento == null)
                    .Where(x => x.ProgramacaoId == dto.ProgramacaoId)
                    .ToListAsync();

                var listOfDate = GetListOfDate(dto);

                var programacaoRateios = await context.ProgramacaoRateio
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.ProgramacaoId.Equals(dto.ProgramacaoId))
                    .ToListAsync();

                // Removendo
                foreach (var x in lancamentosExistentes)
                {
                    if (!listOfDate.Contains(x.DataVencimento))
                    {
                        var lancamentoRateiosToDelete = await context.LancamentoRateio
                            .Where(y => y.AplicacaoId.Equals(AplicacaoId))
                            .Where(y => y.LancamentoId.Equals(x.LancamentoId))
                            .ToListAsync();

                        context.LancamentoRateio.RemoveRange(lancamentoRateiosToDelete);

                        context.Lancamento.Remove(x);
                    }
                }

                await context.SaveChangesAsync();

                foreach (var date in listOfDate)
                {
                    var lancamentoExistente = lancamentosExistentes.FirstOrDefault(x => x.DataVencimento == date);

                    if (lancamentoExistente != null)
                    {
                        //Altera
                        lancamentoExistente.Historico = dto.Historico;
                        lancamentoExistente.ValorVencimento = dto.ValorVencimento;
                        lancamentoExistente.Observacao = dto.Observacao;
                        lancamentoExistente.PessoaId = dto.PessoaId;
                        lancamentoExistente.TipoPessoa = dto.TipoPessoa;
                        lancamentoExistente.ContaId = dto.ContaId;
                        lancamentoExistente.TipoConta = dto.TipoConta;

                        var lancamentoRateiosToDelete = await context.LancamentoRateio
                            .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                            .Where(y => y.LancamentoId.Equals(lancamentoExistente.LancamentoId))
                            .ToListAsync();

                        context.LancamentoRateio.RemoveRange(lancamentoRateiosToDelete);

                        foreach (var item in programacaoRateios)
                        {
                            context.LancamentoRateio.Add(new LancamentoRateio
                            {
                                AplicacaoId = AplicacaoId,
                                LancamentoId = lancamentoExistente.LancamentoId,
                                TipoTransacao = lancamentoExistente.TipoTransacao,
                                PlanoContaId = item.PlanoContaId,
                                CentroCustoId = item.CentroCustoId,
                                Observacao = item.Observacao,
                                Porcentagem = item.Porcentagem,
                            });
                        }
                    }
                    else if (lancamentoExistente == null)
                    {
                        //Incluindo
                        var novoLancamento = context.Lancamento.Add(new Lancamento
                        {
                            AplicacaoId = AplicacaoId,
                            ProgramacaoId = dto.ProgramacaoId,
                            TipoProgramacao = TipoProgramacao.LancamentoProgramado,
                            TipoTransacao = entity.TipoTransacao,
                            Historico = dto.Historico,
                            DataVencimento = date,
                            ValorVencimento = dto.ValorVencimento,
                            Observacao = dto.Observacao,
                            PessoaId = dto.PessoaId,
                            TipoPessoa = dto.TipoPessoa,
                            ContaId = dto.ContaId,
                            TipoConta = dto.TipoConta,
                        });
                        foreach (var item in programacaoRateios)
                        {
                            context.LancamentoRateio.Add(new LancamentoRateio
                            {
                                AplicacaoId = AplicacaoId,
                                LancamentoId = novoLancamento.LancamentoId,
                                TipoTransacao = novoLancamento.TipoTransacao,
                                PlanoContaId = item.PlanoContaId,
                                CentroCustoId = item.CentroCustoId,
                                Observacao = item.Observacao,
                                Porcentagem = item.Porcentagem
                            });
                        }
                    }
                    await context.SaveChangesAsync();
                }

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

            var result = await GetDetail(dto.ProgramacaoId);

            return result;
        }

        public async Task<bool> Remove(long id)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            bool result = false;

            try
            {
                var entity = await context.LancamentoProgramado
                    .Include(x => x.Rateios)
                    .Include(x => x.Lancamentos.Select(y => y.Rateios))
                    .Where(x => x.AplicacaoId == AplicacaoId)
                    .Where(x => x.ProgramacaoId == id)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                foreach (var item in entity.Lancamentos)
                {
                    context.LancamentoRateio.RemoveRange(item.Rateios);
                }

                context.Lancamento.RemoveRange(entity.Lancamentos);                
                context.ProgramacaoRateio.RemoveRange(entity.Rateios);
                context.LancamentoProgramado.Remove(entity);

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

        private LancamentoProgramadoDetailViewDTO ConvertEntityToDTO(LancamentoProgramado entity)
        {
            var rateios = new List<RateioDetailViewDTO>();
            foreach (var item in entity.Rateios)
            {
                rateios.Add(new RateioDetailViewDTO
                (
                    planoConta: PlanoContaConverter.ConvertEntityToDTO(item.PlanoConta),
                    centroCusto: CentroCustoConverter.ConvertEntityToDTO(item.CentroCusto),
                    observacao: item.Observacao,
                    porcentagem: item.Porcentagem
                ));
            }

            var result = new LancamentoProgramadoDetailViewDTO
            (
                programacaoId: entity.ProgramacaoId,
                tipoTransacao: entity.TipoTransacao,
                rateios: rateios,
                pessoaSelectView: entity.PessoaId.HasValue ? new PessoaSelectViewDTO
                (
                    pessoaId: entity.Pessoa.PessoaId,
                    tipoPessoa: entity.Pessoa.TipoPessoa,
                    nome: entity.Pessoa is PessoaFisica ? ((PessoaFisica)entity.Pessoa).NomeCompleto : ((PessoaJuridica)entity.Pessoa).NomeFantasia
                ) : null,
                contaId: entity.ContaId,
                tipoConta: entity.TipoConta,
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
