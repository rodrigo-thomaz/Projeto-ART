var MovimentoDetailScript = function () {

    var movimentoId;
    var tipoTransacao;

    var handleInit = function () {

        //Obtendo parametros
        movimentoId = parseInt(getUrlRouteParameter(3));
        tipoTransacao = parseInt(getUrlRouteParameter(4));

        //Setando default menu
        $("#menuMovimento").addClass("active");

        //Setando os titulos

        var title = 'Movimenta\u00e7\u00e3o financeira';
        var subTitle = '';

        if (tipoTransacao == 0) {
            title = "Detalhes da movimenta\u00e7\u00e3o de cr\u00e9dito";
            $('#divCaption').html('<i class="fa fa-reorder"></i> Cr\u00e9dito');
        }
        else if (tipoTransacao == 1) {
            title = "Detalhes da movimenta\u00e7\u00e3o de d\u00e9bito";
            $('#divCaption').html('<i class="fa fa-reorder"></i> D\u00e9bito');
        }

        setAppTitleSubTitle(title, subTitle);        


        $("#formDetail").submit(function (e) {
            e.preventDefault(); //prevent default form submit            
        });
        
        // Validation
        if ($.validator) {
            $("#formDetail").validate({
                errorElement: 'span',
                errorClass: 'help-block',
                focusInvalid: false,
                //ignore: "",  // validate all fields including form hidden input
                rules: {
                    cmbConta: {
                        required: true,
                    },
                    txtHistorico: {
                        required: true,
                        maxlength: 250,
                    },
                    txtDataMovimento: {
                        required: true,
                        date: true,
                    },
                    txtValorMovimento: {
                        required: true,
                    },                    
                    txtObservacao: {
                        maxlength: 4000,
                    },
                },
                highlight: function (element) {
                    $(element).closest('.form-group').addClass('has-error');
                },
                unhighlight: function (element) {
                    $(element).closest('.form-group').removeClass('has-error');
                },
                success: function (label) {
                    label.closest('.form-group').removeClass('has-error');
                },
                errorPlacement: function (error, element) { // render error placement for each input type
                    if (element.parent(".input-group").size() > 0) {
                        error.insertAfter(element.parent(".input-group"));
                    } else if (element.attr("data-error-container")) {
                        error.appendTo(element.attr("data-error-container"));
                    } else if (element.parents('.radio-list').size() > 0) {
                        error.appendTo(element.parents('.radio-list').attr("data-error-container"));
                    } else if (element.parents('.radio-inline').size() > 0) {
                        error.appendTo(element.parents('.radio-inline').attr("data-error-container"));
                    } else if (element.parents('.checkbox-list').size() > 0) {
                        error.appendTo(element.parents('.checkbox-list').attr("data-error-container"));
                    } else if (element.parents('.checkbox-inline').size() > 0) {
                        error.appendTo(element.parents('.checkbox-inline').attr("data-error-container"));
                    } else {
                        error.insertAfter(element); // for other inputs, just perform default behavior
                    }
                },
                invalidHandler: function (form, validator) {
                    formInvalidDefaultHandler(validator, "custom-validate-error");
                    Metronic.scrollTo($("#custom-validate-error"), 1);
                    $("#divForm").effect("shake", { distance: 6, times: 2 }, 35);
                },
                submitHandler: function (form) {
                    saveForm();
                }
            });
        }
        
        $('#txtObservacao').summernote({ height: 300 });

        $("#btnCancelar").on("click", function () {
            window.location.href = "/Movimento";
        });        

        $("#txtValorMovimento").maskMoney({
            prefix: 'R$', // Simbolo
            decimal: ',', // Separador do decimal
            precision: 2, // Precisão
            thousands: '.', // Separador para os milhares
            allowZero: true, // Permite que o digito 0 seja o primeiro caractere
            affixesStay: true, // definir se o símbolo vai ficar no campo após o usuário existe no campo. padrão: false
            allowNegative: true, // use esta configuração para impedir que os usuários entrando com valores negativos. padrão: false
        });

        $('#txtDataMovimento').datepicker({
            rtl: Metronic.isRTL(),
            autoclose: true,
            'language': 'pt-BR',
            'format': 'dd/mm/yyyy',
        });
        $('body').removeClass("modal-open"); // fix bug when inline picker is used in modal        
    }

    var handleLoadForm = function () {

        if (movimentoId > 0) {//Edit

            $.ajax({
                type: "GET",
                url: ApplicationScript.getAppWebApiUrl() + '/api/movimento/' + movimentoId,
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            }).success(function (data, textStatus, jqXHR) {

                if (data.movimentoImportacao != null) {

                    $('#groupImportadoEm').show();

                    var applicationUrl = $("#txtApplicationUrl").val();
                    $('#txtImportadoEm').data("movimentoimportacaoid", data.movimentoImportacao.movimentoImportacaoId);
                    $('#txtImportadoEm').text(data.movimentoImportacao.importadoEm);
                    $("#txtImportadoEm").attr("href", applicationUrl + "/MovimentoImportacao/Index/" + data.movimentoImportacao.movimentoImportacaoId);
                }
                else {
                    $('#groupImportadoEm').hide();
                }

                if (data.estaConciliado || data.movimentoImportacao != null) {
                    $('#txtHistorico').prop('disabled', true);
                    $('#txtValorMovimento').prop('disabled', true);
                    $('#txtDataMovimento').prop('disabled', true);
                    $('#cmbConta').prop('disabled', true);
                }

                $('#txtEstaConciliado').val(data.estaConciliado);

                $("#txtHistorico").val(data.historico);
                $("#txtValorMovimento").val(formatToLocalMoney(data.valorMovimento));
                $("#txtDataMovimento").datepicker('update', data.dataMovimento);
                $('#txtObservacao').code(data.observacao);

                var cmbContaVal = data.contaId + ',' + data.tipoConta;
                $("#cmbConta").val(cmbContaVal);

                ContaSharedScript.init('cmbConta');

            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });

            var lancamentos = ConciliacaoSharedScript.getLancamentos(movimentoId, tipoTransacao);

            $.each(lancamentos, function (index, value) {

                var valorConciliado = value.valorConciliado.replace('-', '').replace('(', '').replace(')', '');
                var valorPagamento = value.valorPagamento.replace('-', '').replace('(', '').replace(')', '');

                var sOut = '';
                sOut += '<tr>';
                sOut += '<td>' + value.dataPagamento + '</td>';
                sOut += '<td>' + value.historico + '</td>';
                sOut += '<td>' + value.pessoaNome + '</td>';
                sOut += '<td style="text-align: right; width:130px;">' + valorConciliado + '</td>';
                sOut += '<td style="text-align: right; width:130px;">' + valorPagamento + '</td>';
                sOut += '<td><a href="/LancamentoPagoRecebido/Detail/' + value.lancamentoId + '/' + value.tipoTransacao + '" class="btn default btn-xs grey"><i class="fa fa-edit"></i> Detalhes</a></th>';
                sOut += '</tr>';

                $('#dataTableConciliacoes tr:last').after(sOut);
            });
        }
        else {//New         
            ContaSharedScript.init('cmbConta');
        }
    }

    var saveForm = function () {

        var estaConciliado = $('#txtEstaConciliado').val() == 'true' ? true : false;

        var cmbContaVal = $("#cmbConta").select2("val");

        var contaId = cmbContaVal.split(',')[0];
        var tipoConta = cmbContaVal.split(',')[1];
        var historico = $("#txtHistorico").val();

        var dataMovimento = $('#txtDataMovimento').datepicker("getDate");
        dataMovimento = dataMovimento.toJSON();

        var valorMovimento = parseFloat($('#txtValorMovimento').maskMoney('unmasked')[0].toString());

        if (tipoTransacao == 1) {
            valorMovimento = valorMovimento * (-1);
        }

        var observacao = $('#txtObservacao').code();

        var movimentoImportacaoId = null;
        if ($('#txtImportadoEm').data().movimentoimportacaoid !== undefined) {
            movimentoImportacaoId = $('#txtImportadoEm').data().movimentoimportacaoid;
        }        

        if (movimentoId == 0 && !estaConciliado && movimentoImportacaoId == null) {
            var data = {
                'movimentoId': movimentoId,
                'tipoTransacao': tipoTransacao,
                'estaConciliado': estaConciliado,
                'movimentoImportacao': {
                    'movimentoImportacaoId': movimentoImportacaoId
                },
                'contaId': contaId,
                'tipoConta': tipoConta,
                'historico': historico,
                'dataMovimento': dataMovimento,
                'valorMovimento': valorMovimento,
                'observacao': observacao,
            };
            $.ajax({
                type: "POST",
                url: ApplicationScript.getAppWebApiUrl() + '/api/movimento',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/Movimento/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
        else if (movimentoId > 0 && !estaConciliado && movimentoImportacaoId == null)
        {
            var data = {
                'movimentoId': movimentoId,
                'tipoTransacao': tipoTransacao,
                'contaId': contaId,
                'tipoConta': tipoConta,
                'historico': historico,
                'dataMovimento': dataMovimento,
                'valorMovimento': valorMovimento,
                'observacao': observacao,
            };
            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/movimento/movimentomanual',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/Movimento/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
        else if (movimentoId > 0 && !estaConciliado && movimentoImportacaoId != null) {
            var data = {
                'movimentoId': movimentoId,
                'tipoTransacao': tipoTransacao,
                'movimentoImportacaoId': movimentoImportacaoId,
                'observacao': observacao,
            };
            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/movimento/movimentoimportado',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/Movimento/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
        else if (movimentoId > 0 && estaConciliado) {
            var data = {
                'movimentoId': movimentoId,
                'tipoTransacao': tipoTransacao,
                'observacao': observacao,
            };
            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/movimento/movimentoconciliado',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/Movimento/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
        else {
            //erro
        }

        $.ajax({
            type: "POST",
            url: '/Movimento/Save/',
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: {
                'model.MovimentoId': movimentoId,
                'model.TipoTransacao': tipoTransacao,
                'model.EstaConciliado': estaConciliado,
                'model.MovimentoImportacao': {
                    'MovimentoImportacaoId': movimentoImportacaoId
                },
                'model.ContaId': contaId,
                'model.TipoConta': tipoConta,
                'model.Historico': historico,
                'model.DataMovimento': dataMovimento,
                'model.ValorMovimento': valorMovimento,
                'model.Observacao': observacao,
            },
            success: function (data) {
                window.location.href = "/Movimento/Index";
            },
            error: function (request, status, error) {
                alert("Erro /Movimento/Save/");
            }
        });
    }

    return {
        init: function () {

            handleInit();
            handleLoadForm();

        },
    };
}();