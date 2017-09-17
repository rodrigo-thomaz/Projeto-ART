var MovimentoIndexScript = function () {

    var oTable = null;
    var selectedContaId = null;
    var selectedTipoConta = null;
    
    var handleInit = function () {

        var title = 'Movimenta\u00e7\u00f5es';
        var subTitle = 'gerencie suas movimenta\u00e7\u00f5es';

        setAppTitleSubTitle(title, subTitle);

        $("#menuMovimento").addClass("active");

        document.body.addEventListener("tableContaSelectChanged", tableContaSelectChanged, false);        

        $('#divContaSummary').load('/Conta/Summary');

        $("#cmbPeriodos").on("change", function () {
            refreshDataTable();
        });

        $("#cmbTipoTransacao").on("change", function () {
            refreshDataTable();
        });

        $("#cmbConciliacaoStatus").on("change", function () {
            refreshDataTable();
        });

        $("#cmbConciliacaoOrigem").on("change", function () {
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

    var tableContaSelectChanged = function (e) {
        selectedContaId = e.detail.selectedContaId;
        selectedTipoConta = e.detail.selectedTipoConta;
        loadPeriodos();
    };

    var loadPeriodos = function () {        

        $("#cmbPeriodos").empty();
        
        $.ajax({
            type: "GET",
            url: ApplicationScript.getAppWebApiUrl() + '/api/movimento/periodos/',
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

    var handleInitTableMovimento = function () {

        var table = $('#dataTable');

        var tipoCreditoControl = "<span class='badge badge-success'>C</span>";
        var tipoDebitoControl = "<span class='badge badge-important'>D</span>";

        var origemImportacaoControl = "<span class='badge badge-success'>I</span>";
        var origemManualControl = "<span class='badge badge-important'>M</span>";

        oTable = table
            .on('xhr.dt', function (e, settings, json) {
                if(json.saldoAnterior !== ""){
                    $("#txtSaldoAnterior").val(json.saldoAnterior);
                }
                else{
                    $("#txtSaldoAnterior").val("--------");
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

                            var totalConciliado = parseFloat(row.totalConciliado.replace('(', '').replace(')', '').replace('.', '').replace(',', '.'));

                            if (totalConciliado !== 0) {
                                return '<span class="row-details row-details-close"></span>';
                            }
                            else
                            {
                                return null;
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
                        "name": "MovimentoImportacaoId",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {

                            var origemControl = "";

                            if (row.movimentoImportacaoId === null) {
                                origemControl = origemManualControl;
                            }
                            else {
                                origemControl = origemImportacaoControl;
                            }

                            return origemControl;
                        }
                    },
                    {
                        "targets": 3,
                        "name": "DataMovimento",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {
                            return '<p align="center">' + row.dataMovimento + '</p>';
                        }
                    },
                    {
                        "targets": 4,
                        "name": "Historico",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {
                            return row.historico;
                        }
                    },
                    {
                        "targets": 5,
                        "name": "Conciliado",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {

                            var totalConciliado = parseFloat(row.totalConciliado.replace('(', '').replace(')', '').replace('.', '').replace(',', '.'));

                            if (totalConciliado === 0) {
                                return null;
                            }
                            
                            return '<p align="right">' + row.totalConciliado + '</p>';
                        }
                    },
                    {
                        "targets": 6,
                        "name": "NaoConciliado",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {
                                                        
                            var totalConciliado = parseFloat(row.totalConciliado.replace('(', '').replace(')', '').replace('.', '').replace(',', '.'));

                            if (totalConciliado === 0) {
                                return null;
                            }

                            var valorMovimento = parseFloat(row.valorMovimento.replace('(', '').replace(')', '').replace('.', '').replace(',', '.'));
                            var naoConciliado = valorMovimento - totalConciliado;
                            var naoConciliadoFormatted = formatToLocalMoneyWithOutCurrency(naoConciliado.toString().replace(',', '').replace('.', ','));

                            if (naoConciliado < 0 && row.tipoTransacao == "1") {
                                naoConciliadoFormatted = '(' + naoConciliadoFormatted + ')';
                            }

                            return '<p align="right">' + naoConciliadoFormatted + '</p>';
                        }
                    },
                    {
                        "targets": 7,
                        "name": "ValorMovimento",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {
                            return '<p align="right">' + row.valorMovimento + '</p>';
                        }
                    },
                    {
                        "targets": 8,
                        "name": "Saldo",
                        "sortable": false,
                        "searchable": true,
                        "render": function (data, type, row) {

                            var tipoTransacao = $("#cmbTipoTransacao").val();
                            var conciliacaoStatus = $("#cmbConciliacaoStatus").val();
                            var conciliacaoOrigem = $("#cmbConciliacaoOrigem").val();
                            var search = $("#dataTable_filter input").val();

                            if (tipoTransacao === '' && conciliacaoStatus === '' && conciliacaoOrigem  === '' && search === '') {
                                return '<p align="right">' + row.saldo + '</p>';
                            }
                            else {
                                return '<p align="right">' + '--------' + '</p>';
                            }                            
                        }
                    },
                    {
                        "targets": 9,
                        "name": "MovimentoId",
                        "sortable": false,
                        "searchable": false,
                        "render": function (data, type, row) {
                        
                            var actionControls = "<div class='actions'>";

                            actionControls += "<a href=\'/Movimento/Detail/" + row.movimentoId + "/" + row.tipoTransacao + "' class=\'btn default btn-xs grey\'><i class=\'fa fa-edit\'></i> Editar</a>";
                            actionControls += "<a onclick='MovimentoIndexScript.removeMovimento(\"" + row.movimentoId + "\",\"" + row.historico + "\");' href='#' class='btn default btn-xs grey'><i class='fa fa-trash-o'></i> Excluir</a>";

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
            var tableName = 'tableConciliacao' + aData.MovimentoId;

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
                        ContaSummaryScript.refresh();
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

        var tipoTransacao = $("#cmbTipoTransacao").val();
        var conciliacaoStatus = $("#cmbConciliacaoStatus").val();
        var conciliacaoOrigem = $("#cmbConciliacaoOrigem").val();

        var params = {
            'contaId': selectedContaId,
            'tipoConta': selectedTipoConta,
            'tipoTransacao': tipoTransacao,
            'conciliacaoStatus': conciliacaoStatus,
            'conciliacaoOrigem': conciliacaoOrigem,
        };

        var selectedPeriodoMes = getSelectedPeriodoMes();
        var selectedPeriodoAno = getSelectedPeriodoAno();

        if (selectedPeriodoMes != null && selectedPeriodoAno != null) {
            params.periodo = {
                "mes": selectedPeriodoMes,
                "ano": selectedPeriodoAno,
            };
        }

        var result = ApplicationScript.getAppWebApiUrl() + '/api/movimento/masterViewList?' + jQuery.param(params);

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
            handleInitTableMovimento();
        },
        removeMovimento: function (id, historico) {
            handleRemoveMovimento(id, historico);
        }
    };
}();