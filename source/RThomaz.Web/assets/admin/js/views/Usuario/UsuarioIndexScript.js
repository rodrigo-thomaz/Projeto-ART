var UsuarioIndexScript = function () {

    var oTable;
    
    var handleInit = function () {

        var title = 'Usu\u00e1rios';
        var subTitle = 'gerencie os usu\u00e1rios do sistema';

        setAppTitleSubTitle(title, subTitle);

        $("#menuUsuario").addClass("active");

        $("#btnRefresh").click(function (e) {
            refreshDataTable();
        });

        $("#cmbAtivo").select2({
            allowClear: false,
            minimumInputLength: 0,
        });

        $("#cmbAtivo").on("change", function () {
            refreshDataTable();
        });

        /* handle show/hide columns*/
        var tableColumnToggler = $('#usuario_column_toggler');
        $('input[type="checkbox"]', tableColumnToggler).change(function () {
            /* Get the DataTables object again - this is not a recreation, just a get of the object */
            var iCol = parseInt($(this).attr("data-column"));
            var bVis = oTable.fnSettings().aoColumns[iCol].bVisible;
            oTable.fnSetColumnVis(iCol, (bVis ? false : true));
        });
    }

    var handleInitDataTable = function () {

        $.extend(true, $.fn.DataTable.TableTools.classes, {
            "container": "btn-group tabletools-dropdown-on-portlet",
            "buttons": {
                "normal": "btn btn-sm default",
                "disabled": "btn btn-sm default disabled"
            },
            "collection": {
                "container": "DTTT_dropdown dropdown-menu tabletools-dropdown-menu"
            }
        });

        oTable = $('#dataTable').dataTable({
            "language": {
                "aria": {
                    "sortAscending": ": activate to sort column ascending",
                    "sortDescending": ": activate to sort column descending"
                },
                "emptyTable": "No data available in table",
                "info": "Mostrando _START_ at&eacute; _END_ de _TOTAL_ resultados",
                "infoEmpty": "No entries found",
                "infoFiltered": "(filtered1 from _MAX_ total entries)",
                "lengthMenu": "Mostrar _MENU_ resultados",
                "search": "Procurar:",
                "zeroRecords": "No matching records found"
            },
            "bStateSave": true,
            "lengthMenu": [
                    [10, 20, 50, 100, 150, -1],
                    [10, 20, 50, 100, 150, "All"] // change per page values here
            ],
            "pageLength": 10, // default record count per page
            "order": [
                    [0, "asc"]
            ],
            "dom": "<'row' <'col-md-12'T>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // horizobtal scrollable datatable
            // Uncomment below line("dom" parameter) to fix the dropdown overflow issue in the datatable cells. The default datatable layout
            // setup uses scrollable div(table-scrollable) with overflow:auto to enable vertical scroll(see: assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js). 
            // So when dropdowns used the scrollable div should be removed. 
            //"dom": "<'row' <'col-md-12'T>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>",

            "tableTools": {
                "sSwfPath": ApplicationScript.getTemplateUrl() + "/theme/assets/global/plugins/datatables/extensions/TableTools/swf/copy_csv_xls_pdf.swf",
                "aButtons": [{
                    "sExtends": "pdf",
                    "sButtonText": "PDF"
                }, {
                    "sExtends": "csv",
                    "sButtonText": "CSV"
                }, {
                    "sExtends": "xls",
                    "sButtonText": "Excel"
                }, {
                    "sExtends": "print",
                    "sButtonText": "Print",
                    "sInfo": 'Precione "CTR+P" para imprimir ou "ESC" para sair',
                    "sMessage": "Gerado por RThomaz"
                }, {
                    "sExtends": "copy",
                    "sButtonText": "Copy"
                }]
            },
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": ApplicationScript.getAppWebApiUrl() + "/api/usuario/masterViewList?ativo=" + getAtivo(),
                "headers": { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                "content": "application/json; charset=utf-8",
                "type": "GET",
                "data": function (d) {
                    //d.ativo = getAtivo();
                },
                //"success": function (d) {

                //},
                error: function (xhr, textStatus, error) {
                    ApplicationScript.error(xhr, textStatus, errorThrown);
                },
            },
            "columnDefs": [
                            {
                                "targets": 0,
                                "name": "AvatarStorageObject",
                                "searchable": false,
                                "sortable": false,
                                "render": function (data, type, row) {

                                    var avatarControls = "<span class='btn-group'>";
                                    avatarControls += "<div id='mws-user-photo'>";
                                    if (row.avatarStorageObject) {
                                        avatarControls += "<img id=\'imgAvatar\' src=\'/api/perfil/avatar/" + row.avatarStorageObject + "\' style=\'height:28px;\'>";
                                    }
                                    else {
                                        avatarControls += "<img id='imgAvatar' src='../assets/admin/img/avatar.png' style='height:28px;'>";
                                    }
                                    avatarControls += "</div>";
                                    avatarControls += "</span>";

                                    return avatarControls;
                                }
                            },
                            {
                                "targets": 1,
                                "name": "NomeExibicao",
                                "sortable": true,
                                "searchable": true,
                                "render": function (data, type, row) {
                                    return row.nomeExibicao;
                                },
                            },
                            {
                                "targets": 2,
                                "name": "Email",
                                "sortable": true,
                                "searchable": true,
                                "render": function (data, type, row) {
                                    return row.email;
                                },
                            },
                            {
                                "targets": 3,
                                "name": "Ativo",
                                "searchable": false,
                                "sortable": true,
                                "render": function (data, type, row) {
                                    if (row.ativo) {
                                        return "<i class='fa fa-check-square-o'></i>";
                                    } else {
                                        return "<i class='fa fa-square-o'></i>";
                                    }
                                }
                            },
                            {
                                "targets": 4,
                                "name": "UsuarioId",
                                "searchable": false,
                                "sortable": false,
                                "render": function (data, type, row) {

                                    var actionControls = "<div class='actions'>";
                                    actionControls += "<a href=\'/Usuario/Detail/" + row.usuarioId + "\' class=\'btn default btn-xs grey\'><i class=\'fa fa-edit\'></i> Editar</a>";
                                    actionControls += "<a onclick='UsuarioIndexScript.removeUsuario(\"" + row.usuarioId + "\",\"" + row.nomeExibicao + "\",\"" + row.email + "\");' href='#' class='btn default btn-xs grey'><i class='fa fa-trash-o'></i> Excluir</a>";
                                    actionControls += "</div>";

                                    return actionControls;
                                }
                            }
            ],
            "initComplete": function (settings, json) {
                jQuery('#dataTable_wrapper .dataTables_filter input').addClass("form-control input-medium input-inline"); // modify table search input
                jQuery('#dataTable_wrapper .dataTables_length select').addClass("form-control input-small"); // modify table per page dropdown
                jQuery('#dataTable_wrapper .dataTables_length select').select2(); // initialize select2 dropdown
            }
        });

        var oTableColReorder = new $.fn.dataTable.ColReorder(oTable);
    }   

    var handleRemoveUsuario = function (id, nomeExibicao, email) {

        var message = "Deseja realmente excluir o usu\u00e1rio \n '" + nomeExibicao + "' (" + email + ")?";

        bootbox.confirm(message, function (result) {
            if (result) {
                $.ajax({
                    type: "DELETE",
                    url: ApplicationScript.getAppWebApiUrl() + '/api/usuario/' + id,
                    headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                    content: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                }).success(function (data, textStatus, jqXHR) {
                    if (data) {
                        refreshDataTable();
                    }
                    else {
                        bootbox.alert({ message: "O usu\u00e1rio '" + nome + "' n&atilde;o pode ser exclu&iacute;do pois j&aacute; est&aacute; sendo utilizado pelo sistema." });
                    }
                }).error(function (jqXHR, textStatus, errorThrown) {
                    ApplicationScript.error(jqXHR, textStatus, errorThrown);
                });
            }
        });
    };

    var refreshDataTable = function () {
        var oSettings = oTable.fnSettings();
        oSettings.ajax.url = '/api/usuario/masterViewList?ativo=' + getAtivo();
        var aoData = oTable._fnAjaxParameters(oSettings);
        oTable.fnDraw();
    };

    var getAtivo = function () {
        var cmbAtivoValue = $("#cmbAtivo").select2("val");
        if (cmbAtivoValue == "1") { //Sim
            return true;
        }
        else if (cmbAtivoValue == "2") { //Não
            return false;
        }
    };

    return {
        init: function () {
            
            if (!jQuery().dataTable) {
                return;
            }

            handleInit();
            handleInitDataTable();

        },
        removeUsuario: function (id, nomeExibicao, email) {
            handleRemoveUsuario(id, nomeExibicao, email);
        }
    };

}();