using System;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class Banco
    {
        #region Primitive Properties

        public long BancoId { get; set; }

        public Guid AplicacaoId { get; set; }

        public string Nome { get; set; }

        public string NomeAbreviado { get; set; }

        public string Numero { get; set; }

        public string MascaraNumeroAgencia { get; set; }

        public string MascaraNumeroContaCorrente { get; set; }

        public string MascaraNumeroContaPoupanca { get; set; }

        public string CodigoImportacaoOfx { get; set; }

        public string Site { get; set; }

        public string LogoStorageObject { get; set; }

        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        #endregion

        #region Navigation Properties

        public Aplicacao Aplicacao { get; set; }

        public ICollection<ContaCorrente> ContasCorrente { get; set; }

        public ICollection<ContaPoupanca> ContasPoupanca { get; set; }

        #endregion
    }
}