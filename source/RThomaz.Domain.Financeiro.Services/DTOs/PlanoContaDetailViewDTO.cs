using RThomaz.Database.Enums;

namespace RThomaz.Business.DTOs
{
    public class PlanoContaDetailDTO
    {
        private readonly long _planoContaId;
        private readonly long? _parentId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly string _nome;
        private readonly bool _ativo;
        private readonly string _descricao;

        public PlanoContaDetailDTO
            (
                  long planoContaId
                , long? parentId
                , TipoTransacao tipoTransacao
                , string nome
                , bool ativo
                , string descricao
            )
        {
            _planoContaId = planoContaId;
            _parentId = parentId;
            _tipoTransacao = tipoTransacao;
            _nome = nome;
            _ativo = ativo;
            _descricao = descricao;
        }

        public long PlanoContaId { get { return _planoContaId; } }
        public long? ParentId { get { return _parentId; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
        public string Descricao { get { return _descricao; } }
    }
}