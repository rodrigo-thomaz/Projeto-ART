namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class BancoDetailViewDTO
    {
        private readonly long _bancoId;
        private readonly string _nome;
        private readonly string _nomeAbreviado;
        private readonly string _numero;
        private readonly string _mascaraNumeroAgencia;
        private readonly string _mascaraNumeroContaCorrente;
        private readonly string _mascaraNumeroContaPoupanca;
        private readonly string _codigoImportacaoOfx;
        private readonly string _site;
        private readonly string _logoStorageObject;
        private readonly string _descricao;
        private readonly bool _ativo;

        public BancoDetailViewDTO
            (
                  long bancoId
                , string nome
                , string nomeAbreviado
                , string descricao
                , string numero
                , string mascaraNumeroAgencia
                , string mascaraNumeroContaCorrente
                , string mascaraNumeroContaPoupanca
                , string codigoImportacaoOfx
                , string site
                , bool ativo
                , string logoStorageObject                
            )
        {
            _bancoId = bancoId;
            _nome = nome;
            _nomeAbreviado = nomeAbreviado;
            _descricao = descricao;
            _numero = numero;
            _mascaraNumeroAgencia = mascaraNumeroAgencia;
            _mascaraNumeroContaCorrente = mascaraNumeroContaCorrente;
            _mascaraNumeroContaPoupanca = mascaraNumeroContaPoupanca;
            _codigoImportacaoOfx = codigoImportacaoOfx;
            _site = site;
            _ativo = ativo;
            _logoStorageObject = logoStorageObject;
        }

        public long BancoId { get { return _bancoId; } }
        public string Nome { get { return _nome; } }
        public string NomeAbreviado { get { return _nomeAbreviado; } }
        public string Numero { get { return _numero; } }
        public string MascaraNumeroAgencia { get { return _mascaraNumeroAgencia; } }
        public string MascaraNumeroContaCorrente { get { return _mascaraNumeroContaCorrente; } }
        public string MascaraNumeroContaPoupanca { get { return _mascaraNumeroContaPoupanca; } }
        public string CodigoImportacaoOfx { get { return _codigoImportacaoOfx; } }
        public string Site { get { return _site; } }
        public string LogoStorageObject { get { return _logoStorageObject; } }
        public bool Ativo { get { return _ativo; } }
        public string Descricao { get { return _descricao; } }
    }
}