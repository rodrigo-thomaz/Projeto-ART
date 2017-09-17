using System;

namespace RThomaz.Application.Financeiro.Models
{
    public class AccountDetailViewModel
    {
        private readonly long _usuarioId;
        private readonly Guid _aplicacaoId;
        private readonly string _storageBucketName;
        private readonly string _nomeExibicao;
        private readonly string _email;
        private readonly bool _lembrarMe;
        private readonly string _avatarStorageObject;

        public AccountDetailViewModel
            (
                  long usuarioId
                , Guid aplicacaoId
                , string storageBucketName
                , string nomeExibicao
                , string email
                , bool lembrarMe
                , string avatarStorageObject
            )
        {
            _usuarioId = usuarioId;
            _aplicacaoId = aplicacaoId;
            _storageBucketName = storageBucketName;
            _nomeExibicao = nomeExibicao;
            _email = email;
            _lembrarMe = lembrarMe;
            _avatarStorageObject = avatarStorageObject;
        }

        public long UsuarioId { get { return _usuarioId; } }
        public Guid AplicacaoId { get { return _aplicacaoId; } }
        public string StorageBucketName { get { return _storageBucketName; } }
        public string NomeExibicao { get { return _nomeExibicao; } }
        public string Email { get { return _email; } }
        public bool LembrarMe { get { return _lembrarMe; } }
        public string AvatarStorageObject { get { return _avatarStorageObject; } }
    }
}
