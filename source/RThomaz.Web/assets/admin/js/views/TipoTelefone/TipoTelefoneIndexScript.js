var TipoTelefoneIndexScript = function () {

    var oTable;
    var tipoPessoa;
    
    var handleInit = function () {

        //Obtendo parametros
        tipoPessoa = getUrlParameter('tipoPessoa');

        //Setando default menu
        $("#menuTipoTelefone").addClass("active");

        //Setando os titulos

        var title = '';
        var subTitle = 'gerencie seus tipos de telefone';

        if (tipoPessoa == "0") {
            title = "Tipo de telefone - pessoa f\u00edsica";
            $("#menuTipoTelefonePessoaFisica").addClass("active");
        }
        else if (tipoPessoa == "1") {
            title = "Tipo de telefone - pessoa jur\u00eddica";
            $("#menuTipoTelefonePessoaJuridica").addClass("active");
        }

        setAppTitleSubTitle(title, subTitle);

        $("#btnNovoTipoTelefone").on('click', function () {
            window.location.href = "/TipoTelefone/Detail?tipo=" + tipoPessoa;
        });

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
        var tableColumnToggler = $('#tipoTelefone_column_toggler');
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
                "url": ApplicationScript.getAppWebApiUrl() + "/api/tipoTelefone/masterViewList?ativo=" + getAtivo() + '&tipoPessoa=' + tipoPessoa,
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
                    "name": "Nome",
                    "sortable": true,
                    "searchable": true,
                    "render": function (data, type, row) {
                        return row.nome;
                    },
                },
                {
                    "targets": 1,
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
                    "targets": 2,
                    "name": "TipoTelefoneId",
                    "searchable": false,
                    "sortable": false,
                    "render": function (data, type, row) {

                        var actionControls = "<div class='actions'>";
                        actionControls += "<a href=\'/TipoTelefone/Detail?id=" + row.tipoTelefoneId + "&tipo=" + tipoPessoa + "\' class=\'btn default btn-xs grey\'><i class=\'fa fa-edit\'></i> Editar</a>";
                        actionControls += "<a onclick='TipoTelefoneIndexScript.removeTipoTelefone(\"" + row.tipoTelefoneId + "\",\"" + row.nome + "\");' href='#' class='btn default btn-xs grey'><i class='fa fa-trash-o'></i> Excluir</a>";
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

    var handleRemoveTipoTelefone = function (id, nome) {

        var message = "Deseja realmente excluir o tipo de telefone '" + nome + "'?";

        bootbox.confirm(message, function (result) {
            if (result) {
                $.ajax({
                    type: "DELETE",
                    url: ApplicationScript.getAppWebApiUrl() + '/api/tipotelefone/' + id + '/' + tipoPessoa,
                    headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                    content: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                }).success(function (data, textStatus, jqXHR) {
                    if (data) {
                        refreshDataTable();
                    }
                    else {
                        bootbox.alert({ message: "O tipo de telefone '" + nome + "' n&atilde;o pode ser exclu&iacute;do pois j&aacute; est&aacute; sendo utilizado pelo sistema." });
                    }
                }).error(function (jqXHR, textStatus, errorThrown) {
                    ApplicationScript.error(jqXHR, textStatus, errorThrown);
                });
            }
        });
    };

    var refreshDataTable = function () {
        var oSettings = oTable.fnSettings();
        oSettings.ajax.url = '/api/tipoTelefone/masterViewList?ativo=' + getAtivo() + '&tipoPessoa=' + tipoPessoa;
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
        removeTipoTelefone: function (id, nome) {
            handleRemoveTipoTelefone(id, nome);
        }
    };
}();