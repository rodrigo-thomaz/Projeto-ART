namespace RThomaz.Business.DTOs
{
    public class ContaCartaoCreditoInsertDTO
    {
        private readonly long _bandeiraCartaoId;
        private readonly long? _contaCorrenteId;
        private readonly string _nome;
        private readonly string _descricao;
        private readonly bool _ativo;

        public ContaCartaoCreditoInsertDTO
            (
                  long bandeiraCartaoId
                , long? contaCorrenteId
                , string nome
                , string descricao
                , bool ativo
            )
        {
            _bandeiraCartaoId = bandeiraCartaoId;
            _contaCorrenteId = contaCorrenteId;
            _nome = nome;
            _descricao = descricao;
            _ativo = ativo;
        }

        public long BandeiraCartaoId { get { return _bandeiraCartaoId; } }
        public long? ContaCorrenteId { get { return _contaCorrenteId; } }
        public string Nome { get { return _nome; } }
        public string Descricao { get { return _descricao; } }
        public bool Ativo { get { return _ativo; } }
    }
}
