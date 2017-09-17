using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Infra.CrossCutting.ExceptionHandling;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public class TipoEmailService : ServiceBase, ITipoEmailService
    {
        #region constructors

        public TipoEmailService()
        {

        }

        #endregion

        #region public voids

        public async Task<PagedListResponse<TipoEmailMasterDTO>> GetMasterList(PagedListRequest<TipoEmailMasterDTO> pagedListRequest, TipoPessoa tipoPessoa, bool? ativo)
        {
            var pagedList = await GetPagedList(pagedListRequest, tipoPessoa, ativo);
            var masterList = ConvertEntityListToMasterList(pagedList.Data);
            var result = new PagedListResponse<TipoEmailMasterDTO>
            (
                data: masterList,
                totalRecords: pagedList.TotalRecords
            );
            return result;
        }

        public async Task<List<TipoEmailSelectViewDTO>> GetSelectViewList(TipoPessoa tipoPessoa)
        {
            List<TipoEmailSelectViewDTO> result = new List<TipoEmailSelectViewDTO>();

            var context = new RThomazDbContext();

            try
            {
                var data = await context.TipoEmail
                    .Where(x => x.Ativo)
                    .Where(x => x.TipoPessoa == tipoPessoa)
                    .OrderBy(x => x.Nome)
                    .ToListAsync();                

                foreach (var item in data)
                {
                    result.Add(new TipoEmailSelectViewDTO
                    (
                        tipoEmailId: item.TipoEmailId,
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

        public async Task<TipoEmailDetailViewDTO> GetDetail(Guid id, TipoPessoa tipoPessoa)
        {
            TipoEmail entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.TipoEmail
                        .Where(x => x.TipoEmailId.Equals(id))
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

        public async Task<TipoEmailDetailViewDTO> Insert(TipoEmailDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();

            TipoEmailDetailViewDTO result;

            try
            {
                var entity = new TipoEmail
                {
                    AplicacaoId = AplicacaoId,
                    Nome = dto.Nome,
                    TipoPessoa = dto.TipoPessoa,
                    Ativo = dto.Ativo,
                };

                context.TipoEmail.Add(entity);

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

        public async Task<TipoEmailDetailViewDTO> Edit(TipoEmailDetailEditDTO dto)
        {
            var context = new RThomazDbContext();

            TipoEmailDetailViewDTO result;

            try
            {
                var entity = await context.TipoEmail
                        .Where(x => x.TipoEmailId.Equals(dto.TipoEmailId))
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

        public async Task<bool> Remove(Guid tipoEmailId, TipoPessoa tipoPessoa)
        {
            var context = new RThomazDbContext();
            
            bool result = false;

            try
            {
                var entity = await context.TipoEmail
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.TipoEmailId.Equals(tipoEmailId))
                    .Where(x => x.TipoPessoa == tipoPessoa)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                context.TipoEmail.Remove(entity);

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
                IQueryable<TipoEmail> query = context.TipoEmail
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

        public async Task<bool> UniqueNome(Guid tipoEmailId, TipoPessoa tipoPessoa, string nome)
        {
            bool result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<TipoEmail> query = context.TipoEmail
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.TipoPessoa == tipoPessoa)
                        .Where(x => x.Nome.Equals(nome))
                        .Where(x => x.TipoEmailId != tipoEmailId);
                
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

        private async Task<PagedListResponse<TipoEmail>> GetPagedList(PagedListRequest<TipoEmailMasterDTO> pagedListRequest, TipoPessoa tipoPessoa, bool? ativo)
        {
            PagedListResponse<TipoEmail> result;

            try
            {
                List<TipoEmail> dataPaged;

                int totalRecords;

                using (var context = new RThomazDbContext())
                {
                    IQueryable<TipoEmail> query = context.TipoEmail
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

                        ExpressionHelper.ApplyOrder<TipoEmail, string, TipoEmailMasterDTO, string>(
                            ref query, x => x.Nome, pagedListRequest, x => x.Nome, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<TipoEmail, Guid, TipoEmailMasterDTO, Guid>(
                            ref query, x => x.TipoEmailId, pagedListRequest, x => x.TipoEmailId, ref isFirstOrderable);                        
                    }

                    totalRecords = await query.Select(x => x.TipoEmailId).CountAsync();

                    dataPaged = (await query
                        .ToListAsync())
                        .Skip(pagedListRequest.Param.Skip)
                        .Take(pagedListRequest.Param.Take)
                        .ToList();
                }

                result = new PagedListResponse<TipoEmail>(
                    data: dataPaged,
                    totalRecords: totalRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private List<TipoEmailMasterDTO> ConvertEntityListToMasterList(List<TipoEmail> data)
        {
            var result = new List<TipoEmailMasterDTO>(); 

            foreach (var item in data)
            {
                result.Add(new TipoEmailMasterDTO
                   (
                       tipoEmailId: item.TipoEmailId,
                       tipoPessoa: item.TipoPessoa,
                       nome: item.Nome,
                       ativo: item.Ativo
                   ));   
            }

            return result;
        }

        private TipoEmailDetailViewDTO ConvertEntityToDetail(TipoEmail entity)
        {
            var result = new TipoEmailDetailViewDTO
            (
                tipoEmailId: entity.TipoEmailId,
                tipoPessoa: entity.TipoPessoa,
                nome: entity.Nome,
                ativo: entity.Ativo
            );

            return result;
        }        

        #endregion
    }
}
