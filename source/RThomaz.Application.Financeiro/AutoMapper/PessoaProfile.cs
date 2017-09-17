using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Application.Financeiro.Models;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<PessoaMasterDTO, PessoaMasterModel>();

            CreateMap<PessoaSelectViewDTO, PessoaSelectViewModel>();

            CreateMap<PessoaEmailDetailDTO, PessoaEmailDetailModel>();
            CreateMap<PessoaEnderecoDetailDTO, PessoaEnderecoDetailModel>();
            CreateMap<PessoaHomePageDetailDTO, PessoaHomePageDetailModel>();
            CreateMap<PessoaTelefoneDetailDTO, PessoaTelefoneDetailModel>();

            CreateMap<PessoaDetailDTO, PessoaDetailModel>()                
                .Include<PessoaFisicaDetailViewDTO, PessoaFisicaDetailViewModel>()
                .Include<PessoaJuridicaDetailViewDTO, PessoaJuridicaDetailViewModel>();

            CreateMap<PessoaFisicaDetailViewDTO, PessoaFisicaDetailViewModel>();
            CreateMap<PessoaJuridicaDetailViewDTO, PessoaJuridicaDetailViewModel>();   



            CreateMap<PessoaDetailModel, PessoaDetailDTO>()
                .Include<PessoaFisicaDetailInsertModel, PessoaFisicaDetailInsertDTO>()
                .Include<PessoaJuridicaDetailInsertModel, PessoaJuridicaDetailInsertDTO>()
                .Include<PessoaFisicaDetailEditModel, PessoaFisicaDetailEditDTO>()
                .Include<PessoaJuridicaDetailEditModel, PessoaJuridicaDetailEditDTO>();

            CreateMap<PessoaFisicaDetailInsertModel, PessoaFisicaDetailInsertDTO>();
            CreateMap<PessoaJuridicaDetailInsertModel, PessoaJuridicaDetailInsertDTO>();
            CreateMap<PessoaFisicaDetailEditModel, PessoaFisicaDetailEditDTO>();
            CreateMap<PessoaJuridicaDetailEditModel, PessoaJuridicaDetailEditDTO>();

            CreateMap<PessoaEmailDetailModel, PessoaEmailDetailDTO>();
            CreateMap<PessoaEnderecoDetailModel, PessoaEnderecoDetailDTO>();
            CreateMap<PessoaHomePageDetailModel, PessoaHomePageDetailDTO>();
            CreateMap<PessoaTelefoneDetailModel, PessoaTelefoneDetailDTO>();
        }
    }
}