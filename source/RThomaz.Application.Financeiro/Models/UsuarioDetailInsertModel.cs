namespace RThomaz.Application.Financeiro.Models
{
    public class UsuarioDetailInsertModel
    {
        public string Email { get; set; }

        public string NomeExibicao { get; set; }

        public string Senha { get; set; }

        public bool Ativo { get; set; }     

        public string Descricao { get; set; }
    }
}