using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class ContaCorrenteSelectViewDTO : ContaSelectViewDTO
    {
        private readonly BancoSelectViewDTO _banco;
        private readonly DadoBancarioDTO _dadoBancario;

        public ContaCorrenteSelectViewDTO
            (
                  long contaId
                , TipoConta tipoConta
                , BancoSelectViewDTO banco
                , DadoBancarioDTO dadoBancario
            ) : base 
            (
                  contaId: contaId,
                  tipoConta: tipoConta
            )
        {
            _banco = banco;
            _dadoBancario = dadoBancario;
        }

        public BancoSelectViewDTO Banco { get { return _banco; } }
        public DadoBancarioDTO DadoBancario { get { return _dadoBancario; } }
    }
}