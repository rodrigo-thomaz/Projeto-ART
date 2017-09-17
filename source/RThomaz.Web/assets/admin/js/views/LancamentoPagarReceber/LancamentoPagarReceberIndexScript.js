var LancamentoPagarReceberIndexScript = function () {

    var oTable = null;
    var selectedContaId = null;
    var selectedTipoConta = null;
    
    var handleInit = function () {

        var title = 'Lan\u00e7amentos a pagar e a receber';
        var subTitle = 'gerencie seus lan\u00e7amentos';

        setAppTitleSubTitle(title, subTitle);

        $("#menuLancamento").addClass("active");
        $("#menuLancamentoPagarReceber").addClass("active");

        document.body.addEventListener("tableContaSelectChanged", tableContaSelectChanged, false);

        $('#divContaSummary').load('/Conta/Summary');

        $("#cmbPeriodos").on("change", function () {
            refreshDataTable();
        });

        $("#btnRefresh").click(function (e) {
            ContaSummaryScript.refresh();
        });
    }

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

    var loadPeriodos = function () {        

        $("#cmbPeriodos").empty();
        
        $.ajax({
            type: "GET",
            url: ApplicationScript.getAppWebApiUrl() + '/api/lancamentoPagarReceber/periodos/',
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: {
                'contaId': selectedContaId,
                'tipoConta': selectedTipoConta,
            },
            success: function (data) {

                var d = new Date();
                var actualMonth = zeroPad(d.getMonth() + 1, 2);
                var actualYear = d.getFullYear().toString();
                var currentValue = actualMonth + actualYear;

                var cmbPeriodos = $("#cmbPeriodos");

                $.each(data, function () {
                    if (this.id == currentValue) {
                        cmbPeriodos.append($("<option selected/>").val(this.id).text(this.text));
                    }
                    else {
                        cmbPeriodos.append($("<option />").val(this.id).text(this.text));
                    }
                });

                refreshDataTable();

            },
            error: function (request, status, error) {
                alert("Erro GetPeriodos");
            }
        });

    };   

    var tableContaSelectChanged = function (e) {
        selectedContaId = e.detail.selectedContaId;
        selectedTipoConta = e.detail.selectedTipoConta;
        loadPeriodos();
    };

    var handleInitTableLancamento = function () {

        var tipoContaReceberControl = "<span class='badge badge-success'>C</span>";
        var tipoContaPagarControl = "<span class='badge badge-important'>D</span>";

        var tipoTransferenciaControl = "<span class='badge'>T</span>";
        var tipoTransacaoProgramadoControl = "<span class='badge'>P</span>";

        oTable = $('#dataTable')
            .on('xhr.dt', function (e, settings, json) {
                if (json.saldoAnterior) {
                    $("#txtSaldoAnterior").val(json.saldoAnterior);
                }
            })
            .dataTable({
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
                        "name": "TipoTransacao",
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

                            return actionControls;
                        }
                    },
                    {
                        "targets": 1,
                        "name": "TipoProgramacao",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {

                            var actionControls = "";

                            if (row.tipoProgramacao == "T") {
                                actionControls = tipoTransferenciaControl;
                            }
                            else if (row.tipoProgramacao == "P") {
                                actionControls = tipoTransacaoProgramadoControl;
                            }

                            return actionControls;
                        }
                    },
                    {
                        "targets": 2,
                        "name": "DataVencimento",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {
                            return '<p align="center">' + row.dataVencimento + '</p>';
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
                        "name": "PessoaNome",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {
                            return row.pessoaNome;
                        }
                    },
                    {
                        "targets": 5,
                        "name": "ValorVencimento",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {
                            return '<p align="right">' + row.valorVencimento + '</p>';
                        }
                    },
                    {
                        "targets": 6,
                        "name": "Saldo",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {
                            return '<p align="right">' + row.saldo + '</p>';
                        }
                    },
                    {
                        "targets": 7,
                        "name": "ProgramacaoId",
                        "sortable": false,
                        "searchable": false,
                        "render": function (data, type, row) {
                        
                            var actionControls = "<div class='actions'>";

                            if (row.tipoProgramacao == 'T') {
                                actionControls += "<a href=\'/Transferencia/Detail/" + row.transferenciaId + "' class=\'btn default btn-xs grey\'><i class=\'fa fa-edit\'></i> Editar</a>";
                                actionControls += "<a onclick='TransferenciaDetailScript.removeTransferencia(\"" + row.transferenciaId + "\",\"" + row.historico + "\");' href='#' class='btn default btn-xs grey'><i class='fa fa-trash-o'></i> Excluir</a>";
                            }
                            else {
                                actionControls += "<a href=\'/LancamentoPagarReceber/Detail/" + row.lancamentoId + "/" + row.tipoTransacao + "' class=\'btn default btn-xs grey\'><i class=\'fa fa-edit\'></i> Editar</a>";
                                actionControls += "<a onclick='LancamentoPagarReceberIndexScript.removeLancamento(\"" + row.lancamentoId + "\",\"" + row.tipoTransacao + "\",\"" + row.historico + "\");' href='#' class='btn default btn-xs grey'><i class='fa fa-trash-o'></i> Excluir</a>";
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
    };

    var getSelectedPeriodoMes = function () {
        var cmbPeriodoVal = $("#cmbPeriodos").val();
        if (cmbPeriodoVal == null) return null;
        var month = parseInt(cmbPeriodoVal.toString().substring(0, 2));
        return month;
    };

    var getSelectedPeriodoAno = function () {
        var cmbPeriodoVal = $("#cmbPeriodos").val();
        if (cmbPeriodoVal == null) return null;
        var year = parseInt(cmbPeriodoVal.toString().substring(2, 6));
        return year;
    };

    var getMasterViewListUrl = function () {

        var params = {
            'contaId': selectedContaId,
            'tipoConta': selectedTipoConta,
        };

        var selectedPeriodoMes = getSelectedPeriodoMes();
        var selectedPeriodoAno = getSelectedPeriodoAno();

        if (selectedPeriodoMes != null && selectedPeriodoAno != null) {
            params.periodo = {
                "mes": selectedPeriodoMes,
                "ano": selectedPeriodoAno,
            };
        }
        
        var result = ApplicationScript.getAppWebApiUrl() + '/api/lancamentopagarreceber/masterViewList?' + jQuery.param(params);

        return result;
    };

    var refreshDataTable = function () {
        var oSettings = oTable.fnSettings();
        oSettings.ajax.url = getMasterViewListUrl();
        var aoData = oTable._fnAjaxParameters(oSettings);
        oTable.fnDraw();
    };

    var handleRemoveLancamento = function (lancamentoId, tipoTransacao, historico) {

        var message = "Deseja realmente excluir o lan\u00e7amento \"" + historico + "\"?";

        bootbox.confirm(message, function (result) {
            if (result) {
                $.ajax({
                    type: "DELETE",
                    url: ApplicationScript.getAppWebApiUrl() + '/api/lancamentopagarreceber/' + lancamentoId + '/' + tipoTransacao,
                    headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                    content: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                }).success(function (data, textStatus, jqXHR) {
                    if (data) {
                        ContaSummaryScript.refresh();
                    }
                    else {
                        bootbox.alert({ message: "O lan\u00e7amento '" + historico + "' n&atilde;o pode ser exclu&iacute;do pois j&aacute; est&aacute; sendo utilizado pelo sistema." });
                    }
                }).error(function (jqXHR, textStatus, errorThrown) {
                    ApplicationScript.error(jqXHR, textStatus, errorThrown);
                });
            }
        });
    };

    return {
        init: function () {

            if (!jQuery().dataTable) {
                return;
            }

            handleInit();
            handleInitDataTable();
            handleInitTableLancamento();
        },
        removeLancamento: function (id, tipoTransacao, historico) {
            handleRemoveLancamento(id, tipoTransacao, historico);
        }
    };
}();