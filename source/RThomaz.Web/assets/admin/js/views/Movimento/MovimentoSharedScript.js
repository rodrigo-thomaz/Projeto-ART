var MovimentoSharedScript = function () {

    var handleInit = function (controlId, tipoTransacao, selectViewModel) {

        var pageSize = 20;
        var resultCallback = null;

        $('#' + controlId).select2({
            placeholder: 'Selecione',
            minimumInputLength: 0,
            allowClear: true,
            formatResult: formatResult,
            formatSelection: formatSelection,
            ajax: {
                quietMillis: 150,
                url: ApplicationScript.getAppWebApiUrl() + '/api/movimento/selectViewList',
                dataType: 'json',
                data: function (term, page) {
                    return {
                        'param.PageSize': pageSize,
                        'param.PageNumber': page,
                        'param.Search': term,
                        'tipoTransacao': tipoTransacao,
                    };
                },
                transport: function (params) {
                    params.beforeSend = function (request) {
                        request.setRequestHeader("Authorization", 'bearer ' + ApplicationScript.getToken());
                    };
                    return $.ajax(params);
                },
                results: function (data, page) {

                    var conciliacoes = PagamentoSharedScript.getConciliacoes();

                    data.data = jQuery.grep(data.data, function (value) {

                        var conciliacao = jQuery.grep(conciliacoes, function (value1) {
                            return (parseFloat(value1.MovimentoId) === value.movimentoId);
                        });

                        return conciliacao.length === 0;
                    });

                    var more = (page * pageSize) < data.totalRecords;
                    return { results: data.data, more: more };
                }
            },
            initSelection: function (element, callback) {
                if (selectViewModel != null) {
                    callback({
                        id: parseInt(selectViewModel.movimentoId),
                        text: selectViewModel.historico,
                        attr: selectViewModel.tipoTransacao,
                    });
                }
            },            
        }).select2('val', []);

        $('#' + controlId).on("change", function myfunction() {
            var formDetail = $("#formDetail");
            if ($("#formDetail").length) {
                var validator = formDetail.validate();
                validator.element($(this));
            }
        });

        function formatResult(data) {

            var $control = $(
                '<div class="row">' +
                '<div class="col-md-2">' + 'Data' + '</div>' +
                '<div class="col-md-6">' + 'Hist&oacute;rico' + '</div>' +
                '<div class="col-md-2">' + 'Valor' + '</div>' +
                '<div class="col-md-2">' + 'Dispon&iacute;vel' + '</div>' +
                '</div>' +
                '<div class="row">' +
                '<div class="col-md-2">' + data.dataMovimento + '</div>' +
                '<div class="col-md-6">' + data.historico + '</div>' +
                '<div class="col-md-2">' + data.valorMovimento + '</div>' +
                '<div class="col-md-2">' + data.valorDisponivel + '</div>' +
                '</div>');

            return $control;
        };

        function formatSelection(data) {

            var $control = $(
                '<div class="row">' +
                '<div class="col-md-2">' + data.dataMovimento + '</div>' +
                '<div class="col-md-7">' + data.historico + '</div>' +
                '<div class="col-md-3" style="text-align: right;">R$ ' + data.valorMovimento + '</div>' +
                '</div>');

            return $control;
        };
    }    

    return {
        init: function (controlId, tipoTransacao, selectViewModel) {
            handleInit(controlId, tipoTransacao, selectViewModel);
        },
    };

}();