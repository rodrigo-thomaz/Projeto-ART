using AutoMapper;
using RThomaz.Application.Financeiro.Helpers.Storage;
using RThomaz.Infra.CrossCutting.Storage;
using System;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class StorageUploadProfile : Profile
    {
        public StorageUploadProfile()
        {
            CreateMap<StorageUploadModel, StorageUploadDTO>().ConstructUsing(x => new StorageUploadDTO
                (
                    contentType: x.ContentType,
                    buffer: Convert.FromBase64String(x.BufferBase64String)
                ));            
        }
    }
}