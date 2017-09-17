var LancamentoPagarReceberDetailScript = function () {

    var lancamentoId;
    var tipoTransacao;

    var handleInit = function () {

        //Obtendo parametros
        lancamentoId = parseInt(getUrlRouteParameter(3));
        tipoTransacao = parseInt(getUrlRouteParameter(4));

        $('#txtLancamentoId').val(lancamentoId);
        $('#txtTipoTransacao').val(tipoTransacao);

        //Setando default menu
        $("#menuLancamento").addClass("active");
        $("#menuLancamentoPagarReceber").addClass("active");

        //Setando os titulos

        var title = '';
        var subTitle = 'gerencie seus lan\u00e7amentos';

        if (tipoTransacao == 0) {
            title = "Detalhes do lan\u00e7amento do contas a receber";
            $('#lblPessoaTitle').text('Receber de');
        }
        else if (tipoTransacao == 1) {
            title = "Detalhes do lan\u00e7amento do contas a pagar";
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
                    txtDataPagamento: {
                        pagamentoValidator: true,
                        date: true,
                    },
                    txtValorPagamento: {
                        pagamentoValidator: true,
                    },
                    txtObservacao: {
                        maxlength: 4000,
                    },
                    txtTotalValorConciliado: {
                        totalConciliadoRule: true,
                    }
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

        $("#txtValorPagamento").maskMoney({
            prefix: 'R$', // Simbolo
            decimal: ',', // Separador do decimal
            precision: 2, // Precisão
            thousands: '.', // Separador para os milhares
            allowZero: true, // Permite que o digito 0 seja o primeiro caractere
            affixesStay: true, // definir se o símbolo vai ficar no campo após o usuário existe no campo. padrão: false
            allowNegative: true, // use esta configuração para impedir que os usuários entrando com valores negativos. padrão: false
        });

        $("#txtTotalValorConciliado").maskMoney({
            prefix: 'R$', // Simbolo
            decimal: ',', // Separador do decimal
            precision: 2, // Precisão
            thousands: '.', // Separador para os milhares
            allowZero: true, // Permite que o digito 0 seja o primeiro caractere
            affixesStay: true, // definir se o símbolo vai ficar no campo após o usuário existe no campo. padrão: false
            allowNegative: true, // use esta configuração para impedir que os usuários entrando com valores negativos. padrão: false
        });

        $("#txtValorRestante").maskMoney({
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

        $('#txtDataPagamento').datepicker({
            rtl: Metronic.isRTL(),
            autoclose: true,
            'language': 'pt-BR',
            'format': 'dd/mm/yyyy',
        });
        $('body').removeClass("modal-open"); // fix bug when inline picker is used in modal               

        $('#chkPagamento').on("change", chkPagamentoChange);
    }    

    var handleLoadForm = function () {

        PagamentoSharedScript.adicionaConciliacao(null, "0,00");

        if (lancamentoId > 0) {//Edit

            $.ajax({
                type: "GET",
                url: ApplicationScript.getAppWebApiUrl() + '/api/lancamentopagarreceber/' + lancamentoId,
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            }).success(function (data, textStatus, jqXHR) {

                $("#txtHistorico").val(data.historico);
                $("#txtNumero").val(data.numero);
                $("#txtValorVencimento").val(formatToLocalMoney2(data.valorVencimento));
                $("#txtDataVencimento").datepicker('update', new Date(data.dataVencimento.replace('T', ' ')));

                $('#chkPagamento').parent().removeClass('checked');

                consistePagamentoView(false);

                $("#txtValorPagamento").val(formatToLocalMoney2(data.valorPagamento));
                $("#txtDataPagamento").datepicker('update', new Date(data.dataPagamento.replace('T', ' ')));

                $('#txtObservacao').code(data.observacao);

                var cmbContaVal = data.conta.contaId + ',' + data.conta.tipoConta;
                $("#cmbConta").val(cmbContaVal);

                var cmbPessoaVal = null;
                if (data.pessoa != null) {
                    cmbPessoaVal = data.pessoa.pessoaId + ',' + data.pessoa.tipoPessoa;
                }

                $("#cmbPessoa").val(cmbPessoaVal);
                PessoaSharedScript.init('cmbPessoa', data.pessoa);

                ContaSharedScript.init('cmbConta');

                $.each(data.rateios, function (key, value) {
                    RateioSharedScript.adicionaRateio(value.planoConta, value.centroCusto, value.observacao, value.porcentagem)
                });

            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
        else {//New

            $('#chkPagamento').parent().removeClass('checked');
            consistePagamentoView(false);

            ContaSharedScript.init('cmbConta');
            PessoaSharedScript.init('cmbPessoa', null);

            RateioSharedScript.adicionaRateio(null, null, "", "100,00");            
        }
    }   

    var saveForm = function () {

        var historico = $("#txtHistorico").val();
        var numero = $("#txtNumero").val();

        var dataVencimento = $("#txtDataVencimento").datepicker("getDate");
        dataVencimento = dataVencimento.toJSON();

        var valorVencimento = $('#txtValorVencimento').maskMoney('unmasked')[0].toString();
        var estaPago = $('#chkPagamento').parent().hasClass('checked');

        var dataPagamento = $("#txtDataPagamento").datepicker("getDate");
        dataPagamento = dataPagamento.toJSON();

        var valorPagamento = $('#txtValorPagamento').maskMoney('unmasked')[0].toString();
        var observacao = $('#txtObservacao').code();
        var rateios = RateioSharedScript.getRateios();
        var conciliacoes = PagamentoSharedScript.getConciliacoes();

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
            'lancamentoId': lancamentoId,
            'tipoTransacao': tipoTransacao,
            'pessoaId': pessoaId,
            'tipoPessoa': tipoPessoa,
            'contaId': cmbContaData.id,
            'tipoConta': cmbContaData.tipoConta,
            'historico': historico,
            'numero': numero,
            'dataVencimento': dataVencimento,
            'valorVencimento': valorVencimento,
            'estaPago': estaPago,
            'dataPagamento': dataPagamento,
            'valorPagamento': valorPagamento,
            'rateios': rateios,
            'conciliacoes': conciliacoes,
            'observacao': observacao,
        };

        if (lancamentoId == 0) {
            $.ajax({
                type: "POST",
                url: ApplicationScript.getAppWebApiUrl() + '/api/lancamentopagarreceber',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/LancamentoPagarReceber/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
        else {
            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/lancamentopagarreceber',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/LancamentoPagarReceber/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
    }

    var chkPagamentoChange = function () {
        var pagamento = $('#chkPagamento').is(':checked');
        consistePagamentoView(pagamento);
    };

    var consistePagamentoView = function (value) {
        $('#txtValorPagamento').prop('disabled', !value);
        $('#txtDataPagamento').prop('disabled', !value);
        $('#btnAddConciliacao').prop('disabled', !value);
        $('#dataTableConciliacoes').prop('disabled', !value);
        $('.movimentoControl').prop('disabled', !value);
        $('.valorConciliacaoControl').prop('disabled', !value);
        $('.removeMovimento').prop('disabled', !value);
    };

    return {
        init: function () {

            handleInit();
            handleLoadForm();

        },
    };
}();

jQuery.validator.addMethod("pagamentoValidator", function (value, element) {

    var pagamento = $('#chkPagamento').parent().hasClass('checked');

    if (!pagamento) {
        return true;
    }

    if (value == '') {
        return false;
    }

    return true;

}, "Este campo \u00e9 requerido quando pagamento est\u00e1 selecionado.");