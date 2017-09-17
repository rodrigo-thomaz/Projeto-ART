using RThomaz.Domain.Financeiro.Enums;
using System;

namespace RThomaz.Application.Financeiro.Models
{
    public class PessoaFisicaDetailViewModel : PessoaDetailModel
    {
        public string PrimeiroNome { get; set; }
        public string NomeDoMeio { get; set; }
        public string Sobrenome { get; set; }
        public Sexo Sexo { get; set; }
        public EstadoCivil? EstadoCivil { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string OrgaoEmissor { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Naturalidade { get; set; }
        public string Nacionalidade { get; set; }
        public CBOOcupacaoSelectViewModel CBOOcupacao { get; set; }
        public CBOSinonimoSelectViewModel CBOSinonimo { get; set; }
    }
}