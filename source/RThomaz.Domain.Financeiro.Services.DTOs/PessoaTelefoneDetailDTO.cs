using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class PessoaTelefoneDetailDTO
    {
        private readonly long _pessoaTelefoneId;
        private readonly Guid _tipoTelefoneId;
        private readonly string _telefone;

        public PessoaTelefoneDetailDTO
            (
                  long pessoaTelefoneId
                , Guid tipoTelefoneId
                , string telefone
            )
        {
            _pessoaTelefoneId = pessoaTelefoneId;
            _tipoTelefoneId = tipoTelefoneId;
            _telefone = telefone;
        }

        public long PessoaTelefoneId { get { return _pessoaTelefoneId; } }
        public Guid TipoTelefoneId { get { return _tipoTelefoneId; } }
        public string Telefone { get { return _telefone; } }
    }
}