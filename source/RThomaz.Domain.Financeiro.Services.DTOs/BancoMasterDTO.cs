namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class BancoMasterDTO
    {
        private readonly long _bancoId;
        private readonly string _nome;
        private readonly string _nomeAbreviado;
        private readonly string _numero;
        private readonly string _site;
        private readonly bool _ativo;
        private readonly string _logoStorageObject;

        public BancoMasterDTO
            (
                  long bancoId
                , string nome
                , string nomeAbreviado
                , string numero
                , string site
                , bool ativo
                , string logoStorageObject                
            )
        {
            _bancoId = bancoId;
            _nome = nome;
            _nomeAbreviado = nomeAbreviado;
            _numero = numero;
            _site = site;
            _ativo = ativo;
            _logoStorageObject = logoStorageObject;
        }

        public long BancoId { get { return _bancoId; } }
        public string Nome { get { return _nome; } }
        public string NomeAbreviado { get { return _nomeAbreviado; } }
        public string Numero { get { return _numero; } }
        public string Site { get { return _site; } }
        public bool Ativo { get { return _ativo; } }
        public string LogoStorageObject { get { return _logoStorageObject; } }
    }
}