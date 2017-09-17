using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class ContaCartaoCreditoSelectViewDTO : ContaSelectViewDTO
    {
        private readonly string _nome;
        private readonly BandeiraCartaoSelectViewDTO _bandeiraCartao;
        private readonly ContaCorrenteSelectViewDTO _contaCorrente;

        public ContaCartaoCreditoSelectViewDTO
            (
                  long contaId
                , TipoConta tipoConta
                , string nome
                , BandeiraCartaoSelectViewDTO bandeiraCartao
                , ContaCorrenteSelectViewDTO contaCorrente
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
        public BandeiraCartaoSelectViewDTO BandeiraCartao { get { return _bandeiraCartao; } }
        public ContaCorrenteSelectViewDTO ContaCorrente { get { return _contaCorrente; } }
    }
}