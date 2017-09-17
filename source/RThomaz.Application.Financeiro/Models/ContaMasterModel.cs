using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Application.Financeiro.Models
{
    public class ContaMasterModel
    {
        private readonly long _contaId;
        private readonly string _informacao;
        private readonly TipoConta _tipoConta;
        private readonly bool _ativo;
        private readonly string _logoStorageObject;

        public ContaMasterModel(long contaId, string informacao, TipoConta tipoConta, bool ativo, string logoStorageObject)
        {
            _contaId = contaId;
            _informacao = informacao;
            _tipoConta = tipoConta;
            _ativo = ativo;
            _logoStorageObject = logoStorageObject;
        }

        public long ContaId { get { return _contaId; } }

        public string Informacao { get { return _informacao; } }

        public TipoConta TipoConta { get { return _tipoConta; } }        

        public bool Ativo { get { return _ativo; } }

        public string LogoStorageObject { get { return _logoStorageObject; } }
    }
}