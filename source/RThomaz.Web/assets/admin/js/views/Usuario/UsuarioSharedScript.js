var UsuarioSharedScript = function () {

    var handleInit = function (controlId, selectViewModel) {

        var pageSize = 20;
        var resultCallback = null;

        $('#' + controlId).select2(
        {
            placeholder: 'Selecione',
            minimumInputLength: 0,
            allowClear: true,
            ajax: {
                quietMillis: 150,
                url: ApplicationScript.getAppWebApiUrl() + '/api/usuario/selectViewList',
                dataType: 'json',
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
                            attr: data.data[i].avatarStorageObject,
                            children: null,
                            id: data.data[i].usuarioId,
                            text: data.data[i].nomeExibicao + ' (' + data.data[i].email + ')',
                        });
                    }
                    return { results: results, more: more };
                }
            },
            initSelection: function (element, callback) {
                if (selectViewModel != null) {
                    callback({
                        id: parseInt(selectViewModel.usuarioId),
                        text: selectViewModel.nomeExibicao + ' (' + selectViewModel.email + ')',
                        attr: selectViewModel.logoStorageObject,
                    });
                }
            },
        }).select2('val', []);

        $('#' + controlId).on("change", function () {
            var formDetail = $("#formDetail");
            if ($("#formDetail").length) {
                var validator = formDetail.validate();
                validator.element($(this));
            }
        });

    }

    return {
        init: function (controlId, selectViewModel) {
            handleInit(controlId, selectViewModel);
        },
    };

}();