using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Infra.CrossCutting.Storage;
using System;
using System.Collections.Generic;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using RThomaz.Infra.CrossCutting.ExceptionHandling;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public class UsuarioService : ServiceBase, IUsuarioService
    {
        #region private consts

        private const string AvatarStorageFolder = "perfil/avatar";

        #endregion   

        #region constructors

        public UsuarioService()
        {

        }

        #endregion

        #region public voids

        public async Task<PagedListResponse<UsuarioMasterDTO>> GetMasterList(PagedListRequest<UsuarioMasterDTO> pagedListRequest, bool? ativo)
        {
            var pagedList = await GetPagedList(pagedListRequest, ativo);
            var masterList = ConvertEntityListToMasterList(pagedList.Data);
            var result = new PagedListResponse<UsuarioMasterDTO>
            (
                data: masterList,
                totalRecords: pagedList.TotalRecords
            );
            return result;
        }

        public async Task<SelectListResponseDTO<UsuarioSelectViewDTO>> GetSelectViewList(SelectListRequestDTO selectListDTORequest)
        {
            SelectListResponseDTO<UsuarioSelectViewDTO> result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<Usuario> query = context.Usuario
                    .Include(x => x.Perfil)
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.Ativo)
                    .OrderBy(x => x.Email);

                //Busca geral
                if (!string.IsNullOrEmpty(selectListDTORequest.Search))
                {
                    query = query.Where(x => x.Email.Contains(selectListDTORequest.Search));
                }

                var totalRecords = await query.Select(x => x.UsuarioId).CountAsync();

                var dataPaged = await query
                    .Skip(selectListDTORequest.Skip)
                    .Take(selectListDTORequest.PageSize)
                    .ToListAsync();

                var dtos = new List<UsuarioSelectViewDTO>();

                foreach (var item in dataPaged)
                {
                    dtos.Add(new UsuarioSelectViewDTO
                    (
                        usuarioId: item.UsuarioId,    
                        nomeExibicao: item.Perfil.NomeExibicao,
                        email: item.Email,
                        avatarStorageObject: item.Perfil.AvatarStorageObject
                    ));
                }

                result = new SelectListResponseDTO<UsuarioSelectViewDTO>(
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
        
        public async Task<UsuarioDetailViewDTO> GetDetail(long usuarioId)
        {
            Usuario entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.Usuario
                        .Include(x => x.Perfil)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.UsuarioId.Equals(usuarioId))
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

        public async Task<UsuarioDetailViewDTO> Insert(UsuarioDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            UsuarioDetailViewDTO result;

            try
            {
                var entity = new Usuario
                {
                    AplicacaoId = AplicacaoId,
                    Email = dto.Email,
                    Senha = dto.Senha,
                    Ativo = dto.Ativo,
                    Perfil = new Perfil
                    {
                        NomeExibicao = dto.NomeExibicao,
                    },
                    Descricao = dto.Descricao,
                };

                context.Usuario.Add(entity);

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

        public async Task<UsuarioDetailViewDTO> Edit(UsuarioDetailEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            UsuarioDetailViewDTO result;

            try
            {
                var entity = await context.Usuario
                        .Include(x => x.Perfil)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.UsuarioId.Equals(dto.UsuarioId))
                        .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                entity.Descricao = dto.Descricao;
                entity.Ativo = dto.Ativo;

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

        public async Task<bool> Remove(long usuarioId)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();
            
            bool result = false;

            try
            {
                var entity = await context.Usuario
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.UsuarioId.Equals(usuarioId))
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                context.Perfil.Remove(entity.Perfil);
                context.Usuario.Remove(entity);

                await context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(entity.Perfil.AvatarStorageObject))
                {
                    var storageHelper = new StorageHelper();
                    var storageObjectFullPath = GetAvatarStorageFullPath(entity.Perfil.AvatarStorageObject);
                    await storageHelper.DeleteObjectAsync(StorageBucketName, storageObjectFullPath);
                }

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

        public async Task<bool> UniqueEmail(string email)
        {
            bool result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<Usuario> query = context.Usuario
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.Email.Equals(email));

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

        private async Task<PagedListResponse<Usuario>> GetPagedList(PagedListRequest<UsuarioMasterDTO> pagedListRequest, bool? ativo)
        {
            PagedListResponse<Usuario> result;

            try
            {
                List<Usuario> dataPaged;

                int totalRecords;

                using (var context = new RThomazDbContext())
                {
                    IQueryable<Usuario> query = context.Usuario
                        .Include(x => x.Perfil)
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
                            x.Perfil.NomeExibicao.Contains(pagedListRequest.Search.Value) ||
                            x.Email.Contains(pagedListRequest.Search.Value) ||
                            x.Descricao.Contains(pagedListRequest.Search.Value)
                        );
                    }

                    //Ordenando por campo
                    if (pagedListRequest.OrderColumns != null)
                    {
                        bool isFirstOrderable = true;

                        ExpressionHelper.ApplyOrder<Usuario, bool, UsuarioMasterDTO, bool>(
                            ref query, x => x.Ativo, pagedListRequest, x => x.Ativo, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<Usuario, string, UsuarioMasterDTO, string>(
                            ref query, x => x.Email, pagedListRequest, x => x.Email, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<Usuario, string, UsuarioMasterDTO, string>(
                            ref query, x => x.Perfil.AvatarStorageObject, pagedListRequest, x => x.AvatarStorageObject, ref isFirstOrderable);
                    }

                    totalRecords = await query.Select(x => x.UsuarioId).CountAsync();

                    dataPaged = (await query
                        .ToListAsync())
                        .Skip(pagedListRequest.Param.Skip)
                        .Take(pagedListRequest.Param.Take)
                        .ToList();
                }

                result = new PagedListResponse<Usuario>(
                    data: dataPaged,
                    totalRecords: totalRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private List<UsuarioMasterDTO> ConvertEntityListToMasterList(List<Usuario> data)
        {
            var result = new List<UsuarioMasterDTO>();

            foreach (var item in data)
            {
                result.Add(new UsuarioMasterDTO
                   (
                       usuarioId: item.UsuarioId,
                       nomeExibicao: item.Perfil.NomeExibicao,
                       email: item.Email,
                       ativo: item.Ativo,
                       avatarStorageObject: item.Perfil.AvatarStorageObject
                   ));   
            }

            return result;
        }

        private UsuarioDetailViewDTO ConvertEntityToDetail(Usuario entity)
        {
            var result = new UsuarioDetailViewDTO
            (
                usuarioId: entity.UsuarioId,
                nomeExibicao: entity.Perfil.NomeExibicao,
                ativo: entity.Ativo,
                email: entity.Email,
                avatarStorageObject: entity.Perfil.AvatarStorageObject,
                descricao: entity.Descricao
            );

            return result;
        }

        private string GetAvatarStorageFullPath(string storageObject)
        {
            return string.Format("{0}/{1}", AvatarStorageFolder, storageObject);
        }

        #endregion
    }
}
