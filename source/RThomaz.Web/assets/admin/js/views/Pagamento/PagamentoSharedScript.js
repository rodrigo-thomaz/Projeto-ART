var PagamentoSharedScript = function () {

    var handleInit = function () {

        $('#btnAddConciliacao').click(function (e) {
            e.preventDefault();
            handleAdicionaConciliacao(null, "0,00")
        });

        $('#txtValorPagamento').on("change", function () {
            updateTotalValorConciliado();
        });
    }

    var handleGetConciliacoes = function () {

        var conciliacoesResult = [];

        var pagamento = $('#chkPagamento').is(':checked');

        if (pagamento) {
            $("#dataTableConciliacoes tbody tr").each(function (index) {
                conciliacoesResult.push({
                    "MovimentoId": $('.movimentoControl', this).select2("val"),
                    "ValorConciliado": $('.valorConciliacaoControl', this).maskMoney('unmasked')[0].toString(),
                });
            });
        }        

        return conciliacoesResult;

    }

    var pagamentoCounterHelper = 0;

    var handleAdicionaConciliacao = function (movimentoId, valor) {

        pagamentoCounterHelper++;

        var tipoTransacao = $("#txtTipoTransacao").val();

        var movimentoControlName = 'cmbMovimento' + pagamentoCounterHelper;
        var valorDisponivelControlName = 'txtValorDisponivel' + pagamentoCounterHelper;
        var valorControlName = 'txtValor' + pagamentoCounterHelper;
        
        $('#dataTableConciliacoes > tbody:last').append('<tr>'
            + '<td><input id="' + movimentoControlName + '" name="' + movimentoControlName + '" type="text" class="movimentoControl" style="width:500px;" value="' + movimentoId + '"</td>'
            + '<td style="width: 70px;"><input disabled id="' + valorDisponivelControlName + '" name="' + valorDisponivelControlName + '" type="text" class="valorDisponivelConciliacaoControl" style="width:70px;text-align:right;" value="' + valor + '"</td>'
            + '<td style="width: 70px;"><input id="' + valorControlName + '" name="' + valorControlName + '" type="text" class="valorConciliacaoControl" style="width:70px;text-align:right;" value="' + valor + '"</td>'
            + '<td style="width: 50px;"><a href="#" class="btn default btn-xs grey removeMovimento"><i class="fa fa-trash-o"></i> Excluir</a></td>'
            + '</tr>');

        $("#" + movimentoControlName).rules("add", {
            required: true,
        });

        $("#" + valorControlName).rules("add", {
            required: true,
        });

        if ($("#dataTableConciliacoes tbody tr").length > 1) {
            $(".removeMovimento").removeClass("disabled");
        }
        else {
            $(".removeMovimento").addClass("disabled");
        }

        $('#' + movimentoControlName).val(movimentoId);

        MovimentoSharedScript.init(movimentoControlName, tipoTransacao);

        $("#" + valorControlName).maskMoney({
            prefix: 'R$', // Simbolo
            decimal: ',', // Separador do decimal
            precision: 2, // Precisão
            thousands: '.', // Separador para os milhares
            allowZero: true, // Permite que o digito 0 seja o primeiro caractere
            affixesStay: true, // definir se o símbolo vai ficar no campo após o usuário existe no campo. padrão: false
            allowNegative: true, // use esta configuração para impedir que os usuários entrando com valores negativos. padrão: false
        });

        $("#" + valorControlName).on('change', function () {

            updateTotalValorConciliado();

            var valorDisponivelConciliacaoControl = $(this).parents('tr').find('.valorDisponivelConciliacaoControl');

            var movimentoControlName = $(this).parents('tr').find('.movimentoControl:input').prop('name');
            var movimentoControlData = $('#' + movimentoControlName).select2('data');

            if (movimentoControlData === null) {
                $(valorDisponivelConciliacaoControl).val(formatToLocalMoney(0));
                return;
            }

            var valorConciliado = parseFloat($(this).val().toString().replace('R$', '').replace('.', '').replace(',', '.'));
            var valorDisponivel = parseFloat(movimentoControlData.ValorDisponivel.toString().replace('.', '').replace(',', '.'));

            valorDisponivel = valorDisponivel - valorConciliado;
            var valorDisponivelFormatted = formatToLocalMoney(valorDisponivel.toString().replace(',', '').replace('.', ','));

            $(valorDisponivelConciliacaoControl).val(valorDisponivelFormatted);
        });        

        $('.removeMovimento').click(function (e) {
            e.preventDefault();
            $(".removeMovimento").addClass("disabled");//prevenção de click durante a animação
            var tr = $(this).closest('tr');
            tr.fadeOut(300, function () {
                tr.remove();
                if ($("#dataTableConciliacoes tbody tr").length == 1) {
                    $(".removeMovimento").addClass("disabled");
                }
                else {
                    $(".removeMovimento").removeClass("disabled");
                }
            });
            updateTotalValorConciliado();
        });

        $('#' + movimentoControlName).on("change", function myfunction() {
            
            var data = $(this).select2("data");

            if (data == null) {
                $("#" + valorControlName).val(formatToLocalMoney(0));
                $("#" + valorDisponivelControlName).val(formatToLocalMoney(0));
            }
            else {
                $("#" + valorControlName).rules("remove", "max");

                $("#" + valorControlName).rules("add", {
                    valorConciliadoRule: data.ValorDisponivel,
                });

                var valorPagamento = $('#txtValorPagamento').maskMoney('unmasked')[0];
                var totalValorConciliado = getTotalValorConciliado();
                var valorDisponivel = parseFloat(data.ValorDisponivel.replace('R$', '').replace('.', '').replace(',', '.'));

                var valorRestante = valorPagamento - totalValorConciliado;

                var valorControlValue = 0;

                if (valorPagamento == 0 || valorRestante >= valorDisponivel) {
                    valorControlValue = valorDisponivel;
                }
                else {
                    valorControlValue = valorRestante;
                }

                var valorDisponivelControlValue = valorDisponivel - valorControlValue;

                var valorControlValueFormatted = formatToLocalMoney(valorControlValue.toString().replace(',', '').replace('.', ','));
                var valorDisponivelControlValueFormatted = formatToLocalMoney(valorDisponivelControlValue.toString().replace(',', '').replace('.', ','));

                $("#" + valorControlName).val(valorControlValueFormatted);
                $("#" + valorDisponivelControlName).val(valorDisponivelControlValueFormatted);
            }

            updateTotalValorConciliado();

        });

        updateTotalValorConciliado();
    }

    var getTotalValorConciliado = function () {

        var totalValorConciliado = 0;

        $("#dataTableConciliacoes tbody tr").each(function (index) {
            var valorConciliacaoControlValue = $('.valorConciliacaoControl', this).maskMoney('unmasked')[0];
            totalValorConciliado += valorConciliacaoControlValue;
        });

        return totalValorConciliado;
    };

    var updateTotalValorConciliado = function () {

        var totalValorConciliado = getTotalValorConciliado();
                
        var totalValorConciliadoFormatted = totalValorConciliado.toString().replace(',', '').replace('.', ',');
        $("#txtTotalValorConciliado").val(formatToLocalMoney(totalValorConciliadoFormatted));

        var valorPagamento = $('#txtValorPagamento').maskMoney('unmasked')[0];
        var valorRestante = valorPagamento - totalValorConciliado;
        var valorRestanteFormatted = valorRestante.toString().replace(',', '').replace('.', ',');

        $("#txtValorRestante").val(formatToLocalMoney(valorRestanteFormatted));

    };

    return {
        init: function () {
            handleInit();
        },
        getConciliacoes: function () {
            return handleGetConciliacoes();
        },
        adicionaConciliacao: function (movimentoId, valor) {
            handleAdicionaConciliacao(movimentoId, valor);
        },
    };

}();

jQuery.validator.addMethod("totalConciliadoRule", function (value, element) {

    var totalConciliado = 0;

    $(".valorConciliacaoControl").each(function (index) {
        totalConciliado += $(this).maskMoney('unmasked')[0];
    });

    var valorPagamento = $('#txtValorPagamento').maskMoney('unmasked')[0];

    return totalConciliado.toFixed(2) == valorPagamento;

}, "A soma das conciliações deve ser igual ao valor do pagamento.");

jQuery.validator.addMethod("valorConciliadoRule", function (value, element, param) {

    var valorConciliado = parseFloat(value.replace('R$', '').replace('.', '').replace(',', '.'));
    var valorDisponivel = parseFloat(param.replace('R$', '').replace('.', '').replace(',', '.'));
    
    var result = valorDisponivel >= valorConciliado;

    return result;

}, "máximo ultrapassado");
