using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class PessoaEnderecoDetailDTO
    {
        private readonly long _pessoaEnderecoId;
        private readonly Guid _tipoEnderecoId;
        private readonly string _cep;
        private readonly string _logradouro;
        private readonly string _numero;
        private readonly string _complemento;
        private readonly BairroSelectViewDTO _bairro;
        private readonly double _logitude;
        private readonly double _latitude;

        public PessoaEnderecoDetailDTO
            (
                  long pessoaEnderecoId
                , Guid tipoEnderecoId
                , string cep
                , string logradouro
                , string numero
                , string complemento
                , BairroSelectViewDTO bairro
                , double logitude
                , double latitude
            )
        {
            _pessoaEnderecoId = pessoaEnderecoId;
            _tipoEnderecoId = tipoEnderecoId;
            _cep = cep;
            _logradouro = logradouro;
            _numero = numero;
            _complemento = complemento;
            _bairro = bairro;
            _logitude = logitude;
            _latitude = latitude;
        }

        public long PessoaEnderecoId { get { return _pessoaEnderecoId; } }
        public Guid TipoEnderecoId { get { return _tipoEnderecoId; } }
        public string Cep { get { return _cep; } }
        public string Logradouro { get { return _logradouro; } }
        public string Numero { get { return _numero; } }
        public string Complemento { get { return _complemento; } }
        public BairroSelectViewDTO Bairro { get { return _bairro; } }
        public double Longitude { get { return _logitude; } }
        public double Latitude { get { return _latitude; } }
    }
}