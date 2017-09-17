using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Application.Financeiro.Models
{
    public class ContaCorrenteSelectViewModel : ContaSelectViewModel
    {
        private readonly BancoSelectViewModel _banco;
        private readonly DadoBancarioModel _dadoBancario;

        public ContaCorrenteSelectViewModel
            (
                  long contaId
                , TipoConta tipoConta
                , BancoSelectViewModel banco
                , DadoBancarioModel dadoBancario
            ) : base
            (
                  contaId: contaId,
                  tipoConta: tipoConta
            )
        {
            _banco = banco;
            _dadoBancario = dadoBancario;
        }

        public BancoSelectViewModel Banco { get { return _banco; } }
        public DadoBancarioModel DadoBancario { get { return _dadoBancario; } }
    }
}