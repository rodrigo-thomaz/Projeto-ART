var LancamentoProgramadoDetailScript = function () {

    var programacaoId;
    var tipoTransacao;

    var handleInit = function () {

        //Obtendo parametros
        programacaoId = parseInt(getUrlRouteParameter(3));
        tipoTransacao = parseInt(getUrlRouteParameter(4));

        $('#txtLancamentoProgramadoId').val(programacaoId);
        $('#txtTipoTransacao').val(tipoTransacao);

        //Setando default menu
        $("#menuLancamentoProgramado").addClass("active");

        //Setando os titulos

        var title = '';
        var subTitle = 'gerencie seus lan\u00e7amentos programados';

        if (tipoTransacao == 0) {
            title = "Detalhes do lan\u00e7amento programado do contas a receber";
            $('#lblPessoaTitle').text('Receber de');
        }
        else if (tipoTransacao == 1) {
            title = "Detalhes do lan\u00e7amento programado do contas a pagar";
            $('#lblPessoaTitle').text('Pagar para');
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
                    cmbPessoa: {
                        required: false,
                    },
                    cmbConta: {
                        required: true,
                    },
                    cmbFrequencia: {
                        required: true,
                    },
                    txtDataInicial: {
                        required: true,
                        date: true,
                    },
                    txtDataFinal: {
                        required: true,
                        date: true,
                    },
                    txtHistorico: {
                        required: true,
                        maxlength: 250,
                    },
                    txtValorVencimento: {
                        required: true,
                    },
                    txtObservacao: {
                        maxlength: 4000,
                    },
                    DiaDaSemanaField: {
                        DiaDaSemanaRequired: true,
                    }
                },
                highlight: function (element) {
                    $(element).closest('.form-group').addClass('has-error');
                },
                unhighlight: function (element) {
                    if (element.name == 'chkDiaDaSemana') {
                        $("#DiaDaSemanaField").parent().children("span").remove()
                    }
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
            window.location.href = "/LancamentoProgramado/Index";
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

        $('#txtDataInicial').datepicker({
            rtl: Metronic.isRTL(),
            autoclose: true,
            'language': 'pt-BR',
            'format': 'dd/mm/yyyy',
        });
        $('body').removeClass("modal-open"); // fix bug when inline picker is used in modal        

        $('#txtDataFinal').datepicker({
            rtl: Metronic.isRTL(),
            autoclose: true,
            'language': 'pt-BR',
            'format': 'dd/mm/yyyy',
        });
        $('body').removeClass("modal-open"); // fix bug when inline picker is used in modal        

        $("#cmbFrequencia").on("change", function () {
            prepareControlsToFrequencia();
        });
    }

    var handleLoadForm = function () {

        if (programacaoId > 0) {//Edit

            $.ajax({
                type: "GET",
                url: ApplicationScript.getAppWebApiUrl() + '/api/lancamentoProgramado/' + programacaoId,
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            }).success(function (data, textStatus, jqXHR) {

                $("#txtHistorico").val(data.historico);
                $("#txtValorVencimento").val(formatToLocalMoney(data.valorVencimento));
                $("#txtDataInicial").datepicker('update', data.dataInicial);
                $("#txtDataFinal").datepicker('update', data.dataFinal);
                $("#chkHasDomingo").prop("checked", data.hasDomingo)
                $("#chkHasSegunda").prop("checked", data.hasSegunda)
                $("#chkHasTerca").prop("checked", data.hasTerca)
                $("#chkHasQuarta").prop("checked", data.hasQuarta)
                $("#chkHasQuinta").prop("checked", data.hasQuinta)
                $("#chkHasSexta").prop("checked", data.hasSexta)
                $("#chkHasSabado").prop("checked", data.hasSabado)
                $('#txtObservacao').code(data.observacao);

                var cmbContaVal = data.contaId + ',' + data.tipoConta;
                $("#cmbConta").val(cmbContaVal);

                var cmbPessoaVal = null;
                if (data.pessoaId != null && data.tipoPessoa != null) {
                    cmbPessoaVal = data.pessoaId + ',' + data.tipoPessoa;
                }
                $("#cmbPessoa").val(cmbPessoaVal);

                ContaSharedScript.init('cmbConta');
                PessoaSharedScript.init('cmbPessoa', data.pessoaSelectView);

                $.each(data.rateios, function (key, value) {
                    RateioSharedScript.adicionaRateio(value.planoConta, value.centroCusto, value.observacao, value.porcentagem)
                });

                $("#cmbFrequencia option[value=" + data.frequencia + "]").attr('selected', 'selected');
                $("#cmbFrequencia").select2();
                prepareControlsToFrequencia();

                $("#cmbDia option[value=" + data.dia + "]").attr('selected', 'selected');
                $("#cmbDia").select2();

            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });;
        }
        else {//New

            ContaSharedScript.init('cmbConta');
            PessoaSharedScript.init('cmbPessoa');

            RateioSharedScript.adicionaRateio(null, null, "", "100,00")

            $("#cmbFrequencia").select2();
            prepareControlsToFrequencia();
        }
    }

    var saveForm = function () {        

        var historico = $("#txtHistorico").val();
        var valorVencimento = $('#txtValorVencimento').maskMoney('unmasked')[0].toString();
        var dataInicial = $("#txtDataInicial").val();
        var dataFinal = $("#txtDataFinal").val();
        var rateios = RateioSharedScript.getRateios();
        var hasDomingo = $("#chkHasDomingo").prop("checked")
        var hasSegunda = $("#chkHasSegunda").prop("checked")
        var hasTerca = $("#chkHasTerca").prop("checked")
        var hasQuarta = $("#chkHasQuarta").prop("checked")
        var hasQuinta = $("#chkHasQuinta").prop("checked")
        var hasSexta = $("#chkHasSexta").prop("checked")
        var hasSabado = $("#chkHasSabado").prop("checked")
        var observacao = $('#txtObservacao').code();
        var frequencia = $("#cmbFrequencia").select2("val");
        var dia = $("#cmbDia").select2().val();

        //Conta
        var cmbContaData = $("#cmbConta").select2("data");

        //Pessoa
        var pessoaId = null;
        var tipoPessoa = null;

        var cmbPessoaVal = $("#cmbPessoa").select2('data');
        
        if (cmbPessoaVal != null) {
            pessoaId = cmbPessoaVal.id;
            tipoPessoa = cmbPessoaVal.attr;
        }        
        
        var data = {
            'programacaoId': programacaoId,
            'tipoTransacao': tipoTransacao,
            'pessoaId': pessoaId,
            'tipoPessoa': tipoPessoa,
            'contaId': cmbContaData.id,
            'tipoConta': cmbContaData.tipoConta,
            'historico': historico,
            'valorVencimento': valorVencimento,
            'dataInicial': dataInicial,
            'dataFinal': dataFinal,
            'rateios': rateios,
            'frequencia': frequencia,
            'dia': dia,
            'hasDomingo': hasDomingo,
            'hasSegunda': hasSegunda,
            'hasTerca': hasTerca,
            'hasQuarta': hasQuarta,
            'hasQuinta': hasQuinta,
            'hasSexta': hasSexta,
            'hasSabado': hasSabado,
            'observacao': observacao,
        };

        if (programacaoId == 0) {
            $.ajax({
                type: "POST",
                url: ApplicationScript.getAppWebApiUrl() + '/api/lancamentoProgramado',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/LancamentoProgramado/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
        else {
            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/lancamentoProgramado',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/LancamentoProgramado/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
    }

    var prepareControlsToFrequencia = function () {

        var frequencia = $("#cmbFrequencia").val();

        if (frequencia == "0") {
            $("#divDia").hide();
            $("#divDiaDaSemana").hide();
            $("#chkHasDomingo").removeClass("required");
        }
        else if (frequencia == "1") {
            $("#divDia").hide();
            $("#divDiaDaSemana").show();
            $("#chkHasDomingo").addClass("required");
        }
        else {
            $("#divDia").show();
            $("#divDiaDaSemana").hide();
            $("#chkHasDomingo").removeClass("required");
        }
    }

    var handleInitcmbDia = function () {
        var listOfDias = getListOfDias();
        var options = $("#cmbDia");
        $.each(listOfDias, function (index, item) {
            options.append($("<option />").val(item.id).text(item.text));
        });
    };

    var handleInitcmbFrequencia = function () {
        var listOfFrequencia = getListOfFrequencia();
        var options = $("#cmbFrequencia");
        $.each(listOfFrequencia, function (index, item) {
            options.append($("<option />").val(item.id).text(item.text));
        });
    };

    return {
        init: function () {

            handleInit();
            handleLoadForm();
            handleInitcmbDia();
            handleInitcmbFrequencia();

        },
    };
}();

jQuery.validator.addMethod("DiaDaSemanaRequired", function (value, element) {
    var frequencia = $("#cmbFrequencia").val();
    var chkDiaDaSemanaResult = false;
    if (frequencia == "1") {        
        $(".chkDiaDaSemana").each(function (index) {
            if ($(this).prop("checked") == true) {
                chkDiaDaSemanaResult = true;
            }
        });        
    }
    else {
        chkDiaDaSemanaResult = true;
    }
    return chkDiaDaSemanaResult;
}, "Voc\u00ea deve selecionar pelo menos um dia da semana.");