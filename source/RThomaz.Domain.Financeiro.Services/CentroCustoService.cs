using RThomaz.Domain.Financeiro.Services.Converters;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Infra.CrossCutting.ExceptionHandling;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public class CentroCustoService : ServiceBase, ICentroCustoService
    {
        #region constructors

        public CentroCustoService()
        {

        }

        #endregion

        #region public voids

        public async Task<List<CentroCustoMasterDTO>> GetMasterList(string search, bool mostrarInativos)
        {
            List<CentroCustoMasterDTO> result;

            try
            {
                List<CentroCusto> data;

                using (var context = new RThomazDbContext())
                {
                    IQueryable<CentroCusto> query = context.CentroCusto
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId));

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
                var dataClean = new List<CentroCusto>();
                dataClean.AddRange(roots);
                GetChildren(data, roots, ref dataClean);

                //Safistazendo a cláusula where
                if (!string.IsNullOrEmpty(search))
                {
                    var parents = new List<CentroCusto>();
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

            return result;
        }

        public async Task<SelectListResponseDTO<CentroCustoSelectViewDTO>> GetSelectViewList(SelectListRequestDTO selectListDTORequest)
        {
            SelectListResponseDTO<CentroCustoSelectViewDTO> result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<CentroCusto> query = context.CentroCusto
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.Ativo)
                    .OrderBy(x => x.Nome);

                //Busca geral
                if (!string.IsNullOrEmpty(selectListDTORequest.Search))
                {
                    query = query.Where(x => x.Nome.Contains(selectListDTORequest.Search));
                }

                var totalRecords = await query.Select(x => x.CentroCustoId).CountAsync();

                var dataPaged = await query
                    .Skip(selectListDTORequest.Skip)
                    .Take(selectListDTORequest.PageSize)
                    .ToListAsync();

                var roots = dataPaged.Where(x => !x.ParentId.HasValue);

                var dtos = CentroCustoConverter.ConvertEntityToDTO(roots);                

                result = new SelectListResponseDTO<CentroCustoSelectViewDTO>(
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

        public async Task<CentroCustoDetailViewDTO> GetDetail(long centroCustoId)
        {
            CentroCusto entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.CentroCusto
                        .Include(x => x.Responsavel.Perfil)
                        .Include(x => x.Parent)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.CentroCustoId.Equals(centroCustoId))
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

        public async Task<CentroCustoDetailViewDTO> Insert(CentroCustoDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            CentroCustoDetailViewDTO result;

            try
            {
                CentroCusto parent = null;

                if(dto.ParentId.HasValue)
                {
                    parent = await context.CentroCusto
                        .FirstOrDefaultAsync(x => x.CentroCustoId.Equals(dto.ParentId.Value));

                    if (parent == null)
                    {
                        throw new RecordNotFoundException();
                    }
                }                

                var entity = new CentroCusto
                {
                    AplicacaoId = AplicacaoId,
                    ParentId = dto.ParentId,
                    Parent = parent,
                    ResponsavelId = dto.ResponsavelId,
                    Nome = dto.Nome,
                    Ativo = dto.Ativo,
                    Descricao = dto.Descricao,
                };

                context.CentroCusto.Add(entity);

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

        public async Task<CentroCustoDetailViewDTO> Edit(CentroCustoDetailEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            CentroCustoDetailViewDTO result;

            try
            {
                var entity = await context.CentroCusto
                        .Include(x => x.Parent)
                        .Include(x => x.Responsavel.Perfil)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.CentroCustoId.Equals(dto.CentroCustoId))
                        .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                if (dto.ParentId.HasValue && dto.ParentId != entity.ParentId)
                {
                    var parent = await context.CentroCusto
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.CentroCustoId.Equals(dto.ParentId.Value))
                        .FirstOrDefaultAsync();

                    if (parent == null)
                    {
                        throw new RecordNotFoundException();
                    }

                    entity.Parent = parent;
                    entity.ParentId = parent.ParentId;
                }

                if (dto.ResponsavelId.HasValue && dto.ResponsavelId != entity.ResponsavelId)
                {
                    context.Usuario
                        .Include(x => x.Perfil)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.UsuarioId == dto.ResponsavelId)
                        .Load();
                }

                entity.Nome = dto.Nome;
                entity.Ativo = dto.Ativo;
                entity.Descricao = dto.Descricao;
                entity.ResponsavelId = dto.ResponsavelId;

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

        public async Task<bool> Remove(long centroCustoId)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            bool result = false;

            try
            {
                var entity = await context.CentroCusto
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.CentroCustoId.Equals(centroCustoId))
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                context.CentroCusto.Remove(entity);

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

        public async Task<bool> UniqueNome(long? centroCustoId, string nome)
        {
            bool result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<CentroCusto> query = context.CentroCusto
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.Nome.Equals(nome));

                if (centroCustoId.HasValue)
                {
                    query = query.Where(x => x.CentroCustoId != centroCustoId.Value);
                }

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

        public async Task Move(CentroCustoMasterMoveDTO dto)
        {
            var context = new RThomazDbContext();

            try
            {
                var entities = await context.CentroCusto
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => dto.CentroCustoIds.Contains(x.CentroCustoId))
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

        public async Task Rename(long centroCustoId, string nome)
        {
            var context = new RThomazDbContext();

            try
            {
                var entity = await context.CentroCusto
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.CentroCustoId == centroCustoId)
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

        private void GetChildren(List<CentroCusto> data, List<CentroCusto> roots, ref List<CentroCusto> result)
        {
            foreach (var item in roots)
            {
                var children = data.Where(x => x.ParentId.Equals(item.CentroCustoId)).ToList();
                if (children.Count() > 0)
                {
                    result.AddRange(children);
                    GetChildren(data, children, ref result);
                }
            }
        }

        private void GetParents(List<CentroCusto> data, List<CentroCusto> items, ref List<CentroCusto> result)
        {
            foreach (var item in items)
            {
                if (!result.Exists(x => x.CentroCustoId.Equals(item.CentroCustoId)))
                {
                    result.Add(item);
                }
                GetParents(data, item, ref result);
            }
        }

        private void GetParents(List<CentroCusto> data, CentroCusto item, ref List<CentroCusto> result)
        {
            var parent = data.FirstOrDefault(x => x.CentroCustoId.Equals(item.ParentId));
            if (parent != null)
            {
                if (!result.Exists(x => x.CentroCustoId.Equals(parent.CentroCustoId)))
                {
                    result.Add(parent);
                }
                GetParents(data, parent, ref result);
            }
        }

        private List<CentroCustoMasterDTO> ConvertEntityListToMasterList(List<CentroCusto> data)
        {
            var result = new List<CentroCustoMasterDTO>();
            data.ForEach(x => result.Add(new CentroCustoMasterDTO
            (
                centroCustoId: x.CentroCustoId,
                nome: x.Nome,
                ativo: x.Ativo,
                parentId: x.ParentId
            )));
            return result;
        }

        private CentroCustoDetailViewDTO ConvertEntityToDetail(CentroCusto entity)
        {
            CentroCustoSelectViewDTO parent = null;

            if(entity.Parent != null)
            {
                parent = CentroCustoConverter.ConvertEntityToDTO(entity.Parent);
            }

            var result = new CentroCustoDetailViewDTO
            (
                centroCustoId: entity.CentroCustoId,
                parent: parent,
                responsavel: entity.Responsavel == null ? null : new UsuarioSelectViewDTO
                (
                    usuarioId: entity.Responsavel.UsuarioId,
                    email: entity.Responsavel.Email,
                    nomeExibicao: entity.Responsavel.Perfil.NomeExibicao,
                    avatarStorageObject: entity.Responsavel.Perfil.AvatarStorageObject
                ),
                nome: entity.Nome,
                ativo: entity.Ativo,
                descricao: entity.Descricao
            );

            return result;
        }

        #endregion
    }
}
