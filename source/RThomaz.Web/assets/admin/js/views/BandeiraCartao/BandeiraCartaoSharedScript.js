var BandeiraCartaoSharedScript = function () {

    var handleInit = function (controlId, enable) {

        $.ajax({
            type: "GET",
            url: ApplicationScript.getAppWebApiUrl() + '/api/bandeiraCartao/selectViewList',
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
        }).success(function (data, textStatus, jqXHR) {

            var dataSource = [];

            for (var i = 0; i < data.length; i++) {
                dataSource.push({
                    id: data[i].bandeiraCartaoId,
                    text: data[i].nome,
                    logoStorageObject: data[i].logoStorageObject,
                });                
            }

            var id = $('#' + controlId).val();

            $('#' + controlId).select2(
            {
                placeholder: 'Selecione',
                minimumInputLength: 0,
                allowClear: true,
                formatResult: formatComboBandeiraCartao,
                formatSelection: formatComboBandeiraCartao,                
                data: dataSource
            }).select2('val', id);

            $('#' + controlId).on("change", function myfunction() {
                var formDetail = $("#formDetail");
                if ($("#formDetail").length) {
                    var validator = formDetail.validate();
                    validator.element($(this));
                }
            });

            $('#' + controlId).select2("enable", enable);

        }).error(function (jqXHR, textStatus, errorThrown) {
            ApplicationScript.error(jqXHR, textStatus, errorThrown);
        });

    }

    var formatComboBandeiraCartao = function (model) {

        var src = '';

        if (model.logoStorageObject) {
            src = "/api/bandeiracartao/logo/" + model.logoStorageObject;
        }
        else {
            src = "/assets/admin/img/bank.svg";
        }

        return "<img style='width:16px;height:11px' class='flag' src='" + src + "'/>" + model.text;
    }

    return {
        init: function (controlId, enable) {
            handleInit(controlId, enable);
        },
    };

}();