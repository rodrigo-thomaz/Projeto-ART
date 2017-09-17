var LancamentoPagoRecebidoDetailScript = function () {

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
        $("#menuLancamentoPagoRecebido").addClass("active");

        //Setando os titulos

        var title = '';
        var subTitle = 'gerencie seus lan\u00e7amentos';

        if (tipoTransacao == 0) {
            title = "Detalhes do lan\u00e7amento do contas recebidas";
            $('#lblPessoaTitle').text('Receber de');
        }
        else if (tipoTransacao == 1) {
            title = "Detalhes do lan\u00e7amento do contas pagas";
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
            window.location.href = "/LancamentoPagoRecebido/Index";
        });
    }    

    var formatContaNome = function (contaSelectView) {
        var contaNome = '';
        switch (contaSelectView.tipoConta) {
            case 0: //ContaEspecie
                contaNome = contaSelectView.nome;
                break;
            case 1: //ContaCorrente
            case 2: //ContaPoupanca
                contaNome = contaSelectView.banco.nome + ' Ag ' + contaSelectView.dadoBancario.numeroAgencia + ' Cc ' + contaSelectView.dadoBancario.numeroConta;
                break;
            case 3: //ContaCartaoCredito
                contaNome = contaSelectView.bandeiraCartao.Nome + ' ' + contaSelectView.nome;
                break;
            default:
                break;
        }
        return contaNome;
    };

    var handleLoadForm = function () {        

        $.ajax({
            type: "GET",
            url: ApplicationScript.getAppWebApiUrl() + '/api/lancamentopagorecebido/' + lancamentoId,
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
        }).success(function (data, textStatus, jqXHR) {

            var dataVencimento = $.datepicker.formatDate('dd/mm/yy', new Date(data.dataVencimento.replace('T', ' ')))
            var dataPagamento = $.datepicker.formatDate('dd/mm/yy', new Date(data.dataPagamento.replace('T', ' ')))

            $("#txtHistorico").val(data.historico);
            $("#txtNumero").val(data.numero);
            $("#txtValorVencimento").val(formatToLocalMoney2(data.valorVencimento));            
            $("#txtDataVencimento").val(dataVencimento);

            $("#txtValorPagamento").val(formatToLocalMoney2(data.valorPagamento));
            $("#txtDataPagamento").val(dataPagamento);

            $('#txtObservacao').code(data.observacao);            

            $("#txtContaNome").val(formatContaNome(data.conta));
            $("#txtPessoaNome").val(data.pessoa.nome);

            adicionaRateios(data.rateios);

            adicionaConciliacoes(data.conciliacoes);

        }).error(function (jqXHR, textStatus, errorThrown) {
            ApplicationScript.error(jqXHR, textStatus, errorThrown);
        });
    }

    var adicionaRateios = function (rateios) {

        $.each(rateios, function (index, value) {

            var observacao = value.observacao != null ? value.observacao : '';

            $('#dataTableRateios > tbody:last').append('<tr>'
            + '<td>' + value.planoConta.nome + '</td>'
            + '<td>' + value.centroCusto.nome + '</td>'
            + '<td>' + observacao + '</td>'
            + '<td style="width: 70px;">' + value.porcentagem + '%</td>'
            + '</tr>');

        });
    };

    var adicionaConciliacoes = function (conciliacoes) {

        $.each(conciliacoes, function (index, value) {

            var dataMovimento = $.datepicker.formatDate('dd/mm/yy', new Date(value.dataMovimento.replace('T', ' ')))
            var valorMovimento = formatToLocalMoney2(value.valorMovimento);
            var valorConciliado = formatToLocalMoney2(value.valorConciliado);

            $('#dataTableConciliacoes > tbody:last').append('<tr>'
            + '<td style="width: 70px;">' + dataMovimento + '</td>'
            + '<td>' + value.historico + '</td>'
            + '<td style="width: 70px;">' + valorMovimento + '</td>'
            + '<td style="width: 70px;">' + valorConciliado + '</td>'
            + '</tr>');

        });
    };

    var saveForm = function () {

        var observacao = $('#txtObservacao').code();

        var data = {
            'lancamentoId': lancamentoId,
            'tipoTransacao': tipoTransacao,
            'observacao': observacao,
        };

        $.ajax({
            type: "PUT",
            url: ApplicationScript.getAppWebApiUrl() + '/api/lancamentoPagoRecebido',
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            data: data,
        }).success(function (data, textStatus, jqXHR) {
            window.location.href = "/LancamentoPagoRecebido";
        }).error(function (jqXHR, textStatus, errorThrown) {
            ApplicationScript.error(jqXHR, textStatus, errorThrown);
        });
    }

    return {
        init: function () {

            handleInit();
            handleLoadForm();

        },
    };
}();