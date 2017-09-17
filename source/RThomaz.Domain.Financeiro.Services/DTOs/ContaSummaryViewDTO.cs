using RThomaz.Database.Enums;

namespace RThomaz.Business.DTOs
{
    public class ContaSummaryDTO
    {
        private readonly long _contaId;
        private readonly TipoConta _tipoConta;
        private readonly string _logoStorageObject;
        private readonly string _nome;
        private readonly decimal _saldoAtual;

        public ContaSummaryDTO
        (
              long contaId
            , TipoConta tipoConta
            , string logoStorageObject                
            , string nome
            , decimal saldoAtual
        )
        {
            _contaId = contaId;
            _tipoConta = tipoConta;
            _logoStorageObject = logoStorageObject;
            _nome = nome;
            _saldoAtual = saldoAtual;
        }

        public long ContaId { get { return _contaId; } }
        public TipoConta TipoConta { get { return _tipoConta; } }
        public string LogoStorageObject { get { return _logoStorageObject; } }
        public string Nome { get { return _nome; } }
        public decimal SaldoAtual { get { return _saldoAtual; } }
    }
}
