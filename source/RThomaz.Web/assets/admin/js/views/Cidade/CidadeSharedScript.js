var CidadeSharedScript = function () {

    var handleInit = function (controlId) {

        var pageSize = 20;
        var resultCallback = null;

        $('#' + controlId).select2(
        {
            placeholder: 'Selecione',
            minimumInputLength: 0,
            allowClear: true,
            ajax: {
                quietMillis: 150,
                url: '/Cidade/GetSelectList/',
                dataType: 'jsonp',
                data: function (term, page) {
                    return {
                        'param.PageSize': pageSize,
                        'param.PageNum': page,
                        'param.SearchTerm': term,
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
                    return { results: data.Results, more: more };
                }
            },
            initSelection: function (element, callback) {
                var id = $(element).val();
                if (id !== "") {
                    $.ajax({
                        type: "GET",
                        async: false,
                        url: '/Cidade/GetSelect2Result/',
                        headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                        content: "application/json; charset=utf-8",
                        dataType: "json",
                        data: {
                            'id': id,
                        },
                        success: function (data) {
                            resultCallback = data;
                        },
                        error: function (request, status, error) {
                            alert("Erro /Cidade/GetSelect2Result/");
                        }
                    });
                }
                if (resultCallback) {
                    callback({ id: parseInt(resultCallback.id), text: resultCallback.text });
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

    return {
        init: function (controlId) {
            handleInit(controlId);
        },
    };

}();