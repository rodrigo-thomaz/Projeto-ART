using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class SelectListProfile : Profile
    {
        public SelectListProfile()
        {
            CreateMap<SelectListRequestModel, SelectListRequestDTO>();            
        }
    }
}