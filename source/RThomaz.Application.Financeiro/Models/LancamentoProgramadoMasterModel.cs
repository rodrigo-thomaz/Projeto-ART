namespace RThomaz.Application.Financeiro.Models
{
    public class LancamentoProgramadoMasterModel
    {
        private readonly long _programacaoId;
        private readonly string _frequencia;
        private readonly string _dataInicial;
        private readonly string _dataFinal;
        private readonly string _historico;
        private readonly string _valorVencimento;
        private readonly string _tipoTransacao;
        private readonly string _pessoaNome;
        private readonly string _contaNome;

        public LancamentoProgramadoMasterModel
            (
                  long programacaoId
                , string frequencia
                , string dataInicial
                , string dataFinal
                , string historico
                , string valorVencimento
                , string tipoTransacao
                , string pessoaNome
                , string contaNome
            )
        {
            _programacaoId = programacaoId;
            _frequencia = frequencia;
            _dataInicial = dataInicial;
            _dataFinal = dataFinal;
            _historico = historico;
            _valorVencimento = valorVencimento;
            _tipoTransacao = tipoTransacao;
            _pessoaNome = pessoaNome;
            _contaNome = contaNome;
        }        
       
        public long ProgramacaoId { get { return _programacaoId; } }
        public string Frequencia { get { return _frequencia; } }
        public string DataInicial { get { return _dataInicial; } }
        public string DataFinal { get { return _dataFinal; } }
        public string Historico { get { return _historico; } }
        public string ValorVencimento { get { return _valorVencimento; } }
        public string TipoTransacao { get { return _tipoTransacao; } }
        public string PessoaNome { get { return _pessoaNome; } }
        public string ContaNome { get { return _contaNome; } }
    }
}