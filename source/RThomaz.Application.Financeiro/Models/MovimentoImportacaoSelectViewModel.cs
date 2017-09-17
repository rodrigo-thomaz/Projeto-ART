using System;

namespace RThomaz.Application.Financeiro.Models
{
    public class MovimentoImportacaoSelectViewModel
    {
        private readonly long _movimentoImportacaoId;
        private readonly DateTime _importadoEm;

        public MovimentoImportacaoSelectViewModel
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