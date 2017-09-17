using Google;
using RThomaz.Domain.Financeiro.Services.Converters;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Infra.CrossCutting.Storage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using System.Threading.Tasks;
using RThomaz.Infra.CrossCutting.ExceptionHandling;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public class LocalidadeService : ServiceBase, ILocalidadeService
    {
        #region private consts

        private const string BandeiraStorageFolder = "pais/bandeira";

        #endregion

        #region constructors

        public LocalidadeService()
        {

        }

        #endregion

        #region public voids

        public async Task<StorageDownloadDTO> GetPaisBandeira(string storageObject)
        {
            var storageHelper = new StorageHelper();

            var storageObjectFullPath = GetPaisBandeiraStorageFullPath(storageObject);

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

        public async Task<List<PaisSelectViewDTO>> GetPaisSelectViewList()
        {
            var result = new List<PaisSelectViewDTO>();

            var context = new RThomazDbContext();

            try
            {
                var data = await context.Pais
                .OrderBy(x => x.Nome)
                .ToListAsync();

                foreach (var item in data)
                {
                    result.Add(LocalidadeConverter.ConvertEntityToDTO(item));
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

        public async Task<List<EstadoSelectViewDTO>> GetEstadoSelectViewList(long paisId)
        {
            var result = new List<EstadoSelectViewDTO>();

            var context = new RThomazDbContext();

            try
            {
                var data = await context.Estado
                    .Where(x => x.PaisId.Equals(paisId))
                    .OrderBy(x => x.Nome)
                    .ToListAsync();

                foreach (var item in data)
                {
                    result.Add(LocalidadeConverter.ConvertEntityToDTO(item));
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

        public async Task<List<CidadeSelectViewDTO>> GetCidadeSelectViewList(long estadoId)
        {
            var result = new List<CidadeSelectViewDTO>();

            var context = new RThomazDbContext();

            try
            {
                var data = await context.Cidade
                    .Where(x => x.EstadoId.Equals(estadoId))
                    .OrderBy(x => x.Nome)
                    .ToListAsync();

                foreach (var item in data)
                {
                    result.Add(LocalidadeConverter.ConvertEntityToDTO(item));
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

        public async Task<List<BairroSelectViewDTO>> GetBairroSelectViewList(long cidadeId)
        {
            var result = new List<BairroSelectViewDTO>();

            var context = new RThomazDbContext();

            try
            {
                var data = await context.Bairro
                    .Where(x => x.CidadeId.Equals(cidadeId))
                    .OrderBy(x => x.Nome)
                    .ToListAsync();

                foreach (var item in data)
                {
                    result.Add(LocalidadeConverter.ConvertEntityToDTO(item));
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

        public async Task<BairroSelectViewDTO> UpdateLocalidade(LocalidadeDetailUpdateDTO localidade)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            BairroSelectViewDTO result;

            try
            {
                //Pais

                var pais = await context.Pais
                    .FirstOrDefaultAsync(x => x.ISO2.Equals(localidade.PaisISO2));

                if (pais == null)
                {
                    pais = context.Pais.Add(new Pais
                    {
                        Nome = localidade.PaisNome,
                        ISO2 = localidade.PaisISO2,
                        TipoOrigemDado = TipoOrigemDado.RunTime,
                    });
                }

                //Estado

                var estado = await context.Estado
                    .Where(x => x.PaisId.Equals(pais.PaisId))
                    .Where(x => x.Nome.Equals(localidade.EstadoNome))
                    .FirstOrDefaultAsync();

                if (estado == null)
                {
                    estado = context.Estado.Add(new Estado
                    {
                        Pais = pais,
                        Nome = localidade.EstadoNome,
                        Sigla = localidade.EstadoSigla,
                        TipoOrigemDado = TipoOrigemDado.RunTime,
                    });
                }

                //Cidade

                var cidade = await context.Cidade
                    .Where(x => x.EstadoId.Equals(estado.EstadoId))
                    .Where(x => x.Nome.Equals(localidade.CidadeNome))
                    .FirstOrDefaultAsync();

                if (cidade == null)
                {
                    cidade = context.Cidade.Add(new Cidade
                    {
                        Estado = estado,
                        Nome = localidade.CidadeNome,
                        NomeAbreviado = localidade.CidadeNomeAbreviado,
                    });
                }

                //Bairro

                var bairro = await context.Bairro
                    .Where(x => x.CidadeId.Equals(cidade.CidadeId))
                    .Where(x => x.Nome.Equals(localidade.BairroNome))
                    .FirstOrDefaultAsync();

                if (bairro == null)
                {
                    bairro = context.Bairro.Add(new Bairro
                    {
                        Cidade = cidade,
                        Nome = localidade.BairroNome,
                        NomeAbreviado = localidade.BairroNomeAbreviado,
                    });
                }

                await context.SaveChangesAsync();

                dbContextTransaction.Commit();

                result = LocalidadeConverter.ConvertEntityToDTO(bairro);
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

        #endregion

        #region private voids       

        private string GetPaisBandeiraStorageFullPath(string storageObject)
        {
            return string.Format("{0}/{1}", BandeiraStorageFolder, storageObject);
        }        

        #endregion
    }
}
