using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using RThomaz.Infra.CrossCutting.ExceptionHandling;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public class TipoHomePageService : ServiceBase, ITipoHomePageService
    {
        #region constructors

        public TipoHomePageService()
        {

        }

        #endregion

        #region public voids

        public async Task<PagedListResponse<TipoHomePageMasterDTO>> GetMasterList(PagedListRequest<TipoHomePageMasterDTO> pagedListRequest, TipoPessoa tipoPessoa, bool? ativo)
        {
            var pagedList = await GetPagedList(pagedListRequest, tipoPessoa, ativo);
            var masterList = ConvertEntityListToMasterList(pagedList.Data);
            var result = new PagedListResponse<TipoHomePageMasterDTO>
            (
                data: masterList,
                totalRecords: pagedList.TotalRecords
            );
            return result;
        }

        public async Task<List<TipoHomePageSelectViewDTO>> GetSelectViewList(TipoPessoa tipoPessoa)
        {
            List<TipoHomePageSelectViewDTO> result = new List<TipoHomePageSelectViewDTO>();

            var context = new RThomazDbContext();

            try
            {
                var data = await context.TipoHomePage
                    .Where(x => x.Ativo)
                    .Where(x => x.TipoPessoa == tipoPessoa)
                    .OrderBy(x => x.Nome)
                    .ToListAsync();

                foreach (var item in data)
                {
                    result.Add(new TipoHomePageSelectViewDTO
                    (
                        tipoHomePageId: item.TipoHomePageId,
                        tipoPessoa: item.TipoPessoa,
                        nome: item.Nome
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

        public async Task<TipoHomePageDetailViewDTO> GetDetail(Guid id, TipoPessoa tipoPessoa)
        {
            TipoHomePage entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.TipoHomePage
                        .Where(x => x.TipoHomePageId.Equals(id))
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.TipoPessoa == tipoPessoa)
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

        public async Task<TipoHomePageDetailViewDTO> Insert(TipoHomePageDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();

            TipoHomePageDetailViewDTO result;

            try
            {
                var entity = new TipoHomePage
                {
                    AplicacaoId = AplicacaoId,
                    Nome = dto.Nome,
                    TipoPessoa = dto.TipoPessoa,
                    Ativo = dto.Ativo,
                };

                context.TipoHomePage.Add(entity);

                await context.SaveChangesAsync();

                result = ConvertEntityToDetail(entity);
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

        public async Task<TipoHomePageDetailViewDTO> Edit(TipoHomePageDetailEditDTO dto)
        {
            var context = new RThomazDbContext();

            TipoHomePageDetailViewDTO result;

            try
            {
                var entity = await context.TipoHomePage
                        .Where(x => x.TipoHomePageId.Equals(dto.TipoHomePageId))
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.TipoPessoa == dto.TipoPessoa)
                        .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                entity.Nome = dto.Nome;
                entity.Ativo = dto.Ativo;

                await context.SaveChangesAsync();

                result = ConvertEntityToDetail(entity);
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

        public async Task<bool> Remove(Guid tipoHomePageId, TipoPessoa tipoPessoa)
        {
            var context = new RThomazDbContext();
            
            bool result = false;

            try
            {
                var entity = await context.TipoHomePage
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.TipoHomePageId.Equals(tipoHomePageId))
                    .Where(x => x.TipoPessoa == tipoPessoa)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                context.TipoHomePage.Remove(entity);

                await context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException == false)
                {
                    throw ex;
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

        public async Task<bool> UniqueNome(TipoPessoa tipoPessoa, string nome)
        {
            bool result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<TipoHomePage> query = context.TipoHomePage
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.TipoPessoa == tipoPessoa)
                        .Where(x => x.Nome.Equals(nome));

                result = await query.AnyAsync();
            }
            finally
            {
                if (context != null)
                {
                    context.Dispose();
                }
            }

            return !result;
        }

        public async Task<bool> UniqueNome(Guid tipoHomePageId, TipoPessoa tipoPessoa, string nome)
        {
            bool result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<TipoHomePage> query = context.TipoHomePage
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.TipoPessoa == tipoPessoa)
                        .Where(x => x.Nome.Equals(nome))
                        .Where(x => x.TipoHomePageId != tipoHomePageId);
                
                result = await query.AnyAsync();
            }
            finally
            {
                if (context != null)
                {
                    context.Dispose();
                }
            }

            return !result;
        }

        #endregion

        #region private voids

        private async Task<PagedListResponse<TipoHomePage>> GetPagedList(PagedListRequest<TipoHomePageMasterDTO> pagedListRequest, TipoPessoa tipoPessoa, bool? ativo)
        {
            PagedListResponse<TipoHomePage> result;

            try
            {
                List<TipoHomePage> dataPaged;

                int totalRecords;

                using (var context = new RThomazDbContext())
                {
                    IQueryable<TipoHomePage> query = context.TipoHomePage
                        .Where(x => x.TipoPessoa == tipoPessoa)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId));

                    if (ativo.HasValue)
                    {
                        query = query
                            .Where(x => x.Ativo == ativo);
                    }

                    //Busca geral
                    if (pagedListRequest.Search != null)
                    {
                        query = query.Where(x =>
                            x.Nome.Contains(pagedListRequest.Search.Value)
                        );
                    }

                    //Ordenando por campo
                    if (pagedListRequest.OrderColumns != null)
                    {
                        bool isFirstOrderable = true;

                        ExpressionHelper.ApplyOrder<TipoHomePage, string, TipoHomePageMasterDTO, string>(
                            ref query, x => x.Nome, pagedListRequest, x => x.Nome, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<TipoHomePage, Guid, TipoHomePageMasterDTO, Guid>(
                            ref query, x => x.TipoHomePageId, pagedListRequest, x => x.TipoHomePageId, ref isFirstOrderable);                        
                    }

                    totalRecords = await query.Select(x => x.TipoHomePageId).CountAsync();

                    dataPaged = (await query
                        .ToListAsync())
                        .Skip(pagedListRequest.Param.Skip)
                        .Take(pagedListRequest.Param.Take)
                        .ToList();
                }

                result = new PagedListResponse<TipoHomePage>(
                    data: dataPaged,
                    totalRecords: totalRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private List<TipoHomePageMasterDTO> ConvertEntityListToMasterList(List<TipoHomePage> data)
        {
            var result = new List<TipoHomePageMasterDTO>(); 

            foreach (var item in data)
            {
                result.Add(new TipoHomePageMasterDTO
                   (
                       tipoHomePageId: item.TipoHomePageId,
                       tipoPessoa: item.TipoPessoa,
                       nome: item.Nome,
                       ativo: item.Ativo
                   ));   
            }

            return result;
        }

        private TipoHomePageDetailViewDTO ConvertEntityToDetail(TipoHomePage entity)
        {
            var result = new TipoHomePageDetailViewDTO
            (
                tipoHomePageId: entity.TipoHomePageId,
                tipoPessoa: entity.TipoPessoa,
                nome: entity.Nome,
                ativo: entity.Ativo
            );

            return result;
        }

        #endregion
    }
}
