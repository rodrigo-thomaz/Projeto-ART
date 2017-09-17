using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Infra.CrossCutting.Storage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Infra.CrossCutting.Email;

namespace RThomaz.Domain.Financeiro.Services
{
    public class AccountService : IAccountService
    {
        #region public voids

        public async Task<AccountDetailViewDTO> GetDetail(string email)
        {
            AccountDetailViewDTO result = null;

            try
            {
                using (var context = new RThomazDbContext())
                {
                    var entity = await context.Usuario
                        .Include(x => x.Aplicacao)
                       .Include(x => x.Perfil)
                       .FirstAsync(x => x.Email.Equals(email));

                    result = new AccountDetailViewDTO
                        (
                            usuarioId: entity.UsuarioId,
                            aplicacaoId: entity.AplicacaoId,
                            storageBucketName: "entity.Aplicacao.StorageBucketName",
                            nomeExibicao: entity.Perfil.NomeExibicao,
                            email: entity.Email,
                            lembrarMe: true,
                            avatarStorageObject: entity.Perfil.AvatarStorageObject
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<bool> ForgotYourPassword(string email)
        {
            bool result = false;

            try
            {
                using (var context = new RThomazDbContext())
                {
                    var entity = await context.Usuario
                        .Include(x => x.Perfil)
                        .FirstOrDefaultAsync(x => x.Email.Equals(email));

                    if (entity != null)
                    {                        
                        MailHelper.SendMail(
                            entity.Email, 
                            "RThomaz Finanças - Conta usuário", 
                            string.Format("Olá {0}, conforme solicitado, sua senha é {1}.", entity.Perfil.NomeExibicao, entity.Senha));                       

                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;   
        }

        public async Task Register(Guid applicationUserId, string email, string senha)
        {
            var bucketName = string.Format("rthomaz-client-{0}", Guid.NewGuid().ToString().ToLower());

            var aplicacaoEntity = new Aplicacao
            {
                //Ativo = true,
                //StorageBucketName = bucketName,
            };

            var usuarioEntity = new Usuario
            {
                ApplicationUserId = applicationUserId,                
                Ativo = true,
                Email = email,
                Senha = senha,
                Perfil = new Perfil
                {
                    NomeExibicao = email.Split('@')[0],
                }
            };

            aplicacaoEntity.Usuarios = new List<Usuario>();
            aplicacaoEntity.Usuarios.Add(usuarioEntity);

            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            try
            {
                context.Aplicacao.Add(aplicacaoEntity);

                await context.SaveChangesAsync();

                var storageHelper = new StorageHelper();

                var insertBucket = storageHelper.InsertBucket(StorageType.Standard, bucketName);
                
                dbContextTransaction.Commit();
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                throw ex;
            }
            finally
            {
                dbContextTransaction.Dispose();
                context.Dispose();
            }
        }        

        #endregion
    }
}
