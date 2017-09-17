using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class PlanoContaSelectViewDTO
    {
        private readonly long _planoContaId;
        private readonly TipoTransacao _tipoTransacao;
        private readonly List<PlanoContaSelectViewDTO> _children;
        private readonly string _nome;        

        public PlanoContaSelectViewDTO
            (
                  long planoContaId
                , TipoTransacao tipoTransacao
                , List<PlanoContaSelectViewDTO> children                
                , string nome
            )
        {
            _planoContaId = planoContaId;
            _tipoTransacao = tipoTransacao;
            _children = children;            
            _nome = nome;
        }

        public long PlanoContaId { get { return _planoContaId; } }
        public TipoTransacao TipoTransacao { get { return _tipoTransacao; } }
        public List<PlanoContaSelectViewDTO> Children { get { return _children; } }        
        public string Nome { get { return _nome; } }
    }
}