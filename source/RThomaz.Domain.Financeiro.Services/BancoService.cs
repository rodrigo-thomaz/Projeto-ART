using Google;
using RThomaz.Domain.Financeiro.Services.Converters;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
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
    public class BancoService : ServiceBase, IBancoService
    {
        #region private fields

        private const string LogoStorageFolder = "banco/logo";        

        #endregion

        #region constructors

        public BancoService()
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
            catch(GoogleApiException ex)
            {
                if(ex.HttpStatusCode == HttpStatusCode.NotFound)
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

        public async Task<PagedListResponse<BancoMasterDTO>> GetMasterList(PagedListRequest<BancoMasterDTO> pagedListRequest, bool? ativo)
        {
            var pagedList = await GetPagedList(pagedListRequest, ativo);
            var masterList = ConvertEntityListToMasterList(pagedList.Data);
            var result = new PagedListResponse<BancoMasterDTO>
            (
                data: masterList,
                totalRecords: pagedList.TotalRecords
            );

            return result;
        }

        public async Task<SelectListResponseDTO<BancoSelectViewDTO>> GetSelectViewList(SelectListRequestDTO selectListDTORequest)
        {
            SelectListResponseDTO<BancoSelectViewDTO> result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<Banco> query = context.Banco
                .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                .Where(x => x.Ativo)
                .OrderBy(x => x.Nome);

                //Busca geral
                if (!string.IsNullOrEmpty(selectListDTORequest.Search))
                {
                    query = query.Where(x => x.Nome.Contains(selectListDTORequest.Search));
                }

                var totalRecords = await query.Select(x => x.BancoId).CountAsync();

                var dataPaged = await query
                    .Skip(selectListDTORequest.Skip)
                    .Take(selectListDTORequest.PageSize)
                    .ToListAsync();

                var dtos = new List<BancoSelectViewDTO>();

                foreach (var item in dataPaged)
                {
                    dtos.Add(BancoConverter.ConvertEntityToDTO(item));
                }

                result = new SelectListResponseDTO<BancoSelectViewDTO>(
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

        public async Task<BancoDetailViewDTO> GetDetail(long bancoId)
        {
            Banco entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.Banco
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.BancoId.Equals(bancoId))
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

            var result = ConvertEntityToDTO(entity);

            return result;
        }

        public async Task<BancoDetailViewDTO> Insert(BancoDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            BancoDetailViewDTO result;

            try
            {
                var entity = new Banco
                {
                    AplicacaoId = AplicacaoId,
                    Nome = dto.Nome,
                    NomeAbreviado = dto.NomeAbreviado,
                    Numero = dto.Numero,
                    MascaraNumeroAgencia = dto.MascaraNumeroAgencia,
                    MascaraNumeroContaCorrente = dto.MascaraNumeroContaCorrente,
                    MascaraNumeroContaPoupanca = dto.MascaraNumeroContaPoupanca,
                    CodigoImportacaoOfx = dto.CodigoImportacaoOfx,
                    Site = dto.Site,
                    Ativo = dto.Ativo,
                    Descricao = dto.Descricao,
                };

                if (dto.StorageUpload != null)
                {
                    var storageHelper = new StorageHelper();

                    var storageObject = Guid.NewGuid().ToString();
                    var storageObjectFullPath = GetLogoStorageFullPath(storageObject);

                    await storageHelper.UploadBufferAsync(StorageBucketName, storageObjectFullPath, dto.StorageUpload.ContentType, dto.StorageUpload.Buffer);

                    entity.LogoStorageObject = storageObject;
                }

                context.Banco.Add(entity);

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();

                result = ConvertEntityToDTO(entity);
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

        public async Task<BancoDetailViewDTO> Edit(BancoDetailEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            BancoDetailViewDTO result;

            try
            {
                var entity = await context.Banco
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.BancoId.Equals(dto.BancoId))
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                entity.Nome = dto.Nome;
                entity.NomeAbreviado = dto.NomeAbreviado;
                entity.Numero = dto.Numero;
                entity.MascaraNumeroAgencia = dto.MascaraNumeroAgencia;
                entity.MascaraNumeroContaCorrente = dto.MascaraNumeroContaCorrente;
                entity.MascaraNumeroContaPoupanca = dto.MascaraNumeroContaPoupanca;
                entity.CodigoImportacaoOfx = dto.CodigoImportacaoOfx;
                entity.Site = dto.Site;
                entity.Ativo = dto.Ativo;
                entity.Descricao = dto.Descricao;

                var storageHelper = new StorageHelper();

                //Inserindo
                if(string.IsNullOrEmpty(entity.LogoStorageObject) && dto.StorageUpload != null)
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
                    await storageHelper.DeleteObjectAsync(StorageBucketName, deleteStorageObjectFullPath);

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

                result = ConvertEntityToDTO(entity);
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

        public async Task<bool> Remove(long bancoId)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            bool result = false;

            try
            {
                var entity = await context.Banco
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.BancoId.Equals(bancoId))
                    .FirstOrDefaultAsync();

                if(entity == null)
                {
                    throw new RecordNotFoundException();
                }

                context.Banco.Remove(entity);

                await context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(entity.LogoStorageObject))
                {
                    var storageHelper = new StorageHelper();
                    var storageObjectFullPath = GetLogoStorageFullPath(entity.LogoStorageObject);
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
                if(context != null)
                {
                    context.Dispose();
                }                
            }

            return result;
        }

        public async Task<bool> UniqueNome(long? bancoId, string nome)
        {
            bool result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<Banco> query = context.Banco
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.Nome.Equals(nome));

                if (bancoId.HasValue)
                {
                    query = query.Where(x => x.BancoId != bancoId.Value);
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

        public async Task<bool> UniqueNomeAbreviado(long? bancoId, string nomeAbreviado)
        {
            bool result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<Banco> query = context.Banco
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.NomeAbreviado.Equals(nomeAbreviado));

                if (bancoId.HasValue)
                {
                    query = query.Where(x => x.BancoId != bancoId.Value);
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

        public async Task<bool> UniqueNumero(long? bancoId, string numero)
        {
            bool result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<Banco> query = context.Banco
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.Numero.Equals(numero));

                if (bancoId.HasValue)
                {
                    query = query.Where(x => x.BancoId != bancoId.Value);
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

        public async Task<BancoMascarasDetailViewDTO> GetMascaras(long bancoId)
        {
            BancoMascarasDetailViewDTO dto;

            var context = new RThomazDbContext();

            try
            {
                var entity = await context.Banco
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.BancoId.Equals(bancoId))
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                dto = new BancoMascarasDetailViewDTO
                (
                    entity.MascaraNumeroAgencia,
                    entity.MascaraNumeroContaCorrente,
                    entity.MascaraNumeroContaPoupanca
                );
            }
            finally
            {
                if (context != null)
                {
                    context.Dispose();
                }
            }

            return dto;
        }

        #endregion

        #region private voids

        private async Task<PagedListResponse<Banco>> GetPagedList(PagedListRequest<BancoMasterDTO> pagedListRequest, bool? ativo)
        {
            PagedListResponse<Banco> result;

            try
            {
                List<Banco> dataPaged;

                int totalRecords;

                using (var context = new RThomazDbContext())
                {
                    IQueryable<Banco> query = context.Banco
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
                            x.Nome.Contains(pagedListRequest.Search.Value) ||
                            x.Descricao.Contains(pagedListRequest.Search.Value) ||
                            x.NomeAbreviado.Contains(pagedListRequest.Search.Value) ||
                            x.Numero.ToString().Contains(pagedListRequest.Search.Value) ||
                            x.Site.ToString().Contains(pagedListRequest.Search.Value)
                        );
                    }

                    //Ordenando por campo
                    if (pagedListRequest.OrderColumns != null)
                    {
                        bool isFirstOrderable = true;

                        ExpressionHelper.ApplyOrder<Banco, string, BancoMasterDTO, string>(
                            ref query, x => x.Nome, pagedListRequest, x => x.Nome, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<Banco, long, BancoMasterDTO, long>(
                            ref query, x => x.BancoId, pagedListRequest, x => x.BancoId, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<Banco, string, BancoMasterDTO, string>(
                            ref query, x => x.NomeAbreviado, pagedListRequest, x => x.NomeAbreviado, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<Banco, string, BancoMasterDTO, string>(
                            ref query, x => x.Numero, pagedListRequest, x => x.Numero, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<Banco, string, BancoMasterDTO, string>(
                            ref query, x => x.Site, pagedListRequest, x => x.Site, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<Banco, bool, BancoMasterDTO, bool>(
                            ref query, x => x.Ativo, pagedListRequest, x => x.Ativo, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<Banco, string, BancoMasterDTO, string>(
                            ref query, x => x.LogoStorageObject, pagedListRequest, x => x.LogoStorageObject, ref isFirstOrderable);
                    }

                    totalRecords = await query.Select(x => x.BancoId).CountAsync();
                    
                    dataPaged = (await query
                        .ToListAsync())
                        .Skip(pagedListRequest.Param.Skip)
                        .Take(pagedListRequest.Param.Take)
                        .ToList();
                }

                result = new PagedListResponse<Banco>(
                    data: dataPaged,
                    totalRecords: totalRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private List<BancoMasterDTO> ConvertEntityListToMasterList(List<Banco> data)
        {
            var result = new List<BancoMasterDTO>();

            foreach (var item in data)
            {
                result.Add(new BancoMasterDTO
               (
                   bancoId: item.BancoId,
                   nome: item.Nome,
                   nomeAbreviado: item.NomeAbreviado,
                   numero: item.Numero,
                   site: item.Site,
                   ativo: item.Ativo,
                   logoStorageObject: item.LogoStorageObject
               ));
            }

            return result;
        }        

        private BancoDetailViewDTO ConvertEntityToDTO(Banco entity)
        {
            var result = new BancoDetailViewDTO
            (
                bancoId: entity.BancoId,
                nome: entity.Nome,
                nomeAbreviado: entity.NomeAbreviado,
                numero: entity.Numero,
                mascaraNumeroAgencia: entity.MascaraNumeroAgencia,
                mascaraNumeroContaCorrente: entity.MascaraNumeroContaCorrente,
                mascaraNumeroContaPoupanca: entity.MascaraNumeroContaPoupanca,
                codigoImportacaoOfx: entity.CodigoImportacaoOfx,
                site: entity.Site,
                logoStorageObject: entity.LogoStorageObject,
                ativo: entity.Ativo,
                descricao: entity.Descricao
            );

            return result;
        }

        private List<BancoSelectViewDTO> ConvertEntityListToDTOList(List<Banco> entities)
        {
            var dtos = new List<BancoSelectViewDTO>();
            foreach (var item in entities)
            {
                dtos.Add(ConvertEntityToDTOItem(item));
            }
            return dtos;
        }

        private BancoSelectViewDTO ConvertEntityToDTOItem(Banco entity)
        {
            return new BancoSelectViewDTO
            (
                bancoId: entity.BancoId,
                nome: entity.NomeAbreviado,
                numero: entity.Numero,
                logoStorageObject: entity.LogoStorageObject
            );
        }

        private string GetLogoStorageFullPath(string storageObject)
        {
            return string.Format("{0}/{1}", LogoStorageFolder, storageObject);
        }

        #endregion
    }
}
