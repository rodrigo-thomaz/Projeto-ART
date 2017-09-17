using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using RThomaz.Domain.Financeiro.Services.Converters;
using RThomaz.Infra.CrossCutting.ExceptionHandling;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public class PlanoContaService : ServiceBase, IPlanoContaService
    {
        #region constructors

        public PlanoContaService()
        {

        }

        #endregion

        #region public voids

        public async Task<List<PlanoContaMasterDTO>> GetMasterList(TipoTransacao tipoTransacao, string search, bool mostrarInativos)
        {
            List<PlanoContaMasterDTO> result;

            try
            {
                List<PlanoConta> data;

                using (var context = new RThomazDbContext())
                {
                    IQueryable<PlanoConta> query = context.PlanoConta
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId));

                    query = query
                        .Where(x => x.TipoTransacao == tipoTransacao);

                    if (!mostrarInativos)
                    {
                        query = query
                            .Where(x => x.Ativo == true);
                    }
                    
                    data = await query.ToListAsync();                    
                }
                              
                //Removendo os nodes que ficam sem root por causa da propriedade Ativo, 
                //quando um item no meio da árvore está desativado os filhos sobram                
                var roots = data.Where(x => x.ParentId == null).ToList();
                var dataClean = new List<PlanoConta>();
                dataClean.AddRange(roots);
                GetChildren(data, roots, ref dataClean);

                //Safistazendo a cláusula where
                if (!string.IsNullOrEmpty(search))
                {
                    var parents = new List<PlanoConta>();
                    var dataSearch = dataClean.Where(x => x.Nome.Trim().ToLower().Contains(search.Trim().ToLower())).ToList();

                    GetParents(dataClean, dataSearch, ref parents);
                    result = ConvertEntityListToMasterList(parents);
                }
                else
                {
                    result = ConvertEntityListToMasterList(dataClean);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result
                .OrderBy(x => x.ParentId)
                .OrderBy(x => x.Nome)
                .ToList();
        }

        public async Task<SelectListResponseDTO<PlanoContaSelectViewDTO>> GetSelectViewList(SelectListRequestDTO selectListDTORequest, TipoTransacao tipoTransacao)
        {
            SelectListResponseDTO<PlanoContaSelectViewDTO> result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<PlanoConta> query = context.PlanoConta
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.TipoTransacao == tipoTransacao)
                    .Where(x => x.Ativo)
                    .OrderBy(x => x.Nome);

                //Busca geral
                if (!string.IsNullOrEmpty(selectListDTORequest.Search))
                {
                    query = query.Where(x => x.Nome.Contains(selectListDTORequest.Search));
                }

                var totalRecords = await query.Select(x => x.PlanoContaId).CountAsync();

                var dataPaged = await query
                    .Skip(selectListDTORequest.Skip)
                    .Take(selectListDTORequest.PageSize)
                    .ToListAsync();

                var roots = dataPaged.Where(x => !x.ParentId.HasValue);

                var dtos = PlanoContaConverter.ConvertEntityToDTO(roots);

                result = new SelectListResponseDTO<PlanoContaSelectViewDTO>(
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

        public async Task<PlanoContaDetailViewDTO> GetDetail(long planoContaId, TipoTransacao tipoTransacao)
        {
            PlanoConta entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.PlanoConta
                        .Include(x => x.Parent)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.PlanoContaId.Equals(planoContaId))
                        .Where(x => x.TipoTransacao == tipoTransacao)                        
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

        public async Task<PlanoContaDetailViewDTO> Insert(PlanoContaDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            PlanoContaDetailViewDTO result;

            try
            {
                PlanoConta parent = null;

                if (dto.ParentId.HasValue)
                {
                    parent = await context.PlanoConta
                        .FirstOrDefaultAsync(x => x.PlanoContaId.Equals(dto.ParentId.Value));

                    if (parent == null)
                    {
                        throw new RecordNotFoundException();
                    }
                }

                var entity = new PlanoConta
                {
                    AplicacaoId = AplicacaoId,
                    TipoTransacao = dto.TipoTransacao,
                    ParentId = dto.ParentId,
                    Parent = parent,
                    Nome = dto.Nome,
                    Ativo = dto.Ativo,
                    Descricao = dto.Descricao
                };

                context.PlanoConta.Add(entity);

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

        public async Task<PlanoContaDetailViewDTO> Edit(PlanoContaDetailEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            PlanoContaDetailViewDTO result;

            try
            {
                var entity = await context.PlanoConta
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.PlanoContaId.Equals(dto.PlanoContaId))
                        .Where(x => x.TipoTransacao == dto.TipoTransacao)
                        .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                if (dto.ParentId.HasValue && dto.ParentId != entity.ParentId)
                {
                    var parent = await context.PlanoConta
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.PlanoContaId.Equals(dto.ParentId.Value))
                        .FirstOrDefaultAsync();

                    if (parent == null)
                    {
                        throw new RecordNotFoundException();
                    }

                    entity.Parent = parent;
                    entity.ParentId = parent.ParentId;
                }

                entity.Nome = dto.Nome;
                entity.Ativo = dto.Ativo;
                entity.Descricao = dto.Descricao;

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

        public async Task<bool> Remove(long planoContaId, TipoTransacao tipoTransacao)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();
            
            bool result = false;

            try
            {
                var entity = await context.PlanoConta
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.PlanoContaId.Equals(planoContaId))
                    .Where(x => x.TipoTransacao == tipoTransacao)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                context.PlanoConta.Remove(entity);

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

        public async Task<bool> UniqueNome(TipoTransacao tipoTransacao, string nome)
        {
            bool result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<PlanoConta> query = context.PlanoConta
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.TipoTransacao == tipoTransacao)
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

        public async Task Move(PlanoContaMasterMoveDTO dto)
        {
            var context = new RThomazDbContext();

            try
            {
                var entities = await context.PlanoConta
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => dto.PlanoContaIds.Contains(x.PlanoContaId))
                        .ToListAsync();

                foreach (var entity in entities)
                {
                    entity.ParentId = dto.ParentId;
                }

                await context.SaveChangesAsync();
            }
            finally
            {
                if (context != null)
                {
                    context.Dispose();
                }
            }
        }

        public async Task Rename(long planoContaId, string nome)
        {
            var context = new RThomazDbContext();

            try
            {
                var entity = await context.PlanoConta
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.PlanoContaId == planoContaId)
                        .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                entity.Nome = nome;

                await context.SaveChangesAsync();
            }
            finally
            {
                if (context != null)
                {
                    context.Dispose();
                }
            }
        }

        #endregion

        #region private voids

        private List<PlanoContaMasterDTO> ConvertEntityListToMasterList(List<PlanoConta> data)
        {
            var result = new List<PlanoContaMasterDTO>();
            data.ForEach(x => result.Add(new PlanoContaMasterDTO
            (
                planoContaId: x.PlanoContaId,
                nome: x.Nome,
                ativo: x.Ativo,
                tipoTransacao: x.TipoTransacao,
                parentId: x.ParentId
            )));
            return result;
        }

        private PlanoContaDetailViewDTO ConvertEntityToDetail(PlanoConta entity)
        {
            PlanoContaSelectViewDTO parent = null;

            if (entity.Parent != null)
            {
                parent = PlanoContaConverter.ConvertEntityToDTO(entity.Parent);
            }

            var result = new PlanoContaDetailViewDTO
            (
                planoContaId: entity.PlanoContaId,
                tipoTransacao: entity.TipoTransacao,
                parent: parent,
                nome: entity.Nome,
                ativo: entity.Ativo,
                descricao: entity.Descricao
            );

            return result;
        }        

        private void GetChildren(List<PlanoConta> data, List<PlanoConta> roots, ref List<PlanoConta> result)
        {
            foreach (var item in roots)
            {
                var children = data.Where(x => x.ParentId.Equals(item.PlanoContaId)).ToList();
                if (children.Count() > 0)
                {
                    result.AddRange(children);
                    GetChildren(data, children, ref result);
                }
            }
        }

        private void GetParents(List<PlanoConta> data, List<PlanoConta> items, ref List<PlanoConta> result)
        {
            foreach (var item in items)
            {
                if (!result.Exists(x => x.PlanoContaId.Equals(item.PlanoContaId)))
                {
                    result.Add(item);
                }
                GetParents(data, item, ref result);
            }
        }

        private void GetParents(List<PlanoConta> data, PlanoConta item, ref List<PlanoConta> result)
        {
            var parent = data.FirstOrDefault(x => x.PlanoContaId.Equals(item.ParentId));
            if (parent != null)
            {
                if (!result.Exists(x => x.PlanoContaId.Equals(parent.PlanoContaId)))
                {
                    result.Add(parent);
                }
                GetParents(data, parent, ref result);
            }
        }

        #endregion
    }
}
