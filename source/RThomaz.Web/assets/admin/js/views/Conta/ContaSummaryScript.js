var ContaSummaryScript = function () {    

    var oTable = null;

    var selectedContaId = null;
    var selectedTipoConta = null;

    var dataSource = [];

    var handleInit = function () {

        oTable = $('#dataTableConta').dataTable({
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
            "serverSide": false,
            "paginate": false,
            "info": false,
            "filter": false,
            "scrollY": "180px",
            "ajax": function (data, callback, settings) {
                callback( { data: dataSource } );
            },
            "columnDefs": [
                {
                    "targets": 0,
                    "name": "LogoId",
                    "sortable": false,
                    "searchable": false,
                    "render": function (data, type, row) {

                        var srcLogo = '';
                        var nome = '';

                        switch (row.tipoConta) {
                            case 0:
                                srcLogo = "../assets/admin/img/bank.svg";
                                nome = row.conta.nome;
                                break;
                            case 1:
                                if (row.conta.banco.logoStorageObject == null)
                                    srcLogo = "../assets/admin/img/bank.svg";
                                else
                                    srcLogo = "/api/banco/logo/" + row.conta.banco.logoStorageObject;
                                nome = 'Banco: ' + row.conta.banco.nome + ' Ag: ' + row.conta.dadoBancario.numeroAgencia + ' C/C: ' + row.conta.dadoBancario.numeroConta;
                                break;
                            case 2:
                                if (row.conta.banco.logoStorageObject == null)
                                    srcLogo = "../assets/admin/img/bank.svg";
                                else
                                    srcLogo = "/api/banco/logo/" + row.conta.banco.logoStorageObject;
                                nome = 'Banco: ' + row.conta.banco.nome + ' Ag: ' + row.conta.dadoBancario.numeroAgencia + ' C/C: ' + row.conta.dadoBancario.numeroConta;
                                break;
                            case 3:
                                if (row.conta.bandeiraCartao.logoStorageObject == null)
                                    srcLogo = "../assets/admin/img/bank.svg";
                                else
                                    srcLogo = "/api/bandeiracartao/logo/" + row.conta.bandeiraCartao.logoStorageObject;
                                nome = 'Bandeira: ' + row.conta.bandeiraCartao.nome + ' Nome: ' + row.conta.nome;
                                break;
                            default:
                                break;
                        }

                        return "<img style='width:16px;height:11px' class='flag' src='" + srcLogo + "'/>" + nome;

                    }
                },
                {
                    "targets": 1,
                    "name": "Tipo",
                    "sortable": false,
                    "searchable": false,
                    "render": function (data, type, row) {
                        var tipoConta = null;
                        switch (row.tipoConta) {
                            case 0:
                                tipoConta = 'Esp&eacute;cie';
                                break;
                            case 1:
                                tipoConta = 'Conta corrente';
                                break;
                            case 2:
                                tipoConta = 'Conta poupan&ccedil;a';
                                break;
                            case 3:
                                tipoConta = 'Cart&atilde;o de cr&eacute;dito';
                                break;
                            default:

                        }
                        return '<p align="right">' + tipoConta + '</p>';
                    }
                },
                {
                    "targets": 2,
                    "name": "SaldoAtual",
                    "sortable": false,
                    "searchable": false,
                    "render": function (data, type, row) {
                        return '<p align="right">' + formatToLocalMoney2(row.saldoAtual) + '</p>';
                    }
                },
            ],
            "createdRow": function (row, data, index) {
                $(row).attr('id', data.contaId + ',' + data.tipoConta);
            },
            "drawCallback": function (settings) {
                if (dataSource.length > 0) {
                    $('#dataTableConta tbody tr:first').trigger('click');
                }                
            }
        });

        $('#dataTableConta tbody').on('click', 'tr', function () {

            selectedContaId = parseInt(this.id.split(',')[0]);
            selectedTipoConta = parseInt(this.id.split(',')[1]);

            $('#dataTableConta tbody tr').each(function () {
                $(this).removeClass("selected");
            });

            $(this).addClass('selected');

            var tableContaSelectChangedEvent = new CustomEvent("tableContaSelectChanged", {
                'detail': {
                    'selectedContaId': selectedContaId,
                    'selectedTipoConta': selectedTipoConta,
                },
            });

            document.body.dispatchEvent(tableContaSelectChangedEvent);

        });

        handleRefresh();
    };

    var handleRefresh = function () {
        $.ajax({
            type: "GET",
            url: ApplicationScript.getAppWebApiUrl() + '/api/conta/summaryViewList',
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
        }).success(function (data, textStatus, jqXHR) {
            dataSource = data;  
            oTable.fnReloadAjax();
        }).error(function (jqXHR, textStatus, errorThrown) {
            ApplicationScript.error(jqXHR, textStatus, errorThrown);
        });        
    };

    return {
        init: function () {
            handleInit();            
        },
        refresh: function () {
            handleRefresh();
        },
    };

}();