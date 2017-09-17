namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class ContaMasterDTO
    {
        private readonly long _contaId;
        private readonly string _logoStorageObject;
        private readonly string _informacao;
        private readonly byte _tipoConta;
        private readonly bool _ativo;

        public ContaMasterDTO
            (
                  long contaId
                , string logoStorageObject
                , string informacao
                , byte tipoConta
                , bool ativo
            )
        {
            _contaId = contaId;
            _logoStorageObject = logoStorageObject;
            _informacao = informacao;
            _tipoConta = tipoConta;
            _ativo = ativo;
        }

        public long ContaId { get { return _contaId; } }
        public string LogoStorageObject { get { return _logoStorageObject; } }
        public string Informacao { get { return _informacao; } }
        public byte TipoConta { get { return _tipoConta; } }
        public bool Ativo { get { return _ativo; } }
    }
}