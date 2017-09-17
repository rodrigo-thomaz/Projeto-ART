namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class ContaCartaoCreditoDetailViewDTO
    {
        private readonly long _contaId;
        private readonly long _bandeiraCartaoId;
        private readonly long? _contaCorrenteId;
        private readonly string _nome;
        private readonly string _descricao;        
        private readonly bool _ativo;

        public ContaCartaoCreditoDetailViewDTO
            (
                  long contaId
                , long bandeiraCartaoId
                , long? contaCorrenteId
                , string nome
                , string descricao
                , bool ativo
            )
        {
            _contaId = contaId;
            _bandeiraCartaoId = bandeiraCartaoId;
            _contaCorrenteId = contaCorrenteId;
            _nome = nome;
            _descricao = descricao;
            _ativo = ativo;
        }

        public long ContaId { get { return _contaId; } }
        public long BandeiraCartaoId { get { return _bandeiraCartaoId; } }
        public long? ContaCorrenteId { get { return _contaCorrenteId; } }
        public string Nome { get { return _nome; } }
        public string Descricao { get { return _descricao; } }
        public bool Ativo { get { return _ativo; } }
    }
}