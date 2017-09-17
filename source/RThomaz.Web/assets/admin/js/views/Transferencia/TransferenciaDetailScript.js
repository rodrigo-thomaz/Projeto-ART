var TransferenciaDetailScript = function () {

    var transferenciaId;

    var handleInit = function () {

        //Obtendo parametros
        transferenciaId = parseInt(getUrlRouteParameter(3));

        //Setando default menu
        $("#menuLancamento").addClass("active");

        //Setando os titulos

        var title = 'Transfer\u00eancia';
        var subTitle = 'gerencie sua transfer\u00eancia';
        
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
                    cmbContaOrigem: {
                        required: true,
                    },
                    cmbContaDestino: {
                        required: true,
                    },
                    txtHistorico: {
                        required: true,
                        maxlength: 250,
                    },
                    txtNumero: {
                        required: false,
                    },
                    txtDataVencimento: {
                        required: true,
                        date: true,
                    },
                    txtValorVencimento: {
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
            window.location.href = "/LancamentoPagarReceber";
        });

        $("#txtValorVencimento").maskMoney({
            prefix: 'R$', // Simbolo
            decimal: ',', // Separador do decimal
            precision: 2, // Precisão
            thousands: '.', // Separador para os milhares
            allowZero: true, // Permite que o digito 0 seja o primeiro caractere
            affixesStay: true, // definir se o símbolo vai ficar no campo após o usuário existe no campo. padrão: false
            allowNegative: true, // use esta configuração para impedir que os usuários entrando com valores negativos. padrão: false
        });

        $('#txtDataVencimento').datepicker({
            rtl: Metronic.isRTL(),
            autoclose: true,
            'language': 'pt-BR',
            'format': 'dd/mm/yyyy',
        });
        $('body').removeClass("modal-open"); // fix bug when inline picker is used in modal               

    }

    var handleLoadForm = function () {

        if (transferenciaId > 0) {//Edit

            $.ajax({
                type: "GET",
                url: ApplicationScript.getAppWebApiUrl() + '/api/transferencia/' + transferenciaId,
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            }).success(function (data, textStatus, jqXHR) {

                var cmbContaOrigemVal = data.contaOrigem.contaId + ',' + data.contaOrigem.tipoConta;
                var cmbContaDestinoVal = data.contaDestino.contaId + ',' + data.contaDestino.tipoConta;

                $("#txtHistorico").val(data.historico);
                $("#txtNumero").val(data.numero);
                $("#txtValorVencimento").val(formatToLocalMoney(data.valorVencimento));
                $("#txtDataVencimento").datepicker('update', data.dataVencimento);
                $('#txtObservacao').code(data.observacao);
                $("#cmbContaOrigem").val(cmbContaOrigemVal);
                $("#cmbContaDestino").val(cmbContaDestinoVal);

                ContaSharedScript.init('cmbContaOrigem');
                ContaSharedScript.init('cmbContaDestino');

            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });            
        }
        else {//New

            ContaSharedScript.init('cmbContaOrigem');
            ContaSharedScript.init('cmbContaDestino');
        }
    }

    var saveForm = function () {        

        var historico = $("#txtHistorico").val();
        var numero = $("#txtNumero").val();        
        var valorVencimento = $('#txtValorVencimento').maskMoney('unmasked')[0];
        var observacao = $('#txtObservacao').code();

        var dataVencimento = $("#txtDataVencimento").datepicker("getDate");
        dataVencimento = dataVencimento.toJSON();

        var cmbContaOrigemData = $("#cmbContaOrigem").select2("data");
        var cmbContaDestinoData = $("#cmbContaDestino").select2("data");

        if (transferenciaId == 0) {

            var data = {
                'contaOrigemId': cmbContaOrigemData.id,
                'tipoContaOrigem': cmbContaOrigemData.tipoConta,
                'contaDestinoId': cmbContaDestinoData.id,
                'tipoContaDestino': cmbContaDestinoData.tipoConta,
                'historico': historico,
                'numero': numero,
                'dataVencimento': dataVencimento,
                'valorVencimento': valorVencimento,
                'observacao': observacao,
            };

            $.ajax({
                type: "POST",
                url: ApplicationScript.getAppWebApiUrl() + '/api/transferencia',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/LancamentoPagarReceber";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
        else {

            var data = {
                'transferenciaId': transferenciaId,
                'contaOrigemId': cmbContaOrigemData.id,
                'tipoContaOrigem': cmbContaOrigemData.tipoConta,
                'contaDestinoId': cmbContaDestinoData.id,
                'tipoContaDestino': cmbContaDestinoData.tipoConta,
                'historico': historico,
                'numero': numero,
                'dataVencimento': dataVencimento,
                'valorVencimento': valorVencimento,
                'observacao': observacao,
            };

            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/transferencia',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/LancamentoPagarReceber";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
    }

    var handleRemoveTransferencia = function (id, historico) {

        var message = "Deseja realmente excluir a transfer&ecircncia \"" + historico + "\"?";

        bootbox.confirm(message, function (result) {
            if (result) {
                $.ajax({
                    type: "DELETE",
                    url: ApplicationScript.getAppWebApiUrl() + '/api/transferencia/' + id,
                    headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                    content: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                }).success(function (data, textStatus, jqXHR) {
                    if (data) {
                        oTableConta.fnDraw();
                    }
                    else {
                        bootbox.alert({ message: "A transfer&ecircncia '" + nome + "' n&atilde;o pode ser exclu&iacute;da pois j&aacute; est&aacute; sendo utilizada pelo sistema." });
                    }
                }).error(function (jqXHR, textStatus, errorThrown) {
                    ApplicationScript.error(jqXHR, textStatus, errorThrown);
                });
            }
        });
    };

    return {
        init: function () {

            handleInit();
            handleLoadForm();

        },
        removeTransferencia: function (id, historico) {
            handleRemoveTransferencia(id, historico);
        }
    };
}();