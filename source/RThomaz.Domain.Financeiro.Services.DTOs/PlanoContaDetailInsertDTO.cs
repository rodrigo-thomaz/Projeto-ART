using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class PlanoContaDetailInsertDTO
    {
        private readonly TipoTransacao _tipoTransacao;
        private readonly long? _parentId;
        private readonly string _nome;
        private readonly bool _ativo;
        private readonly string _descricao;

        public PlanoContaDetailInsertDTO
            (
                  TipoTransacao tipoTransacao
                , long? parentId
                , string nome
                , bool ativo
                , string descricao
            )
        {
            _tipoTransacao = tipoTransacao;
            _parentId = parentId;
            _nome = nome;
            _ativo = ativo;
            _descricao = descricao;
        }

        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public long? ParentId { get { return _parentId; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
        public string Descricao { get { return _descricao; } }
    }
}
