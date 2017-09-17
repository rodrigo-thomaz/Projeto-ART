using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class MovimentoImportadoEditDTO
    {
        private readonly long _movimentoId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly long _movimentoImportacaoId;
        private readonly string _observacao;

        public MovimentoImportadoEditDTO
            (
                  long movimentoId
                , TipoTransacao tipoTransacao
                , long movimentoImportacaoId
                , string observacao
            )
        {
            _movimentoId = movimentoId;
            _tipoTransacao = tipoTransacao;
            _movimentoImportacaoId = movimentoImportacaoId;
            _observacao = observacao;
        }

        public long MovimentoId { get { return _movimentoId; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public long MovimentoImportacaoId { get { return _movimentoImportacaoId; } }
        public string Observacao { get { return _observacao; } }        
    }
}