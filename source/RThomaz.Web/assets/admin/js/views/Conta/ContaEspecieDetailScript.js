var ContaEspecieDetailScript = function () {

    var contaId;

    var handleInit = function () {

        //Obtendo parametros
        contaId = parseInt(getUrlRouteParameter(3));

        //Setando os titulos

        var title = 'Conta esp\u00e9cie';
        var subTitle = 'gerencie suas contas esp\u00e9cie';

        setAppTitleSubTitle(title, subTitle);
        
        //Setando default menu
        $("#menuConta").addClass("active");

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
                    txtNome: {
                        required: true,
                        maxlength: 100,
                    },
                    txtSaldoInicialData: {
                        required: true,
                    },
                    txtSaldoInicialValor: {
                        required: true,
                    },
                    txtDescricao: {
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

        $('#txtDescricao').summernote({ height: 300 });

        $("#btnCancelar").on("click", function () {
            window.location.href = "/Conta/Index";
        });

        $('#txtSaldoInicialData').datepicker({
            rtl: Metronic.isRTL(),
            autoclose: true,
            'language': 'pt-BR',
            'format': 'dd/mm/yyyy',
        });
        $('body').removeClass("modal-open"); // fix bug when inline picker is used in modal                

        $("#txtSaldoInicialValor").maskMoney({
            prefix: 'R$', // Simbolo
            decimal: ',', // Separador do decimal
            precision: 2, // Precisão
            thousands: '.', // Separador para os milhares
            allowZero: true, // Permite que o digito 0 seja o primeiro caractere
            affixesStay: true, // definir se o símbolo vai ficar no campo após o usuário existe no campo. padrão: false
            allowNegative: true, // use esta configuração para impedir que os usuários entrando com valores negativos. padrão: false
        });

    }

    var handleLoadForm = function () {

        if (contaId > 0) {//Edit

            $.ajax({
                type: "GET",
                url: ApplicationScript.getAppWebApiUrl() + '/api/conta/especie/' + contaId,
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            }).success(function (data, textStatus, jqXHR) {

                $("#txtNome").val(data.nome);

                if (data.ativo) {
                    $('input[name=chkAtivo][value=true]').parent().addClass('checked');
                    $('input[name=chkAtivo][value=false]').parent().removeClass('checked');
                }
                else {
                    $('input[name=chkAtivo][value=true]').parent().removeClass('checked');
                    $('input[name=chkAtivo][value=false]').parent().addClass('checked');
                }

                var saldoInicialData = new Date(data.saldoInicial.data.replace('T', ' '));

                $("#txtSaldoInicialData").datepicker('update', saldoInicialData);

                $("#txtSaldoInicialValor").val(formatToLocalMoney2(data.saldoInicial.valor));
                $('#txtDescricao').code(data.descricao);

            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
        else {//New
            $('input[name=chkAtivo][value=true]').parent().addClass('checked');
            $('input[name=chkAtivo][value=false]').parent().removeClass('checked');
        }
    }

    var saveForm = function () {

        var nome = $("#txtNome").val();
        var ativo = $('input[name=chkAtivo][value=true]').parent().hasClass('checked');

        var saldoInicialData = $('#txtSaldoInicialData').datepicker("getDate");
        saldoInicialData = saldoInicialData.toJSON();
                
        var saldoInicialValor = $('#txtSaldoInicialValor').maskMoney('unmasked')[0].toString();

        var descricao = $('#txtDescricao').code();

        var data = {
            'contaId': contaId,
            'nome': nome,
            'ativo': ativo,
            'saldoInicial': {
                'data': saldoInicialData,
                'valor': saldoInicialValor,
            },
            'descricao': descricao,
        };

        if (contaId == 0) {
            $.ajax({
                type: "POST",
                url: ApplicationScript.getAppWebApiUrl() + '/api/conta/especie',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/Conta/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
        else {
            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/conta/especie',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/Conta/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
    }

    return {
        init: function () {

            handleInit();
            handleLoadForm();

        },
    };
}();