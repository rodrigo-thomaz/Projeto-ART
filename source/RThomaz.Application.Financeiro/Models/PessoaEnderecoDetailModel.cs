namespace RThomaz.Application.Financeiro.Models
{
    public class PessoaEnderecoDetailModel
    {
        public long PessoaEnderecoId { get; set; }

        public long TipoEnderecoId { get; set; }

        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public long BairroId { get; set; }

        public BairroSelectViewModel Bairro { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}