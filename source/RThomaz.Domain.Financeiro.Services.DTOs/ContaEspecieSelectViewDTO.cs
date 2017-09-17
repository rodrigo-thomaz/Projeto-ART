using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class ContaEspecieSelectViewDTO : ContaSelectViewDTO
    {
        private readonly string _nome;

        public ContaEspecieSelectViewDTO
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