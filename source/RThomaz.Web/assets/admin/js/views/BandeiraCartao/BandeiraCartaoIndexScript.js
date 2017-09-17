var BandeiraCartaoIndexScript = function () {

    var oTable;
    
    var handleInit = function () {

        var title = 'Bandeiras de cart\u00f5es';
        var subTitle = 'gerencie suas bandeiras de cart\u00f5es';

        setAppTitleSubTitle(title, subTitle);

        $("#menuBandeiraCartao").addClass("active");

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
        var tableColumnToggler = $('#bandeiraCartao_column_toggler');
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
                "url": ApplicationScript.getAppWebApiUrl() + "/api/bandeiracartao/masterViewList?ativo=" + getAtivo(),
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
               "name": "LogoStorageObject",
               "sortable": false,
               "searchable": false,
               "render": function (data, type, row) {
                    var srcLogoBandeiraCartao = '';

                    if (row.logoStorageObject) {
                        srcLogoBandeiraCartao = "/api/bandeiracartao/logo/" + row.logoStorageObject;
                    }
                    else {
                        srcLogoBandeiraCartao = "../assets/admin/img/bank.svg";
                    }

                    return "<img style='width:16px;height:11px' class='flag' src='" + srcLogoBandeiraCartao + "'/>";
               },               
           },
           {
               "targets": 1,
               "name": "Nome",
               "sortable": true,
               "searchable": true,
               "render": function (data, type, row) {
                   return row.nome;
               },
           },
           {
               "targets": 2,
               "name": "Ativo",
               "sortable": true,
               "searchable": true,
               "render": function (data, type, row) {
                   if (row.ativo) {
                       return "<i class='fa fa-check-square-o'></i>";
                   } else {
                       return "<i class='fa fa-square-o'></i>";
                   }
               },
           },
           {
               "targets": 3,
               "name": "BandeiraCartaoId",
               "sortable": false,
               "searchable": false,
               "render": function (data, type, row) {
                    var actionControls = "<div class='actions'>";
                    actionControls += "<a href=\'/BandeiraCartao/Detail/" + row.bandeiraCartaoId + "\' class=\'btn default btn-xs grey\'><i class=\'fa fa-edit\'></i> Editar</a>";
                    actionControls += "<a onclick='BandeiraCartaoIndexScript.removeBandeiraCartao(\"" + row.bandeiraCartaoId + "\",\"" + row.nome + "\");' href='#' class='btn default btn-xs grey'><i class='fa fa-trash-o'></i> Excluir</a>";
                    actionControls += "</div>";

                    return actionControls;
               },
           },
            ],
            "initComplete": function (settings, json) {
                jQuery('#dataTable_wrapper .dataTables_filter input').addClass("form-control input-medium input-inline"); // modify table search input
                jQuery('#dataTable_wrapper .dataTables_length select').addClass("form-control input-small"); // modify table per page dropdown
                jQuery('#dataTable_wrapper .dataTables_length select').select2(); // initialize select2 dropdown
            }
        });

        var oTableColReorder = new $.fn.dataTable.ColReorder(oTable);
    }
    
    var handleRemoveBandeiraCartao = function (id, nome) {

        var message = "Deseja realmente excluir a bandeira de cart&atilde;o de cr&eacute;dito '" + nome + "'?";

        bootbox.confirm(message, function (result) {
            if (result) {
                $.ajax({
                    type: "DELETE",
                    url: ApplicationScript.getAppWebApiUrl() + '/api/bandeiracartao/' + id,
                    headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                    content: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                }).success(function (data, textStatus, jqXHR) {
                    if (data) {
                        refreshDataTable();
                    }
                    else {
                        bootbox.alert({ message: "A bandeira de cart&atilde;o de cr&eacute;dito '" + nome + "' n&atilde;o pode ser exclu&iacute;da pois j&aacute; est&aacute; sendo utilizada pelo sistema." });
                    }
                }).error(function (jqXHR, textStatus, errorThrown) {
                    ApplicationScript.error(jqXHR, textStatus, errorThrown);
                });
            }
        });
    };

    var refreshDataTable = function () {
        var oSettings = oTable.fnSettings();
        oSettings.ajax.url = '/api/bandeiracartao/masterViewList?ativo=' + getAtivo();
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
        removeBandeiraCartao: function (id, nome) {
            handleRemoveBandeiraCartao(id, nome);
        }
    };
}();