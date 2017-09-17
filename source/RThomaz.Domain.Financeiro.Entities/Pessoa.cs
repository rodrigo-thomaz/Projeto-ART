using System;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public abstract class Pessoa
    {
        #region Primitive Properties

        public long PessoaId { get; set; }

        public Guid AplicacaoId { get; set; }

        public TipoPessoa TipoPessoa { get; protected set; }

        public bool Ativo { get; set; }

        public string Descricao { get; set; }

        #endregion

        #region Navigation Properties

        public Aplicacao Aplicacao { get; set; }

        public ICollection<Lancamento> Lancamentos { get; set; }

        public ICollection<Programacao> Programacoes { get; set; }

        public ICollection<PessoaEmail> Emails { get; set; }

        public ICollection<PessoaEndereco> Enderecos { get; set; }

        public ICollection<PessoaHomePage> HomePages { get; set; }

        public ICollection<PessoaTelefone> Telefones { get; set; }

        #endregion
    }
}