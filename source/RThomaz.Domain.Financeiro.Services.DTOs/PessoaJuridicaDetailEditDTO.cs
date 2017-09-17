using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class PessoaJuridicaDetailEditDTO : PessoaDetailDTO
    {
        private readonly string _nomeFantasia;
        private readonly string _razaoSocial;
        private readonly string _cnpj;
        private readonly string _inscricaoEstadual;
        private readonly string _inscricaoMunicipal;

        public PessoaJuridicaDetailEditDTO
            (
                  long pessoaId
                , string nomeFantasia
                , string razaoSocial
                , string cnpj
                , string inscricaoEstadual
                , string inscricaoMunicipal
                , bool ativo
                , string descricao
                , List<PessoaEmailDetailDTO> emails
                , List<PessoaEnderecoDetailDTO> enderecos
                , List<PessoaHomePageDetailDTO> homePages
                , List<PessoaTelefoneDetailDTO> telefones
            ) : base
            (
                pessoaId: pessoaId,
                tipoPessoa: TipoPessoa.PessoaJuridica,
                ativo: ativo,
                descricao: descricao,
                emails: emails,
                enderecos: enderecos,
                homePages: homePages,
                telefones: telefones
            )
        {
            _nomeFantasia = nomeFantasia;
            _razaoSocial = razaoSocial;
            _cnpj = cnpj;
            _inscricaoEstadual = inscricaoEstadual;
            _inscricaoMunicipal = inscricaoMunicipal;
        }

        public string NomeFantasia { get { return _nomeFantasia; } }
        public string RazaoSocial { get { return _razaoSocial; } }
        public string CNPJ { get { return _cnpj; } }
        public string InscricaoEstadual { get { return _inscricaoEstadual; } }
        public string InscricaoMunicipal { get { return _inscricaoMunicipal; } }
    }
}