namespace RThomaz.Application.Financeiro.Models
{
    public class PessoaJuridicaDetailEditModel : PessoaDetailModel
    {
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
    }
}