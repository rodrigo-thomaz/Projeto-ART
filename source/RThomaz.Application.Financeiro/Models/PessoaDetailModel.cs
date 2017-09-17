using System.Collections.Generic;

namespace RThomaz.Application.Financeiro.Models
{
    public abstract class PessoaDetailModel
    {
        public long PessoaId { get; set; }

        public bool Ativo { get; set; }

        public string Descricao { get; set; }

        public List<PessoaEmailDetailModel> Emails { get; set; }

        public List<PessoaEnderecoDetailModel> Enderecos { get; set; }

        public List<PessoaHomePageDetailModel> HomePages { get; set; }

        public List<PessoaTelefoneDetailModel> Telefones { get; set; }
    }
}