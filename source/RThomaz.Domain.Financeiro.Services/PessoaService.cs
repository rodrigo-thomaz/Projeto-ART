using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using System.Linq.Expressions;
using RThomaz.Domain.Financeiro.Services.Converters;
using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;
using RThomaz.Infra.CrossCutting.ExceptionHandling;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Services
{
    public class PessoaService : ServiceBase, IPessoaService
    {
        #region constructors

        public PessoaService()
        {

        }

        #endregion

        #region public voids

        public async Task<PagedListResponse<PessoaMasterDTO>> GetMasterList(PagedListRequest<PessoaMasterDTO> pagedListRequest, bool? ativo)
        {
            var pagedList = await GetPagedList(pagedListRequest, ativo);
            var masterList = ConvertEntityListToMasterList(pagedList.Data.ToList());
            var result = new PagedListResponse<PessoaMasterDTO>
            (
                data: masterList,
                totalRecords: pagedList.TotalRecords
            );
            return result;
        }

        public async Task<SelectListResponseDTO<PessoaSelectViewDTO>> GetSelectViewList(SelectListRequestDTO selectListDTORequest)
        {
            SelectListResponseDTO<PessoaSelectViewDTO> result;

            var context = new RThomazDbContext();

            try
            {
                IQueryable<Pessoa> query = context.Pessoa
                .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                .Where(x => x.Ativo);

                //Busca geral
                if (!string.IsNullOrEmpty(selectListDTORequest.Search))
                {
                    query = query.Where(x => x is PessoaFisica ?
                    (
                        (x as PessoaFisica).PrimeiroNome +
                        (!string.IsNullOrEmpty((x as PessoaFisica).NomeDoMeio) ? " " + (x as PessoaFisica).NomeDoMeio : "") +
                        (!string.IsNullOrEmpty((x as PessoaFisica).Sobrenome) ? " " + (x as PessoaFisica).Sobrenome : "")
                    ).Contains(selectListDTORequest.Search) :
                    (x as PessoaJuridica).RazaoSocial.Contains(selectListDTORequest.Search));
                }

                Expression<Func<Pessoa, string>> orderByPredicate = x => x is PessoaFisica ?
                                        (x as PessoaFisica).PrimeiroNome :
                                        (x as PessoaJuridica).RazaoSocial;

                query = query.OrderBy(orderByPredicate);
                
                var totalRecords = await query.Select(x => x.PessoaId).CountAsync();

                var dataPaged = await query
                    .Skip(selectListDTORequest.Skip)
                    .Take(selectListDTORequest.PageSize)
                    .ToListAsync();

                var dtos = new List<PessoaSelectViewDTO>();

                foreach (var item in dataPaged)
                {
                    var nome = string.Empty;

                    if (item is PessoaFisica)
                    {
                        var pessoaFisica = ((PessoaFisica)item);
                        nome = pessoaFisica.NomeCompleto;
                    }
                    else
                    {
                        var pessoaJuridica = ((PessoaJuridica)item);
                        nome = pessoaJuridica.NomeFantasia;
                    }

                    dtos.Add(new PessoaSelectViewDTO
                    (
                        pessoaId: item.PessoaId,
                        tipoPessoa: item.TipoPessoa,
                        nome: nome
                    ));
                }

                result = new SelectListResponseDTO<PessoaSelectViewDTO>(
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

        public async Task<PessoaFisicaDetailViewDTO> GetPessoaFisicaDetail(long id)
        {
            PessoaFisica entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.Pessoa
                        .OfType<PessoaFisica>()
                        .Include(x => x.Emails)
                        .Include(x => x.Enderecos.Select(y => y.Bairro.Cidade.Estado.Pais))
                        .Include(x => x.HomePages)
                        .Include(x => x.Telefones)
                        .Include(x => x.CBOOcupacao)
                        .Include(x => x.CBOSinonimo)
                        .Where(x => x.PessoaId.Equals(id))
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
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

        public async Task<PessoaJuridicaDetailViewDTO> GetPessoaJuridicaDetail(long id)
        {
            PessoaJuridica entity;

            var context = new RThomazDbContext();

            try
            {
                entity = await context.Pessoa
                        .OfType<PessoaJuridica>()
                        .Include(x => x.Emails)
                        .Include(x => x.Enderecos.Select(y => y.Bairro.Cidade.Estado.Pais))
                        .Include(x => x.HomePages)
                        .Include(x => x.Telefones)
                        .Where(x => x.PessoaId.Equals(id))
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
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

        public async Task<PessoaFisicaDetailViewDTO> InsertPessoaFisica(PessoaFisicaDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            PessoaFisicaDetailViewDTO result;

            try
            {                
                var emails = ConvertDetailToEntity(dto.Emails);
                var enderecos = ConvertDetailToEntity(dto.Enderecos);
                var homePages = ConvertDetailToEntity(dto.HomePages);
                var telefones = ConvertDetailToEntity(dto.Telefones);

                var entity = new PessoaFisica
                {
                    AplicacaoId = AplicacaoId,
                    PrimeiroNome = dto.PrimeiroNome,
                    NomeDoMeio = dto.NomeDoMeio,
                    Sobrenome = dto.Sobrenome,
                    Sexo = dto.Sexo,
                    EstadoCivil = dto.EstadoCivil,
                    CPF = dto.CPF,
                    RG = dto.RG,
                    OrgaoEmissor = dto.OrgaoEmissor,
                    DataNascimento = dto.DataNascimento,
                    Nacionalidade = dto.Nacionalidade,
                    Naturalidade = dto.Naturalidade,
                    CBOOcupacaoId = dto.CBOOcupacaoId,
                    CBOSinonimoId = dto.CBOSinonimoId,
                    Ativo = dto.Ativo,
                    Descricao = dto.Descricao,
                    Emails = emails,
                    Enderecos = enderecos,
                    HomePages = homePages,
                    Telefones = telefones,
                };

                context.Pessoa.Add(entity);

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

        public async Task<PessoaJuridicaDetailViewDTO> InsertPessoaJuridica(PessoaJuridicaDetailInsertDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            PessoaJuridicaDetailViewDTO result;

            try
            {
                var emails = ConvertDetailToEntity(dto.Emails);
                var enderecos = ConvertDetailToEntity(dto.Enderecos);
                var homePages = ConvertDetailToEntity(dto.HomePages);
                var telefones = ConvertDetailToEntity(dto.Telefones);

                var entity = new PessoaJuridica
                {
                    AplicacaoId = AplicacaoId,
                    RazaoSocial = dto.RazaoSocial,
                    NomeFantasia = dto.NomeFantasia,
                    CNPJ = dto.CNPJ,
                    InscricaoEstadual = dto.InscricaoEstadual,
                    InscricaoMunicipal = dto.InscricaoMunicipal,
                    Ativo = dto.Ativo,
                    Descricao = dto.Descricao,
                    Emails = emails,
                    Enderecos = enderecos,
                    HomePages = homePages,
                    Telefones = telefones,
                };

                context.Pessoa.Add(entity);

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

        public async Task<PessoaFisicaDetailViewDTO> EditPessoaFisica(PessoaFisicaDetailEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            PessoaFisicaDetailViewDTO result;

            try
            {
                var entity = await context.Pessoa
                        .OfType<PessoaFisica>()
                        .Include(x => x.Emails)
                        .Include(x => x.Enderecos.Select(y => y.Bairro.Cidade.Estado.Pais))
                        .Include(x => x.HomePages)
                        .Include(x => x.Telefones)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.PessoaId.Equals(dto.PessoaId))
                        .Where(x => x.TipoPessoa == dto.TipoPessoa)
                        .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                entity.PrimeiroNome = dto.PrimeiroNome;
                entity.NomeDoMeio = dto.NomeDoMeio;
                entity.Sobrenome = dto.Sobrenome;
                entity.Sexo = dto.Sexo;
                entity.EstadoCivil = dto.EstadoCivil;
                entity.CPF = dto.CPF;
                entity.RG = dto.RG;
                entity.OrgaoEmissor = dto.OrgaoEmissor;
                entity.DataNascimento = dto.DataNascimento;
                entity.Nacionalidade = dto.Nacionalidade;
                entity.Naturalidade = dto.Naturalidade;
                entity.CBOOcupacaoId = dto.CBOOcupacaoId;
                entity.CBOSinonimoId = dto.CBOSinonimoId;
                entity.Ativo = dto.Ativo;
                entity.Descricao = dto.Descricao;

                UpdateEmails(context, entity, dto.Emails);
                UpdateEnderecos(context, entity, dto.Enderecos);
                UpdateHomePages(context, entity, dto.HomePages);
                UpdateTelefones(context, entity, dto.Telefones);

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

        public async Task<PessoaJuridicaDetailViewDTO> EditPessoaJuridica(PessoaJuridicaDetailEditDTO dto)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();

            PessoaJuridicaDetailViewDTO result;

            try
            {
                var entity = await context.Pessoa
                        .OfType<PessoaJuridica>()
                        .Include(x => x.Emails)
                        .Include(x => x.Enderecos.Select(y => y.Bairro.Cidade.Estado.Pais))
                        .Include(x => x.HomePages)
                        .Include(x => x.Telefones)
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                        .Where(x => x.PessoaId.Equals(dto.PessoaId))
                        .Where(x => x.TipoPessoa == dto.TipoPessoa)
                        .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                entity.RazaoSocial = dto.RazaoSocial;
                entity.NomeFantasia = dto.NomeFantasia;
                entity.CNPJ = dto.CNPJ;
                entity.InscricaoEstadual = dto.InscricaoEstadual;
                entity.InscricaoMunicipal = dto.InscricaoMunicipal;
                entity.Ativo = dto.Ativo;
                entity.Descricao = dto.Descricao;

                UpdateEmails(context, entity, dto.Emails);
                UpdateEnderecos(context, entity, dto.Enderecos);
                UpdateHomePages(context, entity, dto.HomePages);
                UpdateTelefones(context, entity, dto.Telefones);

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
        
        public async Task<bool> Remove(long pessoaId, TipoPessoa tipoPessoa)
        {
            var context = new RThomazDbContext();
            var dbContextTransaction = context.Database.BeginTransaction();
            
            bool result = false;

            try
            {
                var entity = await context.Pessoa
                    .Include(x => x.Emails)
                    .Include(x => x.Enderecos.Select(y => y.Bairro.Cidade.Estado.Pais))
                    .Include(x => x.HomePages)
                    .Include(x => x.Telefones)
                    .Where(x => x.AplicacaoId.Equals(AplicacaoId))
                    .Where(x => x.PessoaId.Equals(pessoaId))
                    .Where(x => x.TipoPessoa == tipoPessoa)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new RecordNotFoundException();
                }

                context.PessoaEmail.RemoveRange(entity.Emails);
                context.PessoaEndereco.RemoveRange(entity.Enderecos);
                context.PessoaHomePage.RemoveRange(entity.HomePages);
                context.PessoaTelefone.RemoveRange(entity.Telefones);

                context.Pessoa.Remove(entity);

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

        #endregion

        #region private voids

        private async Task<PagedListResponse<Pessoa>> GetPagedList(PagedListRequest<PessoaMasterDTO> pagedListRequest, bool? ativo)
        {
            PagedListResponse<Pessoa> result;

            try
            {
                List<Pessoa> dataPaged;

                int totalRecords;

                using (var context = new RThomazDbContext())
                {
                    IQueryable<Pessoa> query = context.Pessoa
                        .Where(x => x.AplicacaoId.Equals(AplicacaoId));

                    if (ativo.HasValue)
                    {
                        query = query
                            .Where(x => x.Ativo == ativo);
                    }

                    //Busca geral
                    if (pagedListRequest.Search != null)
                    {
                        //query = query.Where(x =>
                        //    x.Nome.Contains(pagedListRequest.Search.Value) ||
                        //    x.Descricao.Contains(pagedListRequest.Search.Value) 
                        //);
                    }

                    //Ordenando por campo
                    if (pagedListRequest.OrderColumns != null)
                    {
                        bool isFirstOrderable = true;

                        //ExpressionHelper.ApplyOrder<Pessoa, string, PessoaMasterContract, string>(
                        //    ref query, x => x.Nome, pagedListRequest, x => x.Nome, ref isFirstOrderable);

                        ExpressionHelper.ApplyOrder<Pessoa, long, PessoaMasterDTO, long>(
                            ref query, x => x.PessoaId, pagedListRequest, x => x.PessoaId, ref isFirstOrderable);                        
                    }

                    totalRecords = await query.Select(x => x.PessoaId).CountAsync();

                    dataPaged = (await query
                        .ToListAsync())
                        .Skip(pagedListRequest.Param.Skip)
                        .Take(pagedListRequest.Param.Take)
                        .ToList();
                }

                result = new PagedListResponse<Pessoa>(
                    data: dataPaged,
                    totalRecords: totalRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private List<PessoaMasterDTO> ConvertEntityListToMasterList(IEnumerable<Pessoa> data)
        {
            var result = new List<PessoaMasterDTO>();

            foreach (var item in data)
            {
                var nome = string.Empty;
                
                if(item is PessoaFisica)
                {
                    var pessoaFisica = ((PessoaFisica)item);                    
                    nome = pessoaFisica.NomeCompleto;
                }
                else
                {
                    var pessoaJuridica = ((PessoaJuridica)item);
                    nome = pessoaJuridica.NomeFantasia;
                }

                result.Add(new PessoaMasterDTO
                   (
                       pessoaId: item.PessoaId,
                       tipoPessoa: item.TipoPessoa,
                       nome: nome,
                       ativo: item.Ativo
                   ));   
            }

            return result;
        }        

        private PessoaFisicaDetailViewDTO ConvertEntityToDetail(PessoaFisica entity)
        {
            var emails = ConvertEntityToDetail(entity.Emails);
            var enderecos = ConvertEntityToDetail(entity.Enderecos);
            var homepages = ConvertEntityToDetail(entity.HomePages);
            var telefones = ConvertEntityToDetail(entity.Telefones);           

            var result = new PessoaFisicaDetailViewDTO
            (
                pessoaId: entity.PessoaId,
                primeiroNome: entity.PrimeiroNome,
                nomeDoMeio: entity.NomeDoMeio,
                sobrenome: entity.Sobrenome,
                sexo: entity.Sexo,
                estadoCivil: entity.EstadoCivil,
                cpf: entity.CPF,
                rg: entity.RG,
                orgaoEmissor: entity.OrgaoEmissor,
                dataNascimento: entity.DataNascimento,
                naturalidade: entity.Naturalidade,
                nacionalidade: entity.Nacionalidade,
                cboOcupacao: entity.CBOOcupacao == null ? null : new CBOOcupacaoSelectViewDTO
                (
                    cboOcupacaoId: entity.CBOOcupacao.CBOOcupacaoId,
                    codigo: entity.CBOOcupacao.Codigo,
                    titulo: entity.CBOOcupacao.Titulo
                ),
                cboSinonimo: entity.CBOSinonimo == null ? null : new CBOSinonimoSelectViewDTO
                (
                    cboSinonimoId: entity.CBOSinonimo.CBOSinonimoId,
                    titulo: entity.CBOSinonimo.Titulo
                ),
                ativo: entity.Ativo,
                descricao: entity.Descricao,
                emails: emails,
                enderecos: enderecos,
                homePages: homepages,
                telefones: telefones
            );

            return result;
        }

        private PessoaJuridicaDetailViewDTO ConvertEntityToDetail(PessoaJuridica entity)
        {
            var emails = ConvertEntityToDetail(entity.Emails);
            var enderecos = ConvertEntityToDetail(entity.Enderecos);
            var homepages = ConvertEntityToDetail(entity.HomePages);
            var telefones = ConvertEntityToDetail(entity.Telefones);

            var result = new PessoaJuridicaDetailViewDTO
            (
                pessoaId: entity.PessoaId,
                razaoSocial: entity.RazaoSocial,
                nomeFantasia: entity.NomeFantasia,
                cnpj: entity.CNPJ,
                inscricaoEstadual: entity.InscricaoEstadual,
                inscricaoMunicipal: entity.InscricaoMunicipal,
                ativo: entity.Ativo,
                descricao: entity.Descricao,
                emails: emails,
                enderecos: enderecos,
                homePages: homepages,
                telefones: telefones
            );

            return result;
        }

        private List<PessoaEmailDetailDTO> ConvertEntityToDetail(IEnumerable<PessoaEmail> emails)
        {
            var result = new List<PessoaEmailDetailDTO>();

            if(emails == null)
            {
                return result;
            }

            foreach (var item in emails)
            {
                result.Add(new PessoaEmailDetailDTO
                (
                    pessoaEmailId: item.PessoaEmailId,
                    tipoEmailId: item.TipoEmailId,
                    email: item.Email
                ));
            }

            return result;
        }

        private List<PessoaEnderecoDetailDTO> ConvertEntityToDetail(IEnumerable<PessoaEndereco> enderecos)
        {
            var result = new List<PessoaEnderecoDetailDTO>();

            if (enderecos == null)
            {
                return result;
            }

            foreach (var item in enderecos)
            {
                result.Add(new PessoaEnderecoDetailDTO
                    (
                        pessoaEnderecoId: item.PessoaEnderecoId,
                        tipoEnderecoId: item.TipoEnderecoId,
                        cep: item.Cep,
                        logradouro: item.Logradouro,
                        numero: item.Numero,
                        complemento: item.Complemento,
                        bairro: LocalidadeConverter.ConvertEntityToDTO(item.Bairro),                        
                        logitude: item.Longitude,
                        latitude: item.Latitude
                    ));
            }

            return result;
        }

        private List<PessoaHomePageDetailDTO> ConvertEntityToDetail(IEnumerable<PessoaHomePage> homePages)
        {
            var result = new List<PessoaHomePageDetailDTO>();

            if (homePages == null)
            {
                return result;
            }

            foreach (var item in homePages)
            {
                result.Add(new PessoaHomePageDetailDTO
                    (
                        pessoaHomePageId: item.PessoaHomePageId,
                        tipoHomePageId: item.TipoHomePageId,
                        homePage: item.HomePage
                    ));
            }

            return result;
        }

        private List<PessoaTelefoneDetailDTO> ConvertEntityToDetail(IEnumerable<PessoaTelefone> telefones)
        {
            var result = new List<PessoaTelefoneDetailDTO>();

            if (telefones == null)
            {
                return result;
            }

            foreach (var item in telefones)
            {
                result.Add(new PessoaTelefoneDetailDTO
                    (
                        pessoaTelefoneId: item.PessoaTelefoneId,
                        tipoTelefoneId: item.TipoTelefoneId,
                        telefone: item.Telefone
                    ));
            }

            return result;
        }

        private ICollection<PessoaEmail> ConvertDetailToEntity(IEnumerable<PessoaEmailDetailDTO> emails)
        {
            if (emails == null)
            {
                return null;
            }

            var result = emails.Select(x => new PessoaEmail
            {
                AplicacaoId = AplicacaoId,
                TipoEmailId = x.TipoEmailId,
                Email = x.Email,
            }).ToList();

            return result;
        }

        private ICollection<PessoaEndereco> ConvertDetailToEntity(IEnumerable<PessoaEnderecoDetailDTO> enderecos)
        {
            if (enderecos == null)
            {
                return null;
            }

            var result = enderecos.Select(x => new PessoaEndereco
            {
                AplicacaoId = AplicacaoId,
                TipoEnderecoId = x.TipoEnderecoId,
                Cep = x.Cep,                
                Logradouro = x.Logradouro,
                Numero = x.Numero,
                BairroId = x.Bairro.BairroId,
                Complemento = x.Complemento,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
            }).ToList();

            return result;
        }

        private ICollection<PessoaHomePage> ConvertDetailToEntity(IEnumerable<PessoaHomePageDetailDTO> homePages)
        {
            if (homePages == null)
            {
                return null;
            }

            var result = homePages.Select(x => new PessoaHomePage
            {
                AplicacaoId = AplicacaoId,
                TipoHomePageId = x.TipoHomePageId,
                HomePage = x.HomePage,
            }).ToList();

            return result;
        }

        private ICollection<PessoaTelefone> ConvertDetailToEntity(IEnumerable<PessoaTelefoneDetailDTO> telefones)
        {
            if (telefones == null)
            {
                return null;
            }

            var result = telefones.Select(x => new PessoaTelefone
            {
                AplicacaoId = AplicacaoId,
                TipoTelefoneId = x.TipoTelefoneId,
                Telefone = x.Telefone,
            }).ToList();

            return result;
        }

        private List<PessoaSelectViewDTO> ConvertEntityListToSelectList(IEnumerable<Pessoa> data)
        {
            var result = new List<PessoaSelectViewDTO>();
            foreach (var item in data)
            {
                result.Add(ConvertEntityToSelectListItem(item));
            }
            return result;
        }

        private PessoaSelectViewDTO ConvertEntityToSelectListItem(Pessoa entity)
        {
            var nome = string.Empty;

            if (entity is PessoaFisica)
            {
                var pessoaFisica = ((PessoaFisica)entity);
                nome = pessoaFisica.NomeCompleto;
            }
            else
            {
                var pessoaJuridica = ((PessoaJuridica)entity);
                nome = pessoaJuridica.NomeFantasia;
            }

            return new PessoaSelectViewDTO
            (
                pessoaId: entity.PessoaId,
                tipoPessoa: entity.TipoPessoa,
                nome: nome
            );
        }

        private void UpdateEmails(RThomazDbContext context, Pessoa entity, IEnumerable<PessoaEmailDetailDTO> contracts)
        {
            //EmailsToAdd
            var emailsToAdd = contracts.Where(x => x.PessoaEmailId.Equals(0));
            foreach (var item in emailsToAdd)
            {
                entity.Emails.Add(new PessoaEmail
                {
                    PessoaId = entity.PessoaId,
                    TipoPessoa = entity.TipoPessoa,
                    TipoEmailId = item.TipoEmailId,
                    Email = item.Email,
                });
            }

            //EmailsToRemove
            var emailsIds = contracts.Where(x => x.PessoaEmailId > 0).Select(x => x.PessoaEmailId);
            var emailsToRemove = entity.Emails.Where(x => x.PessoaEmailId > 0).Where(x => !emailsIds.Contains(x.PessoaEmailId));
            context.PessoaEmail.RemoveRange(emailsToRemove);

            //EmailsToUpdate
            var emails = contracts.Where(x => x.PessoaEmailId > 0);
            foreach (var item in emails)
            {
                var emailToUpdate = entity.Emails.First(x => x.PessoaEmailId.Equals(item.PessoaEmailId));
                emailToUpdate.TipoEmailId = item.TipoEmailId;
                emailToUpdate.Email = item.Email;
            }
        }        

        private void UpdateEnderecos(RThomazDbContext context, Pessoa entity, IList<PessoaEnderecoDetailDTO> contracts)
        {
            //EnderecosToAdd
            var enderecosToAdd = contracts.Where(x => x.PessoaEnderecoId.Equals(0));
            foreach (var item in enderecosToAdd)
            {
                entity.Enderecos.Add(new PessoaEndereco
                {
                    AplicacaoId = AplicacaoId,
                    PessoaId = entity.PessoaId,
                    TipoPessoa = entity.TipoPessoa,
                    TipoEnderecoId = item.TipoEnderecoId,
                    Cep = item.Cep,
                    Logradouro = item.Logradouro,
                    Numero = item.Numero,
                    Complemento = item.Complemento,
                    BairroId = item.Bairro.BairroId,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                });
            }

            //EnderecosToRemove
            var enderecosIds = contracts.Where(x => x.PessoaEnderecoId > 0).Select(x => x.PessoaEnderecoId);
            var enderecosToRemove = entity.Enderecos.Where(x => x.PessoaEnderecoId > 0).Where(x => !enderecosIds.Contains(x.PessoaEnderecoId));
            context.PessoaEndereco.RemoveRange(enderecosToRemove);

            //EnderecosToUpdate
            var enderecos = contracts.Where(x => x.PessoaEnderecoId > 0);
            foreach (var item in enderecos)
            {
                var enderecoToUpdate = entity.Enderecos.First(x => x.PessoaEnderecoId.Equals(item.PessoaEnderecoId));
                enderecoToUpdate.TipoEnderecoId = item.TipoEnderecoId;
                enderecoToUpdate.Complemento = item.Complemento;                
            }
        }

        private void UpdateHomePages(RThomazDbContext context, Pessoa entity, IEnumerable<PessoaHomePageDetailDTO> contracts)
        {
            //HomePagesToAdd
            var homePagesToAdd = contracts.Where(x => x.PessoaHomePageId.Equals(0));
            foreach (var item in homePagesToAdd)
            {
                entity.HomePages.Add(new PessoaHomePage
                {
                    PessoaId = entity.PessoaId,
                    TipoPessoa = entity.TipoPessoa,
                    TipoHomePageId = item.TipoHomePageId,
                    HomePage = item.HomePage,
                });
            }

            //HomePagesToRemove
            var homePagesIds = contracts.Where(x => x.PessoaHomePageId > 0).Select(x => x.PessoaHomePageId);
            var homePagesToRemove = entity.HomePages.Where(x => x.PessoaHomePageId > 0).Where(x => !homePagesIds.Contains(x.PessoaHomePageId));
            context.PessoaHomePage.RemoveRange(homePagesToRemove);

            //HomePagesToUpdate
            var homePages = contracts.Where(x => x.PessoaHomePageId > 0);
            foreach (var item in homePages)
            {
                var homePageToUpdate = entity.HomePages.First(x => x.PessoaHomePageId.Equals(item.PessoaHomePageId));
                homePageToUpdate.TipoHomePageId = item.TipoHomePageId;
                homePageToUpdate.HomePage = item.HomePage;
            }
        }

        private void UpdateTelefones(RThomazDbContext context, Pessoa entity, IEnumerable<PessoaTelefoneDetailDTO> contracts)
        {
            //TelefonesToAdd
            var telefonesToAdd = contracts.Where(x => x.PessoaTelefoneId.Equals(0));
            foreach (var item in telefonesToAdd)
            {
                entity.Telefones.Add(new PessoaTelefone
                {
                    PessoaId = entity.PessoaId,
                    TipoPessoa = entity.TipoPessoa,
                    TipoTelefoneId = item.TipoTelefoneId,
                    Telefone = item.Telefone,
                });
            }

            //TelefonesToRemove
            var telefonesIds = contracts.Where(x => x.PessoaTelefoneId > 0).Select(x => x.PessoaTelefoneId);
            var telefonesToRemove = entity.Telefones.Where(x => x.PessoaTelefoneId > 0).Where(x => !telefonesIds.Contains(x.PessoaTelefoneId));
            context.PessoaTelefone.RemoveRange(telefonesToRemove);

            //TelefonesToUpdate
            var telefones = contracts.Where(x => x.PessoaTelefoneId > 0);
            foreach (var item in telefones)
            {
                var telefoneToUpdate = entity.Telefones.First(x => x.PessoaTelefoneId.Equals(item.PessoaTelefoneId));
                telefoneToUpdate.TipoTelefoneId = item.TipoTelefoneId;
                telefoneToUpdate.Telefone = item.Telefone;
            }
        }
        
        #endregion        
    }
}
