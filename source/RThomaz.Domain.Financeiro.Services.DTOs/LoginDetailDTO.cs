using System;

namespace RThomaz.Domain.Financeiro.Services.DTOs
{
    public class LoginDetailDTO
    {
        private readonly long _usuarioId;
        private readonly Guid _aplicacaoId;
        private readonly string _storageBucketName;
        private readonly string _nomeExibicao;
        private readonly string _email;
        private readonly string _avatarStorageObject;

        public LoginDetailDTO
            (
                  long usuarioId
                , Guid aplicacaoId
                , string storageBucketName
                , string nomeExibicao
                , string email
                , string avatarStorageObject
            )
        {
            _usuarioId = usuarioId;
            _aplicacaoId = aplicacaoId;
            _storageBucketName = storageBucketName;
            _nomeExibicao = nomeExibicao;
            _email = email;
            _avatarStorageObject = avatarStorageObject;
        }

        public long UsuarioId { get { return _usuarioId; } }
        public Guid AplicacaoId { get { return _aplicacaoId; } }
        public string StorageBucketName{ get { return _storageBucketName; } }
        public string NomeExibicao { get { return _nomeExibicao; } }
        public string Email { get { return _email; } }
        public string AvatarStorageObject { get { return _avatarStorageObject; } }
    }
}