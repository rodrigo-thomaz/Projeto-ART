using RThomaz.Database.Enums;

namespace RThomaz.Business.DTOs
{
    public class TipoTelefoneInsertDTO
    {
        private readonly string _nome;
        private readonly TipoPessoa _tipoPessoa;
        private readonly bool _ativo;

        public TipoTelefoneInsertDTO
        (
            string nome,
            TipoPessoa tipoPessoa,
            bool ativo
        )
        {
            _nome = nome;
            _tipoPessoa = tipoPessoa;
            _ativo = ativo;
        }

        public string Nome { get { return _nome; } }
        public TipoPessoa TipoPessoa { get { return _tipoPessoa; } }
        public bool Ativo { get { return _ativo; } }
    }
}
