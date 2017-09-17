using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public abstract class PessoaDetailDTO
    {
        private readonly long _pessoaId;
        private readonly TipoPessoa _tipoPessoa;
        private readonly bool _ativo;
        private readonly string _descricao;
        private readonly List<PessoaEmailDetailDTO> _emails;
        private readonly List<PessoaEnderecoDetailDTO> _enderecos;
        private readonly List<PessoaHomePageDetailDTO> _homePages;
        private readonly List<PessoaTelefoneDetailDTO> _telefones;

        public PessoaDetailDTO
            (
                  long pessoaId
                , TipoPessoa tipoPessoa
                , bool ativo
                , string descricao
                , List<PessoaEmailDetailDTO> emails
                , List<PessoaEnderecoDetailDTO> enderecos
                , List<PessoaHomePageDetailDTO> homePages
                , List<PessoaTelefoneDetailDTO> telefones
            )
        {
            _pessoaId = pessoaId;
            _tipoPessoa = tipoPessoa;
            _ativo = ativo;
            _descricao = descricao;
            _emails = new List<PessoaEmailDetailDTO>(emails);
            _enderecos = enderecos;
            _homePages = new List<PessoaHomePageDetailDTO>(homePages);
            _telefones = new List<PessoaTelefoneDetailDTO>(telefones);
        }

        public long PessoaId { get { return _pessoaId; } }
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }
        public bool Ativo { get { return _ativo; } }
        public string Descricao { get { return _descricao; } }
        public List<PessoaEmailDetailDTO> Emails { get { return _emails; } }
        public List<PessoaEnderecoDetailDTO> Enderecos { get { return _enderecos; } }
        public List<PessoaHomePageDetailDTO> HomePages { get { return _homePages; } }
        public List<PessoaTelefoneDetailDTO> Telefones { get { return _telefones; } }
    }
}