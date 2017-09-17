using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Application.Financeiro.Models
{
    public class ContaEspecieSelectViewModel : ContaSelectViewModel
    {
        private readonly string _nome;

        public ContaEspecieSelectViewModel
            (
                  long contaId
                , TipoConta tipoConta
                , string nome
            ) : base
            (
                  contaId: contaId,
                  tipoConta: tipoConta
            )
        {
            _nome = nome;
        }

        public string Nome { get { return _nome; } }
    }
}