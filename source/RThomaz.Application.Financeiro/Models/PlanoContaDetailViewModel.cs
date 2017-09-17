using RThomaz.Domain.Financeiro.Enums;

namespace RThomaz.Application.Financeiro.Models
{
    public class PlanoContaDetailViewModel
    {
        private readonly long _planoContaId;
        private readonly PlanoContaSelectViewModel _parent;
        private readonly TipoTransacao _tipoTransacao;
        private readonly string _nome;
        private readonly bool _ativo;
        private readonly string _descricao;

        public PlanoContaDetailViewModel
            (
                  long planoContaId
                , PlanoContaSelectViewModel parent
                , TipoTransacao tipoTransacao
                , string nome
                , bool ativo
                , string descricao
            )
        {
            _planoContaId = planoContaId;
            _parent = parent;
            _tipoTransacao = tipoTransacao;
            _nome = nome;
            _ativo = ativo;
            _descricao = descricao;
        }

        public long PlanoContaId { get { return _planoContaId; } }
        public PlanoContaSelectViewModel Parent { get { return _parent; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public string Nome { get { return _nome; } }
        public bool Ativo { get { return _ativo; } }
        public string Descricao { get { return _descricao; } }
    }
}