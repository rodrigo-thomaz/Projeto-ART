using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class PessoaEmailDetailDTO
    {
        private readonly long _pessoaEmailId;
        private readonly Guid _tipoEmailId;
        private readonly string _email;

        public PessoaEmailDetailDTO
            (
                  long pessoaEmailId
                , Guid tipoEmailId
                , string email
            )
        {
            _pessoaEmailId = pessoaEmailId;
            _tipoEmailId = tipoEmailId;
            _email = email;
        }

        public long PessoaEmailId { get { return _pessoaEmailId; } }
        public Guid TipoEmailId { get { return _tipoEmailId; } }
        public string Email { get { return _email; } }
    }
}