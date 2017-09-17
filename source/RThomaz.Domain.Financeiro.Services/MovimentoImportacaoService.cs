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
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using RThomaz.Infra.CrossCutting.ExceptionHandling;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public class MovimentoImportacaoService : ServiceBase, IMovimentoImportacaoService
    {
        #region constructors

        public MovimentoImportacaoService()
        {

        }

        #endregion

        #region public voids             

        public async Task<PagedListResponse<MovimentoImportacaoMasterDTO>> GetMasterList(PagedListRequest<MovimentoImportacaoMasterDTO> pagedListRequest, long movimentoImportacaoId, TipoTransacao? tipoTransacao)
        {
            PagedListResponse<MovimentoImportacaoMasterDTO> result;

            try
            {
                List<MovimentoImportacaoMasterDTO> dataPaged;

                int totalRecords;

                using (var context = new RThomazDbContext())
                {  
                    IQueryable<Movimento> query = context.Movimento                        
                        .Include(x => x.Conciliacoes)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.MovimentoImportacaoId.HasValue)
                        .Where(x => x.MovimentoImportacaoId == movimentoImportacaoId);

                    //Busca geral
                    if (pagedListRequest.Search != null)
                    {
                        query = query.Where(x =>
                            x.Historico.Contains(pagedListRequest.Search.Value)
                        );
                    }

                    if(tipoTransacao.HasValue)
                    {
                        query = query
                            .Where(x => x.TipoTransacao == tipoTransacao.Value);
                    }

                    //Ordenando...
                    query = query
                        .OrderBy(x => x.DataMovimento)
                        .ThenBy(x => x.TipoTransacao);

                    totalRecords = await query.Select(x => x.MovimentoId).CountAsync();

                    dataPaged = ConvertEntityListToMasterList(await query.ToListAsync())
                        .Skip(pagedListRequest.Param.Skip)
                        .Take(pagedListRequest.Param.Take)
                        .ToList();
                }
                result = new PagedListResponse<MovimentoImportacaoMasterDTO>(
                    data: dataPaged,
                    totalRecords: totalRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<SelectListResponseDTO<MovimentoImportacaoSelectViewDTO>> GetSelectViewList(SelectListRequestDTO selectListDTORequest)
        {
            SelectListResponseDTO<MovimentoImportacaoSelectViewDTO> result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<MovimentoImportacao> query = context.MovimentoImportacao
                .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                .OrderBy(x => x.ImportadoEm);

                //Busca geral
                if (!string.IsNullOrEmpty(selectListDTORequest.Search))
                {
                    query = query.Where(x => x.ImportadoEm.ToShortDateString().Contains(selectListDTORequest.Search));
                }

                var totalRecords = await query.Select(x => x.MovimentoImportacaoId).CountAsync();

                var dataPaged = await query
                    .Skip(selectListDTORequest.Skip)
                    .Take(selectListDTORequest.PageSize)
                    .ToListAsync();

                var dtos = new List<MovimentoImportacaoSelectViewDTO>();

                foreach (var item in dataPaged)
                {
                    var dto = new MovimentoImportacaoSelectViewDTO(item.MovimentoImportacaoId, item.ImportadoEm);
                    dtos.Add(dto);
                }

                result = new SelectListResponseDTO<MovimentoImportacaoSelectViewDTO>(
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

        public async Task<bool> Remove(long movimentoImportacaoId)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            bool result = false;

            try
            {
                var entity = await context.MovimentoImportacao
                    .Include(x => x.Movimentacoes)
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.MovimentoImportacaoId.Equals(movimentoImportacaoId))
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                context.Movimento.RemoveRange(entity.Movimentacoes);
                context.MovimentoImportacao.Remove(entity);

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

        private List<MovimentoImportacaoMasterDTO> ConvertEntityListToMasterList(List<Movimento> data)
        {
            var result = new List<MovimentoImportacaoMasterDTO>();

            foreach (var item in data)
            {
                short percentualConciliado = 0;

                if (item.Conciliacoes.Any())
                {
                    var totalConciliado = item.Conciliacoes.Sum(x => x.ValorConciliado);
                    percentualConciliado = Convert.ToInt16((Math.Abs(totalConciliado) * 100) / Math.Abs(item.ValorMovimento));
                }

                result.Add(new MovimentoImportacaoMasterDTO
                (
                    movimentoId: item.MovimentoId,
                    tipoTransacao: item.TipoTransacao,
                    dataMovimento: item.DataMovimento,
                    valorMovimento: item.ValorMovimento,
                    historico: item.Historico,
                    percentualConciliado: percentualConciliado
                ));
            }

            return result;
        }

        #endregion
    }
}
