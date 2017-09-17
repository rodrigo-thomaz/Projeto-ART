using System;
namespace RThomaz.Domain.Financeiro.Entities
{
    public class Perfil
    {
        #region Primitive Properties

        public long UsuarioId { get; set; }

        public Guid AplicacaoId { get; set; }

        public string NomeExibicao { get; set; }

        public string AvatarStorageObject { get; set; }

        #endregion

        #region Navigation Properties

        public Usuario Usuario { get; set; }

        #endregion
    }
}