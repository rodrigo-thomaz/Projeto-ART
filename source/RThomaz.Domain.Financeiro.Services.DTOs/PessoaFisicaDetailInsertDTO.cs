using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class PessoaFisicaDetailInsertDTO : PessoaDetailDTO
    {
        private readonly string _primeiroNome;
        private readonly string _nomeDoMeio;
        private readonly string _sobrenome;
        private readonly Sexo _sexo;
        private readonly EstadoCivil? _estadoCivil;
        private readonly string _cpf;
        private readonly string _rg;
        private readonly string _orgaoEmissor;
        private readonly DateTime? _dataNascimento;
        private readonly string _naturalidade;
        private readonly string _nacionalidade;
        private readonly int? _cboOcupacaoId;
        private readonly int? _cboSinonimoId;

        public PessoaFisicaDetailInsertDTO
            (
                  long pessoaId
                , string primeiroNome
                , string nomeDoMeio
                , string sobrenome
                , Sexo sexo
                , EstadoCivil? estadoCivil
                , string cpf
                , string rg
                , string orgaoEmissor
                , DateTime? dataNascimento
                , string naturalidade
                , string nacionalidade
                , int? cboOcupacaoId
                , int? cboSinonimoId
                , bool ativo
                , string descricao
                , List<PessoaEmailDetailDTO> emails
                , List<PessoaEnderecoDetailDTO> enderecos
                , List<PessoaHomePageDetailDTO> homePages
                , List<PessoaTelefoneDetailDTO> telefones
            ) : base
            (
                pessoaId: pessoaId,
                tipoPessoa: TipoPessoa.PessoaFisica,
                ativo: ativo,
                descricao: descricao,
                emails: emails,
                enderecos: enderecos,
                homePages: homePages,
                telefones: telefones
            )
        {
            _primeiroNome = primeiroNome;
            _nomeDoMeio = nomeDoMeio;
            _sobrenome = sobrenome;
            _sexo = sexo;
            _estadoCivil = estadoCivil;
            _cpf = cpf;
            _rg = rg;
            _orgaoEmissor = orgaoEmissor;
            _dataNascimento = dataNascimento;
            _naturalidade = naturalidade;
            _nacionalidade = nacionalidade;
            _cboOcupacaoId = cboOcupacaoId;
            _cboSinonimoId = cboSinonimoId;
        }

        public string PrimeiroNome { get { return _primeiroNome; } }
        public string NomeDoMeio { get { return _nomeDoMeio; } }
        public string Sobrenome { get { return _sobrenome; } }
        public Sexo Sexo { get { return _sexo; } }
        public EstadoCivil? EstadoCivil { get { return _estadoCivil; } }
        public string CPF { get { return _cpf; } }
        public string RG { get { return _rg; } }
        public string OrgaoEmissor { get { return _orgaoEmissor; } }
        public DateTime? DataNascimento { get { return _dataNascimento; } }
        public string Naturalidade { get { return _naturalidade; } }
        public string Nacionalidade { get { return _nacionalidade; } }
        public int? CBOOcupacaoId { get { return _cboOcupacaoId; } }
        public int? CBOSinonimoId { get { return _cboSinonimoId; } }
    }
}