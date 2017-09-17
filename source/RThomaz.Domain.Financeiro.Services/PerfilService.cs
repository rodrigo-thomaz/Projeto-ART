using Google;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Infra.CrossCutting.Storage;
using System;
using System.Data.Entity;
using System.Linq;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using System.Net;
using System.Threading.Tasks;
using RThomaz.Infra.CrossCutting.ExceptionHandling;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public class PerfilService : ServiceBase, IPerfilService
    {
        #region private consts

        private const string AvatarStorageFolder = "perfil/avatar";

        #endregion        

        #region constructors

        public PerfilService()
        {

        }

        #endregion

        #region public voids

        public async Task<StorageDownloadDTO> GetAvatar(string storageObject)
        {
            var storageHelper = new StorageHelper();

            var storageObjectFullPath = GetAvatarStorageFullPath(storageObject);

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
        
        public async Task<PerfilPersonalInfoViewDTO> GetDetail(long usuarioId)
        {
            Perfil entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.Perfil
                        .Include(x => x.Usuario)
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

            var result = new PerfilPersonalInfoViewDTO
                (
                    usuarioId: entity.UsuarioId,
                    nomeExibicao: entity.NomeExibicao,
                    email: entity.Usuario.Email,
                    avatarStorageObject: entity.AvatarStorageObject
                );

            return result;
        }

        public async Task<PerfilPersonalInfoViewDTO> EditPersonalInfo(PerfilPersonalInfoEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            PerfilPersonalInfoViewDTO result;

            try
            {
                var entity = await context.Perfil
                        .Include(x => x.Usuario)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.UsuarioId.Equals(dto.UsuarioId))
                        .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                entity.NomeExibicao = dto.NomeExibicao;

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();

                result = new PerfilPersonalInfoViewDTO
                (
                    usuarioId: entity.UsuarioId,
                    nomeExibicao: entity.NomeExibicao,
                    email: entity.Usuario.Email,
                    avatarStorageObject: entity.AvatarStorageObject
                );
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

        public async Task<string> EditAvatar(PerfilAvatarEditDTO dto)
        {
            string storageObject = null;

            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            try
            {
                var entity = await context.Perfil
                            .Include(x => x.Usuario)
                            .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                            .Where(x => x.UsuarioId.Equals(dto.UsuarioId))
                            .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                var storageHelper = new StorageHelper();

                //Inserindo
                if (string.IsNullOrEmpty(entity.AvatarStorageObject) && dto.StorageUpload != null)
                {
                    storageObject = Guid.NewGuid().ToString();
                    var storageObjectFullPath = GetAvatarStorageFullPath(storageObject);

                    await storageHelper.UploadBufferAsync(StorageBucketName, storageObjectFullPath, dto.StorageUpload.ContentType, dto.StorageUpload.Buffer);

                    entity.AvatarStorageObject = storageObject;
                }
                //Alterando
                else if (!string.IsNullOrEmpty(entity.AvatarStorageObject) && dto.StorageUpload != null)
                {
                    //removendo
                    var deleteStorageObjectFullPath = GetAvatarStorageFullPath(entity.AvatarStorageObject);
                    storageHelper.DeleteObject(StorageBucketName, deleteStorageObjectFullPath);

                    //incluindo
                    storageObject = Guid.NewGuid().ToString();
                    var storageObjectFullPath = GetAvatarStorageFullPath(storageObject);

                    await storageHelper.UploadBufferAsync(StorageBucketName, storageObjectFullPath, dto.StorageUpload.ContentType, dto.StorageUpload.Buffer);

                    entity.AvatarStorageObject = storageObject;

                }
                //Excluindo
                else if (!string.IsNullOrEmpty(entity.AvatarStorageObject) && dto.StorageUpload == null && string.IsNullOrEmpty(dto.AvatarStorageObject))
                {
                    var deleteStorageObjectFullPath = GetAvatarStorageFullPath(entity.AvatarStorageObject);
                    await storageHelper.DeleteObjectAsync(StorageBucketName, deleteStorageObjectFullPath);

                    entity.AvatarStorageObject = null;
                }

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();
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

            return storageObject;
        }       

        #endregion

        #region private voids

        private string GetAvatarStorageFullPath(string storageObject)
        {
            return string.Format("{0}/{1}", AvatarStorageFolder, storageObject);
        }

        #endregion
    }
}
