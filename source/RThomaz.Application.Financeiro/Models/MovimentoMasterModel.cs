using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Application.Financeiro.Models
{
    public class MovimentoMasterModel
    {
        private readonly long _movimentoId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly string _dataMovimento;
        private readonly string _valorMovimento;
        private readonly string _historico;
        private readonly string _saldo;
        private readonly string _totalConciliado;
        private readonly bool _estaConciliado;
        private readonly long? _movimentoImportacaoId;

        public MovimentoMasterModel
            (
                  long movimentoId
                , TipoTransacao tipoTransacao
                , string dataMovimento
                , string valorMovimento
                , string historico
                , string saldo
                , string totalConciliado
                , bool estaConciliado
                , long? movimentoImportacaoId
            )
        {
            _movimentoId = movimentoId;
            _tipoTransacao = tipoTransacao;
            _dataMovimento = dataMovimento;
            _valorMovimento = valorMovimento;
            _historico = historico;
            _saldo = saldo;
            _totalConciliado = totalConciliado;
            _estaConciliado = estaConciliado;
            _movimentoImportacaoId = movimentoImportacaoId;
        }

        public long MovimentoId { get { return _movimentoId; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public string DataMovimento { get { return _dataMovimento; } }
        public string ValorMovimento { get { return _valorMovimento; } }
        public string Historico { get { return _historico; } }
        public string Saldo { get { return _saldo; } }
        public string TotalConciliado { get { return _totalConciliado; } }
        public bool EstaConciliado { get { return _estaConciliado; } }
        public long? MovimentoImportacaoId { get { return _movimentoImportacaoId; } }
    }
}