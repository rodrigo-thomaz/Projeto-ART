using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Application.Financeiro.Models
{
    public class ContaCartaoCreditoSelectViewModel : ContaSelectViewModel
    {
        private readonly string _nome;
        private readonly BandeiraCartaoSelectViewModel _bandeiraCartao;
        private readonly ContaCorrenteSelectViewModel _contaCorrente;

        public ContaCartaoCreditoSelectViewModel
            (
                  long contaId
                , TipoConta tipoConta
                , string nome
                , BandeiraCartaoSelectViewModel bandeiraCartao
                , ContaCorrenteSelectViewModel contaCorrente
            ) : base
            (
                  contaId: contaId,
                  tipoConta: tipoConta
            )
        {
            _nome = nome;
            _bandeiraCartao = bandeiraCartao;
            _contaCorrente = contaCorrente;
        }

        public string Nome { get { return _nome; } }
        public BandeiraCartaoSelectViewModel BandeiraCartao { get { return _bandeiraCartao; } }
        public ContaCorrenteSelectViewModel ContaCorrente { get { return _contaCorrente; } }
    }
}