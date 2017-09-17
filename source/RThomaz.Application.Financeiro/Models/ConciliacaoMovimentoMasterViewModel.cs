using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Application.Financeiro.Models
{
    public class ConciliacaoMovimentoMasterViewModel
    {
        private readonly long _movimentoId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly long _contaId;
        private readonly string _contaNome;
        private readonly TipoConta _tipoConta;
        private readonly string _historico;
        private readonly string _valorMovimento;
        private readonly string _dataMovimento;
        private readonly string _valorConciliado;
        private readonly bool _estaConciliado;

        public ConciliacaoMovimentoMasterViewModel
            (
                  long movimentoId
                , TipoTransacao tipoTransacao
                , long contaId
                , string contaNome
                , TipoConta tipoConta
                , string historico
                , string valorMovimento
                , string dataMovimento
                , string valorConciliado
                , bool estaConciliado
            )
        {
            _movimentoId = movimentoId;
            _tipoTransacao = tipoTransacao;
            _contaId = contaId;
            _contaNome = contaNome;
            _tipoConta = tipoConta;
            _historico = historico;
            _valorMovimento = valorMovimento;
            _dataMovimento = dataMovimento;
            _valorConciliado = valorConciliado;
            _estaConciliado = estaConciliado;
        }

        public long MovimentoId { get { return _movimentoId; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public long ContaId { get { return _contaId; } }
        public string ContaNome { get { return _contaNome; } }
        public TipoConta TipoConta { get { return _tipoConta; } }
        public string Historico { get { return _historico; } }
        public string ValorMovimento { get { return _valorMovimento; } }
        public string DataMovimento { get { return _dataMovimento; } }
        public string ValorConciliado { get { return _valorConciliado; } }
        public bool EstaConciliado { get { return _estaConciliado; } }
    }
}