var CentroCustoIndexScript = function () {

    var selectedNodes = [];

    var handleInit = function () {
        
        var title = 'Centro de custo';
        var subTitle = 'gerencie seus centros de custo';

        setAppTitleSubTitle(title, subTitle);

        $("#menuCentroCustos").addClass("active");

        $("#jstreeCentroCusto").jstree({
            "core": {
                "themes": {
                    "responsive": false
                },
                "check_callback": true,
                'data': {
                    type: "GET",
                    url: ApplicationScript.getAppWebApiUrl() + '/api/centroCusto/masterViewList',
                    headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                    content: "application/json; charset=utf-8",
                    dataType: "json",
                    data: function () {
                        var mostrarInativos = $('#chkMostrarInativos').is(':checked');
                        var search = $('#txtProcurar').val();
                        return {
                            'search': search,
                            'mostrarInativos': mostrarInativos,
                        };
                    },
                    success: function (data) {
                        //alert("sucesso");
                    },
                    error: function (request, status, error) {
                        alert("Erro /CentroCusto/GetMasterList");
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
                                var tree = $('#jstreeCentroCusto').jstree(true);
                                var parentId = tree.get_selected()
                                window.location.href = "/CentroCusto/Detail/0/" + parentId;

                                //var tree = $('#FileTree').jstree(true);
                                //var id = tree.get_selected();
                                //var newNode = tree.create_node(id);
                                //tree.edit(newNode);                                
                            }
                        },
                        "Edit": {
                            "label": "Editar",
                            "action": function (obj) {
                                var tree = $('#jstreeCentroCusto').jstree(true);
                                var id = tree.get_selected();
                                window.location.href = "/CentroCusto/Detail/" + id + "/" + 0;
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
                                var tree = $('#jstreeCentroCusto').jstree(true);
                                var id = tree.get_selected();
                                tree.edit(id);
                            }
                        },
                    };
                }
            }
        });

        $('#jstreeCentroCusto').bind("changed.jstree", function (e, data) {
            var selecteds = data.instance.get_selected();
            if (selecteds.length == 0) {
                selectedNodes = [];
            }
            else if (selecteds.length == 1) {
                selectedNodes = [{
                    "id": data.node.id,
                    "text": data.node.text,
                }];
            }
            else if (selecteds.length > 1) {
                selectedNodes.push({
                    "id": data.node.id,
                    "text": data.node.text,
                });
            }
        });

        var moveCounter = 0;

        $('#jstreeCentroCusto').bind("move_node.jstree", function (e, data) {
            if (moveCounter < selectedNodes.length) {
                moveCounter++;
            }
            if (moveCounter == selectedNodes.length) {
                moveCounter = 0;

                var parent = data.instance.get_node(data.parent);
                moveToParent(parent.id, parent.text);
            }
        });

        $('#jstreeCentroCusto').bind("rename_node.jstree", function (e, data) {
            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/centrocusto/rename?centroCustoId=' + data.node.id + '&nome=' + data.node.text,
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            }).success(function (data, textStatus, jqXHR) {
                jQuery.jstree.reference("#jstreeCentroCusto").refresh();
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
                message = "Deseja realmente mover a centroCusto '" + selectedNodes[0].text + "' e seus respectivos n\u00f3s filhos para '" + parentText + "' ?";
            }
            else if (selectedNodes.length > 1) {
                message = "Deseja realmente mover as centroCustos abaixo e seus respectivos n\u00f3s filhos para '" + parentText + "' ? <br/> <br/> ";
                for (var i = 0; i < selectedNodes.length; i++) {
                    message += selectedNodes[i].text + " <br/> ";
                }
            }

            var ids = selectedNodesId();

            bootbox.confirm(message, function (result) {
                if (result) {

                    $.ajax({
                        type: "PUT",
                        url: ApplicationScript.getAppWebApiUrl() + '/api/centrocusto/move',
                        headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                        content: "application/json; charset=utf-8",
                        dataType: "json",
                        async: true,
                        data: {
                            centroCustoIds: ids,
                            parentId: parentId,
                        }
                    }).success(function (data, textStatus, jqXHR) {
                        jQuery.jstree.reference("#jstreeCentroCusto").refresh();
                    }).error(function (jqXHR, textStatus, errorThrown) {
                        ApplicationScript.error(jqXHR, textStatus, errorThrown);
                    });
                }
                else {
                    jQuery.jstree.reference("#jstreeCentroCusto").refresh()
                }                
            });
        };

        var removeNodes = function () {

            var message = "";

            if (selectedNodes.length == 1) {
                message = "Deseja realmente remover o centro de custo '" + selectedNodes[0].text + "' ?";
            }
            else if (selectedNodes.length > 1) {
                message = "Deseja realmente remover os centros de custo abaixo ? <br/> <br/> ";
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
                            url: ApplicationScript.getAppWebApiUrl() + '/api/centrocusto/' + ids[i],
                            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                            content: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,
                        }).success(function (data, textStatus, jqXHR) {
                            if (!data) {
                                bootbox.alert({ message: "O centro de custo '" + selectedNodes[i].text + "' n&atilde;o pode ser exclu&iacute;do pois j&aacute; est&aacute; sendo utilizado pelo sistema." });
                            }
                        }).error(function (jqXHR, textStatus, errorThrown) {
                            ApplicationScript.error(jqXHR, textStatus, errorThrown);
                        });
                    }
                    jQuery.jstree.reference("#jstreeCentroCusto").refresh();
                }                
            });
        }

        $("#btnRefresh").click(function (e) {
            jQuery.jstree.reference("#jstreeCentroCusto").refresh()
        });

        $("#chkMostrarInativos").on("change", function () {
            jQuery.jstree.reference("#jstreeCentroCusto").refresh()
        });

        $("#txtProcurar").on("propertychange change keyup paste input", function () {
            jQuery.jstree.reference("#jstreeCentroCusto").refresh()
        });
    }

    return {
                
        init: function () {
            handleInit();
        },

    };

}();