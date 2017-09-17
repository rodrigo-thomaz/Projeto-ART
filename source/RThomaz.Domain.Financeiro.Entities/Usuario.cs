using System;
using System.Collections.Generic;

namespace RThomaz.Domain.Financeiro.Entities
{
    public class Usuario
    {
        #region Primitive Properties

        public long UsuarioId { get; set; }

        public Guid AplicacaoId { get; set; }

        public Guid ApplicationUserId { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public bool Ativo { get; set; }

        public string Descricao { get; set; }

        #endregion

        #region Navigation Properties

        //public ApplicationUser ApplicationUser { get; set; }

        public Aplicacao Aplicacao { get; set; }

        public ICollection<CentroCusto> Responsaveis { get; set; }

        public Perfil Perfil { get; set; }        

        #endregion
    }
}