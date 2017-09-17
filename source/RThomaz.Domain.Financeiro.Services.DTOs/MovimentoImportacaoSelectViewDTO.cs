using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class MovimentoImportacaoSelectViewDTO
    {
        private readonly long _movimentoImportacaoId;
        private readonly DateTime _importadoEm;

        public MovimentoImportacaoSelectViewDTO
            (
                  long movimentoImportacaoId
                , DateTime importadoEm
            )
        {
            _movimentoImportacaoId = movimentoImportacaoId;
            _importadoEm = importadoEm;
        }

        public long MovimentoImportacaoId { get { return _movimentoImportacaoId; } }
        public DateTime ImportadoEm { get { return _importadoEm; } }
    }
}
