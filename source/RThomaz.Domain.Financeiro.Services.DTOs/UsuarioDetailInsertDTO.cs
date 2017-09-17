namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class UsuarioDetailInsertDTO
    {
        private readonly string _nomeExibicao;
        private readonly string _email;
        private readonly string _senha;
        private readonly bool _ativo;
        private readonly string _descricao;

        public UsuarioDetailInsertDTO
            (
                  string nomeExibicao
                , string email
                , string senha
                , bool ativo
                , string descricao
            )
        {
            _nomeExibicao = nomeExibicao;
            _email = email;
            _senha = senha;
            _ativo = ativo;
            _descricao = descricao;
        }

        public string NomeExibicao { get { return _nomeExibicao; } }
        public string Email { get { return _email; } }
        public string Senha { get { return _senha; } }
        public bool Ativo { get { return _ativo; } }
        public string Descricao { get { return _descricao; } }
    }
}
