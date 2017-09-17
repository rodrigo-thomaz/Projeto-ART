using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class PlanoContaMasterDTO
    {
        private readonly long _planoContaId;
        private readonly string _nome;
        private readonly bool _ativo;
        private readonly long? _parentId;
        private readonly TipoTransacao _tipoTransacao;

        public PlanoContaMasterDTO
            (
                  long planoContaId
                , long? parentId
                , TipoTransacao tipoTransacao
                , string nome
                , bool ativo
            )
        {
            _planoContaId = planoContaId;
            _parentId = parentId;
            _tipoTransacao = tipoTransacao;
            _nome = nome;
            _ativo = ativo;
        }

        public long PlanoContaId { get { return _planoContaId; } }
        public long? ParentId { get { return _parentId; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
    }
}