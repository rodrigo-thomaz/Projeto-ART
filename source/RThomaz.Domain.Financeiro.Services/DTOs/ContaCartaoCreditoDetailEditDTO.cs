namespace RThomaz.Business.DTOs
{
    public class ContaCartaoCreditoEditDTO
    {
        private readonly long _contaId;
        private readonly long? _contaCorrenteId;
        private readonly string _nome;
        private readonly string _descricao;
        private readonly bool _ativo;

        public ContaCartaoCreditoEditDTO
            (
                  long contaId
                , long? contaCorrenteId
                , string nome
                , string descricao
                , bool ativo
            )
        {
            _contaId = contaId;
            _contaCorrenteId = contaCorrenteId;
            _nome = nome;
            _descricao = descricao;
            _ativo = ativo;
        }

        public long ContaId { get { return _contaId; } }
        public string Nome { get { return _nome; } }
        public string Descricao { get { return _descricao; } }
        public bool Ativo { get { return _ativo; } }
        public long? ContaCorrenteId { get { return _contaCorrenteId; } }
    }
}
