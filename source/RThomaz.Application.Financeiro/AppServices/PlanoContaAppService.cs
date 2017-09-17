using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Domain.Financeiro.Services.DTOs;
using AutoMapper;
using System.Collections.Generic;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using RThomaz.Application.Financeiro.Helpers.JsTree;
using System.Linq;
using System;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class PlanoContaAppService : AppServiceBase, IPlanoContaAppService
    {
        #region private fields

        private readonly IPlanoContaService _planoContaService;

        #endregion

        #region constructors

        public PlanoContaAppService(IPlanoContaService planoContaService)
        {
            _planoContaService = planoContaService;
        }

        #endregion

        #region public voids

        public async Task<List<JsTreeNode>> GetMasterList(TipoTransacao tipoTransacao, string search, bool mostrarInativos)
        {
            var masterContract = await _planoContaService.GetMasterList(tipoTransacao, search, mostrarInativos);
            var result = new List<JsTreeNode>();

            if (masterContract.Count() == 0)
            {
                return result;
            }

            var roots = masterContract.Where(x => x.ParentId == null);

            foreach (var item in roots)
            {
                var root = new JsTreeNode
                {
                    id = item.PlanoContaId.ToString(),
                    a_attr = new JsTreeAAttribute { id = item.PlanoContaId.ToString() },
                    text = item.Nome,
                    state = new State
                    {
                        opened = true,
                        disabled = !item.Ativo,
                        selected = false,
                    },
                };

                root = ConvertToJsTreeModel(masterContract, root);
                result.Add(root);
            }

            return result;
        }

        public async Task<PlanoContaDetailViewModel> Edit(PlanoContaDetailEditModel model)
        {
            var editDTO = Mapper.Map<PlanoContaDetailEditModel, PlanoContaDetailEditDTO>(model);
            var viewDTO = await _planoContaService.Edit(editDTO);
            var viewModel = Mapper.Map<PlanoContaDetailViewDTO, PlanoContaDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<PlanoContaDetailViewModel> GetDetail(long planoContaId, TipoTransacao tipoTransacao)
        {
            var dto = await _planoContaService.GetDetail(planoContaId, tipoTransacao);
            var model = Mapper.Map<PlanoContaDetailViewDTO, PlanoContaDetailViewModel>(dto);
            return model;
        }

        public async Task<SelectListResponseModel<PlanoContaSelectViewModel>> GetSelectViewList(SelectListRequestModel selectListModelRequest, TipoTransacao tipoTransacao)
        {
            var selectListRequestDTO = Mapper.Map<SelectListRequestModel, SelectListRequestDTO>(selectListModelRequest);
            var selectListDTOResponse = await _planoContaService.GetSelectViewList(selectListRequestDTO, tipoTransacao);
            var selectListModel = Mapper.Map<List<PlanoContaSelectViewDTO>, List<PlanoContaSelectViewModel>>(selectListDTOResponse.Data);
            var selectListModelResponse = new SelectListResponseModel<PlanoContaSelectViewModel>(selectListDTOResponse.TotalRecords, selectListModel);
            return selectListModelResponse;
        }

        public async Task<PlanoContaDetailViewModel> Insert(PlanoContaDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<PlanoContaDetailInsertModel, PlanoContaDetailInsertDTO>(model);
            var viewDTO = await _planoContaService.Insert(insertDTO);
            var viewModel = Mapper.Map<PlanoContaDetailViewDTO, PlanoContaDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task Move(PlanoContaMasterMoveModel model)
        {
            var dto = Mapper.Map<PlanoContaMasterMoveModel, PlanoContaMasterMoveDTO>(model);
            await _planoContaService.Move(dto);
        }

        public async Task<bool> Remove(long planoContaId, TipoTransacao tipoTransacao)
        {
            var result = await _planoContaService.Remove(planoContaId, tipoTransacao);
            return result;
        }

        public async Task Rename(long planoContaId, string nome)
        {
            await _planoContaService.Rename(planoContaId, nome);
        }

        public async Task<bool> UniqueNome(TipoTransacao tipoTransacao, string nome)
        {
            var result = await _planoContaService.UniqueNome(tipoTransacao, nome);
            return result;
        }

        #endregion

        #region private voids

        private JsTreeNode ConvertToJsTreeModel(List<PlanoContaMasterDTO> data, JsTreeNode root)
        {
            var children = data.Where(x => x.ParentId.Equals(Convert.ToInt64(root.a_attr.id)));

            foreach (var item in children)
            {
                if (root.children == null) root.children = new List<JsTreeNode>();

                var child = new JsTreeNode
                {
                    id = item.PlanoContaId.ToString(),
                    a_attr = new JsTreeAAttribute { id = item.PlanoContaId.ToString() },
                    text = item.Nome,
                    state = new State
                    {
                        opened = true,
                        //disabled = root.state.disabled ? true : !item.Ativo,
                        disabled = !item.Ativo,
                        selected = false,
                    },
                };

                child = ConvertToJsTreeModel(data, child);

                root.children.Add(child);
            }

            return root;
        }

        #endregion
    }
}