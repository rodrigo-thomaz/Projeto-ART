using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using System.Collections.Generic;
using RThomaz.Application.Financeiro.Helpers.JsTree;
using System.Linq;
using System;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class CentroCustoAppService : AppServiceBase, ICentroCustoAppService
    {
        #region private fields

        private readonly ICentroCustoService _centroCustoService;

        #endregion

        #region constructors

        public CentroCustoAppService(ICentroCustoService centroCustoService)
        {
            _centroCustoService = centroCustoService;
        }

        #endregion

        #region public voids

        public async Task<List<JsTreeNode>> GetMasterList(string search, bool mostrarInativos)
        {
            var masterContract = await _centroCustoService.GetMasterList(search, mostrarInativos);
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
                    id = item.CentroCustoId.ToString(),
                    a_attr = new JsTreeAAttribute { id = item.CentroCustoId.ToString() },
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

        public async Task<CentroCustoDetailViewModel> Edit(CentroCustoDetailEditModel model)
        {
            var editDTO = Mapper.Map<CentroCustoDetailEditModel, CentroCustoDetailEditDTO>(model);
            var viewDTO = await _centroCustoService.Edit(editDTO);
            var viewModel = Mapper.Map<CentroCustoDetailViewDTO, CentroCustoDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task<CentroCustoDetailViewModel> GetDetail(long centroCustoId)
        {
            var dto = await _centroCustoService.GetDetail(centroCustoId);
            var model = Mapper.Map<CentroCustoDetailViewDTO, CentroCustoDetailViewModel>(dto);
            return model;
        }

        public async Task<SelectListResponseModel<CentroCustoSelectViewModel>> GetSelectViewList(SelectListRequestModel selectListModelRequest)
        {
            var selectListRequestDTO = Mapper.Map<SelectListRequestModel, SelectListRequestDTO>(selectListModelRequest);
            var selectListDTOResponse = await _centroCustoService.GetSelectViewList(selectListRequestDTO);
            var selectListModel = Mapper.Map<List<CentroCustoSelectViewDTO>, List<CentroCustoSelectViewModel>>(selectListDTOResponse.Data);
            var selectListModelResponse = new SelectListResponseModel<CentroCustoSelectViewModel>(selectListDTOResponse.TotalRecords, selectListModel);
            return selectListModelResponse;
        }

        public async Task<CentroCustoDetailViewModel> Insert(CentroCustoDetailInsertModel model)
        {
            var insertDTO = Mapper.Map<CentroCustoDetailInsertModel, CentroCustoDetailInsertDTO>(model);
            var viewDTO = await _centroCustoService.Insert(insertDTO);
            var viewModel = Mapper.Map<CentroCustoDetailViewDTO, CentroCustoDetailViewModel>(viewDTO);
            return viewModel;
        }

        public async Task Move(CentroCustoMasterMoveModel model)
        {
            var dto = Mapper.Map<CentroCustoMasterMoveModel, CentroCustoMasterMoveDTO>(model);
            await _centroCustoService.Move(dto);
        }

        public async Task<bool> Remove(long centroCustoId)
        {
            var result = await _centroCustoService.Remove(centroCustoId);
            return result;
        }

        public async Task Rename(long centroCustoId, string nome)
        {
            await _centroCustoService.Rename(centroCustoId, nome);
        }

        public async Task<bool> UniqueNome(long? centroCustoId, string nome)
        {
            var result = await _centroCustoService.UniqueNome(centroCustoId, nome);
            return result;
        }

        #endregion

        #region private voids

        private JsTreeNode ConvertToJsTreeModel(List<CentroCustoMasterDTO> data, JsTreeNode root)
        {
            var children = data.Where(x => x.ParentId.Equals(Convert.ToInt64(root.a_attr.id)));

            foreach (var item in children)
            {
                if (root.children == null) root.children = new List<JsTreeNode>();

                var child = new JsTreeNode
                {
                    id = item.CentroCustoId.ToString(),
                    a_attr = new JsTreeAAttribute { id = item.CentroCustoId.ToString() },
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