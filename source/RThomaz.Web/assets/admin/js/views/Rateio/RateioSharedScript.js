var RateioSharedScript = function () {

    var handleInit = function () {

        $('#btnAddRateio').click(function (e) {
            e.preventDefault();
            RateioSharedScript.adicionaRateio(null, null, "", "100,00")
        });

    }

    var handleGetRateios = function () {

        var rateiosResult = [];
        $("#dataTable tbody tr").each(function (index) {
            rateiosResult.push({
                "PlanoContaId": $('.planoContaControl', this).select2("val"),
                "CentroCustoId": $('.centroCustoControl', this).select2("val"),
                "Porcentagem": $('.porcentagemControl', this).maskMoney('unmasked')[0].toString(),
                "Observacao": $('.observacaoControl', this).val(),
            });
        });

        return rateiosResult;

    }

    var rateioCounterHelper = 0;

    var handleAdicionaRateio = function (planoConta, centroCusto, observacao, valorVencimento) {

        rateioCounterHelper++;

        var tipoTransacao = $("#txtTipoTransacao").val();

        var planoContaControlName = 'cmbPlanoConta' + rateioCounterHelper;
        var centroCustoControlName = 'cmbCentroCusto' + rateioCounterHelper;
        var observacaoControlName = 'txtObservacao' + rateioCounterHelper;
        var porcentagemControlName = 'txtPorcentagem' + rateioCounterHelper;

        if (!observacao) {
            observacao = '';
        }

        var planoContaId = planoConta == null ? null : planoConta.planoContaId;
        var centroCustoId = centroCusto == null ? null : centroCusto.centroCustoId;

        $('#dataTable > tbody:last').append('<tr>'
            + '<td><input id="' + planoContaControlName + '" name="' + planoContaControlName + '" type="text" class="planoContaControl" style="width:180px;" value="' + planoContaId + '"</td>'
            + '<td><input id="' + centroCustoControlName + '" name="' + centroCustoControlName + '" type="text" class="centroCustoControl" style="width:180px;" value="' + centroCustoId + '"</td>'
            + '<td><input id="' + observacaoControlName + '" name="' + observacaoControlName + '" type="text" class="observacaoControl" style="width:220px;" value="' + observacao + '"</td>'
            + '<td><input id="' + porcentagemControlName + '" name="' + porcentagemControlName + '" type="text" class="porcentagemControl" style="width:60px;text-align:right;" value="' + valorVencimento + '%"</td>'
            + '<td><a href="#" class="btn default btn-xs grey removeRateio"><i class="fa fa-trash-o"></i> Excluir</a></td>'
            + '</tr>');

        $("#" + planoContaControlName).rules("add", {
            required: true,
        });

        $("#" + centroCustoControlName).rules("add", {
            required: true,
        });

        $("#" + porcentagemControlName).rules("add", {
            required: true,
            rateioPercent: true,
        });

        if ($("#dataTable tbody tr").length > 1) {
            $(".removeRateio").removeClass("disabled");
        }
        else {
            $(".removeRateio").addClass("disabled");
        }

        $('#' + planoContaControlName).val(planoContaId);
        $('#' + centroCustoControlName).val(centroCustoId);

        PlanoContaSharedScript.init(planoContaControlName, tipoTransacao, planoConta);
        CentroCustoSharedScript.init(centroCustoControlName, centroCusto);

        $("#" + porcentagemControlName).maskMoney({
            suffix: '%', // Simbolo
            decimal: ',', // Separador do decimal
            precision: 2, // Precisão
            thousands: '', // Separador para os milhares
            allowZero: true, // Permite que o digito 0 seja o primeiro caractere
            affixesStay: true, // definir se o símbolo vai ficar no campo após o usuário existe no campo. padrão: false
            allowNegative: false, // use esta configuração para impedir que os usuários entrando com valores negativos. padrão: false
        });

        $('.removeRateio').click(function (e) {
            e.preventDefault();
            $(".removeRateio").addClass("disabled");//prevenção de click durante a animação
            var tr = $(this).closest('tr');
            tr.fadeOut(300, function () {
                tr.remove();
                if ($("#dataTable tbody tr").length == 1) {
                    $(".removeRateio").addClass("disabled");
                }
                else {
                    $(".removeRateio").removeClass("disabled");
                }
            });

        });
    }

    return {
        init: function () {
            handleInit();
        },
        getRateios: function () {
            return handleGetRateios();
        },
        adicionaRateio: function (planoConta, centroCusto, observacao, valorVencimento) {
            handleAdicionaRateio(planoConta, centroCusto, observacao, valorVencimento);
        }
    };

}();

jQuery.validator.addMethod("rateioPercent", function (value, element) {

    var totalPercentual = 0;

    $(".porcentagemControl").each(function (index) {
        totalPercentual += $(this).maskMoney('unmasked')[0];
    });

    return totalPercentual == 100;

}, "A soma dos percentuais deve ser igual a 100%.");