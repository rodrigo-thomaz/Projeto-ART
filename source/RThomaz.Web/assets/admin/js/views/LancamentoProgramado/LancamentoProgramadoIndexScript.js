var LancamentoProgramadoIndexScript = function () {

    var oTable;

    var handleInit = function () {

        var title = 'Programa\u00e7\u00e3o';
        var subTitle = 'gerencie seus lan\u00e7amentos programados';

        setAppTitleSubTitle(title, subTitle);

        $("#menuLancamentoProgramado").addClass("active");

        $("#btnRefresh").click(function (e) {
            refreshDataTable();
        });

        ContaWithProgramacaoSharedScript.init('cmbConta');

        $("#cmbConta").on("change", function () {
            refreshDataTable();
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

        var tipoContaReceberControl = "<span class='badge badge-success'>C</span>";
        var tipoContaPagarControl = "<span class='badge badge-important'>D</span>";
        var tipoTransferenciaControl = "<span class='badge badge-info'>T</span>";

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
                "url": getMasterViewListUrl(),
                "headers": { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                "content": "application/json; charset=utf-8",
                "type": "GET",
                "data": function (d) {
                    //d.ativo = getAtivo();
                },
                //"success": function (d) {
                //    d = d.result;
                //    return d;
                //},
                error: function (xhr, textStatus, error) {
                    ApplicationScript.error(xhr, textStatus, errorThrown);
                },
            },
            "columnDefs": [
                {
                    "targets": 0,
                    "name": "Tipo",
                    "sortable": false,
                    "searchable": true,
                    "render": function (data, type, row) {

                        var actionControls = "";

                        if (row.tipoTransacao == "0") {
                            actionControls = tipoContaReceberControl;
                        }
                        else if (row.tipoTransacao == "1") {
                            actionControls = tipoContaPagarControl;
                        }
                        else if (row.tipoTransacao == "") {
                            actionControls = tipoTransferenciaControl;
                        }

                        return actionControls;
                    }
                },
                {
                    "targets": 1,
                    "name": "DataInicial",
                    "sortable": true,
                    "searchable": true,
                    "render": function (data, type, row) {
                        return '<p align="center">' + row.dataInicial + '</p>';
                    }
                },
                {
                    "targets": 2,
                    "name": "DataFinal",
                    "sortable": true,
                    "searchable": true,
                    "render": function (data, type, row) {
                        return '<p align="center">' + row.dataFinal + '</p>';
                    }
                },
                {
                    "targets": 3,
                    "name": "Frequencia",
                    "sortable": false,
                    "searchable": true,
                    "render": function (data, type, row) {
                        return row.frequencia;
                    },
                },
                {
                    "targets": 4,
                    "name": "Historico",
                    "sortable": true,
                    "searchable": true,
                    "render": function (data, type, row) {
                        return row.historico;
                    },
                },
                {
                    "targets": 5,
                    "name": "PessoaNome",
                    "sortable": true,
                    "searchable": true,
                    "render": function (data, type, row) {
                        return row.pessoaNome;
                    },
                },
                {
                    "targets": 6,
                    "name": "ContaNome",
                    "sortable": true,
                    "searchable": true,
                    "render": function (data, type, row) {
                        return row.contaNome;
                    },
                },
                {
                    "targets": 7,
                    "name": "ValorVencimento",
                    "sortable": true,
                    "searchable": true,
                    "render": function (data, type, row) {
                        return '<p align="right">' + row.valorVencimento + '</p>';
                    },
                },
                {
                    "targets": 8,
                    "name": "ProgramacaoId",
                    "sortable": false,
                    "searchable": false,
                    "render": function (data, type, row) {
                        
                        var actionControls = "<div class='actions'>";

                        if (row.tipoTransacao == '') {
                            actionControls += "<a href=\'/TransferenciaProgramada/Detail/" + row.programacaoId + "' class=\'btn default btn-xs grey\'><i class=\'fa fa-edit\'></i> Editar</a>";
                            actionControls += "<a onclick='LancamentoProgramadoIndexScript.removeTransferenciaProgramada(\"" + row.programacaoId + "\",\"" + row.historico + "\");' href='#' class='btn default btn-xs grey'><i class='fa fa-trash-o'></i> Excluir</a>";
                        }
                        else {
                            actionControls += "<a href=\'/LancamentoProgramado/Detail/" + row.programacaoId + "/" + row.tipoTransacao + "' class=\'btn default btn-xs grey\'><i class=\'fa fa-edit\'></i> Editar</a>";
                            actionControls += "<a onclick='LancamentoProgramadoIndexScript.removeLancamentoProgramado(\"" + row.programacaoId + "\",\"" + row.historico + "\");' href='#' class='btn default btn-xs grey'><i class='fa fa-trash-o'></i> Excluir</a>";
                        }

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

    }
    
    var handleRemoveLancamentoProgramado = function (id, historico) {

        var message = "Deseja realmente excluir o lan\u00e7amento programado '" + historico + "'?";

        bootbox.confirm(message, function (result) {
            if (result) {
                $.ajax({
                    type: "DELETE",
                    url: ApplicationScript.getAppWebApiUrl() + '/api/lancamentoprogramado/' + id,
                    headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                    content: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                }).success(function (data, textStatus, jqXHR) {
                    if (data) {
                        refreshDataTable();
                    }
                    else {
                        bootbox.alert({ message: "O lan\u00e7amento programado '" + historico + "' n&atilde;o pode ser exclu&iacute;do pois j&aacute; est&aacute; sendo utilizado pelo sistema." });
                    }
                }).error(function (jqXHR, textStatus, errorThrown) {
                    ApplicationScript.error(jqXHR, textStatus, errorThrown);
                });
            }
        });
    };

    var handleRemoveTransferenciaProgramada = function (id, historico) {

        var message = "Deseja realmente excluir a transfer\u00eancia programada '" + historico + "'?";

        bootbox.confirm(message, function (result) {
            if (result) {
                $.ajax({
                    type: "DELETE",
                    url: ApplicationScript.getAppWebApiUrl() + '/api/transferenciaprogramada/' + id,
                    headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                    content: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                }).success(function (data, textStatus, jqXHR) {
                    if (data) {
                        refreshDataTable();
                    }
                    else {
                        bootbox.alert({ message: "A transfer\u00eancia '" + historico + "' n&atilde;o pode ser exclu&iacute;da pois j&aacute; est&aacute; sendo utilizada pelo sistema." });
                    }
                }).error(function (jqXHR, textStatus, errorThrown) {
                    ApplicationScript.error(jqXHR, textStatus, errorThrown);
                });
            }
        });
    };

    var getMasterViewListUrl = function () {

        var result = ApplicationScript.getAppWebApiUrl() + '/api/lancamentoprogramado/masterViewList'

        var cmbContaData = $("#cmbConta").select2("data");

        if (cmbContaData != null && cmbContaData.id != null && cmbContaData.tipoConta != null) {
            var params = {
                'contaId': cmbContaData.id,
                'tipoConta': cmbContaData.tipoConta,
            };

            result = result + '?' + jQuery.param(params);
        }
        else {
            var params = {
                'contaId': null,
                'tipoConta': null,
            };

            result = result + '?' + jQuery.param(params);
        }

        return result;
    };

    var refreshDataTable = function () {
        var oSettings = oTable.fnSettings();
        oSettings.ajax.url = getMasterViewListUrl();
        var aoData = oTable._fnAjaxParameters(oSettings);
        oTable.fnDraw();
    };

    return {
        init: function () {

            if (!jQuery().dataTable) {
                return;
            }

            handleInit();
            handleInitDataTable();
        },
        removeLancamentoProgramado: function (id, historico) {
            handleRemoveLancamentoProgramado(id, historico);
        },
        removeTransferenciaProgramada: function (id, historico) {
            handleRemoveTransferenciaProgramada(id, historico);
        }
    };
}();
