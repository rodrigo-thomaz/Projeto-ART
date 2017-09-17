var MovimentoImportacaoIndexScript = function () {
    
    var oTable = null;

    var handleInit = function () {

        var title = 'Gerenciar Importa\u00e7\u00f5es de movimenta\u00e7\u00f5es financeiras';
        var subTitle = 'gerencie suas importa\u00e7\u00f5es de movimenta\u00e7\u00f5es financeiras';

        setAppTitleSubTitle(title, subTitle);        

        $("#menuMovimento").addClass("active");

        $("#btnGroupRemove").addClass("disabled");
        $("#cmbTipoTransacao").prop("disabled", true);

        $("#btnCancelar").on("click", function () {
            window.location.href = "/Movimento";
        });

        $('#cmbImportacoes').on("change", function () {
            refreshDataTable();

            var movimentoImportacaoId = $("#cmbImportacoes").select2("val");
            if (movimentoImportacaoId == '') {
                $("#btnGroupRemove").addClass("disabled");
                $("#cmbTipoTransacao").prop("disabled", true);
            }
            else {
                $("#btnGroupRemove").removeClass("disabled");
                $("#cmbTipoTransacao").prop("disabled", false);
            }
        });

        $("#btnRefresh").click(function (e) {
            refreshDataTable();
        });

        $("#btnRemoveSelected").on("click", function () {

            var movimentos = [];
            var checkControls = $('#dataTable tbody td .row-checkbox-remove:checked');
              
            if (checkControls.length == 0) {
                return;
            }

            $.each(checkControls, function (index, value) {
                movimentos.push($(this).data());
            });

            var message = '';

            if (movimentos.length > 1) {
                message = "Deseja realmente excluir as (" + movimentos.length + ") movimenta&ccedil;&otilde;es selecionadas?";
            }
            else {
                message = "Deseja realmente excluir a movimenta&ccedil;&atilde;o '" + movimentos[0].historico + "'?";
            }
                       
            bootbox.confirm(message, function (result) {
                if (result) {
                    for (var i = 0; i < movimentos.length; i++) {
                        $.ajax({
                            type: "DELETE",
                            url: ApplicationScript.getAppWebApiUrl() + '/api/movimento/' + movimentos[i].movimentoid,
                            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                            content: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,
                        }).success(function (data, textStatus, jqXHR) {
                            if (!data) {
                                bootbox.alert({ message: "A movimenta&ccedil;&atilde;o '" + movimentos[i].historico + "' n&atilde;o pode ser exclu&iacute;da pois j&aacute; est&aacute; sendo utilizada pelo sistema." });
                            }
                        }).error(function (jqXHR, textStatus, errorThrown) {
                            ApplicationScript.error(jqXHR, textStatus, errorThrown);
                        });
                    }
                    refreshDataTable();
                }
            });            

        });

        $("#btnRemoveImportacao").on("click", function () {

            var message = "Deseja realmente excluir a importa&ccedil;&atilde;o e todos os seus movimentos?";

            bootbox.confirm(message, function (result) {
                if (result) {
                    var movimentoImportacaoId = $("#cmbImportacoes").select2("val");
                    $.ajax({
                        type: "DELETE",
                        url: ApplicationScript.getAppWebApiUrl() + '/api/movimentoimportacao/' + movimentoImportacaoId,
                        headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                        content: "application/json; charset=utf-8",
                        dataType: "json",
                        async: true,
                    }).success(function (data, textStatus, jqXHR) {
                        if (data) {
                            refreshDataTable();
                            $("#cmbImportacoes").select2("val", null);
                            $("#btnGroupRemove").addClass("disabled");
                        }
                        else {
                            bootbox.alert({ message: "A importa&ccedil;&atilde;o n&atilde;o pode ser exclu&iacute;da pois j&aacute; est&aacute; sendo utilizada pelo sistema." });
                        }
                    }).error(function (jqXHR, textStatus, errorThrown) {
                        ApplicationScript.error(jqXHR, textStatus, errorThrown);
                    });
                }
            });

        });

        $("#cmbTipoTransacao").on("change", function () {
            refreshDataTable();
        });
    };     

    var handleInitDataTable = function () {

        $.extend(true, $.fn.DataTable.TableTools.classes, {
            "container": "btn-group tabletools-dropdown-on-portlet table-export-buttons",
            "buttons": {
                "normal": "btn btn-sm default blue",
                "disabled": "btn btn-sm default disabled"
            },
            "collection": {
                "container": "DTTT_dropdown dropdown-menu tabletools-dropdown-menu"
            }
        });

    }

    var handleInitTableMovimento = function () {

        var table = $('#dataTable');

        var tipoCreditoControl = "<span class='badge badge-success'>C</span>";
        var tipoDebitoControl = "<span class='badge badge-important'>D</span>";

        oTable = table.dataTable({
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
                        [2, "asc"]
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
                "sort": false,
                "deferLoading": 0,
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
                        "name": "Detail",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {
                            if (row.percentualConciliado > 0) {
                                return '<span class="row-details row-details-close"></span>';
                            }
                            else {
                                return '<input type="checkbox" class="row-checkbox-remove" data-movimentoid="' + row.movimentoId + '" data-historico="' + row.historico  + '"/>';
                            }
                        }
                    },
                    {
                        "targets": 1,
                        "name": "TipoTransacao",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {

                            var actionControls = "";

                            if (row.tipoTransacao == "0") {
                                actionControls = tipoCreditoControl;
                            }
                            else if (row.tipoTransacao == "1") {
                                actionControls = tipoDebitoControl;
                            }

                            return actionControls;
                        }
                    },
                    {
                        "targets": 2,
                        "name": "DataMovimento",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {
                            return '<p align="center">' + row.dataMovimento + '</p>';
                        }
                    },
                    {
                        "targets": 3,
                        "name": "Historico",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {
                            return row.historico;
                        }
                    },
                    {
                        "targets": 4,
                        "name": "PercentualConciliado",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {

                            if (row.percentualConciliado == 0) {
                                return null;
                            }

                            var progressBarColor = '';

                            if (row.percentualConciliado == 100) {
                                progressBarColor = 'progress-bar-success';
                            }
                            else {
                                progressBarColor = 'progress-bar-warning';
                            }

                            var percentualConciliadoControl = '<div id="bar" class="progress progress-striped" role="progressbar">';
                            percentualConciliadoControl += '<div aria-valuenow="70" aria-valuemin="0" aria-valuemax="100" class="progress-bar ' + progressBarColor + '" style="width: ' + row.percentualConciliado + '%;">';
                            percentualConciliadoControl += row.percentualConciliado + '%';
                            percentualConciliadoControl += '</div>';
                            percentualConciliadoControl += '</div>';

                            return percentualConciliadoControl;
                        }
                    },
                    {
                        "targets": 5,
                        "name": "ValorMovimento",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {
                            return '<p align="right">' + row.valorMovimento + '</p>';
                        }
                    },
                    {
                        "targets": 6,
                        "name": "MovimentoId",
                        "sortable": false,
                        "searchable": false,
                        "render": function (data, type, row) {

                            var actionControls = "<div class='actions'>";

                            actionControls += "<a href=\'/Movimento/Detail/" + row.movimentoId + "/" + row.tipoTransacao + "' class=\'btn default btn-xs grey\'><i class=\'fa fa-edit\'></i> Editar</a>";

                            if (row.percentualConciliado == 0) {
                                actionControls += "<a onclick='MovimentoImportacaoIndexScript.removeMovimento(\"" + row.movimentoId + "\",\"" + row.historico + "\");' href='#' class='btn default btn-xs grey'><i class='fa fa-trash-o'></i> Excluir</a>";
                            }
                            else {
                                actionControls += "<a onclick='MovimentoImportacaoIndexScript.removeMovimento(\"" + row.movimentoId + "\",\"" + row.historico + "\");' href='#' class='btn default btn-xs grey disabled'><i class='fa fa-trash-o'></i> Excluir</a>";
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

        /* Add event listener for opening and closing details
        * Note that the indicator for showing which row is open is not controlled by DataTables,
        * rather it is done here
        */
        table.on('click', ' tbody td .row-details', function () {
            var nTr = $(this).parents('tr')[0];
            if (oTable.fnIsOpen(nTr)) {
                /* This row is already open - close it */
                $(this).addClass("row-details-close").removeClass("row-details-open");
                oTable.fnClose(nTr);
            } else {
                /* Open this row */
                $(this).addClass("row-details-open").removeClass("row-details-close");
                oTable.fnOpen(nTr, fnFormatDetails(oTable, nTr), 'details');
            }
        });

        /* Formatting function for row details */
        function fnFormatDetails(oTable, nTr) {

            var aData = oTable.fnGetData(nTr);

            var sOut = '';
            var tableName = 'tableConciliacao' + aData.movimentoId;

            sOut += '<table class="table table-striped table-bordered table-hover table-full-width" id="' + tableName + '">';
            sOut += '<thead>';
            sOut += '<tr>';
            sOut += '<th>Data</th>';
            sOut += '<th>Hist&oacute;rico</th>';
            sOut += '<th>Favorecido</th>';
            sOut += '<th style="text-align: right; width:130px;">Valor conciliado</th>';
            sOut += '<th style="text-align: right; width:130px;">Valor pagamento</th>';
            sOut += '<th class="actionColumn"></th>';
            sOut += '</tr>';
            sOut += '</thead>';
            sOut += '<tbody>';

            var lancamentos = ConciliacaoSharedScript.getLancamentos(aData.movimentoId, aData.tipoTransacao);

            $.each(lancamentos, function (index, value) {
                sOut += '<tr>';
                sOut += '<td>' + value.dataPagamento + '</td>';
                sOut += '<td>' + value.historico + '</td>';
                sOut += '<td>' + value.pessoaNome + '</td>';
                sOut += '<td style="text-align: right; width:130px;">' + value.valorConciliado + '</td>';
                sOut += '<td style="text-align: right; width:130px;">' + value.valorPagamento + '</td>';
                sOut += '<td><a href="/LancamentoPagoRecebido/Detail/' + value.lancamentoId + '/' + value.tipoTransacao + '" class="btn default btn-xs grey"><i class="fa fa-edit"></i> Detalhes</a></th>';
                sOut += '</tr>';
            });

            sOut += '</tbody>';
            sOut += '</table>';

            return sOut;
        }

        table.on('click', ' tbody td .row-checkbox-remove', function () {            
            var countChecked = $('#dataTable tbody td .row-checkbox-remove:checked').length;
            if (countChecked > 0) {
                $("#btnRemoveSelected").removeClass("disabled");
            }
            else {
                $("#btnRemoveSelected").addClass("disabled");
            }        
        });

    };

    var handleInitcmbImportacoes = function () {

        var pageSize = 20;
        var resultCallback = null;

        $('#cmbImportacoes').select2(
        {
            placeholder: 'Selecione',
            minimumInputLength: 0,
            allowClear: true,
            ajax: {
                quietMillis: 150,
                url: ApplicationScript.getAppWebApiUrl() + '/api/movimentoImportacao/selectViewList',
                dataType: 'json',
                data: function (term, page) {
                    return {
                        'param.PageSize': pageSize,
                        'param.PageNumber': page,
                        'param.Search': term,
                    };
                },
                transport: function (params) {
                    params.beforeSend = function (request) {
                        request.setRequestHeader("Authorization", 'bearer ' + ApplicationScript.getToken());
                    };
                    return $.ajax(params);
                },
                results: function (data, page) {
                    var more = (page * pageSize) < data.Total;
                    var results = [];
                    for (var i = 0; i < data.data.length; i++) {
                        results.push({
                            attr: null,
                            children: null,
                            id: data.data[i].movimentoImportacaoId,
                            text: data.data[i].importadoEm,
                        });
                    }
                    return { results: results, more: more };
                }
            }, initSelection: function (element, callback) {

                movimentoImportacaoId = getUrlParameter('id');

                if (movimentoImportacaoId !== undefined) {
                    $.ajax({
                        type: "GET",
                        async: false,
                        url: '/MovimentoImportacao/GetMovimentoImportacaoSelect2Result/',
                        headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                        content: "application/json; charset=utf-8",
                        dataType: "json",
                        data: {
                            'id': movimentoImportacaoId,
                        },
                        success: function (data) {
                            resultCallback = data;
                        },
                        error: function (request, status, error) {
                            alert("Erro /MovimentoImportacao/GetMovimentoImportacaoSelect2Result/");
                        }
                    });
                }
                if (resultCallback) {
                    callback({ id: parseInt(resultCallback.id), text: resultCallback.text });
                }

            },
        }).select2('val', []);       

    }

    var handleRemoveMovimento = function (id, historico) {

        var message = "Deseja realmente excluir a movimenta&ccedil;&atilde;o \"" + historico + "\"?";

        bootbox.confirm(message, function (result) {
            if (result) {
                $.ajax({
                    type: "DELETE",
                    url: ApplicationScript.getAppWebApiUrl() + '/api/movimento/' + id,
                    headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                    content: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                }).success(function (data, textStatus, jqXHR) {
                    if (data) {
                        refreshDataTable();
                    }
                    else {
                        bootbox.alert({ message: "A movimenta&ccedil;&atilde;o '" + historico + "' n&atilde;o pode ser exclu&iacute;da pois j&aacute; est&aacute; sendo utilizada pelo sistema." });
                    }
                }).error(function (jqXHR, textStatus, errorThrown) {
                    ApplicationScript.error(jqXHR, textStatus, errorThrown);
                });
            }
        });
    };

    var getMasterViewListUrl = function () {

        $("#btnRemoveSelected").addClass("disabled");

        var movimentoImportacaoId = $("#cmbImportacoes").select2("val");
        var tipoTransacao = $("#cmbTipoTransacao").val();

        var params = {
            'movimentoImportacaoId': movimentoImportacaoId,
            'tipoTransacao': tipoTransacao,
        };
        
        var result = ApplicationScript.getAppWebApiUrl() + '/api/movimentoimportacao/masterViewList?' + jQuery.param(params);

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
            handleInitcmbImportacoes();

            handleInitDataTable();            
            handleInitTableMovimento();
        },
        removeMovimento: function (id, historico) {
            handleRemoveMovimento(id, historico);
        },
    };
}();