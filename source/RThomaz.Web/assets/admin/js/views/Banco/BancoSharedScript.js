var BancoSharedScript = function () {

    var handleInit = function (controlId, selectViewModel) {

        var pageSize = 20;
        var resultCallback = null;

        $('#' + controlId).select2(
        {
            placeholder: 'Selecione',
            minimumInputLength: 0,
            allowClear: true,
            formatResult: formatComboBanco,
            formatSelection: formatComboBanco,
            ajax: {
                quietMillis: 150,
                url: ApplicationScript.getAppWebApiUrl() + '/api/banco/selectViewList',
                dataType: "json",
                data: function (term, page) {
                    return {
                        'param.PageSize': pageSize,
                        'param.PageNumber': page,
                        'param.Search': term,
                    };
                },
                transport: function (params) {
                    params.beforeSend = function (request) {
                        request.setRequestHeader("Authorization", 'bearer ' + ApplicationScript.getToken());
                    };
                    return $.ajax(params);
                },
                results: function (data, page) {
                    var more = (page * pageSize) < data.Total;
                    var results = [];
                    for (var i = 0; i < data.data.length; i++) {
                        results.push({
                            attr: data.data[i].logoStorageObject,
                            children: null,
                            id: data.data[i].bancoId,
                            text: data.data[i].nome + ' (' + data.data[i].numero + ')',
                        });
                    }
                    return { results: results, more: more };
                }
            },
            initSelection: function (element, callback) {
                if (selectViewModel != null) {
                    callback({
                        id: parseInt(selectViewModel.bancoId),
                        text: selectViewModel.nome,
                        attr: selectViewModel.logoStorageObject,
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

    }

    var formatComboBanco = function (state) {

        var src = '';

        if (state.attr) {
            src = "/api/banco/logo/" + state.attr;
        }
        else {
            src = "/assets/admin/img/bank.svg";
        }

        return "<img style='width:16px;height:11px' class='flag' src='" + src + "'/>" + state.text;
    }

    return {
        init: function (controlId, selectViewModel) {
            handleInit(controlId, selectViewModel);
        },
    };

}();