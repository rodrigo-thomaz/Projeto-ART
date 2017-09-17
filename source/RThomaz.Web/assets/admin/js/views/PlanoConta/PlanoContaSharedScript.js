var PlanoContaSharedScript = function () {

    var handleInit = function (controlId, tipoTransacao, selectViewModel) {

        var pageSize = 20;
        var resultCallback = null;

        $('#' + controlId).select2({
            placeholder: 'Selecione',
            minimumInputLength: 0,
            allowClear: true,
            ajax: {
                quietMillis: 150,
                url: ApplicationScript.getAppWebApiUrl() + '/api/planoconta/selectViewList/',
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
                    var more = (page * pageSize) < data.Total;
                    var results = loadModel(data.data);
                    return { results: results, more: more };
                }
            },
            initSelection: function (element, callback) {
                if (selectViewModel != null) {
                    callback({
                        id: parseInt(selectViewModel.planoContaId),
                        text: selectViewModel.nome,
                        attr: null,
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

    var loadModel = function (models) {
        var results = [];
        for (var i = 0; i < models.length; i++) {
            var children = null;
            if (models[i].children != null && models[i].children.length > 0) {
                children = loadModel(models[i].children);
            }
            results.push({
                attr: null,
                children: children,
                id: models[i].planoContaId,
                text: models[i].nome,
            });
        }
        return results;
    };

    return {
        init: function (controlId, tipoTransacao, selectViewModel) {
            handleInit(controlId, tipoTransacao, selectViewModel);
        },
    };

}();