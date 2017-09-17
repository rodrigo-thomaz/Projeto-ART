using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using RThomaz.Infra.CrossCutting.ExceptionHandling;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public class TipoTelefoneService : ServiceBase, ITipoTelefoneService
    {
        #region constructors

        public TipoTelefoneService()
        {

        }

        #endregion

        #region public voids

        public async Task<PagedListResponse<TipoTelefoneMasterDTO>> GetMasterList(PagedListRequest<TipoTelefoneMasterDTO> pagedListRequest, TipoPessoa tipoPessoa, bool? ativo)
        {
            var pagedList = await GetPagedList(pagedListRequest, tipoPessoa, ativo);
            var masterList = ConvertEntityListToMasterList(pagedList.Data);
            var result = new PagedListResponse<TipoTelefoneMasterDTO>
            (
                data: masterList,
                totalRecords: pagedList.TotalRecords
            );
            return result;
        }

        public async Task<List<TipoTelefoneSelectViewDTO>> GetSelectViewList(TipoPessoa tipoPessoa)
        {
            List<TipoTelefoneSelectViewDTO> result = new List<TipoTelefoneSelectViewDTO>();

            var context = new RThomazDbContext();

            try
            {
                var data = await context.TipoTelefone
                    .Where(x => x.Ativo)
                    .Where(x => x.TipoPessoa == tipoPessoa)
                    .OrderBy(x => x.Nome)
                    .ToListAsync();

                foreach (var item in data)
                {
                    result.Add(new TipoTelefoneSelectViewDTO
                    (
                        tipoTelefoneId: item.TipoTelefoneId,
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

        public async Task<TipoTelefoneDetailViewDTO> GetDetail(Guid id, TipoPessoa tipoPessoa)
        {
            TipoTelefone entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.TipoTelefone
                        .Where(x => x.TipoTelefoneId.Equals(id))
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

        public async Task<TipoTelefoneDetailViewDTO> Insert(TipoTelefoneDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();

            TipoTelefoneDetailViewDTO result;

            try
            {
                var entity = new TipoTelefone
                {
                    AplicacaoId = AplicacaoId,
                    Nome = dto.Nome,
                    TipoPessoa = dto.TipoPessoa,
                    Ativo = dto.Ativo,
                };

                context.TipoTelefone.Add(entity);

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

        public async Task<TipoTelefoneDetailViewDTO> Edit(TipoTelefoneDetailEditDTO dto)
        {
            var context = new RThomazDbContext();

            TipoTelefoneDetailViewDTO result;

            try
            {
                var entity = await context.TipoTelefone
                        .Where(x => x.TipoTelefoneId.Equals(dto.TipoTelefoneId))
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

        public async Task<bool> Remove(Guid tipoTelefoneId, TipoPessoa tipoPessoa)
        {
            var context = new RThomazDbContext();
            
            bool result = false;

            try
            {
                var entity = await context.TipoTelefone
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.TipoTelefoneId.Equals(tipoTelefoneId))
                    .Where(x => x.TipoPessoa == tipoPessoa)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                context.TipoTelefone.Remove(entity);

                await context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                if(ex is DbUpdateException == false)
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
                IQueryable<TipoTelefone> query = context.TipoTelefone
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

        public async Task<bool> UniqueNome(Guid tipoTelefoneId, TipoPessoa tipoPessoa, string nome)
        {
            bool result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<TipoTelefone> query = context.TipoTelefone
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.TipoPessoa == tipoPessoa)
                        .Where(x => x.Nome.Equals(nome))
                        .Where(x => x.TipoTelefoneId != tipoTelefoneId);
                
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

        private async Task<PagedListResponse<TipoTelefone>> GetPagedList(PagedListRequest<TipoTelefoneMasterDTO> pagedListRequest, TipoPessoa tipoPessoa, bool? ativo)
        {
            PagedListResponse<TipoTelefone> result;

            try
            {
                List<TipoTelefone> dataPaged;

                int totalRecords;

                using (var context = new RThomazDbContext())
                {
                    IQueryable<TipoTelefone> query = context.TipoTelefone
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

                        ExpressionHelper.ApplyOrder<TipoTelefone, string, TipoTelefoneMasterDTO, string>(
                            ref query, x => x.Nome, pagedListRequest, x => x.Nome, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<TipoTelefone, Guid, TipoTelefoneMasterDTO, Guid>(
                            ref query, x => x.TipoTelefoneId, pagedListRequest, x => x.TipoTelefoneId, ref isFirstOrderable);                        
                    }

                    totalRecords = await query.Select(x => x.TipoTelefoneId).CountAsync();

                    dataPaged = (await query
                        .ToListAsync())
                        .Skip(pagedListRequest.Param.Skip)
                        .Take(pagedListRequest.Param.Take)
                        .ToList();
                }

                result = new PagedListResponse<TipoTelefone>(
                    data: dataPaged,
                    totalRecords: totalRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private List<TipoTelefoneMasterDTO> ConvertEntityListToMasterList(List<TipoTelefone> data)
        {
            var result = new List<TipoTelefoneMasterDTO>(); 

            foreach (var item in data)
            {
                result.Add(new TipoTelefoneMasterDTO
                   (
                       tipoTelefoneId: item.TipoTelefoneId,
                       tipoPessoa: item.TipoPessoa,
                       nome: item.Nome,
                       ativo: item.Ativo
                   ));   
            }

            return result;
        }

        private TipoTelefoneDetailViewDTO ConvertEntityToDetail(TipoTelefone entity)
        {
            var result = new TipoTelefoneDetailViewDTO
            (
                tipoTelefoneId: entity.TipoTelefoneId,
                tipoPessoa: entity.TipoPessoa,
                nome: entity.Nome,
                ativo: entity.Ativo
            );

            return result;
        }

        #endregion
    }
}
