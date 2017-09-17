using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class PessoaHomePageDetailDTO
    {
        private readonly long _pessoaHomePageId;
        private readonly Guid _tipoHomePageId;
        private readonly string _homePage;

        public PessoaHomePageDetailDTO
            (
                  long pessoaHomePageId
                , Guid tipoHomePageId
                , string homePage
            )
        {
            _pessoaHomePageId = pessoaHomePageId;
            _tipoHomePageId = tipoHomePageId;
            _homePage = homePage;
        }

        public long PessoaHomePageId { get { return _pessoaHomePageId; } }
        public Guid TipoHomePageId { get { return _tipoHomePageId; } }
        public string HomePage { get { return _homePage; } }
    }
}