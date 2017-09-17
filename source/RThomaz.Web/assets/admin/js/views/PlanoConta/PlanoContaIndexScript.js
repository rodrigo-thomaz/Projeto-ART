var PlanoContaIndexScript = function () {

    var tipoTransacao;
    var selectedNodes = [];

    var handleInit = function () {
        
        //Obtendo parametros
        tipoTransacao = parseInt(getUrlParameter('tipotransacao'));

        //Setando default menu
        $("#menuPlanoContas").addClass("active");

        //Setando os titulos

        var title = '';
        var subTitle = 'gerencie seu plano de contas';

        if (tipoTransacao == 0) {
            title = "Plano de contas - contas a receber";
            $("#menuPlanoContaContaReceber").addClass("active");
        }
        else if (tipoTransacao == 1) {
            title = "Plano de contas - contas a pagar";
            $("#menuPlanoContaContaPagar").addClass("active");
        }

        setAppTitleSubTitle(title, subTitle);
               

        $("#jstreePlanoConta").jstree({
            "core": {
                "themes": {
                    "responsive": false
                },
                "check_callback": true,
                'data': {
                    type: "GET",
                    url: ApplicationScript.getAppWebApiUrl() + '/api/planoConta/masterViewList',
                    headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                    content: "application/json; charset=utf-8",
                    dataType: "json",
                    data: function () {
                        var mostrarInativos = $('#chkMostrarInativos').is(':checked');
                        var search = $('#txtProcurar').val();
                        return {
                            'tipoTransacao': tipoTransacao,
                            'search': search,
                            'mostrarInativos': mostrarInativos,
                        };
                    },
                    success: function (data) {
                        //alert("sucesso");
                    },
                    error: function (request, status, error) {
                        alert("Erro /PlanoConta/GetTreeData");
                    }
                }
            },
            "types": {
                "default": {
                    "icon": "fa fa-folder icon-warning icon-lg"
                },
                "file": {
                    "icon": "fa fa-file icon-warning icon-lg"
                }
            },
            "plugins": ["dnd", "contextmenu", "state", "types"],
            "state": { "key": "demo3" },
            "contextmenu": {
                "items": function ($node) {
                    return {
                        "Create": {
                            "label": "Novo",
                            "action": function (obj) {
                                var tree = $('#jstreePlanoConta').jstree(true);
                                var id = tree.get_selected()
                                window.location.href = "/PlanoConta/Detail/0/" + tipoTransacao + "/" + id;

                                //var tree = $('#FileTree').jstree(true);
                                //var id = tree.get_selected();
                                //var newNode = tree.create_node(id);
                                //tree.edit(newNode);                                
                            }
                        },
                        "Edit": {
                            "label": "Editar",
                            "action": function (obj) {
                                if (selectedNodes.length == 1) {
                                    var id = selectedNodes[0].id;
                                    var parent = selectedNodes[0].parent;
                                    window.location.href = "/PlanoConta/Detail/" + id + "/" + tipoTransacao + '/' + parent;
                                }                                
                            }
                        },
                        "Delete": {
                            "label": "Excluir",
                            "action": function (obj) {
                                removeNodes();
                            }
                        },
                        "Rename": {
                            "label": "Renomear",
                            "action": function (obj) {
                                var tree = $('#jstreePlanoConta').jstree(true);
                                var id = tree.get_selected();
                                tree.edit(id);
                            }
                        },
                    };
                }
            }
        });

        $('#jstreePlanoConta').bind("changed.jstree", function (e, data) {
            var selecteds = data.instance.get_selected();
            if (selecteds.length == 0) {
                selectedNodes = [];
            }
            else if (selecteds.length == 1) {
                selectedNodes = [{
                    "id": data.node.id,
                    "text": data.node.text,
                    "parent": data.node.parent,
                }];
            }
            else if (selecteds.length > 1) {
                selectedNodes.push({
                    "id": data.node.id,
                    "text": data.node.text,
                    "parent": data.node.parent,
                });
            }
        });

        var moveCounter = 0;

        $('#jstreePlanoConta').bind("move_node.jstree", function (e, data) {
            if (moveCounter < selectedNodes.length) {
                moveCounter++;
            }
            if (moveCounter == selectedNodes.length) {
                moveCounter = 0;

                var parent = data.instance.get_node(data.parent);
                moveToParent(parent.id, parent.text);
            }
        });

        $('#jstreePlanoConta').bind("rename_node.jstree", function (e, data) {
            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/planoconta/rename?planoContaId=' + data.node.id + '&nome=' + data.node.text,
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            }).success(function (data, textStatus, jqXHR) {
                jQuery.jstree.reference("#jstreePlanoConta").refresh();
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        });

        var selectedNodesId = function () {
            var resultIds = [];
            for (var i = 0; i < selectedNodes.length; i++) {
                resultIds.push(selectedNodes[i].id)
            }
            return resultIds;
        };

        var moveToParent = function (parentId, parentText) {

            if (parentId == "#")
            {
                parentId = null;
                parentText = "Root";
            }

            var message = "";

            if (selectedNodes.length == 1) {
                message = "Deseja realmente mover a planoConta '" + selectedNodes[0].text + "' e seus respectivos n\u00f3s filhos para '" + parentText + "' ?";
            }
            else if (selectedNodes.length > 1) {
                message = "Deseja realmente mover as planoContas abaixo e seus respectivos n\u00f3s filhos para '" + parentText + "' ? <br/> <br/> ";
                for (var i = 0; i < selectedNodes.length; i++) {
                    message += selectedNodes[i].text + " <br/> ";
                }
            }

            var ids = selectedNodesId();

            bootbox.confirm(message, function (result) {
                if (result) {

                    $.ajax({
                        type: "PUT",
                        url: ApplicationScript.getAppWebApiUrl() + '/api/planoconta/move',
                        headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                        content: "application/json; charset=utf-8",
                        dataType: "json",
                        async: true,
                        data: {
                            planoContaIds: ids,
                            parentId: parentId,
                        }
                    }).success(function (data, textStatus, jqXHR) {
                        jQuery.jstree.reference("#jstreePlanoConta").refresh();
                    }).error(function (jqXHR, textStatus, errorThrown) {
                        ApplicationScript.error(jqXHR, textStatus, errorThrown);
                    });
                }
                else {
                    jQuery.jstree.reference("#jstreePlanoConta").refresh();
                }                
            });
        };

        var removeNodes = function () {

            var message = "";

            if (selectedNodes.length == 1) {
                message = "Deseja realmente remover o plano de conta '" + selectedNodes[0].text + "' ?";
            }
            else if (selectedNodes.length > 1) {
                message = "Deseja realmente remover os planos de conta abaixo ? <br/> <br/> ";
                for (var i = 0; i < selectedNodes.length; i++) {
                    message += selectedNodes[i].text + " <br/> ";
                }
            }

            message += " <div id='jstreeRemoveNodes'></div> ";

            var ids = selectedNodesId();

            bootbox.confirm(message, function (result) {
                if (result) {
                    for (var i = 0; i < ids.length; i++) {
                        $.ajax({
                            type: "DELETE",
                            url: ApplicationScript.getAppWebApiUrl() + '/api/planoconta/' + ids[i] + '/' + tipoTransacao,
                            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                            content: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,
                        }).success(function (data, textStatus, jqXHR) {
                            if (!data) {
                                bootbox.alert({ message: "O plano de conta '" + selectedNodes[i].text + "' n&atilde;o pode ser exclu&iacute;do pois j&aacute; est&aacute; sendo utilizado pelo sistema." });
                            }
                        }).error(function (jqXHR, textStatus, errorThrown) {
                            ApplicationScript.error(jqXHR, textStatus, errorThrown);
                        });
                    }
                    jQuery.jstree.reference("#jstreePlanoConta").refresh();
                }                
            });
        }

        $("#btnRefresh").click(function (e) {
            jQuery.jstree.reference("#jstreePlanoConta").refresh()
        });

        $("#chkMostrarInativos").on("change", function () {
            jQuery.jstree.reference("#jstreePlanoConta").refresh()
        });

        $("#txtProcurar").on("propertychange change keyup paste input", function () {
            jQuery.jstree.reference("#jstreePlanoConta").refresh()
        });

        $("#btnNovo").on("click", function () {
            window.location.href = '/PlanoConta/Detail/0/' + tipoTransacao + '/0';
        });
    }

    return {
                
        init: function () {
            handleInit();
        },

    };

}();