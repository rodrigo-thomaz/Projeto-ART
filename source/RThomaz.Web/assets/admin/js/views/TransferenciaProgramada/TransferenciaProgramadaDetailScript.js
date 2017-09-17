var TransferenciaProgramadaDetailScript = function () {

    var programacaoId;

    var handleInit = function () {

        //Obtendo parametros
        programacaoId = parseInt(getUrlRouteParameter(3));

        //Setando default menu
        $("#menuLancamentoProgramado").addClass("active");

        //Setando os titulos

        var title = 'Detalhes da transfer\u00eancia programada';
        var subTitle = 'gerencie sua transfer\u00eancia programada';

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
                url: ApplicationScript.getAppWebApiUrl() + '/api/transferenciaprogramada/' + programacaoId,
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            }).success(function (data, textStatus, jqXHR) {

                var dataInicial = new Date(data.dataInicial.replace('T', ' '));
                var dataFinal = new Date(data.dataFinal.replace('T', ' '));

                $("#txtHistorico").val(data.historico);
                $("#txtValorVencimento").val(formatToLocalMoney2(data.valorVencimento));
                $("#txtDataInicial").datepicker('update', dataInicial);
                $("#txtDataFinal").datepicker('update', dataFinal);
                $("#chkHasDomingo").prop("checked", data.hasDomingo)
                $("#chkHasSegunda").prop("checked", data.hasSegunda)
                $("#chkHasTerca").prop("checked", data.hasTerca)
                $("#chkHasQuarta").prop("checked", data.hasQuarta)
                $("#chkHasQuinta").prop("checked", data.hasQuinta)
                $("#chkHasSexta").prop("checked", data.hasSexta)
                $("#chkHasSabado").prop("checked", data.hasSabado)
                $('#txtObservacao').code(data.observacao);

                var cmbContaOrigemVal = data.contaOrigemId + ',' + data.tipoContaOrigem;
                var cmbContaDestinoVal = data.contaDestinoId + ',' + data.tipoContaDestino;

                $("#cmbContaOrigem").val(cmbContaOrigemVal);
                $("#cmbContaDestino").val(cmbContaDestinoVal);

                ContaSharedScript.init('cmbContaOrigem');
                ContaSharedScript.init('cmbContaDestino');

                $("#cmbFrequencia option[value=" + data.frequencia + "]").attr('selected', 'selected');
                $("#cmbFrequencia").select2();
                prepareControlsToFrequencia();

                $("#cmbDia option[value=" + data.dia + "]").attr('selected', 'selected');
                $("#cmbDia").select2();

            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
        else {//New

            ContaSharedScript.init('cmbContaOrigem');
            ContaSharedScript.init('cmbContaDestino');

            $("#cmbFrequencia").select2();
            prepareControlsToFrequencia();
        }
    }

    var saveForm = function () {

        var historico = $("#txtHistorico").val();
        var valorVencimento = $('#txtValorVencimento').maskMoney('unmasked')[0].toString();
        var dataInicial = $("#txtDataInicial").datepicker("getDate").toJSON();;
        var dataFinal = $("#txtDataFinal").datepicker("getDate").toJSON();;
        var hasDomingo = $("#chkHasDomingo").prop("checked")
        var hasSegunda = $("#chkHasSegunda").prop("checked")
        var hasTerca = $("#chkHasTerca").prop("checked")
        var hasQuarta = $("#chkHasQuarta").prop("checked")
        var hasQuinta = $("#chkHasQuinta").prop("checked")
        var hasSexta = $("#chkHasSexta").prop("checked")
        var hasSabado = $("#chkHasSabado").prop("checked")
        var observacao = $('#txtObservacao').code();
        var contaOrigemId = $("#cmbContaOrigem").select2("val");
        var contaDestinoId = $("#cmbContaDestino").select2("val");
        var frequencia = $("#cmbFrequencia").select2("val");
        var dia = $("#cmbDia").select2().val();

        var cmbContaOrigemData = $("#cmbContaOrigem").select2("data");
        var cmbContaDestinoData = $("#cmbContaDestino").select2("data");

        var data = {
            'programacaoId': programacaoId,
            'contaOrigemId': cmbContaOrigemData.id,
            'tipoContaOrigem': cmbContaOrigemData.tipoConta,
            'contaDestinoId': cmbContaDestinoData.id,
            'tipoContaDestino': cmbContaDestinoData.tipoConta,
            'historico': historico,
            'valorVencimento': valorVencimento,
            'dataInicial': dataInicial,
            'dataFinal': dataFinal,
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
                url: ApplicationScript.getAppWebApiUrl() + '/api/transferenciaprogramada',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/LancamentoProgramado/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });;
        }
        else {
            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/transferenciaprogramada',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/LancamentoProgramado/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });;
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