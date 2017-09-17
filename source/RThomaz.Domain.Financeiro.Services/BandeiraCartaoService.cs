using Google;
using RThomaz.Domain.Financeiro.Services.Converters;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Infra.CrossCutting.Storage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Infra.CrossCutting.ExceptionHandling;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public class BandeiraCartaoService : ServiceBase, IBandeiraCartaoService
    {
        #region private consts

        private const string LogoStorageFolder = "bandeiracartao/logo";

        #endregion

        #region constructors

        public BandeiraCartaoService()
        {
            
        }

        #endregion

        #region public voids

        public async Task<StorageDownloadDTO> GetLogo(string storageObject)
        {
            var storageHelper = new StorageHelper();

            var storageObjectFullPath = GetLogoStorageFullPath(storageObject);

            try
            {
                var bufferResult = await storageHelper.DownloadBufferAsync(StorageBucketName, storageObjectFullPath);
                return bufferResult;
            }
            catch (GoogleApiException ex)
            {
                if (ex.HttpStatusCode == HttpStatusCode.NotFound)
                {
                    throw new RecordNotFoundException(ex);
                }
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PagedListResponse<BandeiraCartaoMasterDTO>> GetMasterList(PagedListRequest<BandeiraCartaoMasterDTO> pagedListRequest, bool? ativo)
        {
            var pagedList = await GetPagedList(pagedListRequest, ativo);
            var masterList = ConvertEntityListToMasterList(pagedList.Data);
            var result = new PagedListResponse<BandeiraCartaoMasterDTO>
            (
                data: masterList,
                totalRecords: pagedList.TotalRecords
            );

            return result;
        }

        public async Task<List<BandeiraCartaoSelectViewDTO>> GetSelectViewList()
        {
            List<BandeiraCartaoSelectViewDTO> result = new List<BandeiraCartaoSelectViewDTO>();

            var context = new RThomazDbContext();

            try
            {
                var data = await context.BandeiraCartao
                    .Where(x => x.AplicacaoId == AplicacaoId)
                    .Where(x => x.Ativo)                    
                    .OrderBy(x => x.Nome)
                    .ToListAsync();

                foreach (var item in data)
                {
                    result.Add(BandeiraCartaoConverter.ConvertEntityToDTO(item));
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

        public async Task<BandeiraCartaoDetailViewDTO> GetDetail(long id)
        {
            BandeiraCartao entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.BandeiraCartao
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.BandeiraCartaoId.Equals(id))
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

        public async Task<BandeiraCartaoDetailViewDTO> Insert(BandeiraCartaoDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            BandeiraCartaoDetailViewDTO result;

            try
            {
                var entity = new BandeiraCartao
                {
                    AplicacaoId = AplicacaoId,
                    Nome = dto.Nome,
                    Ativo = dto.Ativo,
                };

                if (dto.StorageUpload != null)
                {
                    var storageHelper = new StorageHelper();

                    var storageObject = Guid.NewGuid().ToString();
                    var storageObjectFullPath = GetLogoStorageFullPath(storageObject);
                    
                    storageHelper.UploadBuffer(StorageBucketName, storageObjectFullPath, dto.StorageUpload.ContentType, dto.StorageUpload.Buffer);

                    entity.LogoStorageObject = storageObject;
                }

                context.BandeiraCartao.Add(entity);

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

        public async Task<BandeiraCartaoDetailViewDTO> Edit(BandeiraCartaoDetailEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            BandeiraCartaoDetailViewDTO result;

            try
            {
                var entity = await context.BandeiraCartao
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.BandeiraCartaoId.Equals(dto.BandeiraCartaoId))
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                entity.Nome = dto.Nome;
                entity.Ativo = dto.Ativo;

                var storageHelper = new StorageHelper();

                //Inserindo
                if (string.IsNullOrEmpty(entity.LogoStorageObject) && dto.StorageUpload != null)
                {
                    var storageObject = Guid.NewGuid().ToString();
                    var storageObjectFullPath = GetLogoStorageFullPath(storageObject);

                    await storageHelper.UploadBufferAsync(StorageBucketName, storageObjectFullPath, dto.StorageUpload.ContentType, dto.StorageUpload.Buffer);

                    entity.LogoStorageObject = storageObject;
                }
                //Alterando
                else if (!string.IsNullOrEmpty(entity.LogoStorageObject) && dto.StorageUpload != null)
                {
                    //removendo
                    var deleteStorageObjectFullPath = GetLogoStorageFullPath(entity.LogoStorageObject);
                    storageHelper.DeleteObject(StorageBucketName, deleteStorageObjectFullPath);

                    //incluindo
                    var storageObject = Guid.NewGuid().ToString();
                    var storageObjectFullPath = GetLogoStorageFullPath(storageObject);

                    await storageHelper.UploadBufferAsync(StorageBucketName, storageObjectFullPath, dto.StorageUpload.ContentType, dto.StorageUpload.Buffer);

                    entity.LogoStorageObject = storageObject;

                }
                //Excluindo
                else if (!string.IsNullOrEmpty(entity.LogoStorageObject) && dto.StorageUpload == null && string.IsNullOrEmpty(dto.LogoStorageObject))
                {
                    var deleteStorageObjectFullPath = GetLogoStorageFullPath(entity.LogoStorageObject);
                    await storageHelper.DeleteObjectAsync(StorageBucketName, deleteStorageObjectFullPath);

                    entity.LogoStorageObject = null;
                }

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

        public async Task<bool> Remove(long id)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            bool result = false;

            try
            {
                var entity = await context.BandeiraCartao
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.BandeiraCartaoId.Equals(id))
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                context.BandeiraCartao.Remove(entity);

                await context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(entity.LogoStorageObject))
                {
                    var storageHelper = new StorageHelper();
                    var storageObjectFullPath = GetLogoStorageFullPath(entity.LogoStorageObject);
                    storageHelper.DeleteObject(StorageBucketName, storageObjectFullPath);
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

        public async Task<bool> UniqueNome(long? bandeiraCartaoId, string nome)
        {
            bool result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<BandeiraCartao> query = context.BandeiraCartao
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.Nome.Equals(nome));

                if (bandeiraCartaoId.HasValue)
                {
                    query = query.Where(x => x.BandeiraCartaoId != bandeiraCartaoId.Value);
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

        #endregion

        #region private voids

        private async Task<PagedListResponse<BandeiraCartao>> GetPagedList(PagedListRequest<BandeiraCartaoMasterDTO> pagedListRequest, bool? ativo)
        {
            PagedListResponse<BandeiraCartao> result;

            try
            {
                List<BandeiraCartao> dataPaged;

                int totalRecords;

                using (var context = new RThomazDbContext())
                {
                    IQueryable<BandeiraCartao> query = context.BandeiraCartao
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

                        ExpressionHelper.ApplyOrder<BandeiraCartao, string, BandeiraCartaoMasterDTO, string>(
                            ref query, x => x.Nome, pagedListRequest, x => x.Nome, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<BandeiraCartao, long, BandeiraCartaoMasterDTO, long>(
                            ref query, x => x.BandeiraCartaoId, pagedListRequest, x => x.BandeiraCartaoId, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<BandeiraCartao, bool, BandeiraCartaoMasterDTO, bool>(
                            ref query, x => x.Ativo, pagedListRequest, x => x.Ativo, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<BandeiraCartao, string, BandeiraCartaoMasterDTO, string>(
                            ref query, x => x.LogoStorageObject, pagedListRequest, x => x.LogoStorageObject, ref isFirstOrderable);
                    }

                    totalRecords = await query.Select(x => x.BandeiraCartaoId).CountAsync();

                    dataPaged = (await query
                        .ToListAsync())
                        .Skip(pagedListRequest.Param.Skip)
                        .Take(pagedListRequest.Param.Take)
                        .ToList();
                }

                result = new PagedListResponse<BandeiraCartao>(
                    data: dataPaged,
                    totalRecords: totalRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private List<BandeiraCartaoMasterDTO> ConvertEntityListToMasterList(List<BandeiraCartao> data)
        {
            var result = new List<BandeiraCartaoMasterDTO>();

            foreach (var item in data)
            {
                result.Add(new BandeiraCartaoMasterDTO
               (
                   bandeiraCartaoId: item.BandeiraCartaoId,
                   nome: item.Nome,
                   ativo: item.Ativo,
                   logoStorageObject: item.LogoStorageObject
               ));
            }

            return result;
        }

        private BandeiraCartaoDetailViewDTO ConvertEntityToDetail(BandeiraCartao entity)
        {
            var result = new BandeiraCartaoDetailViewDTO
            (
                bandeiraCartaoId: entity.BandeiraCartaoId,
                nome: entity.Nome,
                logoStorageObject: entity.LogoStorageObject,
                ativo: entity.Ativo
            );

            return result;
        }

        #endregion

        #region private static

        private static string GetLogoStorageFullPath(string storageObject)
        {
            return string.Format("{0}/{1}", LogoStorageFolder, storageObject);
        }

        #endregion
    }
}
