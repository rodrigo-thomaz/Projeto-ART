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
    public class TipoEnderecoService : ServiceBase, ITipoEnderecoService
    {
        #region constructors

        public TipoEnderecoService()
        {

        }

        #endregion

        #region public voids

        public async Task<PagedListResponse<TipoEnderecoMasterDTO>> GetMasterList(PagedListRequest<TipoEnderecoMasterDTO> pagedListRequest, TipoPessoa tipoPessoa, bool? ativo)
        {
            var pagedList = await GetPagedList(pagedListRequest, tipoPessoa, ativo);
            var masterList = ConvertEntityListToMasterList(pagedList.Data);
            var result = new PagedListResponse<TipoEnderecoMasterDTO>
            (
                data: masterList,
                totalRecords: pagedList.TotalRecords
            );
            return result;
        }

        public async Task<List<TipoEnderecoSelectViewDTO>> GetSelectViewList(TipoPessoa tipoPessoa)
        {
            List<TipoEnderecoSelectViewDTO> result = new List<TipoEnderecoSelectViewDTO>();

            var context = new RThomazDbContext();

            try
            {
                var data = await context.TipoEndereco
                    .Where(x => x.Ativo)
                    .Where(x => x.TipoPessoa == tipoPessoa)
                    .OrderBy(x => x.Nome)
                    .ToListAsync();

                foreach (var item in data)
                {
                    result.Add(new TipoEnderecoSelectViewDTO
                    (
                        tipoEnderecoId: item.TipoEnderecoId,
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

        public async Task<TipoEnderecoDetailViewDTO> GetDetail(Guid id, TipoPessoa tipoPessoa)
        {
            TipoEndereco entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.TipoEndereco
                        .Where(x => x.TipoEnderecoId.Equals(id))
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

        public async Task<TipoEnderecoDetailViewDTO> Insert(TipoEnderecoDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();

            TipoEnderecoDetailViewDTO result;

            try
            {
                var entity = new TipoEndereco
                {
                    AplicacaoId = AplicacaoId,
                    Nome = dto.Nome,
                    TipoPessoa = dto.TipoPessoa,
                    Ativo = dto.Ativo,
                };

                context.TipoEndereco.Add(entity);

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

        public async Task<TipoEnderecoDetailViewDTO> Edit(TipoEnderecoDetailEditDTO dto)
        {
            var context = new RThomazDbContext();

            TipoEnderecoDetailViewDTO result;

            try
            {
                var entity = await context.TipoEndereco
                        .Where(x => x.TipoEnderecoId.Equals(dto.TipoEnderecoId))
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

        public async Task<bool> Remove(Guid tipoEnderecoId, TipoPessoa tipoPessoa)
        {
            var context = new RThomazDbContext();
            
            bool result = false;

            try
            {
                var entity = await context.TipoEndereco
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.TipoEnderecoId.Equals(tipoEnderecoId))
                    .Where(x => x.TipoPessoa == tipoPessoa)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                context.TipoEndereco.Remove(entity);

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
                IQueryable<TipoEndereco> query = context.TipoEndereco
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

        public async Task<bool> UniqueNome(Guid tipoEnderecoId, TipoPessoa tipoPessoa, string nome)
        {
            bool result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<TipoEndereco> query = context.TipoEndereco
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.TipoPessoa == tipoPessoa)
                        .Where(x => x.Nome.Equals(nome))
                        .Where(x => x.TipoEnderecoId != tipoEnderecoId);
                
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

        private async Task<PagedListResponse<TipoEndereco>> GetPagedList(PagedListRequest<TipoEnderecoMasterDTO> pagedListRequest, TipoPessoa tipoPessoa, bool? ativo)
        {
            PagedListResponse<TipoEndereco> result;

            try
            {
                List<TipoEndereco> dataPaged;

                int totalRecords;

                using (var context = new RThomazDbContext())
                {
                    IQueryable<TipoEndereco> query = context.TipoEndereco
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

                        ExpressionHelper.ApplyOrder<TipoEndereco, string, TipoEnderecoMasterDTO, string>(
                            ref query, x => x.Nome, pagedListRequest, x => x.Nome, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<TipoEndereco, Guid, TipoEnderecoMasterDTO, Guid>(
                            ref query, x => x.TipoEnderecoId, pagedListRequest, x => x.TipoEnderecoId, ref isFirstOrderable);                        
                    }

                    totalRecords = await query.Select(x => x.TipoEnderecoId).CountAsync();

                    dataPaged = (await query
                        .ToListAsync())
                        .Skip(pagedListRequest.Param.Skip)
                        .Take(pagedListRequest.Param.Take)
                        .ToList();
                }

                result = new PagedListResponse<TipoEndereco>(
                    data: dataPaged,
                    totalRecords: totalRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private List<TipoEnderecoMasterDTO> ConvertEntityListToMasterList(List<TipoEndereco> data)
        {
            var result = new List<TipoEnderecoMasterDTO>(); 

            foreach (var item in data)
            {
                result.Add(new TipoEnderecoMasterDTO
                   (
                       tipoEnderecoId: item.TipoEnderecoId,
                       tipoPessoa: item.TipoPessoa,
                       nome: item.Nome,
                       ativo: item.Ativo
                   ));   
            }

            return result;
        }

        private TipoEnderecoDetailViewDTO ConvertEntityToDetail(TipoEndereco entity)
        {
            var result = new TipoEnderecoDetailViewDTO
            (
                tipoEnderecoId: entity.TipoEnderecoId,
                tipoPessoa: entity.TipoPessoa,
                nome: entity.Nome,
                ativo: entity.Ativo
            );

            return result;
        }
        
        #endregion
    }
}
