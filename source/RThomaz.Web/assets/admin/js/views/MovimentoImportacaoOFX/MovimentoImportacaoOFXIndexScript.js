var MovimentoImportacaoOFXIndexScript = function () {
    
    var oTable = null;
    var _importPreviewModel = null;

    var handleInit = function () {

        var title = 'Importar movimenta\u00e7\u00f5es financeiras';
        var subTitle = 'importe suas movimenta\u00e7\u00f5es financeiras';

        setAppTitleSubTitle(title, subTitle);

        $("#menuMovimento").addClass("active");

        $("#fileOfx").on("change", importPreview);

        $("#btnImportar").on("click", importMovimentacoes);

        $("#btnCancelar").on("click", function () {
            window.location.href = "/Movimento";
        });

        $('#dataTable').on('click', 'a.btnRemove', function () {

            var row = $(this).parents('tr');
            var dataRow = oTable.fnGetData(row);

            var message = "Deseja realmente excluir a movimenta&ccedil;&atilde;o \"" + dataRow.Historico + "\"?";

            bootbox.confirm(message, function (result) {
                if (result) {
                    oTable.fnDeleteRow(row);
                }
            });            
        });        
    };   

    var initDataTable = function () {

        var tipoCreditoControl = "<span class='badge badge-success'>C</span>";
        var tipoDebitoControl = "<span class='badge badge-important'>D</span>";

        oTable = $('#dataTable')            
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
                        [1, "asc"]
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
                "serverSide": false,
                data: _importPreviewModel.movimentacoes,
                "columnDefs": [
                    {
                        "targets": 0,
                        "name": "TipoTransacao",
                        "sortable": true,
                        "searchable": false,
                        "render": function (data, type, row) {

                            var actionControls = "";

                            if (row.tipoTransacao == 0) {
                                actionControls = tipoCreditoControl;
                            }
                            else if (row.tipoTransacao == 1) {
                                actionControls = tipoDebitoControl;
                            }

                            return actionControls;
                        }
                    },
                    {
                        "targets": 1,
                        "type": 'date-uk',
                        "name": "DataMovimento",
                        "sortable": true,
                        "searchable": true,
                        "render": function (data, type, row) {
                            return '<p align="center">' + row.dataMovimento + '</p>';
                        }
                    },
                    {
                        "targets": 2,
                        "name": "Historico",
                        "sortable": true,
                        "searchable": true,
                        "render": function (data, type, row) {
                            return row.historico;
                        }
                    },
                    {
                        "targets": 3,
                        "name": "ValorMovimento",
                        "sortable": true,
                        "searchable": true,
                        "render": function (data, type, row) {
                            return '<p align="right">' + row.valorMovimento + '</p>';
                        }
                    },
                    {
                        "targets": 4,
                        "name": "Existe",
                        "sortable": true,
                        "searchable": true,
                        "render": function (data, type, row) {
                            if (row.existe) {
                                return "Sim";
                            }
                            return "N&atilde;o";
                        }
                    },
                    {
                        "targets": 5,
                        "name": "",
                        "sortable": false,
                        "searchable": false,
                        "render": function (data, type, row) {

                            if (row.existe) {
                                return null;
                            }

                            var actionControls = "<div class='actions'>";

                            actionControls += "<a class='btnRemove' href='#' class='btn default btn-xs grey'><i class='fa fa-trash-o'></i> Excluir</a>";

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

    var importPreview = function () {

        var fileOfx = $("#fileOfx")[0].files[0];

        if (fileOfx) {

            var reader = new FileReader();

            reader.onload = function (e) {
                var contentType = 'OFX';
                var bytes = reader.result.split(',')[1];
                getImportPreview(contentType, bytes);
            };

            reader.readAsDataURL(fileOfx);
        }

    };

    var getImportPreview = function (contentType, buffer) {

        var model = {
            'contentType': contentType,
            'bufferBase64String': buffer,
        };

        $.ajax({
            type: "PUT",
            url: ApplicationScript.getAppWebApiUrl() + '/api/movimentoimportacaoofx/preview',
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            data: model,
            error: function (request, status, error) {
                alert("Erro GetImportPreview");
            },
        }).done(function (data) {

            _importPreviewModel = data;

            $("#txtBancoNome").val(data.bancoNome);
            $("#txtContaNome").val(data.contaNome);
            $("#txtDataInicio").val(data.dataInicio);
            $("#txtDataFim").val(data.dataFim);

            var tipoContaText = "";

            if (data.tipoConta === 1) {
                tipoContaText = "Conta corrente";
            }
            else if (data.tipoConta === 2) {
                tipoContaText = "Conta poupan&ccedil;a";
            }
            else if (data.tipoConta === 3) {
                tipoContaText = 'Cartao de credito';
            }

            $("#txtTipoConta").val(tipoContaText);

            initDataTable();
        });
    };

    var importMovimentacoes = function () {

        var movimentacoes = oTable.fnGetData();
        
        movimentacoes = jQuery.grep(movimentacoes, function (n, i) {
            return !n.Existe;
        });

        if (movimentacoes == null || movimentacoes.length == 0) {

            var message = "Nao existe nenhuma movimento para importacao!";           

            bootbox.alert({
                size: 'small',
                message: message,                
            });

            return;
        }

        _importPreviewModel.Movimentacoes = movimentacoes;
        
        $.ajax({
            type: "post",
            url: ApplicationScript.getAppWebApiUrl() + '/api/movimentoimportacaoofx/import',
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            data: _importPreviewModel,
            error: function (request, status, error) {
                alert("Erro Import");
            },
        }).done(function (data) {
            bootbox.alert({
                size: 'small',
                message: 'Arquivo OFX importado com sucesso!',
                callback: function () {
                    window.location.href = "/Movimento";
                }
            });            
        });

    };
        
    return {
        init: function () {

            if (!jQuery().dataTable) {
                return;
            }

            handleInit();           
        },
    };
}();

jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "date-uk-pre": function (a) {
        if (a == null || a == "") {
            return 0;
        }
        var ukDatea = a.substr(18, 10).split('/');
        return (ukDatea[2] + ukDatea[1] + ukDatea[0]) * 1;
    },

    "date-uk-asc": function (a, b) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },

    "date-uk-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    }
});